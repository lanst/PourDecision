using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowErrorScript : MonoBehaviour {

	public InputField textField;
	public Text error;
	public Button searchBtn;
	public Text searchResult;

	// Use this for initialization
	void Start () {
		error.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (textField.text == "") {
			searchBtn.interactable = false;
		} else {
			searchBtn.interactable = true;
			searchBtn.onClick.AddListener (CheckTextField);
		}
	}

	// Update is called once per frame
	void CheckTextField() {
		if (textField.text == "") {
			error.gameObject.SetActive (true);
		} 
		else {
			error.gameObject.SetActive (false);
			searchResult.text = textField.text;
		}
	}
}
