using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonUI : MonoBehaviour {
	[SerializeField] AudioClip audioClip;
	
	Button button;

	void Awake() => button = GetComponentInChildren<Button>();

	void Start() => button.onClick.AddListener(OnClick);

	void OnClick() => AudioManager.instance.PlayInteractionSound(audioClip, 0.5f);
}
