using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState 
{
	START,
	RESTART,
	PAUSED,
	RUNNING,
	DIE,
	NULL
}

public class GameController : MonoBehaviour {

	public DeathPopupBehaviour deathPopup;
	public ScoreManager scoreManager;
	public ParticleSystem particleSystem;

	private GameState currentState = GameState.START;
	private GameState previousState = GameState.NULL;

	public GameState CurrentState 
	{
		get { return currentState; }
	}

	void SwitchState(GameState nextState)
	{
		previousState = currentState;
		currentState = nextState;
	}

	void GameStateMachine()
	{
		switch(currentState)
		{
		case GameState.START:
			SwitchState (GameState.RUNNING);
			break;
		case GameState.RESTART:
			SwitchState (GameState.START);
			break;
		case GameState.PAUSED:
			particleSystem.gameObject.SetActive (false);
			break;
		case GameState.RUNNING:
			particleSystem.gameObject.SetActive (true);
			break;
		case GameState.DIE:
			deathPopup.gameObject.SetActive(true);
			deathPopup.Activate ();
			particleSystem.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}

	public void PlayerDied()
	{
		// TODO read player name
		SwitchState (GameState.DIE);
	}

	void BasicInputs()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.RUNNING)
			SwitchState (GameState.PAUSED);
		else if(Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.PAUSED)
			SwitchState (GameState.RUNNING);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		GameStateMachine();
		BasicInputs ();
	}
}