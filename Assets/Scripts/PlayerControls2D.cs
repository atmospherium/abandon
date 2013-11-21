using UnityEngine;
using System.Collections;

public class PlayerControls2D : MonoBehaviour {

	float speed = 6f;

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxisRaw("Horizontal")* speed;
		bool j = Input.GetButtonDown("Jump");

		float currentY = rigidbody2D.velocity.y;

		if(j){
			currentY = 5;
		}

		rigidbody2D.velocity = new Vector2(h,currentY);
	}
}