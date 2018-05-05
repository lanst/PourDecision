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
	public Text recipeText;
	public Text direction;

	private string myJson;
	private MyDrinks[] myDrinks;
	private string result = "";
	private string[] splittedString;
	private List<string> resultArray = new List<string>();
	private Dictionary<string, string> drinks = new Dictionary<string, string> ();
	private List<string> ingredientPerms;
	public Dropdown dropdown;
	private List<string> recipe = new List<string>();
	private Dictionary<string, List<string>> recipeDict = new Dictionary<string, List<string>> ();
	private string dropDownText = "";

	void Start () {
		initialize ();
		myDrinks = JsonHelper.FromJson<MyDrinks>(myJson);

		for (int i=0; i < myDrinks.Length; i++){
			try{
				//Debug.Log (myDrinks [i].name);
				recipe.Clear();
				drinks.Add(myDrinks[i].ingredients, myDrinks[i].name);
				recipe.Add(myDrinks[i].ingredients);
				recipe.Add(myDrinks[i].instruction);
				recipe.Add(myDrinks[i].alcoholic);
				recipeDict.Add(myDrinks[i].name, recipe);
			}
			catch (Exception e){
				Debug.Log (e);
				continue;
			}
		}
	}

	void Update () {
		if (result != input.text){
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

		if (dropDownText != dropdown.options [dropdown.value].text) {
			string p = dropdown.options [dropdown.value].text;
			recipeText.text = recipeDict [dropdown.options [dropdown.value].text].ElementAt(0);
			direction.text = recipeDict [dropdown.options [dropdown.value].text].ElementAt(1);
			dropDownText = dropdown.options [dropdown.value].text;
		}
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
