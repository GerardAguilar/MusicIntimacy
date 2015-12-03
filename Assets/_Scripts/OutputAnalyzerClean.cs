using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * 1. This script will be attached to our track's parent object
 * 2. It will listen to the current track playing and analyze it's spectrum and output data
 * 3. Doing so allows our Draw Background and LineRenderer to react to it.
 * 4. During the for loop to access the values of the track, we will 
 * 4a.	Store the current highestAmplitude onto the track's script
 * 4b.	Store the 5 highest frequency responses onto the track's script
 * 5. When "J" (Trace) button is pressed, the time based on the timesamples will be stored onto the track
 * 6. When "K" (New Song) button is pressed, the track switches to the 2nd song.
 * 7. When "I" (Match Song) button is pressed, the track's script checks for a beat match
 * 8. When "I" (Match Song) button is pressed, the track's script checks for a note match
 * 9. When "O" (Transition Song) button is pressed, the tracks switches to the first song and will transition given there is a transition.
 * */


//1. This script will be attached to our track's parent object
public class OutputAnalyzerClean : MonoBehaviour {

	float[] spectrumData;
	public float[] spectrumStorage;
	float[] outputData;
	int highestCell;
	int[] topFour;
	float[] topFourVals;
	float tempSpectrumData;

	float newAngle;
	int freqCircleX;
	int freqCircleY;
	float radius;
	float spectrumValue;
	Color freqCircleColor;
	float newy;
	float newx;
	int beatRadiusChange;

	PixelChanger pxChanger;

	public GameObject plane;
	public GameObject line;
	ModifyLine modifyLine;
	float lineX;
	float lineY;
	float lineZ;

	GameObject[] rightCubes;
	GameObject[] leftCubes;

	bool flip;
	AudioSource[] tracks;
	AudioSource audio;
	int currentTrack;
	TrackArtifacts[] artifacts;
	bool readyTransition;
	int transitionStart;
	int warp;

	Button transButton;

	void Awake(){
		spectrumData = new float[8192];
		spectrumStorage = new float[108];
		outputData = new float[8192];
		newAngle = 0;
		pxChanger = plane.GetComponent<PixelChanger> ();
		radius=pxChanger.width/7;
		modifyLine = line.GetComponent<ModifyLine> ();
		rightCubes = GameObject.FindGameObjectsWithTag ("SpinMeRight");
		leftCubes = GameObject.FindGameObjectsWithTag ("SpinMeLeft");
		flip = false;
		tracks = GetComponentsInChildren<AudioSource> ();
		currentTrack = 0;
		audio = tracks [0];
		artifacts = GetComponentsInChildren<TrackArtifacts> ();
		readyTransition = false;
		highestCell = 0;
		tempSpectrumData = 0;
		topFour = new int[4];
		for (int i=0; i<topFour.Length; i++) {
			topFour[i] = 0;
		}
		topFourVals = new float[4];
		for (int i=0; i<topFourVals.Length; i++) {
			topFourVals[i] = 0;
		}
		transButton = GameObject.Find ("ButtonTransition").GetComponent<Button>();

	}

