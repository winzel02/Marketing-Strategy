using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour {


	public GameObject storeButton, storePanel, shopInfo,levelImage, editBaseButton, editBaseExitButton, gridGO;
	Text cashText;
	public List<GameObject> storeShopList = new List<GameObject>();
	TouchInput touchInput;
	void Awake()
	{
		cashText = transform.FindChild ("CashText").GetComponent<Text> ();
		touchInput = Camera.main.GetComponent<TouchInput> ();
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
					if(touchInput.currentSelectedStore != null)
						touchInput.currentSelectedStore.transform.SendMessage("DeSelected", SendMessageOptions.DontRequireReceiver);
					Instantiate (store, new Vector3 (0f, 0f, 0f), Quaternion.identity);
					touchInput.currentSelectedStore = store;
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
		ObjectDrag dragScrip =  selectedToMove.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.AddComponent<ObjectDrag>();
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
	public void BaseEdit()
	{
		if(touchInput.currentSelectedStore != null)
			touchInput.currentSelectedStore.transform.SendMessage ("DeSelected", SendMessageOptions.DontRequireReceiver);
		editBaseButton.SetActive (false);
		storeButton.SetActive (false);
		cashText.gameObject.SetActive (false);
		levelImage.SetActive (false);
		editBaseExitButton.SetActive (true);
		gridGO.SetActive (true);
		GameManager.GM.editBase = true;
		foreach (GameObject crowd in GameManager.GM.crowdList) {
			crowd.SetActive (false);
		}

	}
	public void BaseEditExit()
	{
		if(touchInput.currentSelectedStore != null)
			touchInput.currentSelectedStore.transform.SendMessage ("DeSelected", SendMessageOptions.DontRequireReceiver);
		editBaseExitButton.SetActive (false);
		editBaseButton.SetActive (true);
		storeButton.SetActive (true);
		cashText.gameObject.SetActive (true);
		levelImage.SetActive (true);
		gridGO.SetActive (false);
		GameManager.GM.editBase = false;
		foreach (GameObject crowd in GameManager.GM.crowdList) {
			crowd.SetActive (true);
			crowd.transform.SendMessage ("ReEnable", SendMessageOptions.DontRequireReceiver);
		}
	}
}
