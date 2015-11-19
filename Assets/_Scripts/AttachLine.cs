using UnityEngine;
using System.Collections;

//Will be attached to player object to attach the line renderer and change it's position depending on
//last position of line renderer;
public class AttachLine : MonoBehaviour {

	public GameObject line;
	ModifyLine lineScript;

	void Start () {
		lineScript = line.GetComponent<ModifyLine> ();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = lineScript.GetFinalPosition ();
	}
}