	void Update(){
//		2. It will listen to the current track playing and analyze it's spectrum and output data
		AudioListener.GetSpectrumData (spectrumData, 0, FFTWindow.BlackmanHarris);
		AudioListener.GetOutputData (outputData, 0);

		//Please See Note Frequencies Excel Sheet for Note to Cell conversion

		spectrumStorage[0] = spectrumData[6];
		spectrumStorage[1] = spectrumData[6];
		spectrumStorage[2] = spectrumData[6];
		spectrumStorage[3] = spectrumData[7];
		spectrumStorage[4] = spectrumData[7];
		spectrumStorage[5] = spectrumData[7];
		spectrumStorage[6] = spectrumData[8];
		spectrumStorage[7] = spectrumData[8];
		spectrumStorage[8] = spectrumData[9];
		spectrumStorage[9] = spectrumData[9];
		spectrumStorage[10] = spectrumData[10];
		spectrumStorage[11] = spectrumData[11];
		spectrumStorage[12] = spectrumData[11];
		spectrumStorage[13] = spectrumData[12];
		spectrumStorage[14] = spectrumData[13];
		spectrumStorage[15] = spectrumData[13];
		spectrumStorage[16] = spectrumData[14];
		spectrumStorage[17] = spectrumData[15];
		spectrumStorage[18] = spectrumData[16];
		spectrumStorage[19] = spectrumData[17];
		spectrumStorage[20] = spectrumData[18];
		spectrumStorage[21] = spectrumData[19];
		spectrumStorage[22] = spectrumData[20];
		spectrumStorage[23] = spectrumData[21];
		spectrumStorage[24] = spectrumData[22];
		spectrumStorage[25] = spectrumData[24];
		spectrumStorage[26] = spectrumData[25];
		spectrumStorage[27] = spectrumData[27];
		spectrumStorage[28] = spectrumData[28];
		spectrumStorage[29] = spectrumData[30];
		spectrumStorage[30] = spectrumData[32];
		spectrumStorage[31] = spectrumData[33];
		spectrumStorage[32] = spectrumData[35];
		spectrumStorage[33] = spectrumData[38];
		spectrumStorage[34] = spectrumData[40];
		spectrumStorage[35] = spectrumData[42];
		spectrumStorage[36] = spectrumData[45];
		spectrumStorage[37] = spectrumData[47];
		spectrumStorage[38] = spectrumData[50];
		spectrumStorage[39] = spectrumData[53];
		spectrumStorage[40] = spectrumData[56];
		spectrumStorage[41] = spectrumData[60];
		spectrumStorage[42] = spectrumData[63];
		spectrumStorage[43] = spectrumData[67];
		spectrumStorage[44] = spectrumData[71];
		spectrumStorage[45] = spectrumData[75];
		spectrumStorage[46] = spectrumData[80];
		spectrumStorage[47] = spectrumData[84];
		spectrumStorage[48] = spectrumData[89];
		spectrumStorage[49] = spectrumData[95];
		spectrumStorage[50] = spectrumData[100];
		spectrumStorage[51] = spectrumData[106];
		spectrumStorage[52] = spectrumData[113];
		spectrumStorage[53] = spectrumData[119];
		spectrumStorage[54] = spectrumData[126];
		spectrumStorage[55] = spectrumData[134];
		spectrumStorage[56] = spectrumData[142];
		spectrumStorage[57] = spectrumData[150];
		spectrumStorage[58] = spectrumData[159];
		spectrumStorage[59] = spectrumData[169];
		spectrumStorage[60] = spectrumData[179];
		spectrumStorage[61] = spectrumData[189];
		spectrumStorage[62] = spectrumData[200];
		spectrumStorage[63] = spectrumData[212];
		spectrumStorage[64] = spectrumData[225];
		spectrumStorage[65] = spectrumData[238];
		spectrumStorage[66] = spectrumData[253];
		spectrumStorage[67] = spectrumData[268];
		spectrumStorage[68] = spectrumData[284];
		spectrumStorage[69] = spectrumData[300];
		spectrumStorage[70] = spectrumData[318];
		spectrumStorage[71] = spectrumData[337];
		spectrumStorage[72] = spectrumData[357];
		spectrumStorage[73] = spectrumData[378];
		spectrumStorage[74] = spectrumData[401];
		spectrumStorage[75] = spectrumData[425];
		spectrumStorage[76] = spectrumData[450];
		spectrumStorage[77] = spectrumData[477];
		spectrumStorage[78] = spectrumData[505];
		spectrumStorage[79] = spectrumData[535];
		spectrumStorage[80] = spectrumData[567];
		spectrumStorage[81] = spectrumData[601];
		spectrumStorage[82] = spectrumData[636];
		spectrumStorage[83] = spectrumData[674];
		spectrumStorage[84] = spectrumData[714];
		spectrumStorage[85] = spectrumData[757];
		spectrumStorage[86] = spectrumData[802];
		spectrumStorage[87] = spectrumData[850];
		spectrumStorage[88] = spectrumData[900];
		spectrumStorage[89] = spectrumData[954];
		spectrumStorage[90] = spectrumData[1010];
		spectrumStorage[91] = spectrumData[1070];
		spectrumStorage[92] = spectrumData[1134];
		spectrumStorage[93] = spectrumData[1201];
		spectrumStorage[94] = spectrumData[1273];
		spectrumStorage[95] = spectrumData[1349];
		spectrumStorage[96] = spectrumData[1429];
		spectrumStorage[97] = spectrumData[1514];
		spectrumStorage[98] = spectrumData[1604];
		spectrumStorage[99] = spectrumData[1699];
		spectrumStorage[100] = spectrumData[1800];
		spectrumStorage[101] = spectrumData[1907];
		spectrumStorage[102] = spectrumData[2021];
		spectrumStorage[103] = spectrumData[2141];
		spectrumStorage[104] = spectrumData[2268];
		spectrumStorage[105] = spectrumData[2403];
		spectrumStorage[106] = spectrumData[2546];
		spectrumStorage[107] = spectrumData[2697];

		storeTopFour ();
		cleanStorage ();
		drawFrequencyCircle ();
		drawLine ();

		if (Input.GetButtonDown ("Trace")) {
			Debug.Log ("Trace: audio.timeSamples: " + audio.timeSamples);
//			artifacts[currentTrack].AddBeat(audio.timeSamples);
			Debug.Log ("Audio.name: " + audio.name);
//			Debug.Log ("TopFour: \n");
//			for(int i=0; i<4; i++){
//				Debug.Log (topFour[i]);
//			}
			artifacts[currentTrack].AddArtifact(audio.timeSamples, topFour);
		}

		if (Input.GetButtonDown ("NewFlower")) {
			
			if(flip){
				flip=!flip;
				tracks[1].Stop ();
				tracks[0].Play ();
				currentTrack=0;
				audio=tracks[currentTrack];
			}
			else{
				flip=!flip;
				tracks[0].Stop ();
				tracks[1].Play ();
				currentTrack=1;
				audio=tracks[currentTrack];
			}
			
		}



		if (Input.GetButtonDown ("MatchSong")) {
			if(artifacts[0].MatchSong(artifacts[1])){
//				Debug.Log ("Enable Button");
				transButton.interactable = true;
			}
		}
		
		//Restarts the song and will transition to the other song later on
		if(Input.GetButtonDown ("TransitionSong")){
			readyTransition=true;
			tracks[0].Stop();
			tracks[1].Stop();
			tracks[0].time=0;
			tracks[0].Play();
			audio=tracks[0];
			transitionStart = artifacts[0].FindTransition();//this function returns where to stop on tracks[0]
			warp = artifacts[0].FindWarpPoint ();//this function returns where to start on tracks[1]
		}
		
		if (readyTransition) {
			Debug.Log ("Transition at: " + transitionStart);
			Debug.Log ("tracks[0].time: " + tracks[0].time);
			if(tracks[0].time >= transitionStart){
				tracks[0].Stop();
				tracks[1].time = artifacts[1].beatStorage[warp]/AudioSettings.outputSampleRate;
				tracks[1].Play ();
				audio=tracks[1];
				currentTrack = 1;
				Debug.Log ("Did the song switch?");
				readyTransition=false;
			}
			
		}

		clearTopFour ();

	}

