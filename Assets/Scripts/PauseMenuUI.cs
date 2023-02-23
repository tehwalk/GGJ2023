using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {

	[SerializeField] GameObject basePanel;
	[SerializeField] Button continueButton;
	[SerializeField] Button quitButton;
	[SerializeField] AudioClip toggleClip;

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
		//GameManager.Instance.TogglePause(isOpen);
		AudioManager.instance.PlayInteractionSound(toggleClip, 0.5f);
	}

	void RestartGame() {
		SceneManager.LoadScene("MainMenuScene");
		Destroy(MainGameControl.Instance.gameObject);
	}
}
