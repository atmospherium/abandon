using UnityEngine;
using System.Collections;

public class CameraScript : Supporter {
	
	public float _moveSpeed = 2f;
	Vector3 cameraOffset;
	PlayerControls playerControl;
	
	private float moveSpeed{
		get{return _moveSpeed*Time.deltaTime;}
	}
	
	/*
	// Update is called once per frame
	void Update () {
		if(target!=null){
			transform.position = Vector3.Lerp(transform.position,target.transform.position-cameraOffset,Time.deltaTime*3f);
		}
	}
	
	public void calibrate(GameObject target){
		this.target = target;
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
		cameraOffset = target.transform.position - transform.position;
	}
	*/
	
	void LateUpdate(){
		Vector3 targetPos = target;
		targetPos.z = transform.position.z;
		targetPos.y += 0.5f;
		transform.position = Vector3.Lerp(transform.position,targetPos,moveSpeed);
		//transform.LookAt(target);
		transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( target - transform.position ), 0.1f*Time.deltaTime );
	}
}
