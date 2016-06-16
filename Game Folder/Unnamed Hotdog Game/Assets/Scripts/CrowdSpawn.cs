using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdSpawn : MonoBehaviour {

	[SerializeField] private GameObject crowdGO;


	void Start()
	{
		InstantiateCrowds ();
	}
	void InstantiateCrowds()
	{
		for (int i = 0; i < GameManager.GM.numberOfCrowd; i++) {
			GameObject instantiated = (GameObject)Instantiate (crowdGO, new Vector3(0, -10f, 0), Quaternion.identity);
			//CrowdAI aiScript = instantiated.GetComponent<CrowdAI>();
			instantiated.name = "Crowd";
			instantiated.transform.parent = GameManager.GM.crowdPool.transform;
			GameManager.GM.crowdList.Add (instantiated);
		}
	}
}
