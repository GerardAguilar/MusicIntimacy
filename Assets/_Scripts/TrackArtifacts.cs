using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackArtifacts : MonoBehaviour {

	public List<int> beatStorage;
	public List<int> beatDifferenceOneStep;
	public List<int[]> noteStorage;

	bool newStep;
	int beatCount;
	int noteSetCount;
	int ceiling;
	int previousBeatTime;
	int match;
	int targetMatch;
	int newTransition;

	// Use this for initialization
	void Awake () {
		beatStorage = new List<int>();
		beatDifferenceOneStep = new List<int> ();
		noteStorage = new List<int[]> ();
		beatCount = 0;
		previousBeatTime = 0;
		match = 0;
		targetMatch = 0;
		noteSetCount = 0;
	}

	public void AddBeat(int newBeat){
		beatStorage.Add (newBeat);
		beatCount++;
		MapBeats(newBeat);
	}

	public void AddNote(int[] newNote){
		noteStorage.Add (newNote);
		noteSetCount++;
	}
	public void AddArtifact(int newBeat, int[] keyNotes){
		AddBeat (newBeat);
		AddNote (keyNotes);
//		for(int i=0; i<noteStorage.Count; i++){
//			Debug.Log ("\n");
//			for(int j=0; j<noteStorage[i].Length; j++){
//				Debug.Log ("i: " + noteStorage[i][j] + "\t");
//			}
//		}
	}

	void MapBeats(int newBeat){//check the time difference on each consecutive step

		beatDifferenceOneStep.Add ((int)((newBeat - previousBeatTime)/10000));
		previousBeatTime = newBeat;
	}
	//Find current song's beats and how it matches the target
	public bool MatchSong(TrackArtifacts target){
		bool temp = false;
		Debug.Log ("Finding Beat Match");
		for (int j=0; j<beatDifferenceOneStep.Count-1; j=j+2) {
			for (int i=0; i<target.beatDifferenceOneStep.Count-1; i=i+2) {
				if(beatDifferenceOneStep[j] == target.beatDifferenceOneStep[i]){
					if(beatDifferenceOneStep[j+1] == target.beatDifferenceOneStep[i+1]){
//						if(beatDifferenceOneStep[j+2] == target.beatDifferenceOneStep[i+2]){
						Debug.Log ("Found a match at source[" + j + "] and target[" + i + "]: \n" 
						           );

						if(MatchUpNotes(target, j, i)){
							Debug.Log ("Found a TRUE match at source[" + j + "] and target[" + i + "]: \n" 
							           );
							match=j;
							targetMatch=i;
							temp = true;
//							j=beatDifferenceOneStep.Count-1;//terminate loop
//							i=target.beatDifferenceOneStep.Count-1;//terminate loop
						}
//						}
					}
				}
			}
		}
		return temp;


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

	bool MatchUpNotes(TrackArtifacts other, int pseudoMatch, int pseudoTargetMatch){
		Debug.Log ("MatchUpNotes(): noteSetCount: " + noteSetCount + "(" +pseudoMatch +"," +pseudoTargetMatch+")");
		int matched=0;
		for (int i=0; i<noteStorage[pseudoMatch].Length; i++) {
			for(int j=0; j<other.noteStorage[pseudoTargetMatch].Length; j++){
				if(noteStorage[pseudoMatch][i] == other.noteStorage[pseudoTargetMatch][j]){
					matched++;
				}
			}
		}

		if (matched > 3) {
			return true;
		} else {
			return false;
		}
	}

}
