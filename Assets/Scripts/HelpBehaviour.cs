using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpBehaviour : MonoBehaviour {
	public void OnBackClick() {
		SceneManager.LoadScene ("SplashScreen");
	}
}
