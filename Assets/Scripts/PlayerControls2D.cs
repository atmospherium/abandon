using UnityEngine;
using System.Collections;

public class PlayerControls2D : MonoBehaviour {
	private Animator animator;
	float speed = 4f;
	float xScale;
	float scaleMod = 1;

	private bool isRunning = false;
	private float _runMod = 1.5f;

	private float runMod {
		get{
			if(isRunning){
				return _runMod;
			}else{
				return 1f;
			}
		}
	}


	void Start(){
		animator = GetComponent<Animator>();
		xScale = Mathf.Abs(transform.localScale.x);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftShift)){
			isRunning = true;
		}else{
			isRunning = false;
		}
		float h = Input.GetAxis("Horizontal") * speed * runMod;
		bool j = Input.GetButtonDown("Jump");

		float currentY = rigidbody2D.velocity.y;

		if(h>0){
			scaleMod = Mathf.Abs(scaleMod);
		}else if(h<0){
			scaleMod = -Mathf.Abs(scaleMod);
		}

		transform.localScale = new Vector2(xScale*scaleMod,transform.localScale.y);

		if(j){
			currentY = 5;
		}

		animator.SetFloat("Speed",Mathf.Abs(h));
		animator.SetFloat("YSpeed",currentY);

		rigidbody2D.velocity = new Vector2(h,currentY);
	}
}