using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smoothTimeX;
	public float smoothTimeY;
	
	public GameObject player;
	
	public bool bounds;
	
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;
	public Quaternion cameraRotation;

	private Vector2 velocity;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		cameraRotation = new Quaternion (95,transform.rotation.y, transform.rotation.z, transform.rotation.w);
		transform.rotation = cameraRotation;
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y+5, ref velocity.y, smoothTimeY);
		
		transform.position = new Vector3 (posX, posY, transform.position.z);
		
		if (bounds) {
			transform.position = new Vector3(
				(Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x)),
				(Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y)),
				(Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z))
				);
		}
		
	}
}
