using UnityEngine;
using System.Collections;

public class LightScript : Supporter {
	
	Vector3 lightOffset;
	PlayerControls playerControl;
	float zMax = 1.4f;
	float zMin = 0.9f;
	float zOffset = 1f;
	float zSpeed = 0.005f;
	float zDir = -1f;
	
	// Update is called once per frame
	void LateUpdate () {
		if(target!=null){
			//float lightSpeed = 10;
			if(zOffset>=zMax){
				zDir = -1f;
			}else if(zOffset<= zMin){
				zDir = 1f;
			}
			
			zOffset += zSpeed*zDir;
			
			transform.position = new Vector3(target.x-0.13f,target.y,target.z-zOffset);
			//transform.position = Vector3.Lerp(transform.position,target.transform.position-lightOffset,Time.deltaTime*lightSpeed);
		}
	}
	
	/*public void calibrate(GameObject target){
		this.target = target;
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
		playerControl = target.GetComponent<PlayerControls>();
		transform.position = new Vector3(target.transform.position.x,target.transform.position.y, transform.position.z);
		lightOffset = target.transform.position - transform.position;
	}*/
}
