using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public Camera cam;
	public Button BtnStart;


	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}

		Button pause = BtnStart.GetComponent<Button>();
		pause.onClick.AddListener (StartGame);



	}


	// Use this for initialization
	void StartGame () {
		SceneManager.LoadScene ("Main", 
			LoadSceneMode.Single
		);
	}


	

}
