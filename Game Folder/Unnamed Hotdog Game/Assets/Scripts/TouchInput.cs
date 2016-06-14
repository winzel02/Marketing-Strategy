using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	//Specifically on the GameObject.
	public LayerMask mask;
	public GameObject currentSelectedStore, lastSelectedStore;
	public float dragSpeed = 2f;
	Vector3 dragOrigin;
	void Update(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		//EDITOR_BUILD
		#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0)){
			dragOrigin = Input.mousePosition;
			if(Physics.Raycast(ray, out hit,100f, mask)){
				currentSelectedStore = hit.collider.gameObject;
				if(lastSelectedStore == null){
					currentSelectedStore.transform.SendMessage("Selected", hit.point, SendMessageOptions.DontRequireReceiver);
					lastSelectedStore = currentSelectedStore;
				} else if(lastSelectedStore != null) {
					if(currentSelectedStore == lastSelectedStore){
						currentSelectedStore.transform.SendMessage("Selected", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if(currentSelectedStore != lastSelectedStore){
						currentSelectedStore.transform.SendMessage("Selected", hit.point, SendMessageOptions.DontRequireReceiver);
						lastSelectedStore.transform.SendMessage("DeSelected", hit.point, SendMessageOptions.DontRequireReceiver);
						lastSelectedStore = currentSelectedStore;
					}
				}
			}	
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0f);
			transform.Translate(move, Space.Self);
			Vector3 clampPosition = transform.position;
			clampPosition.x = Mathf.Clamp(clampPosition.x, -11f, -7f);
			clampPosition.y = Mathf.Clamp(clampPosition.y, 7f ,8f);
			clampPosition.z = Mathf.Clamp(clampPosition.z, -12f, -6f);
			transform.position = clampPosition;
			//Touching On Specifically on the GameObject's Collider.
		}

		#endif
		/*ANDROID BUILD
		if (Input.touchCount > 0)
		{
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			foreach(Touch touch in Input.touches)
			{
				//Touching On Screen.
				if(hit.collider == null)
				{	
					if(touch.phase == TouchPhase.Began){
						SendMessage("ScreenTouchDown", SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Ended){
						SendMessage("ScreenTouchUp",SendMessageOptions.DontRequireReceiver);
					}
				}
				//Touching On Specifically on the GameObject's Collider.
				if(hit.collider != null)
				{
					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);
					if(touch.phase == TouchPhase.Began){
						recipient.SendMessage("OnTouchDown", hit.point,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Ended){
						recipient.SendMessage("OnTouchUp", hit.point,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Moved){
						recipient.SendMessage("OnMouseDrag", hit.point,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Canceled){
						recipient.SendMessage("OnTouchExit", hit.point,SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			foreach (GameObject g in touchesOld)
			{
				if(!touchList.Contains(g)){
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}*/
	}
}
