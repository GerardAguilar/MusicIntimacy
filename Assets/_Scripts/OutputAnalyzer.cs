using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * CONTEXT: Attached to the object with the audio source (our main camera) and analyzes the music
 * SUBSTANCE: AudioListener.GetOutputData(), float[] outputData, GameObject plane, float amp, int jump
 * FORM: Start(), Update()
 * PATH:
 * 	On start, initialize outputData to 1024
 * 	During update cycle, use GetOutputData() to extract amps
 * 	Classify amp ranges with colors
 * 	Link the amp values to plane sprite renderer via its pixel changer
 * 	Radius * sin(360/1024) = y; 
 * 		Slice the circles full 360 to 1024 parts
 * 		From sin(theta) = opp/hyp we have hyp * sin(0.3515625) = opp, where hyp is our radius and opp is our new y coordinate
 *  Radius * cos(360/1024) = y; 
 * 		Slice the circles full 360 to 1024 parts
 * 		From cos(theta) = adj/hyp we have hyp * cos(0.3515625) = adj, where hyp is our radius and adj is our new y coordinate
 * 	Call the plane's redraw sprite to visualize change
 */

public class OutputAnalyzer : MonoBehaviour, AudioProcessor2.AudioCallbacks {
	float[] outputData;
	public GameObject plane;
//	public GameObject plane2;
//	public GameObject plane3;
//	public GameObject plane4;
	public GameObject cube;
	float amp;
	int jump;

	int newRadius;
	double newy;
	int y;
	double newx;
	int x;
	float newAngle;
	Color color;
	float highest;

	PixelChanger pxChanger;
	bool red;
	Vector3 newScale;
	int triggered;


	AudioSource audio;

	private Rigidbody myRigidbody;

	public GameObject line;
	ModifyLine modifyLine;

	int segmentCount;
	float segmentRow;
	float segmentCol;
	Vector3 segmentOrigin;
	bool needJump;
	float segmentJump;
	float floor;

	public float lineX;
	public float lineY;
	public float lineAngle;
	public float lineRadius;

	double anglePerSegment;

	bool shiftRotation;

	float offsetX;
	float offsetY;
	Random rnd;
	int randomDirection;

	bool beatHappened;
	bool traceCheck;
	int beatRadiusChange;

//	public List<int> beatTimes;

	public List<int> beatStorage;
	int storageStepper;


	public AudioSource[] tracks;
	public TrackArtifacts[] artifacts;
	int currentArtifact;

	int startBranch;

	bool flip;

	int transitionStart;

	bool readyTransition;

	int warp;

	// Use this for initialization
	void Start () {
		flip = false;
		startBranch = 100000;
		beatRadiusChange = 0;
		storageStepper = 0;
		beatHappened = false;
//		audio = GetComponent<AudioSource> ();
		outputData = new float[1024];
//		beatStorage = new List<int>();
//		beatStorage.Add (new List<int> ());
		amp = 0;
		newRadius = 1;
		pxChanger = plane.GetComponent<PixelChanger> ();
		red = false;
		highest = 0;
		newScale = new Vector3 (0, 0, 0);
		triggered = 1;
		myRigidbody = plane.GetComponent<Rigidbody> ();
		segmentCount = 0;
		segmentOrigin = new Vector3 (0, 0, 0);
		segmentRow = 0;
		segmentCol = 0;
		segmentJump = 0;
		floor = 0;

		modifyLine = line.GetComponent<ModifyLine> ();

		shiftRotation = false;
		lineRadius = 1;
		anglePerSegment = 1;

		offsetX = 0;
		offsetY = 0;

		AudioProcessor2 processor = gameObject.GetComponent<AudioProcessor2> ();
		processor.addAudioCallback (this);

		tracks = GetComponentsInChildren<AudioSource> ();
		artifacts = GetComponentsInChildren<TrackArtifacts> ();
		audio = tracks [0];
		Debug.Log ("tracks: " + tracks);
		currentArtifact = 0;

		readyTransition = false;
	}

	public void onOnbeatDetected(){
//		Debug.Log ("Beat!!!");
		beatHappened = true;
		beatRadiusChange++;
//		Debug.Log ("Beat: audio.timeSamples: " + audio.timeSamples);

	}
	public void onSpectrum(float[] spectrum){
	
	}
	
