using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour {


	public GameObject storeButton, storePanel, shopInfo,levelImage;
	Text cashText;
	public List<GameObject> storeShopList = new List<GameObject>();

	void Awake()
	{
		cashText = transform.FindChild ("CashText").GetComponent<Text> ();
	}

	void Update()
	{
		cashText.text = "Cash: " + GameManager.GM.currentCash.ToString();
	}
	public void StoreEnter()
	{
		storePanel.SetActive (true);
		storeButton.SetActive (false);
		cashText.gameObject.SetActive (false);
		levelImage.SetActive (false);
	}
	public void StoreExit()
	{
		storePanel.SetActive (false);
		storeButton.SetActive (true);
		cashText.gameObject.SetActive (true);
		levelImage.SetActive (true);
	}
	public void StoreClick()
	{
		foreach (GameObject store in storeShopList) {
			if (store.name == EventSystem.current.currentSelectedGameObject.name) {
				StoreInfo info = store.GetComponent<StoreInfo> ();
				if (GameManager.GM.currentCash >= info.cashRequiredToBuy) {
					Instantiate (store, new Vector3 (0f, 0.53f, 0f), Quaternion.identity);
					storePanel.SetActive (false);
					storeButton.SetActive (true);
					cashText.gameObject.SetActive (true);

				} else
					print ("Not Enough Money To Buy");
			}
		}

	}

	public void ShopMove() //Init in script
	{
		GameObject selectedToMove = EventSystem.current.currentSelectedGameObject;
		selectedToMove.transform.parent.gameObject.SetActive (false);
		selectedToMove.transform.parent.transform.parent.transform.parent.gameObject.AddComponent<ObjectDrag>();
		ObjectDrag dragScrip = selectedToMove.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<ObjectDrag> ();
		dragScrip.justMoving = true;
	}
	public void ShopInfo() //Init in script
	{
		shopInfo.SetActive (true);
	}
	public void ShopInfoExit()
	{
		shopInfo.SetActive (false);
	}
}
