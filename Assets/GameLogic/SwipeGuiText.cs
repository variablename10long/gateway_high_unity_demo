using UnityEngine;
using System.Collections;

public class SwipeGuiText : MonoBehaviour {
	private float _higestY = 0;
	
	
	GameObject _player = null;
	
	public void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void OnGUI() {
	Touch[] touches = GameLogic.touches;
		string msg = "";
		foreach(Touch touch in touches) {
			if(touch.phase == TouchPhase.Moved) {
				Vector2 dp = touch.deltaPosition;
				if(dp.y > _higestY) {
					_higestY = dp.y;
					msg += dp.ToString()+"\n";
				}
			}
		}
		msg += _player.GetComponent<GravitySwitchComponent>().CurrentGravityDirection == GravitySwitchComponent.GravityDirection.UP ? "UP" : "DOWN";
		guiText.text = msg;
	}
}
