//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
///*
// * CONTEXT: This will be attached to the gameObject with the music source.
// * 			It will crank out data and push it onto the currently playing songs.
// * SUBSTANCE: 
// * 		AudioListener.GetOutputData() 
// * 		AudioListener.GetSpectrumData() 
// * 		int samplingSize 
// * 		float[] outputData
// * 		float[] spectrumData 
// * 		TrackArtifacts[] artifacts
// * FORM: 
// * 		Start()
// * 		Update()
// * PATH: 
// * 	Start() initializes our fields 
// * 	Update() uses GetOutputData and GetSpectrumData to sample music
// *	Update() senses user controls
// *	When Trace ('j') is pressed, the timeSample is stored into an array found in TrackArtifacts.cs 
// *		which is a child of the song object
// *	When NewSong ('l') is pressed, the track changes
// *	When MatchSong ('o') is pressed, the TrackArtifacts beatArray is searched through for a 3 beat match
// *	When MatchSong ('o') is pressed, the TrackArtifacts noteArray is searched through for the above set
// *	When MatchSong ('o') is pressed, the TrackArtifacts ampArray is searched through for the above set
// *	If there is a match, then it will notify the user
// *	It will also tell the user where the match is (in timesamples)
// *	Transition Song ('p') can then be pressed to play the first song, then at the proper timesample, will switch to the next song
// *	If everything goes well, the transition should sound clean.
// *
// *	NOTES:
// *		The previous model's sprite representation of each frequency is nice, but it should be a lower priority right now.
// *		Size limits of our GetOutputData and GetSpectrumData is 64 to 8192
// */
//
//public class OutputAnalyzer2 : MonoBehaviour {
//
//	float[] outputData;
//	float[] spectrumData;
//	int samplingSize;
//
//	public TrackArtifacts[] artifacts;
//	int audioID;
//
//
//	AudioSource[] tracks;
//	AudioSource audio;
//
//	int tempAmp;
//	int tempHighestAmp;
//
//
//	// Use this for initialization
//	void Start () {
//		samplingSize = 8192;
//		outputData = new float[samplingSize];
//		spectrumData = new float[samplingSize];
//
//		tracks = GetComponentsInChildren<AudioSource> ();
//		artifacts = GetComponentsInChildren<TrackArtifacts> ();
//		audio = tracks [0];
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		AudioListener.GetOutputData (outputData, 0);
//		AudioListener.GetSpectrumData (spectrumData, 0, FFTWindow.BlackmanHarris);
//		
//		//When a button is held, store the beat and note into the object as an array of times
//		if (Input.GetButtonDown ("Trace")) {
//			Debug.Log ("Trace: audio.timeSamples: " + audio.timeSamples);
//			artifacts[int.Parse(audio.name)].AddBeat(audio.timeSamples);
//
//		}
//
//		//Compare the times with other objects' array and see if there's a match
//		//Give the user the choice to transition to that other song
//
//		for(int i=0; i<samplingSize; i++){
//			tempAmp= outputData[i];
//			spectrumVal = spectrumData[i];
//			
//			//see MusicalNotes Info.cs for amp# designations of notes
//			if(tempAmp>highest){
//				highest = amp;
//				whichI.Add(i);
//				currentHighest = i;
//			}
//			
//			if(spectrumVal>spectrumCheck){
//				spectrumCheck = spectrumVal;
//				spectrumI = i;
//			}
//		}
//		
//		Debug.Log ("spectrumI: " + spectrumI);
//		spectrumI = 0;
//		//		for (int i=0; i<whichI.Count; i++) {
//		//			Debug.Log ("Index of highest: " + whichI[i]);
//		//		}
//		//		Debug.Log ("highest: " + highest + " currentHighest: " + currentHighest);
//		//		Debug.Log ("currentHighest: " + currentHighest);
//		//		highest = 0;
//		//radiates our radius outwards
//		newRadius = newRadius + jump;
//		//resets the radius when outside the circle
//		if (newRadius > pxChanger.width/2) {
//			newRadius = 0;
//		}
//		//		if (Input.GetButtonDown ("Trace")) {
//		
//		plane.GetComponent<PixelChanger> ().RedrawSprite ();
//		//			Debug.Log ("Pitch: " + audio.pitch);
//		//		}
//		
//		if (Input.GetButtonDown ("NewFlower")) {
//			
//			//			for(int i=0; i<beatStorage.Count; i++){
//			//				for(int j=0; j<beatStorage[i].Count; j++){
//			//					Debug.Log (i + ": " + beatStorage[i][j]);
//			//				}
//			//
//			//			}
//			//			shiftRotation = !shiftRotation;
//			//			lineRadius = (float)(lineRadius + .5);
//			//Dampens speed by changing the angles used to draw each point and 
//			//keeping the distance between two points consecutive points constant, no matter the radius
//			anglePerSegment = (360/(360*lineRadius));
//			
//			
//			Debug.Log ("Shift Rotation.");
//			
//			
//			randomDirection = Random.Range(1,7);
//			if(randomDirection==1){
//				offsetX = (float)offsetX+lineRadius+1;
//				offsetY = (float)offsetY+lineRadius+1;
//			}
//			else if(randomDirection==2){
//				offsetX = (float)offsetX-lineRadius-1;
//				offsetY = (float)offsetY+lineRadius+1;
//			}
//			else if(randomDirection==3){
//				offsetX = (float)offsetX+lineRadius+1;
//				offsetY = (float)offsetY-lineRadius-1;
//			}
//			else if(randomDirection==4){
//				offsetX = (float)offsetX-lineRadius-1;
//				offsetY = (float)offsetY-lineRadius-1;
//			}
//			else if(randomDirection==5){
//				offsetX = 0;
//				offsetY = (float)offsetY-lineRadius-1;
//			}
//			else if(randomDirection==6){
//				offsetX = (float)offsetX-lineRadius-1;
//				offsetY = 0;
//			}
//			//			else if(randomDirection==7){
//			//				offsetX = (float)offsetX-lineRadius-1;
//			//				offsetY = (float)offsetY-lineRadius-1;
//			//			}
//			//			else if(randomDirection==8){
//			//				offsetX = (float)offsetX-lineRadius-1;
//			//				offsetY = (float)offsetY-lineRadius-1;
//			//			}
//			
//			lineRadius = 1;
//			floor = Random.Range (0,14);
//			
//			//Change the cylinder's location
//			plane.transform.position = new Vector3(offsetX, offsetY, floor);
//		}
//		
//		//flat lines or spikes
//		if (needJump) {
//			needJump = false;
//			segmentRow=(float)(segmentRow+segmentJump);
//		}
//		
//		//increases line size
//		modifyLine.IncreaseLineSize();
//		
//		
//		lineAngle = (float)anglePerSegment * segmentCol;
//		//		lineX = Mathf.Sin (lineAngle) * lineRadius + segmentRow + floor;
//		lineX = Mathf.Sin (lineAngle) * lineRadius;
//		//		lineY = Mathf.Sin (lineAngle) * lineRadius;
//		lineY = Mathf.Cos (lineAngle) * lineRadius;
//		
//		//sets the last segment to the new x, y, and z coordinates
//		
//		modifyLine.SetLastSegmentPosition (lineX + offsetX, lineY + offsetY, segmentRow);
//		
//		if (shiftRotation) {
//			segmentCol = (float)(segmentCol - .5);
//		} else {
//			segmentCol = (float)(segmentCol + .5);
//		}
//		
//		//		segmentCol = (float)(segmentCol + .1);
//		
//		if (Mathf.Abs(lineAngle) > 6.28) {//it's in radians
//			lineRadius = (float)(lineRadius + .2);
//			segmentCol = 0;
//			floor = (float)(floor+.1);
//			//			floor = amp;
//			
//			//			highest = 0;
//			//Dampens speed by changing the angles used to draw each point and 
//			//keeping the distance between two points consecutive points constant, no matter the radius
//			anglePerSegment = (360/(360*lineRadius));
//		}
//		
//		
//		
//		if (Input.GetButtonDown ("MatchSong")) {
//			artifacts[0].MatchSong(artifacts[1]);
//		}
//		
//		if (Input.GetButtonDown ("NewSong")) {
//			if(flip){
//				flip=!flip;
//				tracks[1].Stop ();
//				tracks[0].Play ();
//				audioID=0;
//				audio=tracks[0];
//			}
//			else{
//				flip=!flip;
//				tracks[0].Stop ();
//				tracks[1].Play ();
//				audioID=1;
//				audio=tracks[1];
//			}
//		}
//		
//		//Restarts the song and will transition to the other song later on
//		if(Input.GetButtonDown ("TransitionSong")){
//			readyTransition=true;
//			tracks[0].Stop();
//			tracks[0].time=0;
//			tracks[0].Play();
//			audio=tracks[0];
//			transitionStart = artifacts[0].FindTransition();//this function returns where to stop on tracks[0]
//			warp = artifacts[0].FindWarpPoint ();//this function returns where to start on tracks[1]
//		}
//		
//		if (readyTransition) {
//			//			readyTransition=false;
//			
//			//			Debug.Log ("Artifact 0: ");
//			//			artifacts[0].DisplayBeatsFrom (transitionStart);
//			//			Debug.Log ("Artifact 1: ");
//			//			artifacts[1].DisplayBeatsFrom (transitionStart);
//			//			Debug.Log ("transitionStart: " + transitionStart);
//			Debug.Log ("Transition at: " + transitionStart);
//			Debug.Log ("tracks[0].time: " + tracks[0].time);
//			if(tracks[0].time >= transitionStart){
//				tracks[0].Stop();
//				tracks[1].time = artifacts[1].beatStorage[warp]/AudioSettings.outputSampleRate;
//				tracks[1].Play ();
//				audio=tracks[1];
//				audioID = 1;
//				Debug.Log ("Did the song switch?");
//				readyTransition=false;
//			}
//			
//		}
//		//		Debug.Log (audio.time);
//		
//		
//		
//		
//	}
//	
//	//	void PlaySong(){
//	//		Debug.Log ("PlaySong");
//	//		tracks[0].Stop();
//	//		tracks[1].Play();
//	//
//	//	}
//}
