using UnityEngine;
using System.Collections;

public class CrowdAI : MonoBehaviour {

	public bool isBuying, doneBuying, isPassing;
	MeshRenderer mesh;
	Vector3 storeTargetPos, sides;
	private Vector3 storeTargetOffset;
	bool eating;


	GameObject storeTarget;
	void Awake()
	{
		mesh = GetComponent<MeshRenderer> ();
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
	IEnumerator Reuse()
	{
		eating = true;
		isBuying = false;
		doneBuying = false;
		isPassing = false;
		yield return new WaitForSeconds (Random.Range (2f, 20f));
		GenerateRandomSidePosition ();
		transform.position = sides;
		int possibilityToBuy = Random.Range (0, 100);
		if (possibilityToBuy < GameManager.GM.buyerPercentage) {
			storeTarget = GameManager.GM.storeList [Random.Range(0, GameManager.GM.storeList.Count)];
			storeTargetPos = storeTarget.transform.position;
			storeTargetOffset = new Vector3 (storeTargetPos.x - 2.41f, storeTargetPos.y - 0.52f, storeTargetPos.z + Random.Range(-0.5f, 0.5f));
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
			StartCoroutine (DoneBuying ());
		if (isPassing)
			StartCoroutine (JustPassing ());
	}
	IEnumerator BuyingAI()
	{
		if (eating) {
			transform.LookAt (storeTargetOffset);
			transform.Translate (Vector3.forward * Time.deltaTime * GameManager.GM.crowdMovespeed);
		}
		if (Vector3.Distance (transform.position, storeTargetOffset) < 0.01f) {
			eating = false;
			transform.LookAt (new Vector3(storeTargetPos.x, storeTargetPos.y - 0.52f, storeTargetPos.z));
			yield return new WaitForSeconds (Random.Range (3, 7));
			isBuying = false;
			doneBuying = true;
		}
		yield break;
	}
	IEnumerator DoneBuying()
	{	
		if (!eating) {
			GenerateRandomSidePosition ();
			eating = true;
			StoreInfo storeScript = storeTarget.GetComponent<StoreInfo> ();
			GameManager.GM.currentCash += storeScript.cashToEarn;
			print ("Just earned " + storeScript.cashToEarn + " from level " + storeScript.storeLevel + " " + storeScript.selectedStoreType.ToString() + " store");
		}
		transform.LookAt (sides);
		transform.Translate (Vector3.forward * Time.deltaTime * GameManager.GM.crowdMovespeed);
		if (Vector3.Distance (transform.position, sides) < 0.01f) {
			doneBuying = false;
			DisEnable ();
			yield break;
		}
	}
	IEnumerator JustPassing()
	{
		transform.LookAt (sides);
		transform.Translate (Vector3.forward * Time.deltaTime * GameManager.GM.crowdMovespeed);
		if (Vector3.Distance (transform.position, sides) < 0.01f) {
			isPassing = false;
			DisEnable ();
		}
		yield break;
	}
	void DisEnable()
	{
		transform.position = new Vector3 (0f, -10f, 0f);
		StartCoroutine (Reuse ());
	}
}
