using UnityEngine;
using System.Collections;

public class PlayerGravityAnim : MonoBehaviour {
	
	private bool _shouldAnimate = false;
	
	public int animationThrottle = 3;
	private int _animationThrottleCounter = 0;
	
	public bool ShouldAnimate {
		set {
		_shouldAnimate = value;	
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_shouldAnimate) {
			_animationThrottleCounter++;
			if(_animationThrottleCounter > animationThrottle) {
				Quaternion locRot = transform.localRotation;
				locRot = Random.rotation;
				transform.localRotation = locRot;
				_animationThrottleCounter = 0;	
			}
		}
	}
}
