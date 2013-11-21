using UnityEngine;
using System.Collections;

public class FireTexture : MonoBehaviour {
	
	static float yOffset = 0.00002f;
	static float xOffset = 0.00002f;
	
	Vector2 offset;
	
	// Use this for initialization
	void Start () {
		offset = new Vector2(xOffset,yOffset);
		renderer.sharedMaterial.mainTextureOffset = new Vector2(0,0);
		renderer.sharedMaterial.SetTextureOffset("_BumpMap",new Vector2(0,0));
	}
	
	void FixedUpdate(){
		Vector2 mainTexOffset = renderer.sharedMaterial.mainTextureOffset;
		mainTexOffset.x = (mainTexOffset.x-xOffset/2);
		mainTexOffset.y = (mainTexOffset.y+yOffset/2);
		mainTexOffset = NormalizeOffset(mainTexOffset);
		renderer.sharedMaterial.mainTextureOffset = mainTexOffset;
		
		Vector2 bumpTexOffset = renderer.sharedMaterial.GetTextureOffset("_BumpMap");
		bumpTexOffset += offset;
		bumpTexOffset = NormalizeOffset(bumpTexOffset);
		//renderer.sharedMaterial.SetTextureOffset("_BumpMap",renderer.sharedMaterial.mainTextureOffset);
		renderer.sharedMaterial.SetTextureOffset("_BumpMap",bumpTexOffset);
	}
	
	private Vector2 NormalizeOffset(Vector2 offset){
		if(Mathf.Abs(offset.x)>=1){
			offset.x = Mathf.Sign(offset.x)*(Mathf.Abs(offset.x)-Mathf.Floor(Mathf.Abs(offset.x)));
		}
		
		if(Mathf.Abs(offset.y)>=1){
			offset.y = Mathf.Sign(offset.y)*(Mathf.Abs(offset.y)-Mathf.Floor(Mathf.Abs(offset.y)));
		}
		
		return offset;
	}
}