	// Update is called once per frame
	void Update () {
		AudioListener.GetOutputData (outputData, 0);
		segmentRow = floor;

		//When a button is held, store the beat into the object as an array of times
		if (Input.GetButtonDown ("Trace")) {
			traceCheck = true;
			Debug.Log ("Trace: audio.timeSamples: " + audio.timeSamples);
			artifacts[currentArtifact].AddBeat(audio.timeSamples);
//			storageStepper++;
		}

//		if (Input.GetButtonUp ("Trace")) {
//			traceCheck = false;
//		}

//		if (traceCheck && beatHappened) {
////			Debug.Log ("Traced!!!");
//			beatStorage[storageStepper].Add(audio.timeSamples);
//			beatHappened = false;
//			Debug.Log ("Trace: audio.timeSamples: " + audio.timeSamples);
//		}
		//Compare the times with other objects' array and see if there's a match
		//Give the user the choice to transition to that other song

		//draws each frequency (creates a circle) depending on sampled amp of that freq from GetOutputData
		for(int i=0; i<1024; i++){
			amp = outputData[i];
			if(amp>highest){
				highest = amp;
			}
			//We'll have to modify the coordinates inside this for loop to link the array cells with the new coordinates
			//represents each of the 1024 frequencies as a point in the circumference of a circle
			newAngle = (float)0.3515625*i; //(from 360/1024)
			newy = Mathf.Sin(newAngle)*newRadius;
			newx = Mathf.Cos(newAngle)*newRadius;

			x = (int)newx;
			y = (int)newy;

			jump=2;

			//Anytime the input wave is louder than .2 amps, draw it, anything else is a flatline
			if(amp>=.2){
				needJump=true;
				segmentJump = (float)amp*10;
			}

			if(amp<.2){
				color = Color.black;
			}
			else if(amp<.4){
				color = Color.black;
			}
			else if(amp<.6){
				color = Color.green;
			}
			else if(amp<.8){
				color = Color.yellow;
			}
			else if(amp>=.8){
				color = Color.red;
				if(beatRadiusChange%5 == 0){
					newRadius=pxChanger.width/5;
					beatRadiusChange++;
				}
				else if(beatRadiusChange%5 == 4){
					newRadius=pxChanger.width/4;
					beatRadiusChange++;
				}
//				else if(beatRadiusChange%5 == 3){
//					newRadius=pxChanger.width/3;
//					beatRadiusChange++;
//				}
//				else if(beatRadiusChange%5 == 2){
//					newRadius=pxChanger.width/2;
//					beatRadiusChange++;
//				}
//				else if(beatRadiusChange%5 == 1){
//					newRadius=pxChanger.width/1;
//					beatRadiusChange++;
//				}
				else{
					newRadius=pxChanger.width/6;
					beatRadiusChange++;
				}

//				triggered = triggered + 1;
			}
			else{
				color = Color.black;
			}

			//segment thickness
			for(int j=1; j<=1; j++){//slows down at 1000
				//here, we draw the x and y that was changed by the sin and cos functions above
				//for thickness we use j
				plane.GetComponent<PixelChanger>().DrawPixel (x, y, color);
				plane.GetComponent<PixelChanger>().DrawPixel (x+j, y+j, color);
				plane.GetComponent<PixelChanger>().DrawPixel (x-j, y-j, color);
				plane.GetComponent<PixelChanger>().DrawPixel (x+j, y-j, color);
				plane.GetComponent<PixelChanger>().DrawPixel (x-j, y+j, color);
			} 
//			
		}
		//radiates our radius outwards
		newRadius = newRadius + jump;
		//resets the radius when outside the circle
		if (newRadius > pxChanger.width/2) {
			newRadius = 0;
		}
//		if (Input.GetButtonDown ("Trace")) {

			plane.GetComponent<PixelChanger> ().RedrawSprite ();
//			Debug.Log ("Pitch: " + audio.pitch);
//		}

		if (Input.GetButtonDown ("NewFlower")) {

//			for(int i=0; i<beatStorage.Count; i++){
//				for(int j=0; j<beatStorage[i].Count; j++){
//					Debug.Log (i + ": " + beatStorage[i][j]);
//				}
//
//			}
//			shiftRotation = !shiftRotation;
//			lineRadius = (float)(lineRadius + .5);
			//Dampens speed by changing the angles used to draw each point and 
			//keeping the distance between two points consecutive points constant, no matter the radius
			anglePerSegment = (360/(360*lineRadius));


			Debug.Log ("Shift Rotation.");


			randomDirection = Random.Range(1,7);
			if(randomDirection==1){
				offsetX = (float)offsetX+lineRadius+1;
				offsetY = (float)offsetY+lineRadius+1;
			}
			else if(randomDirection==2){
				offsetX = (float)offsetX-lineRadius-1;
				offsetY = (float)offsetY+lineRadius+1;
			}
			else if(randomDirection==3){
				offsetX = (float)offsetX+lineRadius+1;
				offsetY = (float)offsetY-lineRadius-1;
			}
			else if(randomDirection==4){
				offsetX = (float)offsetX-lineRadius-1;
				offsetY = (float)offsetY-lineRadius-1;
			}
			else if(randomDirection==5){
				offsetX = 0;
				offsetY = (float)offsetY-lineRadius-1;
			}
			else if(randomDirection==6){
				offsetX = (float)offsetX-lineRadius-1;
				offsetY = 0;
			}
//			else if(randomDirection==7){
//				offsetX = (float)offsetX-lineRadius-1;
//				offsetY = (float)offsetY-lineRadius-1;
//			}
//			else if(randomDirection==8){
//				offsetX = (float)offsetX-lineRadius-1;
//				offsetY = (float)offsetY-lineRadius-1;
//			}

			lineRadius = 1;
			floor = Random.Range (0,14);

			//Change the cylinder's location
			plane.transform.position = new Vector3(offsetX, offsetY, floor);
		}

		//flat lines or spikes
		if (needJump) {
			needJump = false;
			segmentRow=(float)(segmentRow+segmentJump);
		}

		//increases line size
		modifyLine.IncreaseLineSize();


		lineAngle = (float)anglePerSegment * segmentCol;
//		lineX = Mathf.Sin (lineAngle) * lineRadius + segmentRow + floor;
		lineX = Mathf.Sin (lineAngle) * lineRadius;
		//		lineY = Mathf.Sin (lineAngle) * lineRadius;
		lineY = Mathf.Cos (lineAngle) * lineRadius;

		//sets the last segment to the new x, y, and z coordinates

		modifyLine.SetLastSegmentPosition (lineX + offsetX, lineY + offsetY, segmentRow);

		if (shiftRotation) {
			segmentCol = (float)(segmentCol - .5);
		} else {
			segmentCol = (float)(segmentCol + .5);
		}

//		segmentCol = (float)(segmentCol + .1);

		if (Mathf.Abs(lineAngle) > 6.28) {//it's in radians
			lineRadius = (float)(lineRadius + .2);
			segmentCol = 0;
			floor = (float)(floor+.1);
//			floor = amp;

//			highest = 0;
			//Dampens speed by changing the angles used to draw each point and 
			//keeping the distance between two points consecutive points constant, no matter the radius
			anglePerSegment = (360/(360*lineRadius));
		}


		
		if (Input.GetButtonDown ("MatchSong")) {
			artifacts[0].MatchSong(artifacts[1]);
		}

		if (Input.GetButtonDown ("NewSong")) {
			if(flip){
				flip=!flip;
				tracks[1].Stop ();
				tracks[0].Play ();
				currentArtifact=0;
				audio=tracks[0];
			}
			else{
				flip=!flip;
				tracks[0].Stop ();
				tracks[1].Play ();
				currentArtifact=1;
				audio=tracks[1];
			}
		}

		//Restarts the song and will transition to the other song later on
		if(Input.GetButtonDown ("TransitionSong")){
			readyTransition=true;
			tracks[0].Stop();
			tracks[0].time=0;
			tracks[0].Play();
			audio=tracks[0];
			transitionStart = artifacts[0].FindTransition();//this function returns where to stop on tracks[0]
			warp = artifacts[0].FindWarpPoint ();//this function returns where to start on tracks[1]
		}

		if (readyTransition) {
//			readyTransition=false;

//			Debug.Log ("Artifact 0: ");
//			artifacts[0].DisplayBeatsFrom (transitionStart);
//			Debug.Log ("Artifact 1: ");
//			artifacts[1].DisplayBeatsFrom (transitionStart);
//			Debug.Log ("transitionStart: " + transitionStart);
			Debug.Log ("Transition at: " + transitionStart);
			Debug.Log ("tracks[0].time: " + tracks[0].time);
			if(tracks[0].time >= transitionStart){
				tracks[0].Stop();
				tracks[1].time = artifacts[1].beatStorage[warp]/AudioSettings.outputSampleRate;
				tracks[1].Play ();
				audio=tracks[1];
				currentArtifact = 1;
				Debug.Log ("Did the song switch?");
				readyTransition=false;
			}

		}
//		Debug.Log (audio.time);




	}

//	void PlaySong(){
//		Debug.Log ("PlaySong");
//		tracks[0].Stop();
//		tracks[1].Play();
//
//	}
}
