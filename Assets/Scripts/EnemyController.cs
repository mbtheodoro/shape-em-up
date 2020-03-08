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

	public void Kill() {
		GameObject obj = Instantiate (deathFX);
		obj.transform.position = transform.position;
		Destroy (gameObject);
	}
}
