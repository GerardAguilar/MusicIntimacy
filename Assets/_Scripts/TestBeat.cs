using UnityEngine;
using System.Collections;

public class TestBeat : MonoBehaviour, AudioProcessor2.AudioCallbacks {

//	AudioProcessor processor;
	AudioProcessor2 processor2;
	bool flip;

	// Use this for initialization
	void Start () {
//		Debug.Log ("1. Processor: " + processor + "This: " + this);
//		processor = FindObjectOfType<AudioProcessor> ();
//		Debug.Log ("2. Processor: " + processor + "This: " + this);
//		processor.addAudioCallback (this);//this one is erroring out, is it that the processor is not being found?
//		//never gets to here
//		Debug.Log ("3. Processor: " + processor + "This: " + this);
		processor2 = FindObjectOfType<AudioProcessor2> ();
		processor2.addAudioCallback (this);
		flip = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onOnbeatDetected(){
		Debug.Log ("Beat!!!");
		flip = !flip;
		if (flip) {
//			gameObject.GetComponent<SpriteRenderer> ().material.color = Color.green;
			gameObject.transform.Rotate (Vector3.forward * -45);
		} else {
//			gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
			gameObject.transform.Rotate (Vector3.forward * 45);
		}


	}
	public void onSpectrum(float[] spectrum){

	}
}
