using UnityEngine;

/// <summary>
/// Gravity switch component.
/// 
/// Useful for creating objects that respond to switches in gravity. For example the player character
/// or objects that can be used to obstruct progress or unlock puzzles.
/// </summary>
public class GravitySwitchComponent : MonoBehaviour {
	
	/// <summary>
	/// Gravity direction, used as a scalar value in our vector multiplication.
	/// </summary>
	public enum GravityDirection {DOWN = -1, UP = 1};
	
	/// <summary>
	/// The current gravity direction to use in our vector multiplication.
	/// This is being store on an instance basis instead of as a static member var
	/// for flexibility at this point.
	/// </summary>
	private GravityDirection _currentGravityDirection = GravityDirection.DOWN;
	
	/// <summary>
	/// The predefined vector to repersenting normal gravity forces.
	/// </summary>
	public  Vector3 GRAVITY_NORMAL = new Vector3(0f,-11f,0f);//Physics.gravity;
	
	/// <summary>
	/// The predefined vector to repersenting reverse gravity forces.
	/// </summary>
	public  Vector3 GRAVITY_REVERSED = new Vector3(0f,11f,0f);
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	public void Start() {
		_currentGravityDirection = GravityDirection.DOWN;	
	}
	
	/// <summary>
	/// Fixeds the update.
	/// </summary>
	public void FixedUpdate() {
		Vector3 forceVector = (_currentGravityDirection == GravityDirection.DOWN ? GRAVITY_NORMAL : GRAVITY_REVERSED);
		rigidbody.AddForce(forceVector);
	}
	
	/// <summary>
	/// Toggles the direction of gravity on this object.
	/// </summary>
	public GravityDirection Swtich() {
		return _currentGravityDirection = (_currentGravityDirection == GravityDirection.DOWN ? GravityDirection.UP : GravityDirection.DOWN);	
	}
	
	public void MagnifyGravity() {
		
		GRAVITY_REVERSED *= 3;
		GRAVITY_NORMAL *= 3;
	}	
	
	public GravityDirection CurrentGravityDirection {
		get {
			return _currentGravityDirection;
		}
	}
}