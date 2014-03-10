using UnityEngine;
using System.Collections;

/// <summary>
/// Class is bloated and acts as an input handler, attempts to tie together several components to 
/// make the system usable but isn't following the best reuse pattern.
/// </summary>
public class Jumper : MonoBehaviour {
	
	private bool _isJumping = false;
	private bool _isOnGround = false;
	private bool _isRunning = false;
	
	private enum RunDirection { LEFT, RIGHT };
	private RunDirection _currRunDirection = RunDirection.RIGHT;
	
	public float horizontalAcceleration = 25f;
	
	private Vector3 _originalPosition = Vector3.zero;
	private Quaternion _originalRotation;
	
	public float jumpForce = 11f;
	
	public ForceMode RUN_FORCE_MODE = ForceMode.Force;
	public ForceMode JUMP_FORCE_MODE = ForceMode.Impulse;
	
	private Vector3 JUMP_VECTOR = new Vector3(0f,1f,0f);
	
	// Use this for initialization
	void Start () {
		_originalPosition = this.transform.position;
		_originalRotation = this.transform.rotation;

		_isOnGround = false;	
		_isRunning = false;
	}
	
	public void FixedUpdate() {		
		if(_isRunning) {
			int mult = (_currRunDirection == RunDirection.LEFT ? -1 : 1);
			rigidbody.AddForce(Vector3.forward * horizontalAcceleration * mult,RUN_FORCE_MODE);
		}
	}
	
	public void OnGUI() {
		Event e = Event.current;
		if(e.isKey) {
			
			if(Input.GetKeyDown(e.keyCode)) {
				switch(e.keyCode) {
				case KeyCode.A:
					_Run(RunDirection.LEFT);
					break;
				case KeyCode.D:
					_Run(RunDirection.RIGHT);
					break;
				case KeyCode.Space:
					_DoJump();
					break;
				case KeyCode.R:
					_Reset();
					break;
				case KeyCode.S:
					_DoSuperGravity();
					break;
				case KeyCode.Return:
					_DoGravitySwap();
					break;
				}
			}
			
			if(Input.GetKeyUp(e.keyCode)) {
				switch(e.keyCode) {
				case KeyCode.D:
				case KeyCode.A:
					if(_isRunning) {
						_isRunning = false;
					}
					break;
				}
				
			}
		} else {
			//Touch[] touches = Input.touches;
			if(GameLogic.touches == null) {
				GameLogic.touches = Input.touches;	
			}
			Touch[] touches = GameLogic.touches;
			int fingerCount = 0;
			bool isSwip = false;
			foreach(Touch touch in touches) {
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
                	fingerCount++;
				}
				if(touch.phase == TouchPhase.Ended) {
					if(touch.deltaPosition.y > 50) {
						isSwip = true;	
						break;
					}
				}
			}
			
			if(isSwip) {
				_DoGravitySwap();
			} else {
				switch (fingerCount) {
				case 1:
					_DoJump();
					break;
				case 2:
					break;
				}
			}
		}
	}
	
	private void _DoJump() {
		if(_isJumping == false && _isOnGround) {
			_isJumping = true;
			_isOnGround = false;
			rigidbody.AddRelativeForce(JUMP_VECTOR * jumpForce,JUMP_FORCE_MODE);
		}	
	}
	
	private void _DoGravitySwap() {
		_isOnGround = false;
		this.GetComponent<GravitySwitchComponent>().Swtich();
		this.GetComponent<PlayerGravityAnim>().ShouldAnimate = true;
		jumpForce *= -1;
	}
				
	private void _DoSuperGravity() {
		this.GetComponent<GravitySwitchComponent>().MagnifyGravity();				
	}
	
	private void _Run(RunDirection direction) {
		if(_isOnGround) {
			_isRunning = true;
			_currRunDirection = direction;
		}
	}
	
	private void _Reset() {
		this.transform.position = _originalPosition;
		this.transform.rotation = _originalRotation;	
	}
	
	void OnCollisionEnter(Collision collision) {
		// This is not a very good idea since you are not checking what the collision
		// is with. The level is so simple right now it doesn't matter.
		// It should however check that you are colliding with the top of something (a platform)
		// sine that will cause a bug where the player can infinietly jump by touching the side of a platform. 
		_isJumping = false;	
		_isOnGround = true;

		// Referncing a nother component makes this component coupled to the PlayGravityAnimation
		// component. This dependency should be removed so the components can be as mutally exclusive as possible
		this.GetComponent<PlayerGravityAnim>().ShouldAnimate = false;
		this.transform.rotation = _originalRotation;
	}
	
	void OnCollisionExit(Collision collision) {
		// Not a good idea, should check what type of object the collision is exiting off of.
		_isOnGround = false;	
		_isRunning = false;
	}
}
