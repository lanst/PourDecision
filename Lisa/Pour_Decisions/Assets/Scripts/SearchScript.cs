using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SearchScript : MonoBehaviour {
	public TextAsset textFile;
	public Text searchResult;

	private string myJson;
	private MyDrinks[] myDrinks;
	private string result;
	private string[] resultArray;

	void Start () {
		initialize ();
		myDrinks = JsonHelper.FromJson<MyDrinks>(myJson);
		Debug.Log (result);

		/*for (int i=0; i < myDrinks.Length; i++){
			Debug.Log (myDrinks [i].name);
		}*/
	}

	void Update () {
		result = searchResult.text.ToLower();
		resultArray = result.Split(',');
		searchResult.text = result;
		for (int i = 0; i < resultArray.Length; i++) {
			Debug.Log (resultArray [i]);
		}
	}

	public void initialize(){
		myJson = textFile.ToString();
	}
}
