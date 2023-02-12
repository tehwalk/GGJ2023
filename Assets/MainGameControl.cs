using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameControl : MonoBehaviour {
	public static MainGameControl instance { get; private set; }
	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else { DestroyImmediate(gameObject); }
	}
}
