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
	private string FILE_NAME = "favorites.json";
	public Text searchResult;
	public InputField input;
	public Text recipeText;
	public Text direction;
	public Text FavRecipeText;
	public Text FavDirection;
	public Dropdown dropdown;
	public Dropdown sortDropdown;
	public Dropdown FavDropdown;
	public Button addToFavoriteBtn;
	public Text alreadyInFav;

	private string myJson;
	private MyDrinks[] myDrinks;
	private MyDrinks[] favorites;
	private string result = "";
	private string[] splittedString;
	private List<string> resultArray = new List<string>();
	private Dictionary<string, string> drinks = new Dictionary<string, string> ();
	private List<string> ingredientPerms;
	private List<string> recipe = new List<string>();
	private Dictionary<string, List<string>> recipeDict = new Dictionary<string, List<string>> ();
	private List<String> favList = new List<string>();

	void Start () {
		initialize (textFile);
		myDrinks = JsonHelper.FromJson<MyDrinks>(myJson);

		foreach (var drink in myDrinks){
			try{
				//Debug.Log (myDrinks [i].name);
				recipe.Clear();
				drinks.Add(drink.ingredients, drink.name);
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
		if (result != input.text.ToLower()){
			result = input.text.ToLower();
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
			recipeText.text = "";
			direction.text = "";
			dropdown.ClearOptions ();
			dropdown.AddOptions (resultArray);
			FavDropdown.ClearOptions ();
		}

		if (resultArray.Count > 0) {
			string p = dropdown.options [dropdown.value].text;
			recipeText.text = recipeDict [dropdown.options [dropdown.value].text].ElementAt(0);
			direction.text = recipeDict [dropdown.options [dropdown.value].text].ElementAt(1);
		}

		sortDropdown.onValueChanged.AddListener (delegate {
			sortDrinks (sortDropdown);
		});

		FavDropdown.onValueChanged.AddListener (delegate {
			showFavDropDown (FavDropdown);
		});

		addToFavoriteBtn.onClick.AddListener(delegate{AddToFavorite();});
	}

	void AddToFavorite(){
		if (favList.Count > 0) {
			foreach (var fav in favList) {
				if (fav == dropdown.options [dropdown.value].text) {
					alreadyInFav.gameObject.SetActive (true);
					return;
				}
			}
			alreadyInFav.gameObject.SetActive (false);
			favList.Add (dropdown.options [dropdown.value].text);
			FavDropdown.AddOptions (favList);
		} 
		else {
			alreadyInFav.gameObject.SetActive (false);
			favList.Add (dropdown.options [dropdown.value].text);
		}
		/* string fileName = Application.persistentDataPath + "/" + FILE_NAME;

		FileStream fs = File.Open (fileName, FileMode.OpenOrCreate);

		if (favorites.Length > 0) {
			foreach (var fav in favorites) {
				if (fav.name == dropdown.options [dropdown.value].text) {
					alreadyInFav.gameObject.SetActive (true);
				} 
			}
		}
		else {
			alreadyInFav.gameObject.SetActive (false);

			MyDrinks newDrink = new MyDrinks();
			newDrink.name = dropdown.options [dropdown.value].text;
			newDrink.ingredients = recipeText.text;
			newDrink.instruction = direction.text;
			newDrink.alcoholic = "alcoholic";

			List<MyDrinks> favList = new List<MyDrinks> ();
			favList = favorites.ToList ();
			favList.Add (newDrink);
			favorites = favList.ToArray ();

			string favJson = JsonHelper.ToJson(favorites, true);

			
			StreamWriter fileWriter = File.CreateText(fileName);
			fileWriter.WriteLine("Hello world");
			fileWriter.Close();
		}*/
	}

	public void sortDrinks(Dropdown dropD){
		if (dropD.value == 0) {
			resultArray.Sort ();
			dropdown.ClearOptions ();
			dropdown.AddOptions (resultArray);
		} else if (dropD.value == 1){
			resultArray.Sort ();
			resultArray.Reverse ();
			dropdown.ClearOptions ();
			dropdown.AddOptions (resultArray);
		}
	}

	public void showFavDropDown(Dropdown dropDF){
		dropDF.ClearOptions ();
		dropDF.AddOptions (favList);

		if (favList.Count > 0) {
			FavRecipeText.text = recipeDict [FavDropdown.options [FavDropdown.value].text].ElementAt (0);
			FavDirection.text = recipeDict [FavDropdown.options [FavDropdown.value].text].ElementAt (1);
		}
	}

	public void initialize(TextAsset text){
		myJson = text.ToString();
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
