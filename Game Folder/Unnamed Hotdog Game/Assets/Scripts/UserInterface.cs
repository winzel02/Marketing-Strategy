﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour {


	public GameObject storeButton, storePanel, shopInfo,levelImage, editBaseButton, editBaseExitButton, gridGO, moveButton,
					  infoButton, sellButton,cancelMoveButton, shopStoresPanel, shopHousesPanel, shopDecorationsPanel, restockButton;
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
		editBaseButton.SetActive (false);
	}
	public void StoreExit()
	{
		storePanel.SetActive (false);
		storeButton.SetActive (true);
		cashText.gameObject.SetActive (true);
		levelImage.SetActive (true);
		editBaseButton.SetActive (true);
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
					storeButton.SetActive (false);
					gridGO.SetActive (true);
					foreach (GameObject crowd in GameManager.GM.crowdList) {
						crowd.SetActive (false);
					}
				} else
					print ("Not Enough Money To Buy");
			}
		}

	}

	public void ShopMove() //Init in script
	{
		GameObject selectedToMove = touchInput.currentSelectedStore;
		selectedToMove.AddComponent<ObjectDrag> ();
		editBaseExitButton.SetActive (false);
		moveButton.SetActive (false);
		sellButton.SetActive (false);
	}
	public void ShopInfo() //Init in script
	{
		shopInfo.SetActive (true);
		infoButton.SetActive (false);
		restockButton.SetActive (false);
	}
	public void RestockSupply()
	{
		
		StoreInfo currentStore = Camera.main.GetComponent<TouchInput>().currentSelectedStore.GetComponent<StoreInfo>();
		float cost = ((float)(currentStore.maximumSupplyHold - currentStore.currentSupply) / currentStore.maximumSupplyHold) * currentStore.costForRestock;
		GameManager.GM.currentCash -= (int)cost;
		currentStore.currentSupply = currentStore.maximumSupplyHold;

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
		if (touchInput.currentSelectedStore != null) {
			touchInput.currentSelectedStore.transform.SendMessage ("DeSelected", SendMessageOptions.DontRequireReceiver);
			if (touchInput.currentSelectedStore.GetComponent<ObjectDrag> ())
				touchInput.currentSelectedStore.transform.SendMessage ("OnMouseUp", SendMessageOptions.DontRequireReceiver);
		}
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
	public void ShopStoresPanel()
	{
		shopStoresPanel.SetActive (true);
		shopHousesPanel.SetActive (false);
		shopDecorationsPanel.SetActive (false);
	}
	public void ShopHousesPanel()
	{
		shopStoresPanel.SetActive (false);
		shopHousesPanel.SetActive (true);
		shopDecorationsPanel.SetActive (false);
	}
	public void ShopDecorationsPanel()
	{
		shopStoresPanel.SetActive (false);
		shopHousesPanel.SetActive (false);
		shopDecorationsPanel.SetActive (true);
	}

}
