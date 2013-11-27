using UnityEngine;
using System.Collections;

public class PlayerControls2D : MonoBehaviour {
	private Animator animator;
	float speed = 6f;


	void Start(){
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal")* speed;
		bool j = Input.GetButtonDown("Jump");

		float currentY = rigidbody2D.velocity.y;

		if(j){
			currentY = 5;
		}

		animator.SetFloat("Speed",Mathf.Abs(h));
		animator.SetFloat("YSpeed",currentY);

		rigidbody2D.velocity = new Vector2(h,currentY);
	}
}