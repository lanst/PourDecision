using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchResult : MonoBehaviour {
	public Text result;
	// Use this for initialization
	void Start () {
		result.text = ShowErrorScript.searchResult;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
