using UnityEngine;
using System.Collections;

public class DateTimeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string currentDateTimeString = System.DateTime.Now.ToString();
		Debug.Log (currentDateTimeString);
		string[] stringBits = currentDateTimeString.Split (' ');
		string t_dateString = stringBits [0];
		stringBits = t_dateString.Split ('/');
		string monthString = stringBits [0];
		string dayString = stringBits [1];
		string yearString = stringBits [2];
		string currentDateString = yearString + monthString + dayString;
		Debug.Log (currentDateString);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
