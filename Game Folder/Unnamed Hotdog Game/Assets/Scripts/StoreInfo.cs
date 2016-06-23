using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreInfo : MonoBehaviour {

	public enum SelectedStoreType
	{
		Hotdog,
		Lemonnade
	}
	public SelectedStoreType storeType;

	public int storeLevel = 1;
	public int cashToEarn;
	public int cashRequiredToBuy = 100;
	public int buyerChance;

	int cashEarning;
	GameObject moveButton, sellButton, infoButton, canvas;
	//Fix Chance of Buyer to buy
	void Awake()
	{
		canvas = GameObject.Find ("Canvas");

		moveButton = canvas.transform.Find ("MoveButton").gameObject;
		sellButton = canvas.transform.Find ("SellButton").gameObject;
		infoButton = canvas.transform.Find ("InfoButton").gameObject;

		switch (storeType) {
		case SelectedStoreType.Lemonnade:
			cashEarning = 20;
			cashToEarn = storeLevel * cashEarning;
			break;
		case SelectedStoreType.Hotdog:
			cashEarning = 50;
			cashToEarn = storeLevel * cashEarning;
			break;
		}
	}
	void Selected()
	{
		if (!GameManager.GM.editBase) {
			infoButton.SetActive (true);
		} else {
			moveButton.SetActive (true);
			sellButton.SetActive (true);
		}
	}
	void DeSelected()
	{
		if (!GameManager.GM.editBase) {
			infoButton.SetActive (false);
		} else {
			moveButton.SetActive (false);
			sellButton.SetActive (false);
		}
	}
}
