using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.UI;

public class APIRequests : MonoBehaviour {

	//public APIInfo apiInfo;

	public string screenShotURL= "http://www.my-server.com/cgi-bin/screenshot.pl";
		
	private static string authURL = "https://api.moves-app.com/oauth/v1/access_token?grant_type=authorization_code";
	private static string apiURL = "https://api.moves-app.com/api/1.1/user/summary/daily/";

	//public static string ACCESS_TOKEN = "H8rOew4KfvOrAp2jDrxeryX391ANUY6z3xWVKwudt3pZU51fD27q6N0YOt92N18j";
	public static string ACCESS_TOKEN = "M4gZwNSV6fnxFsMF9tSdJ8B0lylooCxc7ai90OhXtXcrAu6ACls6jfzJnC9E2yCJ";

	//public static string ACCESS_TOKEN = "";
	public static string CLIENT_ID = "81BlGaldSj5KIjUdz374fcMocXbu6AMC";
	public static string CLIENT_SECRET = "uXeALhVX4zefkW7j73tVMtKE35R0777z8XmK14yaOp91sE41XrvcWfx5q9N9evvc";
	public static string CODE="";
	public static string REDIRECT_URI = "/";
	
	public string dateString = "20150404";

	//UI Elements
	public Button authButton;
	public Button apiButton;
	public Text displayText;

	JSONDataObject jsonDataObj;
	public SimpleJSON.JSONNode JSONData;


	public void Start(){
		Debug.Log ("before Start dateString is :" + dateString);
		Debug.Log ("starting in APIRequests");

//		authButton = GameObject.Find ("AuthButton").GetComponent<Button>();
//		apiButton = GameObject.Find ("APIButton").GetComponent<Button>();
//		displayText = GameObject.Find("DisplayText").GetComponent<Text>();

		jsonDataObj = GameObject.Find ("JSONDataObject").GetComponent<JSONDataObject> ();
		JSONData = jsonDataObj.JSONData;

		if (PlayerPrefs.GetString("CLIENT_ID")=="")
			PlayerPrefs.SetString("CLIENT_ID",CLIENT_ID);
		
		if (PlayerPrefs.GetString("CLIENT_SECRET")=="")
			PlayerPrefs.SetString("CLIENT_SECRET",CLIENT_SECRET);

		dateString = getCurrentDate ();

		Debug.Log ("APIRequests Start Func dateString is:" + dateString);
		Debug.Log ("end of start in APIRequests");
	}

	public void callGetData(){
		Debug.Log("APIRequests callGetData is called");
		StartCoroutine (getData ());
	}
	
	public void callGetToken () {
		Debug.Log("APIRequests callGetToken is Called!");
		StartCoroutine (getToken ());		
	}

	IEnumerator getData(){

		string api = getAPIString ();

		WWW request = new WWW(api);
		yield return request;
		if (request.error == null || request.error == ""){

			Debug.Log(request.text);

			SimpleJSON.JSONNode N = JSON.Parse(request.text);
			var newJSONObj = N[0];
			storeDataObj(newJSONObj);

			Debug.Log(newJSONObj);
			Debug.Log("^^^^This is the data ^^^^");
			Debug.Log(newJSONObj["date"]);
			Debug.Log("^^^^This is the N['date'] ^^^^");

		}else{
			Debug.Log("WWW error: " + request.error);
		}
	}

	//store the newly acquired JSONObj into the Gameobject call "JSONDataObject" field "JSONData"
	public void storeDataObj(SimpleJSON.JSONNode JSONObj)
	{
		jsonDataObj.JSONData = JSONObj;
	}

	public string getAPIString(){
		Debug.Log ("APIRequests getAPIString called");
		if (ACCESS_TOKEN == "")
		ACCESS_TOKEN = PlayerPrefs.GetString ("ACCESS_TOKEN");

		Debug.Log ("APIRequests getAPIString Func dateString is :" + dateString);

		return apiURL + dateString + "?access_token=" + ACCESS_TOKEN;
	}

	IEnumerator getToken(){
		
		Debug.Log("getToken is Called!");

		authURL = getAuthString ();

		WWWForm form = new WWWForm ();

		form.AddField ("some field","some data");
		WWW w = new WWW (authURL, form);
		yield return w;

		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log("something went wrong with retreiving the token");
			Debug.Log(w.error);
		}
		else {
			Debug.Log("Success getting the Access Token!!");
			Debug.Log(w.text);
			storeToken(w);
		}
	}

	public void storeToken(WWW w){
		Debug.Log("store Token is called!");
		string tokenString = parseToken (w.text);
		
		PlayerPrefs.SetString ("ACCESS_TOKEN", tokenString);
		
		ACCESS_TOKEN = tokenString;
		Debug.Log (ACCESS_TOKEN);
		Debug.Log ("This is the access token: ^^");
	}

	public string getAuthString(){
		CODE = PlayerPrefs.GetString("CODE");
		return authURL + "&code=" + CODE + "&client_id=" + CLIENT_ID + "&client_secret=" + CLIENT_SECRET + "&redirect_uri=" + REDIRECT_URI;
	}

	public string parseToken(string dataText){
		string[] result = dataText.Split('"');
		string t_string = result [3];
		return t_string;
	}

	public string getCurrentDate(){
		Debug.Log ("APIRequest getCurrentDateString called");
		string currentDateTimeString = System.DateTime.Now.ToString();
		Debug.Log ("currentDateTimeString is :"+ currentDateTimeString);
		string[] stringBits = currentDateTimeString.Split (' ');
		string t_dateString = stringBits [0];
		stringBits = t_dateString.Split ('/');
		string monthString = stringBits [0];
		string dayString = stringBits [1];
		string yearString = stringBits [2];
		string currentDateString = yearString + monthString + dayString;
		Debug.Log ("current date string is :"+ currentDateString);
		PlayerPrefs.SetString ("CURRENT_DATE_STRING",currentDateString);
		return currentDateString;
	}

}

