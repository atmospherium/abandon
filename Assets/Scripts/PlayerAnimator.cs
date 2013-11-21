using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	
	GameObject animTarget;
	PlayerControls playerControls;
	
	float animFrames = 8;
	float animRows = 4;
	float currentFrame = 0;
	float currentRow = 0;
	int pauseLength = 4;
	int position = 0;
	float scaleX;
	float scaleY;
	float animDir = 1;

	// Use this for initialization
	void Start () {
		playerControls = GetComponent<PlayerControls>();
		FindAnimTarget();
		scaleX = 1f/animFrames;
		scaleY = 1f/animRows;
	}
	
	void FixedUpdate(){
		if(animTarget==null){
			FindAnimTarget();
		}
		if(playerControls._playerState == PlayerControls.PlayerState.jumping){
			currentRow = 0;
		}else if(playerControls._playerState == PlayerControls.PlayerState.running){
			currentRow = 1;
		}else if(playerControls._playerState == PlayerControls.PlayerState.walking){
			currentRow = 2;
		}else{
			currentRow = 3;
		}
		
		currentRow = currentRow*scaleY-0.005f;
		
		float playerDir = Input.GetAxisRaw("Horizontal");
		if(playerDir>0){
			animDir = 1;
		}else if(playerDir<0){
			animDir = -1;
		}
		
		if(animDir>0){
			animTarget.renderer.material.mainTextureScale = new Vector2(scaleX,scaleY);
		}else if(animDir<0){
			animTarget.renderer.material.mainTextureScale = new Vector2(scaleX*-1,scaleY);
		}
		
		if(position == pauseLength){
			if(animDir>0){
				animTarget.renderer.material.SetTextureOffset("_MainTex", new Vector2(currentFrame/animFrames,currentRow));
			}else if(animDir<0){
				animTarget.renderer.material.SetTextureOffset("_MainTex", new Vector2((currentFrame+1)/animFrames,currentRow));
			}
			currentFrame = (currentFrame+1)%animFrames;
		}
			
		
		position = (position==pauseLength) ? 0 : position+1;
	}
	
	void FindAnimTarget(){
		animTarget = GameObject.Find("PlayerAnim");
		animTarget.renderer.material.mainTextureScale = new Vector2(scaleX,scaleY);
	}
}
