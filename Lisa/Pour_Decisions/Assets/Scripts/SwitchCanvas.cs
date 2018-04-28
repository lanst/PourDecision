using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCanvas : MonoBehaviour {
	public Button button;
	public GameObject canvasToEnable;
	public GameObject canvasToDisable;
	public Text text;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		button.onClick.AddListener(Instruction);
	}

	void Instruction(){
		canvasToEnable.SetActive(true);
		canvasToDisable.SetActive (false);
	}
}
