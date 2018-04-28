using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

	private static string searchInput;

	public static string SearchInput{
		get{
			return searchInput;
		}
		set{
			searchInput = value;
		}
	}
}
