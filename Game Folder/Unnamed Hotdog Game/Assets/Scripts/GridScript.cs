using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour {

	public Sprite blank, placable, notPlacable;
	SpriteRenderer spriteRenderer;
	public LayerMask sense;

	void FixedUpdate(){
		foreach (Transform nodes in transform) {
			spriteRenderer = nodes.GetComponent<SpriteRenderer> ();
			RaycastHit hit = new RaycastHit();
			Ray ray = new Ray (nodes.transform.position, transform.up);
			if (Physics.Raycast (ray ,out hit, 10f, sense)) {
				if (hit.transform.gameObject.GetComponent<ObjectDrag> ().placable)
					spriteRenderer.sprite = placable;
				else
					spriteRenderer.sprite = notPlacable;
			}
			if (!Physics.Raycast (ray, out hit, 10f, sense)) {
				spriteRenderer.sprite = blank;
			}
		}
	}
}
