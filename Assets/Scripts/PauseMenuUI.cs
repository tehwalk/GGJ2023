using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {

	[SerializeField] GameObject basePanel;
	[SerializeField] Button continueButton;
	[SerializeField] Button quitButton;

	public bool isOpen { get; private set; }

	void Start() {
		basePanel.SetActive(isOpen);
		continueButton.onClick.AddListener(TogglePanel);
		quitButton.onClick.AddListener(RestartGame); 
	}

	void Update() {
		if (Input.GetButtonDown("Cancel")) { TogglePanel(); }
	}

	void TogglePanel() {
		isOpen = !isOpen;
		basePanel.SetActive(isOpen);
        GameManager.Instance.TogglePause(isOpen);
	}

	void RestartGame() => SceneManager.LoadScene("MainMenuScene");
}
