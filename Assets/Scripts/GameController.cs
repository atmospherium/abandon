using UnityEngine;
using System.IO;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject player;
	public GameObject playerObject;
	private Vector2 playerTransform;
	private PlayerControls playerControls;
	public GameObject playerLight;
	public GameObject floor;
	public GameObject altFloor;
	public GameObject fire;
	public GameObject sun;
	public GameObject camera;
	public GameObject backWall;
	public GameObject sky;
	public GameObject levelHolder;
	public GameObject cameraObject;
	
	//public TextAsset levelText;
	//public GameObject textHolder;
	//public GameObject exit;
	
	public Transform explosion;
	
	public Texture2D level;
	
	private CameraScript cameraScript;
	private LightScript lightScript;
	private GameObject explosionTarget;
	private int screenWidth;
	private int screenHeight;
	
	int segmentSize = 32;
	
	Vector2 playerSpawn;
	
	GameObject[,] segObject;
	
	// Use this for initialization
	void Start () {
		
		Application.targetFrameRate = 60;
		
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		int height = level.height;
		int width = level.width;
		
		int segmentsX = width/segmentSize;
		int segmentsY = height/segmentSize;
		
		segObject = new GameObject[segmentsX,segmentsY];
		
		Debug.Log("Segments X - "+segmentsX.ToString());
		Debug.Log("Segments Y - "+segmentsY.ToString());
		
		//string[] lines = levelText.text.Split("\n"[0]);
		//int fileLineCount = 0;
		
		GameObject levelHolderObject = Instantiate(levelHolder) as GameObject;
		levelHolderObject.name = "LevelHolder";
		
		//GameObject floorObject = levelHolderObject.gameObject.transform.FindChild("Floors").gameObject as GameObject;
		//GameObject falseFloorObject = levelHolderObject.gameObject.transform.FindChild("FalseFloors").gameObject as GameObject;
		//GameObject backWallObject = levelHolderObject.gameObject.transform.FindChild("BackWall").gameObject as GameObject;
		//GameObject fireObject = levelHolderObject.gameObject.transform.FindChild("Fires").gameObject as GameObject;
		
		
		for(var segX = 0; segX < segmentsX; segX++){
			int segXStart = segX*segmentSize;
			int segXEnd = segXStart+segmentSize;
			for(var segY = 0; segY<segmentsY; segY++){
				int segYStart = segY*segmentSize;
				int segYEnd = segYStart+segmentSize;
				segObject[segX,segY] = new GameObject(segX.ToString()+","+segY.ToString());
				segObject[segX,segY].transform.position = new Vector3(segXStart,segYStart);
				
				GameObject floorObject = new GameObject("Floors");
				floorObject.transform.parent = segObject[segX,segY].transform;
				floorObject.transform.position = new Vector3(0,0,0);
				
				GameObject falseFloorObject = new GameObject("FalseFloors");
				falseFloorObject.transform.parent = segObject[segX,segY].transform;
				falseFloorObject.transform.position = new Vector3(0,0,0);
				
				GameObject backWallObject = new GameObject("BackWall");
				backWallObject.transform.parent = segObject[segX,segY].transform;
				backWallObject.transform.position = new Vector3(0,0,0);
				
				GameObject fireObject = new GameObject("Fires");
				fireObject.transform.parent = segObject[segX,segY].transform;
				fireObject.transform.position = new Vector3(0,0,0);
				
				for(var x = segXStart; x < segXEnd; x++){
					for(var y = segYStart; y < segYEnd; y++){
						var p = level.GetPixel(x,y);
						var r = p.r;
						var g = p.g;
						var b = p.b;
						var a = p.a;
						if(g==1&&r+b==0){
							playerSpawn = new Vector2(x,y);
							/*
						}else if(r==1&&g+b==0){
							GameObject textHolderObject = Instantiate(textHolder, new Vector3(x,y,0),Quaternion.identity) as GameObject;
							textHolderObject.name = "TextHolder-"+x+","+y;
							textHolderObject.GetComponent<TextScript>().setText(lines[fileLineCount]);
							fileLineCount++;
						}else if(b==1&&r+g==0){
							GameObject exitPiece = Instantiate(exit, new Vector3(x,y,0),Quaternion.identity) as GameObject;
							exit.name = "Exit";
						*/}else if(r==1&&b+g==0){
							GameObject firePiece = Instantiate(fire, new Vector3(x,y,0), Quaternion.identity) as GameObject;
							firePiece.name = "fire_"+x+"/"+y;
							firePiece.transform.parent = fireObject.transform;
						}else if(r+g+b==0){
							GameObject floorPiece = Instantiate(floor, new Vector3(x,y,0),Quaternion.identity) as GameObject;
							floorPiece.name = "floor_"+x+"/"+y;
							floorPiece.transform.parent = floorObject.transform;
							floorPiece.renderer.material.mainTexture.mipMapBias = 1.2f;
						}else if(g*b==1&&r==0){
							GameObject floorPiece = Instantiate(altFloor, new Vector3(x,y,0),Quaternion.identity) as GameObject;
							floorPiece.name = "floor_"+x+"/"+y;
							floorPiece.transform.parent = falseFloorObject.transform;
						}else if(r*g==1&&b==0){
							GameObject sunPiece = Instantiate(sun, new Vector3(x,y,0),Quaternion.identity) as GameObject;
							sunPiece.name = "Sun";
						}
						
						
						if(y<height-3){
							GameObject wallPiece = Instantiate(backWall, new Vector3(x,y,-0.5f),Quaternion.identity) as GameObject;
							wallPiece.name = "wall_"+x+"/"+y;
							wallPiece.transform.parent = backWallObject.transform;
						}else{
							//GameObject skyPiece = Instantiate(sky, new Vector3(x,y,-0.5f),Quaternion.identity) as GameObject;
							//skyPiece.name = "sky_"+x+"/"+y;
						}
					}
				}
				
				if(floorObject.transform.childCount>0){
					floorObject.AddComponent<MeshCombiner>();
					floorObject.GetComponent<MeshCombiner>().Combine();
				}
				
				if(falseFloorObject.transform.childCount>0){
					falseFloorObject.AddComponent<MeshCombiner>();
					falseFloorObject.GetComponent<MeshCombiner>().Combine();
				}
				
				if(fireObject.transform.childCount>0){
					fireObject.AddComponent<MeshCombiner>();
					fireObject.GetComponent<MeshCombiner>().Combine();
					fireObject.gameObject.layer = LayerMask.NameToLayer("Fire");
				}
				
				if(backWallObject.transform.childCount>0){
					backWallObject.AddComponent<MeshCombiner>();
					backWallObject.GetComponent<MeshCombiner>().Combine();
				}
			}
		}
		
		CreatePlayer();
		
		if(playerObject){
			/*GameObject playerLightObject = Instantiate(playerLight) as GameObject;
			LightScript lightScript = playerLightObject.GetComponentInChildren<LightScript>();
			lightScript.calibrate(playerObject);
			playerLightObject.name = "FollowLight";*/
		}
	}
	
	public void CreatePlayer(){
		Debug.Log("Trying to create Player");
		float x = playerSpawn.x;
		float y = playerSpawn.y;
		if(x>=0){
			playerTransform.x = x;
			playerTransform.y = y;
		}else{
			x = playerTransform.x;
			y = playerTransform.y;
		}
		playerObject = Instantiate(player,new Vector3(x,y,0),Quaternion.identity) as GameObject;
		playerControls = playerObject.GetComponent<PlayerControls>();
		playerObject.name = "Player";
		
		if(GameObject.Find("MainCamera")){
			cameraObject = GameObject.Find("MainCamera");
		}else{
			cameraObject = Instantiate(camera) as GameObject;
			cameraObject.name = "MainCamera";
		}
		cameraScript = cameraObject.GetComponentInChildren<CameraScript>();
		cameraScript.Calibrate(playerObject);
		
		GameObject playerLightObject;
		if(GameObject.Find("FollowLight")){
			playerLightObject = GameObject.Find("FollowLight");
		}else{
			playerLightObject = Instantiate(playerLight) as GameObject;
			playerLightObject.name = "FollowLight";
		}
		lightScript = playerLightObject.GetComponentInChildren<LightScript>();
		lightScript.Calibrate(playerObject);
		
		if(GameObject.Find("Sun")){
			GameObject sunObject = GameObject.Find("Sun");
			Sun sunScript = sunObject.GetComponentInChildren<Sun>();
			sunScript.Calibrate(playerObject);
		}
		
	}
	
	void Update(){
		
		//SegmentController();
		
		if(Input.GetKeyDown("r")){
			PlayerKill();
		}
		if(Input.GetKeyDown("f12")){
			if(Screen.fullScreen){
				Screen.SetResolution(screenWidth,screenHeight,false);
			}else{
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
			}
		}
	}
	
	void SegmentController(){
		Vector3 playerPos = playerObject.transform.position;
		int playerSegX = (int)Mathf.Floor(playerPos.x/segmentSize);
		int playerSegY = (int)Mathf.Floor(playerPos.y/segmentSize);
		for(var x = 0; x < segObject.GetLength(0); x++){
			if(Mathf.Abs(playerSegX-x)>1){
				//segObject[x,y].gameObject.active = false;
				continue;
			}
			for(var y = 0; y < segObject.GetLength(1); y++){
				if(Mathf.Abs(playerSegX-x)>1||Mathf.Abs(playerSegY-y)>1){
					segObject[x,y].gameObject.active = false;
				}else{
					segObject[x,y].gameObject.active = true;
				}
			}
		}
			
	}
	
	private IEnumerator _playerKill(){
		explosionTarget = Instantiate(explosion,playerObject.transform.position,Quaternion.identity) as GameObject;
		cameraScript.Calibrate(playerObject.transform.position);
		lightScript.Calibrate(playerObject.transform.position);
		Destroy(playerObject);
		yield return new WaitForSeconds(0.5f);
		CreatePlayer();
	}
	
	public void PlayerKill(){
		if(playerObject!=null){
			StartCoroutine(_playerKill());
		}
	}
}
