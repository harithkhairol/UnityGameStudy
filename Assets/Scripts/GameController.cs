using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject [] balls;
	public float timeLeft;
	public Text timerText;
	private float maxWidth;
	public GameObject gameOverText;  
	public GameObject restartButton;
	public Button btnPause;
	public GameObject pausePanel;
	public Button btnResume;
	public Button btnMenu;
	public Button btnRestart;


	private bool playing;
	public HatController hatController1,hatController2;
	GameObject[] pauseObjects;


	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}

		playing = true;

		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent <Renderer>().bounds.extents.x;﻿
		maxWidth = targetWidth.x - ballWidth;

		UpdateText ();		 
		btnPause.gameObject.SetActive(false);

		pausePanel.gameObject.SetActive(false);
		btnResume.gameObject.SetActive(false);
		btnMenu.gameObject.SetActive(false);
		btnRestart.gameObject.SetActive(false);

		Button pause = btnPause.GetComponent<Button>();
		pause.onClick.AddListener (PauseOnClick);

		Button resume = btnResume.GetComponent<Button>();
		resume.onClick.AddListener (ResumeOnClick);

		Button menu = btnMenu.GetComponent<Button> ();
		menu.onClick.AddListener (MenuOnClick);


		StartCoroutine (Spawn());
		hatController1.ToggleControl (true);
		hatController2.ToggleControl (true);
		btnPause.gameObject.SetActive(true);

		//pause study

		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();

	}

	void PauseOnClick(){

		pausePanel.gameObject.SetActive(true);
		btnResume.gameObject.SetActive(true);
		btnMenu.gameObject.SetActive(true);
		btnRestart.gameObject.SetActive(true);


		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
		} else if (Time.timeScale == 0){
			Debug.Log ("high");
			Time.timeScale = 1;
			hidePaused();
		}

	}

	void ResumeOnClick(){

		pausePanel.gameObject.SetActive(false);
		btnResume.gameObject.SetActive(false);
		btnMenu.gameObject.SetActive(false);
		btnRestart.gameObject.SetActive(false);

if (Time.timeScale == 0){
			Debug.Log ("high");
			Time.timeScale = 1;
			hidePaused();
		}

	}



	void MenuOnClick(){

		SceneManager.LoadScene ("Main Menu", 
			LoadSceneMode.Single
		);
	
	
	
	
	}


	void FixedUpdate(){

		if (playing) {

		
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;

			}
			UpdateText ();


		}
			}

//	public void StartGame(){

//		StartCoroutine (Spawn());
//		hatController1.ToggleControl (true);
//		hatController2.ToggleControl (true);
//		btnPause.gameObject.SetActive(true);

		//pause study

//		Time.timeScale = 1;
//		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
//		hidePaused();

	
	
//	}

	//controls the pausing of the scene
	public void pauseControl(){
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			hidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}



	IEnumerator Spawn(){

		yield return new WaitForSeconds (2.0f);
		playing = true;

		while(timeLeft>0){

		GameObject ball = balls [Random.Range (0, balls.Length)];
		

/*			Vector3 spawnPosition = new Vector3 (Random.Range(-maxWidth, maxWidth),
			transform.position.y,
			0.0f);                       */

			Vector3 spawnPosition = new Vector3 (
				transform.position.x + Random.Range (-maxWidth, maxWidth), 
				transform.position.y, 
				0.0f
			);



		Quaternion spawnRotation = Quaternion.identity;

		Instantiate (ball, spawnPosition, spawnRotation);

			yield return new WaitForSeconds (Random.Range (1.0f, 2.0f));

	}
		yield return new WaitForSeconds (2.0f);
		gameOverText.SetActive(true);
		yield return new WaitForSeconds (2.0f);
		restartButton.SetActive (true); 
	}

	void UpdateText(){
		timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft).ToString();

	}

}