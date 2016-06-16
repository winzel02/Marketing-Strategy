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
	GameObject selectedButton, editSelectedButton;
	Button moveButton, infoButton;
	UserInterface uiScript;
	//Fix Chance of Buyer to buy
	void Awake()
	{
		uiScript = GameObject.Find ("Canvas").GetComponent<UserInterface> ();
		selectedButton = gameObject.transform.GetChild (0).gameObject.transform.FindChild ("ButtonSelect").gameObject;
		editSelectedButton = gameObject.transform.GetChild (0).gameObject.transform.FindChild ("EditSelect").gameObject;

		moveButton = editSelectedButton.transform.FindChild ("Move").gameObject.GetComponent<Button>();
		moveButton.onClick.AddListener (uiScript.ShopMove);

		infoButton = selectedButton.transform.FindChild ("Info").gameObject.GetComponent<Button>();
		infoButton.onClick.AddListener (uiScript.ShopInfo);

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
		if (!GameManager.GM.editBase)
			selectedButton.SetActive (true);
		else
			editSelectedButton.SetActive (true);
	}
	void DeSelected()
	{
		if (!GameManager.GM.editBase)
			selectedButton.SetActive (false);
		else
			editSelectedButton.SetActive (false);
	}
}
