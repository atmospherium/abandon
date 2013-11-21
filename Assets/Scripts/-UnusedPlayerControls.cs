using UnityEngine;
using System.Collections;

public class PlayerControls : Entity {
	
	GameController gameController;
	
	[System.Serializable]
	public class PlayerAttributes{
		public float speed = 3.5f;
		public float runSpeed = 7.0f;
		public float acceleration = 20;
		public float jumpHeight = 15;
		public float fallSpeed = 12;
		public int jumpLimit = 2;
		public bool canMove = true;
		public bool canJump = true;
		public bool canWallSlide = false;
		public bool canWallJump = false;
		public bool canSlide = true;
	}
	public PlayerAttributes _attributes = new PlayerAttributes();
	
	[System.Serializable]
	public class PlayerPhysics{
		public float gravity = 30f;
		public int jumpIndex = 0;
		
	}
	public PlayerPhysics _physics = new PlayerPhysics();
	
	public enum PlayerState {idle,walking,running,jumping,falling,sliding,wallsliding,ducking,hanging,dead}
	public PlayerState _playerState = new PlayerState();
	
	enum PhysicsState {grounded,falling,walled}
	PhysicsState _physicsState = new PhysicsState();
	
	enum collisionDirection {Left, Right, None};
	collisionDirection _collisionDirection = new collisionDirection();
	
	private float currentSpeed = 0;
	private float targetSpeed = 0;
	private Vector2 movement;	
	
	public LayerMask collisionMask;
	private BoxCollider2D playerCollider;
	private float skin = 0.005f;
	
	private Vector2 p;
	private Vector3 c;
	private Vector3 s;
	private Ray ray;
	private RaycastHit hit;
	
	public bool isWalled = false;
	public bool isGrounded = false;
	public bool isWallSliding = false;
	float wallSlideCounter = 0;
	float wallSlideLength = 0.5f;
	
	float deltaX = 0;
	float deltaY = 0;
	
	bool destroyed = false;
	
	//Accessor list;
	private float Gravity{
		get{
			return _physics.gravity*GravMod*Time.deltaTime;
		}
		set{
			_physics.gravity = value;
		}
	}
	
	private float GravMod{
		get{ return 1f; }
	}
	
	void Start(){
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		_playerState = PlayerState.idle;
		playerCollider = GetComponent<BoxCollider2D>();
		s = playerCollider.size;
		c = playerCollider.center;
	}
	
	float jumpTime = 0;
	
	void Update(){
		p = transform.position;
		
		float speed = Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift) ? _attributes.runSpeed : _attributes.speed;
		
		movement.x = IncrementToward(movement.x,Input.GetAxisRaw("Horizontal")*speed,_attributes.acceleration);
		
		float gravMod = 1;
		if(isGrounded){
			if(Mathf.Abs(movement.x)>_attributes.speed){
				_playerState = PlayerState.running;
			}else if(Mathf.Abs(movement.x)>0){
				_playerState = PlayerState.walking;
			}else{
				_playerState = PlayerState.idle;
			}
			_physics.jumpIndex = 0;
			wallSlideCounter = 0;
			Jump();
		}else{
			/*if(wallSlideCounter>0&&movement.y<5){
				//Debug.Log(wallSlideCounter);
				movement.x = 0;
				gravMod = 0.7f;
				WallJump();
				wallSlideCounter -= Time.deltaTime;
			}else{*/
				if(_physics.jumpIndex<_attributes.jumpLimit){
					Jump();
				}
			//}
		}
		//JumpHold();
		
		movement.y -= Gravity;
		
		deltaX = movement.x*Time.deltaTime;
		deltaY = movement.y*Time.deltaTime;
		
		if(_attributes.canWallSlide){
		
			isWallSliding = !isGrounded&&isWalled ? true : false;
			
			if(isWallSliding){
				wallSlideCounter = wallSlideLength;
			}
			
		}
		
		if(isGrounded){
			if(jumpTime>0){
				Debug.Log(jumpTime);
				jumpTime = 0;
			}
			_physics.jumpIndex = 0;
			movement.y = 0;
			if(isWalled){
				movement.x = 0;
			}
		}else{
			if(jumpTime>0){
				jumpTime += Time.deltaTime;
			}
			_playerState = PlayerState.jumping;
			if(Mathf.Abs(movement.y)>_attributes.fallSpeed){
				movement.y = _attributes.fallSpeed*Mathf.Sign(movement.y);
			}
		}
		transform.Translate(new Vector2(deltaX,deltaY));
	}
	
	void FixedUpdate(){
		JumpHold();	
	}
	private int jumpHold = 0;
	
	void Jump(){
		if(Input.GetButtonDown("Jump")){
			_physics.jumpIndex++;
			movement.y = _attributes.jumpHeight;
			jumpHold = 20;
			jumpTime = 0.00001f;
		}
	}
	
	void JumpHold(){
		if(jumpHold>0&&Input.GetButton("Jump")){
			Debug.Log("Holding button");
			movement.y *= 1.04f;
			jumpHold--;
		}else{
			jumpHold = 0;
		}
	}
	
	void WallJump(){
		if(Input.GetButtonDown("Jump")){
			_physics.jumpIndex = _attributes.jumpLimit;
			movement.x = _attributes.jumpHeight*Mathf.Sign(Input.GetAxisRaw("Horizontal"))*0.8f;
			movement.y = _attributes.jumpHeight*0.8f;
			wallSlideCounter = 0;
		}
	}
}
