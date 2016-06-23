using UnityEngine;
using System.Collections;

public class ObjectDrag : MonoBehaviour {

	public float gridSize = 1f;
	public bool placable = true;
	GameObject gridGO,exitEditButton;
	UserInterface ui;
	Vector3 mousePosition;

	Transform graphic;
	void Awake()
	{
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
		cancelMoveButton = ui.cancelMoveButton;
		exitEditButton = ui.editBaseExitButton;
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.x = (int)((mousePosition.x + mousePosition.y)/gridSize)*gridSize + 1;
		mousePosition.y = 2f;
		mousePosition.z = (int)((mousePosition.z + mousePosition.y)/gridSize)*gridSize + 9;
		transform.position = mousePosition;
	}
	void OnMouseUp()
	{
		if (placable) {
			
			StoreInfo info = GetComponent<StoreInfo> ();
			GameManager.GM.storeList.Add (this.gameObject);
			GameManager.GM.currentCash -= info.cashRequiredToBuy;
			transform.parent = GameObject.Find ("Stores").transform;
			gridGO.SetActive (true);
			exitEditButton.SetActive (true);
			Destroy (this);

			transform.position = new Vector3 (transform.position.x,0f, transform.position.z);
		}
	}
}
