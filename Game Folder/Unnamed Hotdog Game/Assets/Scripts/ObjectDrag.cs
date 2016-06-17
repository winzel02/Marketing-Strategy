using UnityEngine;
using System.Collections;

public class ObjectDrag : MonoBehaviour {

	public float gridSize = 1f;
	public bool placable = true;
	GameObject moveImage;
	public bool justMoving;

	void Awake()
	{
		moveImage = gameObject.transform.GetChild(0).transform.GetChild(0).transform.Find ("MoveImage").gameObject;
		moveImage.SetActive (true);
	}

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
		mousePosition.y = 0f;
		mousePosition.z = (int)((mousePosition.z + mousePosition.y)/gridSize)*gridSize + 9;
		transform.position = mousePosition;
	}
	void OnMouseUp()
	{
		if (placable) {
			if (justMoving) {
				moveImage.SetActive (false);
				Destroy (this);
			} else {
				StoreInfo info = GetComponent<StoreInfo> ();
				GameManager.GM.storeList.Add (this.gameObject);
				GameManager.GM.currentCash -= info.cashRequiredToBuy;
				transform.parent = GameObject.Find ("Stores").transform;
				moveImage.SetActive (false);
				Destroy (this);
			}
		}
	}
}
