using UnityEngine;
using System.Collections;



public class FPController : MonoBehaviour {

	public Rigidbody player;
	public CapsuleCollider playerColl;

	public float speed = 10.0f;
	public float sprintSpeed = 2f;
	public float strafeSpeed = 0.75f;
	public float backSpeed = 0.5f;
	public float crouchSpeed = 0.5f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public bool grounded = false;
	public int maxSlope = 60;

	public bool crouch;

	void Awake () {
		player = GetComponent<Rigidbody> ();
		player.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		playerColl = GetComponent<CapsuleCollider> ();
		player.freezeRotation = true;
		player.useGravity = false;
		crouch = false;
	}

	void FixedUpdate () { 

		if (grounded) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			targetVelocity = transform.TransformDirection (targetVelocity);
			if (Input.GetKey ("left shift") && crouch == false) {
			
				targetVelocity *= speed;
				targetVelocity *= sprintSpeed;

			} else if (Input.GetKeyDown ("left ctrl") && crouch == false) {

				playerColl.height *= 0.5f;
			} else {
			
				targetVelocity *= speed;
				
			}
			if (Input.GetKeyUp ("left ctrl") && crouch == true) {
			
				playerColl.height *= 2f;
				crouch = false;

			}
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = player.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			player.AddForce (velocityChange, ForceMode.VelocityChange);

			// Jump
			if (canJump && Input.GetButton ("Jump")) {
				if (Input.GetKey ("left shift")) {
					player.velocity = new Vector3 (velocity.x, CalculateJumpSprintSpeed (), velocity.z);
				} else {
					player.velocity = new Vector3 (velocity.x, CalculateJumpVerticalSpeed (), velocity.z);
				}
			}
		} else {
			
		}

		// We apply gravity manually for more tuning control
		player.AddForce(new Vector3 (0, -gravity * player.mass, 0));

		grounded = false;
	}

	void OnCollisionStay (Collision collision) {
		foreach(ContactPoint contact in collision.contacts){

			if (Vector3.Angle (contact.normal, Vector3.up) < maxSlope) {
			
				grounded = true;

			}

		}
	}

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	float CalculateJumpSprintSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(4 * jumpHeight * gravity);
	}
}