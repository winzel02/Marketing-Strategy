using UnityEngine;
using System.Collections;

public class ObjectDrag : MonoBehaviour {

	public float gridSize = 1f;
	public bool placable = true;
	GameObject moveImage, gridGO, levelBar, editButton, storeButton;
	public bool justMoving;
	UserInterface ui;
	Vector3 mousePosition;

	Transform graphic;
	void Awake()
	{
		moveImage = gameObject.transform.GetChild(0).transform.GetChild(0).transform.Find ("MoveImage").gameObject;
		moveImage.SetActive (true);
		ui = GameObject.Find ("Canvas").GetComponent<UserInterface> ();
		transform.position = new Vector3 (transform.position.x, 2f, transform.position.z);
		graphic = transform.GetChild (0);
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Store")
			placable = false;
	}
	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Store")
			placable = false;
	}
	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "Store")
			placable = true;
	}
	void Update()
	{
		RaycastHit hit = new RaycastHit ();
		placable = !Physics.BoxCast (new Vector3(transform.position.x, transform.position.y, transform.position.z ),
			new Vector3(graphic.localScale.x/2 - 0.5f, graphic.localScale.y/2, graphic.localScale.z/2),
									 -transform.up, out hit, Quaternion.identity, 2f, 1 << LayerMask.NameToLayer("Touchable"));
	}
	void OnMouseDrag()
	{
		gridGO = ui.gridGO;
		levelBar = ui.levelImage;
		editButton = ui.editBaseButton;
		storeButton = ui.storeButton;
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.x = (int)((mousePosition.x + mousePosition.y)/gridSize)*gridSize + 1;
		mousePosition.y = 2f;
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
				gridGO.SetActive (false);
				levelBar.SetActive (true);
				editButton.SetActive (true);
				storeButton.SetActive (true);
				Destroy (this);
			}
			transform.position = new Vector3 (transform.position.x,0f, transform.position.z);
		}
	}
}
