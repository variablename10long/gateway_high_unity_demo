using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {
	
	public static Touch[] touches = null;
	
	public void Awake() {
		// Touch device input, only relevant on a touch screen.
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
	
	public void OnGUI() {
		GameLogic.touches = Input.touches;	
	}
}
