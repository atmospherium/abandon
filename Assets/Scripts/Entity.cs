using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	public float IncrementToward(float n, float t, float a){
		if(n==t){
			return n;
		}else{
			float dir = Mathf.Sign(t-n);
			n+=a*Time.deltaTime*dir;
			return (dir==Mathf.Sign(t-n)) ? n : t;
		}
	}
	
}
