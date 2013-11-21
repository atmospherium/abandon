using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	private static Game _instance;
	
	public static Game instance {
		get{
			if(!_instance){
				_instance = new Game();
			}
			return _instance;
		}
	}
	
	public void Test(){
		Debug.Log("Test");
	}
}
