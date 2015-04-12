using UnityEngine;
using System.Collections;


/**
 * JavaCallback is called by the Java library to pass on the Authentication Code( abtained from moves auth) to Unity
**/
public class JavaCallback : MonoBehaviour {

	void onActivityResult(string theString){
		Debug.Log ("onActivityResult in Unity is Called: "+theString);

		string[] result = theString.Split('=');
		string authCode = result [1];
		result = authCode.Split ('&');
		authCode = result [0];//authCode is the authentication code from Moves.
		
		Debug.Log ("the Code is: "+authCode);
		PlayerPrefs.SetString ("CODE", authCode);
		APIRequests APIObj = GameObject.Find ("APIRequests").GetComponent<APIRequests> ();
		APIObj.callGetToken ();
	}//end of onActivityResult

}
