using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovingObject {

	public EnemySpawner spawner;
	public GameObject deathFX;

	// Update is called once per frame
	public override void FixedUpdate ()
	{
		if (spawner.gameController.CurrentState != GameState.RUNNING)
			return;
		base.FixedUpdate ();
		if (transform.position.x < spawner.despawnX) {
			Destroy (gameObject);
		}
	}

	public void Kill(float freezeDuration) {
		GameObject obj = Instantiate (deathFX);
		obj.transform.position = transform.position;

        StartCoroutine(FreezeFrame(freezeDuration));
	}

    private IEnumerator FreezeFrame(float freezeDuration)
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(freezeDuration);

        Time.timeScale = currentTimeScale;
        Destroy(gameObject);
    }
}
