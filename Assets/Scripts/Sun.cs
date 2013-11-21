using UnityEngine;
using System.Collections;

public class Sun : Supporter {
	
	GameObject sun;
	
	// Use this for initialization
	void Start () {
		sun = GameObject.Find("Sun");
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Abs(transform.position.y-target.y);
		if(distance < 2.7){
			light.intensity = 1;
		}else{
			light.intensity = Mathf.Clamp(1-(distance*distance)/60,0,3);
		}
		
		if(light.intensity==0){
			Destroy(sun);
		}
	}
}
