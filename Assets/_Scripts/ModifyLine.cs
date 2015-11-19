using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This script needs to grab info from the output analyzer to change this line's amplitudes
//This script needs to store line positions in a Vector3 array
public class ModifyLine : MonoBehaviour {

	LineRenderer lineRenderer;
	int segmentCount;
	float segmentX;
	float segmentY;
	float segmentZ;
	int lastSegment;
	Vector3 newPosition;
	public List<Vector3> positions;

	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		segmentCount = 2;//initial and final
		positions = new List<Vector3>();
		positions.Add (new Vector3 (0, 0, 0));//initial position
		positions.Add (new Vector3 (0, 0, 0));//final position
	}
	public void IncreaseLineSize(){
		//increases line size
		segmentCount++;
		lineRenderer.SetVertexCount (segmentCount);
		lastSegment = segmentCount - 1;

	}
	//sets the last segment to the new x, y, and z coordinates
	public void SetLastSegmentPosition(float x, float y, float z){
		segmentX = x;
		segmentY = y;
		segmentZ = z;
		newPosition = new Vector3 (x, y, z);
		positions.Add (newPosition);
		lineRenderer.SetPosition (lastSegment, newPosition);
	}

	//Grabs last Vector3 from position list
	public Vector3 GetFinalPosition(){
		Vector3 finalPos = positions [lastSegment];
		return finalPos;
	}
}
