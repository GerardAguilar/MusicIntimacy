using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour {

	public void Play(){
		Time.timeScale = 1;
	}
	public void Pause(){
		Time.timeScale = 0;
	}
}
