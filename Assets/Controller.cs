//using UnityEngine;
//using System.Collections;
//
//public class Controller : MonoBehaviour {
//	
//	private Rigidbody2D myrigidbody;
//	
//	public float maxSpeed=10f;
//	bool facingRight = true;
//	
////	public bool grounded;
////	public float jumpPower;
////	public Transform shotSpawn;
////	public GameObject shot;
////	public float nextFire;
////	public float fireRate;
//	
////	public bool onLadder = false;
////	public float climbSpeed;
////	private float climbVelocity;
////	private float gravityStore;
//	
////	Animator anim;
//	
//	// Use this for initialization
//	void Start () {
////		anim = GetComponent<Animator> ();
//		myrigidbody = GetComponent<Rigidbody2>();
//		
//		grounded = false;
//		
//		gravityStore = myrigidbody2D.gravityScale;
//		Debug.Log("GravityStore = " + gravityStore);
//	}
//	
//	void Update() {
//		if (Input.GetButtonDown("Jump") && grounded) {
//			Debug.Log("Jump button pressed.");
//			myrigidbody2D.AddForce(Vector2.up * jumpPower);
//		}
//		
//		if (Input.GetButton ("Fire4") && Time.time > nextFire) {
//			Debug.Log ("Fire4 button pressed.");
//			nextFire = Time.time + fireRate;
//			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
//		}
//		
//		//		if (Input.GetButton("Fire1") && Time.time > nextFire) {
//		//			nextFire = Time.time + fireRate;
//		//			//GameObject clone = 
//		//			Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
//		//			
//		//			GetComponent<AudioSource>().Play ();
//		//		}
//		
//		
//	}
//	
//	void FixedUpdate () {
//		//Rigidbody2D myRigidBody = GetComponentInParent<Rigidbody2D> ();
//		float move = Input.GetAxis ("Horizontal");
//		
//		//		anim.SetFloat ("Speed", Mathf.Abs (move));
//		
//		myrigidbody2D.velocity = new Vector2 (move * maxSpeed, myrigidbody2D.velocity.y);
//		
//		if (move > 0 && !facingRight)
//			Flip ();
//		else if (move < 0 && facingRight)
//			Flip ();
//		
//		//Debug.Log("onLadder? " + onLadder);
//		if (onLadder)
//		{
//			myrigidbody2D.gravityScale = 0f;
//			
//			climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
//			
//			myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, climbVelocity);
//			
//		}
//		if (!onLadder)
//		{
//			myrigidbody2D.gravityScale = gravityStore;
//		}
//	}
//	
//	void Flip(){
//		facingRight = !facingRight;
//		Vector3 theScale = GameObject.FindGameObjectWithTag("Player").transform.localScale;
//		theScale.x *= -1;
//		//transform.localScale = theScale;
//		GameObject.FindGameObjectWithTag("Player").transform.localScale = theScale;
//	}
//}
