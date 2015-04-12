using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCCommand : MonoBehaviour {

	public NPCRaycast npcPrefab;
	public List<NPCRaycast> allMyNpcs = new List<NPCRaycast>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition);
		RaycastHit rayHit = new RaycastHit();

		if ( Physics.Raycast ( ray, out rayHit, 100f ) ){
			if (Input.GetMouseButton(0) && rayHit.collider.tag != "Player"){
				NPCRaycast newNPC = Instantiate(npcPrefab, rayHit.point, Quaternion.Euler(0f,0f,0f)) as NPCRaycast;
				allMyNpcs.Add(newNPC);
			}
		}
	}
}
