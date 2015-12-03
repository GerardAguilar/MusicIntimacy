using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

	GameObject line;
	OutputAnalyzerClean analyzer;
	Text songName;
	Text currentTime;
	Text output;
	Text top4;

	// Use this for initialization
	void Start () {
		line = GameObject.Find ("Line");
		analyzer = line.GetComponent<OutputAnalyzerClean> ();
		songName = GameObject.Find ("ValSongTitle").GetComponent<Text> ();
		currentTime = GameObject.Find ("ValTime").GetComponent<Text> ();
		output = GameObject.Find ("ValOutput").GetComponent<Text> ();
//		top4 = GameObject.Find ("ValFreq").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		songName.text = analyzer.GetSongName ()+"";
		currentTime.text = analyzer.GetCurrentTime ()+"";
		output.text = analyzer.GetCurrentOutput ()+"";
//		top4.text = analyzer.GetTopFourFrequency ()+"";
	}
}
