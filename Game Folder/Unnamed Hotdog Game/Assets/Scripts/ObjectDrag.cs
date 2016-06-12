using UnityEngine;
using System.Collections;

public class ObjectDrag : MonoBehaviour {

	float gridSize = 1f;
	public bool placable;

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Store")
			placable = false;
	}
	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "Store")
			placable = true;
	}
	void OnMouseDrag()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.x = (int)((mousePosition.x + mousePosition.y)/gridSize)*gridSize + 1;
		mousePosition.y = 0.53f;
		mousePosition.z = (int)((mousePosition.z + mousePosition.y)/gridSize)*gridSize + 9;
		transform.position = mousePosition;
	}
	void OnMouseUp()
	{
		if (placable) {
			StoreInfo info = GetComponent<StoreInfo> ();
			GameManager.GM.storeList.Add (this.gameObject);
			print ("Store Placed");
			GameManager.GM.currentCash -= info.cashRequiredToBuy;
			Destroy (this);
		}
	}
}