	public void TransitionButton(){
		transButton.interactable = false;

		readyTransition=true;
		tracks[0].Stop();
		tracks[1].Stop();
		tracks[0].time=0;
		tracks[0].Play();
		audio=tracks[0];
		transitionStart = artifacts[0].FindTransition();//this function returns where to stop on tracks[0]
		warp = artifacts[0].FindWarpPoint ();//this function returns where to start on tracks[1]

		
		if (readyTransition) {
			Debug.Log ("Transition at: " + transitionStart);
			Debug.Log ("tracks[0].time: " + tracks[0].time);
			if(tracks[0].time >= transitionStart){
				tracks[0].Stop();
				tracks[1].time = artifacts[1].beatStorage[warp]/AudioSettings.outputSampleRate;
				tracks[1].Play ();
				audio=tracks[1];
				currentTrack = 1;
				Debug.Log ("Did the song switch like planned?");
				readyTransition=false;
			}
			
		}
	}

	void cleanStorage(){
		for (int i=0; i<spectrumStorage.Length; i++) {
			if(spectrumStorage[i] <.01){
				spectrumStorage[i] = 0;
			}
		}
	}

	void storeTopFour(){
		for (int i=0; i<spectrumStorage.Length; i++) {
			if(topFourVals[0]<spectrumStorage[i]){
//				Debug.Log ("Storing: " + i + "from: " + topFour[0] + " vs. " + spectrumStorage[i]);
				topFour[0]=i;
				topFourVals[0]=spectrumStorage[i];
			}else if(topFourVals[1]<spectrumStorage[i]){
				topFour[1]=i;
				topFourVals[1]=spectrumStorage[i];
			}else if(topFourVals[2]<spectrumStorage[i]){
				topFour[2]=i;
				topFourVals[2]=spectrumStorage[i];
			}else if(topFourVals[3]<spectrumStorage[i]){
				topFour[3]=i;
				topFourVals[3]=spectrumStorage[i];
			}
		}
	}

	void clearTopFour(){
		for (int i=0; i<topFour.Length; i++) {
			topFour[i] = 0;
			topFourVals[i] = 0;
		}
	}


