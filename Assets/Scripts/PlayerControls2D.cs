using UnityEngine;
using System.Collections;

public class PlayerControls2D : MonoBehaviour {
	private Animator animator;
	float speed = 6f;
	float xScale;
	float scaleMod = 1;

	void Start(){
		animator = GetComponent<Animator>();
		xScale = Mathf.Abs(transform.localScale.x);
	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal")* speed;
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