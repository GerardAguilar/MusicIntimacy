using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackArtifacts : MonoBehaviour {

	public List<int> beatStorage;
	public List<int> beatDifferenceOneStep;
//	public List<int> beatDifferenceTwoStep;
//	public List<int> beatDifferenceThreeStep;
//	public List<int> beatDifferenceFourStep;
//	public List<int> beatDifferenceFiveStep;
//	public List<int> beatDifferenceSixStep;

	bool newStep;
	int beatCount;
	int ceiling;
	int previousBeatTime;
	int match;
	int targetMatch;
	int newTransition;
//	int newWarpPoint;
	// Use this for initialization
	void Start () {
		beatStorage = new List<int>();
		beatDifferenceOneStep = new List<int> ();
//		beatDifferenceTwoStep = new List<int> ();
//		beatDifferenceThreeStep = new List<int> ();
//		beatDifferenceFourStep = new List<int> ();
//		beatDifferenceFiveStep = new List<int> ();
//		beatDifferenceSixStep = new List<int> ();
//		newStep = false;
		beatCount = 0;
//		ceiling = 1;
		previousBeatTime = 0;
		match = 0;
		targetMatch = 0;
	}
	
//	// Update is called once per frame
//	void Update () {
////		//if new beat
////		if (newStep) {
////			newStep = false;
////			mapBeats();
////		}
////
//	}
	public void AddBeat(int newBeat){
		beatStorage.Add (newBeat);
		beatCount++;
		MapBeats(newBeat);
	}
	void MapBeats(int newBeat){//check the time difference on each consecutive step

		beatDifferenceOneStep.Add ((int)((newBeat - previousBeatTime)/10000));
		previousBeatTime = newBeat;
	}
	//Find current song's beats and how it matches the target
	public void MatchSong(TrackArtifacts target){
		Debug.Log ("Finding Match");
		for (int j=0; j<beatDifferenceOneStep.Count-1; j=j+2) {
			for (int i=0; i<target.beatDifferenceOneStep.Count-1; i=i+2) {
				if(beatDifferenceOneStep[j] == target.beatDifferenceOneStep[i]){
					if(beatDifferenceOneStep[j+1] == target.beatDifferenceOneStep[i+1]){
//						if(beatDifferenceOneStep[j+2] == target.beatDifferenceOneStep[i+2]){
						Debug.Log ("Found a match at source[" + j + "] and target[" + i + "]: \n" 
//						           + beatDifferenceOneStep[j] + "==" + beatDifferenceOneStep[i] + "\n"
						           );
						match=j;
						targetMatch=i;
//						}
					}
				}
			}
		}
	}

	//grab a song and start it at the matched index
	public int FindTransition(){

		newTransition = beatStorage[match]/AudioSettings.outputSampleRate;
//		newSong.time = newTransition;
//		newSong.Play ();
		return newTransition;
	}

	public int FindWarpPoint(){
		return targetMatch;
	}

	public void DisplayBeatsFrom(int start){
		for(int i=start; i<start+3; i++){
			Debug.Log (beatStorage[i] + "-" + beatStorage[i+1] + "-" + beatStorage[i+2]);
		}
	}


}