	void drawFrequencyCircle(){
		for(int i=0; i<spectrumStorage.Length; i++){

			spectrumValue = spectrumStorage[i];
			newAngle = (360/spectrumStorage.Length)*i; // divides the circle into equal parts to store the spectrum info

			//x and y values
			freqCircleY = (int)((Mathf.Sin (newAngle))*radius);
			freqCircleX = (int)((Mathf.Cos (newAngle))*radius);

			//color
			if(spectrumValue<.01){
				freqCircleColor = new Color(0,0,0,1);
			}else if(spectrumValue<.02){
				freqCircleColor = Color.black;
			}else if(spectrumValue<.04){
				freqCircleColor = Color.blue;
			}else if(spectrumValue<.06){
				freqCircleColor = Color.green;
			}else if(spectrumValue<.08){
				freqCircleColor = Color.yellow;
			}else if(spectrumValue>=.08){
				freqCircleColor = Color.red;
			}

			for(int j=0; j<5; j++){
				plane.GetComponent<PixelChanger>().DrawPixel (freqCircleX, freqCircleY, freqCircleColor);
				plane.GetComponent<PixelChanger>().DrawPixel (freqCircleX+j, freqCircleY+j, freqCircleColor);
				plane.GetComponent<PixelChanger>().DrawPixel (freqCircleX-j, freqCircleY-j, freqCircleColor);
				plane.GetComponent<PixelChanger>().DrawPixel (freqCircleX+j, freqCircleY-j, freqCircleColor);
				plane.GetComponent<PixelChanger>().DrawPixel (freqCircleX-j, freqCircleY+j, freqCircleColor);

			}
		}
		plane.GetComponent<PixelChanger> ().RedrawSprite ();

	}

	void drawLine(){

		lineX = (float)(lineX + .1);
		lineY = findHighestAmp () * -10;
		lineZ = -50;

//		if (Mathf.Abs (lineY) > 9) {
////			modifyLine.BudOnLastSegmentPosition(lineX, lineY, lineZ);
//			modifyLine.IncreaseLineSize ();
//			modifyLine.SetLastSegmentPosition (lineX, lineY, lineZ);
//			modifyLine.IncreaseLineSize ();
//			modifyLine.SetLastSegmentPosition (lineX+.7f, lineY-3f, lineZ);
//			modifyLine.IncreaseLineSize ();
//			modifyLine.SetLastSegmentPosition (lineX, lineY-6f, lineZ);
//			modifyLine.IncreaseLineSize ();
//			modifyLine.SetLastSegmentPosition (lineX-.7f, lineY-3f, lineZ);
//			modifyLine.IncreaseLineSize ();
//			modifyLine.SetLastSegmentPosition (lineX, lineY, lineZ);
//		} else {
			modifyLine.IncreaseLineSize();
			modifyLine.SetLastSegmentPosition (lineX, lineY, lineZ);
//		}
	}

	float findHighestAmp(){
		float temp = 0;
		for(int i=0; i<outputData.Length; i++){
			if(temp < outputData[i]){
				temp = outputData[i];
			}
		}
		changeCube (temp);
		return temp;
	}

	void changeCube(float amp){
		if (amp > .4) {
			for(int i=0; i<rightCubes.Length; i++){

			
				rightCubes[i].transform.Rotate (0, (float)(amp*3), 0);			
				
				Vector3 previousScale0 = rightCubes[i].transform.localScale;        
				previousScale0.z = Mathf.Lerp(previousScale0.z, amp/5, Time.deltaTime * 30);
				previousScale0.x = Mathf.Lerp(previousScale0.x, amp/5, Time.deltaTime * 30);
				rightCubes[i].transform.localScale = previousScale0;
			}
			for(int i=0; i<leftCubes.Length; i++){
				
				
				leftCubes[i].transform.Rotate (0, (float)(amp*-3), 0);			
				
				Vector3 previousScale0 = leftCubes[i].transform.localScale;        
				previousScale0.z = Mathf.Lerp(previousScale0.z, amp/10, Time.deltaTime * 30);
				previousScale0.x = Mathf.Lerp(previousScale0.x, amp/10, Time.deltaTime * 30);
				leftCubes[i].transform.localScale = previousScale0;
			}
		}
	}

	public string GetSongName(){
		return tracks [currentTrack].GetComponent<AudioSource> ().clip.name;
	}

	public float GetCurrentTime(){
		return audio.timeSamples;
	}

	public float GetCurrentOutput(){
		float sum = 0;
		float avg = 0;
		float rms = 0;
		for(int i=0; i<outputData.Length; i++){
			sum = sum+(outputData[i]*outputData[i]);
		}
		avg = sum / outputData.Length;
		rms = Mathf.Sqrt (avg);
		return rms;
	}

//	public string GetTopFourFrequency(){
//		string temp = "";
//		for (int i=0; i<topFour.Length; i++) {
//			temp = temp + topFour[i] + "\t";
//		}
//
//		return temp;
//	}


}
