using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehaviour : MonoBehaviour {
	public void OnBackClick() {
		SceneManager.LoadScene ("SplashScreen");
	}
}
