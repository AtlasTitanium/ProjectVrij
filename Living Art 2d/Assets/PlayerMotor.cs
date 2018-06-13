using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
	public float MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
	public float JumpSpeed = 400f;                  // Amount of force added when the player jumps.
	[Range(0, 1)] public float CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
	public bool moveInAir = false;                 // Whether or not a player can steer while jumping;
	public LayerMask GroundLayer;                  // A mask determining what is ground to the character

	private Transform CheckIfGround;    // A position marking where to check if the player is grounded.
	const float CheckIfGroundRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool Grounded;            // Whether or not the player is grounded.
	private Transform CeilingCheck;   // A position marking where to check for ceilings
	const float CeilingCheckRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
	public Animator Animator;            // Reference to the player's animator component.
	private Rigidbody2D Rigidbody2D;
	public bool FacingRight = true; // For determining which way the player is currently facing.
	public GameObject Head; 
	private float angle;
	private Vector3 dir;
	private Vector3 theScale;

	private void Awake(){
		CheckIfGround = transform.Find("GroundCheck");
		CeilingCheck = transform.Find("CeilingCheck");
		Rigidbody2D = GetComponent<Rigidbody2D>();
		theScale = transform.localScale;
	}

	void Update(){
		dir = Input.mousePosition - Camera.main.WorldToScreenPoint(Head.transform.position);
		if(FacingRight){
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			Head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		} else {
			angle = Mathf.Atan2(dir.y, -dir.x) * Mathf.Rad2Deg;
			Head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * -1);
		}
	}
	private void FixedUpdate(){
		Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(CheckIfGround.position, CheckIfGroundRadius, GroundLayer);
		for (int i = 0; i < colliders.Length; i++){
			if (colliders[i].gameObject != gameObject)
				Grounded = true;
		}

		

		//Animator.SetBool("Ground", Grounded);

		// Set the vertical animation
		//Animator.SetFloat("vSpeed", Rigidbody2D.velocity.y);
	}


	public void Move(float move, bool crouch, bool jump){
		// If crouching, check to see if the character can stand up
		if (!crouch /*&& Animator.GetBool("Crouch")*/){
			if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingCheckRadius, GroundLayer)){
				crouch = true;
			}
		}

		// Set whether or not the character is crouching in the animator
		//Animator.SetBool("Crouch", crouch);

		//only control the player if grounded or airControl is turned on
		if (Grounded || moveInAir){
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move*CrouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			//Animator.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			Rigidbody2D.velocity = new Vector2(move*MaxSpeed, Rigidbody2D.velocity.y);
			
			//Debug.Log(Head.transform.rotation.z);
			if (FacingRight && Head.transform.rotation.z < 0.7f && Head.transform.rotation.z > -0.7f){
				//Debug.Log("Switch to Left");
				Flip();
			}
			if (!FacingRight && Head.transform.rotation.z < 0.7f && Head.transform.rotation.z > -0.7f){
				//Debug.Log("Switch to Right");
				Flip();
			}
		}
		// If the player should jump...
		if (Grounded && jump /*&& Animator.GetBool("Ground")*/){
			// Add a vertical force to the player.
			Grounded = false;
			//Animator.SetBool("Ground", false);
			Rigidbody2D.AddForce(new Vector2(0f, JumpSpeed));
		}
	}


	private void Flip(){
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		theScale.x *= -1;
		transform.localScale = theScale;
		Head.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
	}
}
