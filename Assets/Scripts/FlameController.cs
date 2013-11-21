using UnityEngine;
using System.Collections;

public class FlameController : MonoBehaviour {

	float speed = 200;
	
	// Update is called once per frame
	void Update () {
		float angleValX = transform.eulerAngles.x;
		float angleValY = transform.eulerAngles.y;
		float angleVal = angleValX;
		
		if(angleValY>0){
			angleVal = 360-angleValX;
		}else{
			angleVal = angleValX < 180 ? angleValX+180 : angleValX;
		}
		
		transform.Rotate(Vector3.left, speed*Time.deltaTime);
	}
	
}
