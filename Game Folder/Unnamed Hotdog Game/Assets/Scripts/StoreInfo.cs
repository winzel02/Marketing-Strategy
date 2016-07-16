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
	public int maximumSupplyHold;
	public int costForRestock;
	public int currentSupply;

	int cashEarning;
	GameObject moveButton, sellButton, infoButton, canvas, restockButton;
	//Fix Chance of Buyer to buy
	void Awake()
	{
		canvas = GameObject.Find ("Canvas");

		moveButton = canvas.transform.Find ("MoveButton").gameObject;
		sellButton = canvas.transform.Find ("SellButton").gameObject;
		infoButton = canvas.transform.Find ("InfoButton").gameObject;
		restockButton = canvas.transform.Find ("RestockSupplyButton").gameObject;

		switch (storeType) {
		case SelectedStoreType.Lemonnade:
			cashEarning = 20;
			cashToEarn = storeLevel * cashEarning;
			currentSupply = 50;
			maximumSupplyHold = 50;
			costForRestock = 100;
			break;
		case SelectedStoreType.Hotdog:
			cashEarning = 50;
			cashToEarn = storeLevel * cashEarning;
			currentSupply = 75;
			maximumSupplyHold = 75;
			costForRestock = 125;
			break;
		}
	}
	void Update()
	{
		if (currentSupply < 10) {
			//Low on Supply reaction.
		}
	}
	void Selected()
	{
		if (!GameManager.GM.editBase) {
			infoButton.SetActive (true);
			restockButton.SetActive (true);
		} else {
			moveButton.SetActive (true);
			sellButton.SetActive (true);
		}
	}
	void DeSelected()
	{
		if (!GameManager.GM.editBase) {
			infoButton.SetActive (false);
			restockButton.SetActive (false);
		} else {
			moveButton.SetActive (false);
			sellButton.SetActive (false);
		}
	}
}
