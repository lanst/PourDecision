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

		foreach (var drink in myDrinks){
			try{
				if (drink.name.Equals("\'57 chevy with a white license plate")){
					Debug.Log("L");
				}
				//Debug.Log (myDrinks [i].name);
				recipe.Clear();
				drinks.Add(drink.ingredients, drink.name);
				Debug.Log(drink.name);
				recipe.Add(drink.ingredients);
				recipe.Add(drink.instruction);
				recipe.Add(drink.alcoholic);
				recipeDict.Add(drink.name, new List<string>(recipe));
			}
			catch (Exception e){
				Debug.Log (e);
				continue;
			}
		}
		recipe.Clear ();
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

			resultArray.Clear ();

			for (int i = 0; i < ingredientPerms.Count; i++) {
				try {
					if(drinks.ContainsKey(ingredientPerms[i])){
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

		if (resultArray.Count > 0) {
			
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
