using UnityEngine;
using System.Collections;

public class StoreInfo : MonoBehaviour {

	public enum StoreType
	{
		Hotdog,
		Lemonnade
	}
	public StoreType selectedStoreType;

	public int storeLevel = 1;
	public int cashToEarn;
	public int cashRequiredToBuy = 100;
	int cashEarning;
	//Fix Chance of Buyer to buy
	void Awake()
	{
		switch (selectedStoreType) {
		case StoreType.Lemonnade:
			cashEarning = 20;
			cashToEarn = storeLevel * cashEarning;
			break;
		case StoreType.Hotdog:
			cashEarning = 50;
			cashToEarn = storeLevel * cashEarning;
			break;
		}
	}
}
