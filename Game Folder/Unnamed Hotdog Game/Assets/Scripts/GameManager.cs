using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {

	private static GameManager _gManager;
	public static GameManager GM
	{
		get
		{ 
			if (_gManager == null) {
				GameObject go = new GameObject ("_GameMaster");
				go.AddComponent<GameManager> ();
			}
			return _gManager;
		}
	}
	public int buyerPercentage = 20;
	public int numberOfCrowd;
	public List<GameObject> storeList = new List<GameObject>();
	public GameObject[] sides;
	public GameObject crowdPool;
	public float crowdMovespeed = 1;

	public int currentCash;

	void Awake()
	{
		_gManager = this;
		crowdPool = GameObject.Find ("CrowdPooling");
		sides = GameObject.FindGameObjectsWithTag ("Side");

		GameObject[] storesArray = GameObject.FindGameObjectsWithTag ("Store");
		foreach (GameObject go in storesArray) {
			storeList.Add (go);
		}
	}
}
