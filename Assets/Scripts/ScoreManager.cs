using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public int healCombo;
	public int healthUpCombo;
	public PlayerController player;

	public GameObject healingFX;

	public Text killsText, comboText;

	public class Score {
		public int maxCombo;
		public int points;
		public string name;

		public Score() {}

		public Score(int points, int maxCombo, string name) {
			this.maxCombo = maxCombo;
			this.points = points;
			this.name = name;
		}
	}

	public static int MAX_N = 10;

	public int combo;
	public int maxCombo;
	public int points;

	public void Start() {
		combo = 0;
		maxCombo = 0;
		points = 0;
	}

	public void Hit()
	{
		points += (combo / 5) + 1;
		combo++;

		AudioSource.PlayClipAtPoint (player.killSFX, Camera.main.transform.position, 0.6f);
		player.normalHSpeed += player.speedBoost;
		player.dashHSpeed += player.speedBoost;

		bool audio = false;

		if (combo % healthUpCombo == 0) {
			if (player.health == player.startingHealth) {
				player.IncreaseHealth (1);

				var vfx = Instantiate (healingFX);
				vfx.transform.parent = player.transform;
				vfx.transform.localScale = Vector3.one;
				vfx.transform.position = player.transform.position;
				vfx.transform.Translate (Vector3.back);
				audio = true;
			}
		}
		if (combo % healCombo == 0) {
			if (player.health + 1 <= player.startingHealth) {
				player.IncreaseHealth (1);
				var vfx = Instantiate (healingFX);
				vfx.transform.parent = player.transform;
				vfx.transform.localScale = Vector3.one;
				vfx.transform.position = player.transform.position;
				vfx.transform.Translate (Vector3.back);
				var trails = vfx.GetComponent<ParticleSystem> ().trails;
				trails.widthOverTrailMultiplier = 0;
				audio = true;
			}
		}
		if (audio) {
			AudioSource.PlayClipAtPoint (player.comboSFX, Camera.main.transform.position);
		}

		killsText.text = "Score: " + points;
		comboText.text = "Combo: " + combo;
	}

	public void EndCombo()
	{
		if (combo > maxCombo)
			SetMaxCombo (combo);
		combo = 0;
	}

	public void SetMaxCombo(int cmb) {
		maxCombo = cmb;
	}

	public void AddScore(string name) {
		Score s = new Score (points, maxCombo, name);
		Score[] scores;

		if (PlayerPrefs.HasKey ("n")) {
			int n = PlayerPrefs.GetInt("n");
			int i;

			Score[] oldScores = GetHighScores (n);

			n++;
			scores = new Score[n];
			for (i = 0; i < oldScores.Length; i++)
				scores [i] = oldScores[i];
			
			// Sorted insert
			i = oldScores.Length-1;
			while (i >= 0 && scores [i].points< s.points) {
				scores [i + 1] = scores [i];
				i--;
			}
			if (i < n - 1)
				scores [i + 1] = s;
		} else {
			scores = new Score[] { s };
		}

		SetScores (scores);
	}

	private void SetScores(Score[] scores) {
		int n = Mathf.Min (scores.Length, MAX_N);
		PlayerPrefs.SetInt ("n", n);
		for (int i = 0; i < n; i++) {
			PlayerPrefs.SetString ("HS_" + i + "_name", scores [i].name);
			PlayerPrefs.SetInt ("HS_" + i + "_points", scores[i].points);
			PlayerPrefs.SetInt ("HS_" + i + "_combo", scores[i].maxCombo);
		}
	}

	public Score[] GetHighScores(int n) {
		if (!PlayerPrefs.HasKey ("n"))
			return new Score[] { };
		
		n = Mathf.Min (n, PlayerPrefs.GetInt ("n"));
		
		Score[] scores = new Score[n];
		for (int i = 0; i < n; i++) {
			var s = new Score ();
			s.name = PlayerPrefs.GetString ("HS_" + i + "_name");
			s.points = PlayerPrefs.GetInt ("HS_" + i + "_points");
			s.maxCombo = PlayerPrefs.GetInt ("HS_" + i + "_combo");

			scores [i] = s;
		}

		return scores;
	}

}
