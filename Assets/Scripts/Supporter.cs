using UnityEngine;
using System.Collections;

public class Supporter : MonoBehaviour {
	
	protected GameObject gTarget;
	protected Vector3 tTarget;
	protected Vector3 target {
		get {
			if(gTarget!=null){
				return gTarget.transform.position;
			}else{
				return tTarget;
			}
		}
	}
	
	public void Calibrate(GameObject target){
		this.gTarget = target;
	}
	
	public void Calibrate(Vector3 target){
		gTarget=null;
		this.tTarget = target;
	}
	
	protected float IncrementToward(float start, float target, float speed){
		return (target-start)*speed;
	}
}
