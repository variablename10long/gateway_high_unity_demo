using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	
	GameObject _player = null;
	
	public void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void Update() {
		Vector3 cameraPos = transform.position;
		cameraPos.z = _player.transform.position.z;
		this.transform.position = cameraPos;
	}
}
