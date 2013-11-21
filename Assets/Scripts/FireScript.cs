using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {
	
	private Light fireLight;
	private static GameController gameController;
	
	void Start(){
		if(gameController==null){
			gameController = GameObject.Find("GameController").GetComponent<GameController>();
		}
		fireLight = transform.FindChild("FireLight").light;
	}
	
	void LateUpdate(){
		if(gameController.playerObject!=null){
			if(Mathf.Abs(transform.position.y-gameController.cameraObject.transform.position.y)<5&&Mathf.Abs(transform.position.x-gameController.cameraObject.transform.position.x)<9){
				fireLight.enabled = true;
			}else{
				fireLight.enabled = false;
			}
		}
	}
}
