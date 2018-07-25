using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
	public float MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
	public float JumpSpeed = 400f;                  // Amount of force added when the player jumps.
	public bool moveInAir = false;                 // Whether or not a player can steer while jumping;
	public LayerMask GroundLayer;   
	public LayerMask NoClipGroundLayer;                // A mask determining what is ground to the character

	private Transform CheckIfGround;    // A position marking where to check if the player is grounded.
	const float CheckIfGroundRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool Grounded;            // Whether or not the player is grounded.
	private Transform CeilingCheck; 
	public Transform RightCheck;
	public Transform LeftCheck;  // A position marking where to check for ceilings
	const float CeilingCheckRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
	public Animator PlayerAnimator;         // Reference to the player's animator component.
	public Rigidbody2D Rigidbody2D;
	public bool FacingRight = true;
	public Collider2D bodyCollider; // For determining which way the player is currently facing.
	public GameObject Arm; 
	private float angle;
	private Vector3 dir;
	private Vector3 theScale;
	private float movo;
	public float aim_angle;
	public Collider2D feetCollider;
	public Collider2D HeadCollider;
	public float dist = 1.0f;
	private bool fall = false;
	public int Xbox_One_Controller = 0;
	public bool Level2 = false;
	public Sprite idleSprite;
	private void Awake(){
		CheckIfGround = transform.Find("GroundCheck");
		CeilingCheck = transform.Find("CeilingCheck");
		RightCheck = transform.Find("RightCheck");
		LeftCheck = transform.Find("LeftCheck");
		Rigidbody2D = GetComponent<Rigidbody2D>();
		theScale = transform.localScale;
	}

	void Update(){
		if(Level2){PlayerAnimator.SetBool("Level2", true);return;}

		string[] names = Input.GetJoystickNames();
		for (int f = 0; f < names.Length; f++)
		{
			if (names[f].Length >= 20)
			{
				Debug.Log("XBOX ONE CONTROLLER IS CONNECTED");
				Xbox_One_Controller = 1;
			}
		}
 
 
		if(Xbox_One_Controller == 1){
			float x = Input.GetAxis ("J_Horizontal");
			float y = Input.GetAxis ("J_Vertical");
	
			// CANCEL ALL INPUT BELOW THIS FLOAT
			
	
			// USED TO CHECK OUTPUT
			Debug.Log(" horz: " + x + " vert: " + y);
	
			// CALCULATE ANGLE AND ROTATE
			if (x != 0.0f || y != 0.0f) {
					aim_angle = Mathf.Atan2 (y, -x) * Mathf.Rad2Deg;
					// USED TO CHECK OUTPUT
					Debug.Log ("angle: " + aim_angle);
					if(FacingRight){
						aim_angle += 90f;
					} else {
						aim_angle -= 90f;
					}
					Arm.transform.rotation = Quaternion.AngleAxis(aim_angle, Vector3.forward);
			}//do something
		} else {
			dir = Input.mousePosition - Camera.main.WorldToScreenPoint(Arm.transform.position);

			if(FacingRight){
				angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
				Arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			} else {
				angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
				Arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * -1);
			}
		}
		 /*
		dir = Input.mousePosition - Camera.main.WorldToScreenPoint(Arm.transform.position);
		/*
		if(movo < 0.01f && movo > -0.01f){
			Debug.Log("lookAround");
			dir = Input.mousePosition - Camera.main.WorldToScreenPoint(Head.transform.position);
		}
		if(movo > 0.01f){
			Debug.Log("lookright");
			var angles = transform.rotation.eulerAngles;
			angles.z = -180;
			Head.transform.rotation = Quaternion.Euler(angles);
		}
		if(movo < -0.01f){
			Debug.Log("lookleft");
			var angles = transform.rotation.eulerAngles;
			angles.z = -180;
			Head.transform.rotation = Quaternion.Euler(angles);
		}
		

		if(FacingRight){
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			
		} else {
			angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
			Arm.transform.rotation = Quaternion.AngleAxis(aim_angle, Vector3.forward * -1);
		}
		*/

		RaycastHit2D hit = Physics2D.Raycast(CheckIfGround.transform.position, -Vector2.up, dist, GroundLayer + NoClipGroundLayer);
        if (hit.collider != null) {
            Grounded = true;
			feetCollider.enabled = true;
        } else {
			Grounded = false;
			feetCollider.enabled = false;
		}

		RaycastHit2D hitUp = Physics2D.Raycast(CeilingCheck.transform.position, Vector2.up, dist, NoClipGroundLayer);
        if (hitUp.collider != null) {
			HeadCollider.enabled = true;
        } else {
			HeadCollider.enabled = false;
		}

		RaycastHit2D hitRight = Physics2D.Raycast(RightCheck.transform.position, Vector2.right, dist*2.5f, NoClipGroundLayer);
        if (hitRight.collider != null) {
			//Rigidbody2D.velocity = new Vector2(-1000, Rigidbody2D.velocity.y);
			bodyCollider.enabled = true;
        }

		RaycastHit2D hitLeft = Physics2D.Raycast(LeftCheck.transform.position, Vector2.left, dist*2.5f, NoClipGroundLayer);
        if (hitLeft.collider != null) {
			bodyCollider.enabled = true;
        }

		if(hitRight.collider == null && hitLeft.collider == null){
			bodyCollider.enabled = false;
		}
	}
	private void FixedUpdate(){
		/*
		Collider2D[] colliders = Physics2D.OverlapCircleAll(CheckIfGround.position, CheckIfGroundRadius, GroundLayer);
		for (int i = 0; i < colliders.Length; i++){
			if (colliders[i].gameObject != gameObject)
				Grounded = true;
		}

		//Animator.SetBool("Ground", Grounded);

		// Set the vertical animation
		//Animator.SetFloat("vSpeed", Rigidbody2D.velocity.y);
		*/
	}


	public void Move(float move, float height, bool jump){
		movo = move;
		//only control the player if grounded or airControl is turned on
		if (Grounded || moveInAir){
			// Reduce the speed if crouching by the crouchSpeed multiplier
			RaycastHit2D hit = Physics2D.Raycast(CheckIfGround.transform.position, -Vector2.up, dist, GroundLayer);
			if (hit.collider != null) {
				if(feetCollider.enabled == true){
					if(height < -0.9f){
						StartCoroutine(Vall(0.4f));
					}
				}
			}

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			PlayerAnimator.SetFloat("Speed", Mathf.Abs(move));
			if(move > 0.01f || move < -0.01f){
				Arm.GetComponent<SpriteRenderer>().enabled = false;
			} 
			if(move < 0.01f && move > -0.01f){
				this.GetComponent<SpriteRenderer>().sprite = idleSprite;
				Arm.GetComponent<SpriteRenderer>().enabled = true;
			}
			
			// Move the character
			Rigidbody2D.velocity = new Vector2(move*MaxSpeed, Rigidbody2D.velocity.y);
			
			if(Xbox_One_Controller == 1){
				//Debug.Log(Head.transform.rotation.z);
				if (FacingRight && move > 0.1f){
					//Debug.Log("Switch to Left");
					Flip();
				}
				if (!FacingRight && move < -0.1f){
					//Debug.Log("Switch to Right");
					Flip();
				}
			} else {
				if(move > 0.1 || move < -0.1){
					if (FacingRight && move > 0.1f){
						//Debug.Log("Switch to Left");
						Flip();
					}
					if (!FacingRight && move < -0.1f){
						//Debug.Log("Switch to Right");
						Flip();
					}
				} else {
					if (FacingRight && Arm.transform.rotation.z < 0.7f && Arm.transform.rotation.z > -0.7f){
						//Debug.Log("Switch to Left");
						Flip();
					}
					if (!FacingRight && Arm.transform.rotation.z < 0.7f && Arm.transform.rotation.z > -0.7f){
						//Debug.Log("Switch to Right");
						Flip();
					}
				}
			}
			/*
			//Debug.Log(Head.transform.rotation.z);
			if (FacingRight && move > 0.1f){
				//Debug.Log("Switch to Left");
				Flip();
			}
			if (!FacingRight && move < -0.1f){
				//Debug.Log("Switch to Right");
				Flip();
			}
			*/
		}
		// If the player should jump...
		if (Grounded && jump /*&& Animator.GetBool("Ground")*/){
			if(Rigidbody2D.velocity.y <= 1){
				// Add a vertical force to the player.
				Grounded = false;
				//Animator.SetBool("Ground", false);
				Rigidbody2D.AddForce(new Vector2(0f, JumpSpeed));
			}
		}
	}


	private void Flip(){
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		theScale.x *= -1;
		transform.localScale = theScale;
		Arm.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
	}

	IEnumerator Vall(float TimeForText){
		feetCollider.enabled = false;
        yield return new WaitForSeconds(TimeForText);
		feetCollider.enabled = true;
    }
}
