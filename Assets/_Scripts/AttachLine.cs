using UnityEngine;
using System.Collections;

//Will be attached to player object to attach the line renderer and change it's position depending on
//last position of line renderer;
public class AttachLine : MonoBehaviour {

	public GameObject line;
	ModifyLine lineScript;
	float posX;
	float posY;
//	Vector3 newPosition;
	public bool justX;
	public float offsetX;

	private Vector2 velocity;

	void Start () {
		lineScript = line.GetComponent<ModifyLine> ();
	}
	
	// Update is called once per frame
	void Update () {
//		newPosition = new Vector3 (lineScript.GetFinalPosition ().x, 0, lineScript.GetFinalPosition ().z);
//		gameObject.transform.position = lineScript.GetFinalPosition ();
//		gameObject.transform.position = newPosition;

		posX = Mathf.SmoothDamp (transform.position.x, (float)(lineScript.GetFinalPosition().x), ref velocity.x, .7f);
		posY = Mathf.SmoothDamp (transform.position.y, (float)(lineScript.GetFinalPosition().y), ref velocity.y, .1f);

		if (justX) {
			transform.position = new Vector3 (posX+offsetX, transform.position.y, transform.position.z);
		} else {
			transform.position = new Vector3 ((float)(posX + offsetX), posY, transform.position.z);
		}
//		transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}
}
