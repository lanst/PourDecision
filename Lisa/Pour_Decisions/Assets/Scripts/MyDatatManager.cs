using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyDatatManager : MonoBehaviour {

	public Dictionary<string, string> myDict;
	// Use this for initialization
	void Start () {
		
	}
	
	public void loadJSONFile(string fileName){
		myDict = new Dictionary<string, string> ();
	}
}
