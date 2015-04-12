using UnityEngine;
using System.Collections;

/*
 * All the functions called by clicking elements in the game.
 * The functions need to be re-classified into a Quest class, an API class, a json data class, etc.
 */


public class ButtonEvent : MonoBehaviour {

	// No setter function for APIInterval yet. Should belong to API class.
	private int APIInterval = 10;// represents how often (in seconds/time) are the API calls made.

	public bool questFinished = true;

	APIRequests APIObj;
	JSONDataObject jsonDataObj;

	public void Start(){
		APIObj = GameObject.Find ("APIRequests").GetComponent<APIRequests> ();
		jsonDataObj = GameObject.Find ("JSONDataObject").GetComponent<JSONDataObject> ();

	}

	public void doRequestMovesAuthInApp()
	{
		Debug.Log("ButtonEvent doRequstMovesAuthInApp Called!");
		//Bridge.ShowCamera (12345);
		Bridge.doRequestMovesAuthInApp ();
	}

	public void getMovesAPIData(){
		Debug.Log ("ButtonEvent getMovesAPIData called");
		APIObj.callGetData ();
	}

	public void callGetDataForQuest(){
		Debug.Log("ButtonEvent callGetDataForQuest called");
		StartCoroutine (getDataForQuest());
	}

	public void callTimedAPICalls(){
		Debug.Log("ButtonEvent callTimedAPICalls called");
		StartCoroutine (timedAPICalls());
	}

	public void changeQuestStatus(){
		questFinished = !questFinished;
	}

	IEnumerator getDataForQuest(){
		Debug.Log ("Button Event getDataForQuest called");
			
		string ACCESS_TOKEN = PlayerPrefs.GetString ("ACCESS_TOKEN");
		if (ACCESS_TOKEN == "") {
			doRequestMovesAuthInApp();

			yield return new WaitForSeconds(2);

			while (PlayerPrefs.GetString("ACCESS_TOKEN")==""){}// IMPERFECT WAY TO YIELD

			getMovesAPIData();
		} else {
			getMovesAPIData();
			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator timedAPICalls(){
		Debug.Log("ButtonEvent timedAPICalls called");

		while (!questFinished) {
			getMovesAPIData ();
			yield return new WaitForSeconds (APIInterval);
		}

		// need to design a flag mechanism to turn off the calls and exit the function.
	}

	public void doSomethingWithData(){
		jsonDataObj.doSomethingWithData ();
	}


}
