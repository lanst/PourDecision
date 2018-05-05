using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class SearchScript : MonoBehaviour {
	public TextAsset textFile;
	public Text searchResult;
	public InputField input;

	private string myJson;
	private MyDrinks[] myDrinks;
	private string result = "";
	private string[] splittedString;
	private List<string> resultArray = new List<string>();
	private Dictionary<string, string> drinks = new Dictionary<string, string> ();
	private List<string> ingredientPerms;
	public Dropdown dropdown;

	void Start () {
		initialize ();
		myDrinks = JsonHelper.FromJson<MyDrinks>(myJson);

		for (int i=0; i < myDrinks.Length; i++){
			try{
				//Debug.Log (myDrinks [i].name);
				drinks.Add(myDrinks[i].ingredients, myDrinks[i].name);
			}
			catch (Exception e){
				Debug.Log (e);
				continue;
			}
		}
	}

	void Update () {
		if (result != searchResult.text){
			result = input.text;
			splittedString = result.Split (',');

			for (int i=0; i<splittedString.Length;i++) {
				splittedString[i] = splittedString[i].Trim();
			}

			Array.Sort<string> (splittedString);
			ingredientPerms = permutation (splittedString);

			for (int i = 0; i < ingredientPerms.Count; i++) {
				try {
					if(drinks[ingredientPerms[i]] != null){
						resultArray.Add(drinks[ingredientPerms[i]]);
					}
				} catch (Exception e) {
					Debug.Log (e);
					continue;
				}
			}

			dropdown.ClearOptions ();
			dropdown.AddOptions (resultArray);
		}
		/*result = searchResult.text.ToLower();
		resultArray = result.Split(',');
		searchResult.text = result;
		for (int i = 0; i < resultArray.Length; i++) {
			//Debug.Log (resultArray [i]);
		}*/
	}

	public void initialize(){
		myJson = textFile.ToString();
	}

	public List<string> permutation(string[] splittedStrings){
		List<string> permutatedStrings = new List<string>();
		string combination;

		for (int i = 0; i < splittedStrings.Length; i++) {
			combination = splittedStrings [i].Trim();
			permutatedStrings.Add (combination);
			for (int j = i+1; j < splittedStrings.Length; j++) {
				combination = splittedStrings [i].Trim() + "," + splittedStrings[j].Trim();
				permutatedStrings.Add (combination);
				for (int k = j + 1; k < splittedStrings.Length; k++) {
					combination += "," + splittedStrings [k].Trim();
					permutatedStrings.Add (combination);
				}
			}
		}

		return permutatedStrings;
	}
}
