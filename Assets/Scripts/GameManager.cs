using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }
	[SerializeField] public SceneIndex nextScene;
	public int rootsNecessery;
	private int rootsCut = 0;
	public int hitableLayerValue;
	[SerializeField] private TextMeshProUGUI scoreText;

	// Start is called before the first frame update
	private void Awake() {
		if (_instance != null && _instance != this)
			_instance = null;
		_instance = this;
		Time.timeScale = 1;
	}
	void Start() {
		Cursor.visible = false;
		//rootsNecessery = FindGameObjectsInLayer(hitableLayerValue);
		UpdateScoreText();
	}

	int FindGameObjectsInLayer(int layervalue) {
		List<GameObject> requiredObjects = new List<GameObject>();
		var gameObjectList = FindObjectsOfType<GameObject>();
		foreach (GameObject g in gameObjectList) {
			if (g.layer == layervalue) {
				requiredObjects.Add(g);
			}
		}
		return requiredObjects.Count;
	}

	// Update is called once per frame
	void Update() {

	}

	public void CutRoot() {
		rootsCut++;
		UpdateScoreText();
		AudioManager.instance.PlayCutClip();

		if (rootsCut >= rootsNecessery) {
			Debug.Log("You won!!");
			NextLevel();
		}
	}

	public void NextLevel() {
		SceneManager.LoadScene((int) nextScene);
	}

	public void ResetScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void TogglePause(bool isPauseMenuOpen) {
		if (isPauseMenuOpen == true) {
			Time.timeScale = 0;
		}
		else {
			Time.timeScale = 1;
		}
	}

	void UpdateScoreText() {
		scoreText.text = rootsCut.ToString() + " / " + rootsNecessery.ToString();
	}
}
