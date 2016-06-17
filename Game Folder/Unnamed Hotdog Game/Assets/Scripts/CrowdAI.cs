using UnityEngine;
using System.Collections;

public class CrowdAI : MonoBehaviour {

	public bool isBuying, doneBuying, isPassing;
	Vector3 storeTargetPos, sides;
	private Vector3 storeTargetOffset;
	bool eating;
	float randomNumber;
	int totalChances = 0;
	GameObject storeTarget;
	void Awake()
	{
		isBuying = false;


	}
	void Start()
	{
		StartCoroutine(Reuse ());
	}
	void GenerateRandomSidePosition()
	{
		sides = GameManager.GM.sides[Random.Range(0, GameManager.GM.sides.Length)].transform.position;
		sides.x = Random.Range (-4.8f, 1f);

	}
	void GenerateRandomStoreTarget()
	{
		for (int i = 0; i < GameManager.GM.storeList.Count; i++) {
			StoreInfo info = GameManager.GM.storeList [i].GetComponent<StoreInfo> ();
			totalChances += info.buyerChance;
		}
		float chance;
		randomNumber = Random.Range (0f, GameManager.GM.buyerPercentage);
		chance = 0f;
		GameObject[] storeListToArray = GameManager.GM.storeList.ToArray ();
		chance = (float)storeListToArray[0].GetComponent<StoreInfo>().buyerChance / totalChances * GameManager.GM.buyerPercentage;

		if (randomNumber > 0 && randomNumber <= chance) {
			storeTarget = storeListToArray [0];
			storeTargetPos = storeTarget.transform.position;
			storeTargetOffset = new Vector3 (storeTargetPos.x- 2.41f, storeTargetPos.y, storeTargetPos.z + Random.Range (-0.5f, 0.5f));
		} else if(randomNumber > chance) {
			for (int i = 1; i < GameManager.GM.storeList.Count; i++) {
				StoreInfo info = GameManager.GM.storeList [i].GetComponent<StoreInfo> ();
				chance = (float)info.buyerChance / totalChances * GameManager.GM.buyerPercentage + chance;
				if (randomNumber > (float)info.buyerChance / totalChances * GameManager.GM.buyerPercentage - info.buyerChance && randomNumber <= chance) {
					storeTarget = info.gameObject;
					storeTargetPos = storeTarget.transform.position;
					storeTargetOffset = new Vector3 (storeTargetPos.x - 2.41f, storeTargetPos.y, storeTargetPos.z + Random.Range (-0.5f, 0.5f));
				}
			}
		}
	}
	IEnumerator Reuse()
	{
		eating = true;
		isBuying = false;
		doneBuying = false;
		isPassing = false;
		yield return new WaitForSeconds (Random.Range (2f, 20f));
		GenerateRandomSidePosition ();
		transform.position = sides;
		int buyNumber = Random.Range (0, 100);

		if (buyNumber < GameManager.GM.buyerPercentage) {
			GenerateRandomStoreTarget ();
			GenerateRandomSidePosition ();
			isBuying = true;
		} else{
			foreach (GameObject gSides in GameManager.GM.sides) {
				if (Vector3.Distance (transform.position, gSides.transform.position) > 5f) {
					sides = gSides.transform.position;
				}
			}
			sides.x = Random.Range (-4.8f, 1f);
			int randomAII = Random.Range (0, 0);
			if (randomAII == 0)
				isPassing = true;
		}

		yield break;
	}
	void FixedUpdate()
	{
		if (isBuying)
			StartCoroutine (BuyingAI ());
		if (doneBuying)
			DoneBuying ();
		if (isPassing)
			JustPassing ();
	}
	IEnumerator BuyingAI()
	{
		doneBuying = false;
		if (eating) {
			transform.LookAt (storeTargetOffset);
			transform.Translate (Vector3.forward * Time.deltaTime * GameManager.GM.crowdMovespeed);
		}
		if (Vector3.Distance (transform.position, storeTargetOffset) < 0.01f) {
			eating = false;
			transform.LookAt (new Vector3(storeTargetPos.x, storeTargetPos.y, storeTargetPos.z));
			yield return new WaitForSeconds (Random.Range (3, 7));
			doneBuying = true;
			isBuying = false;
		}
		yield break;
	}
	void DoneBuying()
	{	
		
		if (!eating) {
			
			StoreInfo storeScript = storeTarget.GetComponent<StoreInfo> ();
			GameManager.GM.currentCash += storeScript.cashToEarn;
			print ("Just earned " + storeScript.cashToEarn + " from level " + storeScript.storeLevel + " " + storeScript.storeType.ToString() + " store");
			eating = true;
		}
		transform.LookAt (sides);
		transform.Translate (Vector3.forward * Time.deltaTime * GameManager.GM.crowdMovespeed);
		if (Vector3.Distance (transform.position, sides) < 0.01f) {
			DisEnable ();
			doneBuying = false;
		}
	}
	void JustPassing()
	{
		transform.LookAt (sides);
		transform.Translate (Vector3.forward * Time.deltaTime * GameManager.GM.crowdMovespeed);
		if (Vector3.Distance (transform.position, sides) < 0.01f) {
			DisEnable ();
			isPassing = false;
		}
	}
	void DisEnable()
	{
		transform.rotation = new Quaternion (0f, 0f, 0f,0f);
		transform.position = new Vector3 (8f, -10f, 10f);
		StartCoroutine (Reuse ());
	}
	void ReEnable()
	{
		DisEnable ();
	}
}
