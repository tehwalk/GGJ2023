using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
	[SerializeField] Button playButton;
	//[SerializeField] Button creditsButton;
	[SerializeField] Button exitButton;

	void Start() {
		playButton.onClick.AddListener(StartGame);
		//creditsButton.onClick.AddListener();				
		exitButton.onClick.AddListener(ExitGame);
	}

	void StartGame() => SceneManager.LoadScene((int)SceneIndex.Scene1);

	//void DisplayCredits() { }

	void ExitGame() => Application.Quit();
}
