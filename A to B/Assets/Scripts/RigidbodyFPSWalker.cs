﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class RigidbodyFPSWalker : MonoBehaviour {

	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public bool grounded = false;
	public bool airMovement;
	public float shiftMultiplier;
	public Rigidbody rigid;


	void Awake () {
		rigid = GetComponent<Rigidbody> ();
		rigid.freezeRotation = true;
		rigid.useGravity = false;
	}

	void FixedUpdate () {
		if (grounded || (!grounded && airMovement)) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			if (Input.GetKey (KeyCode.LeftShift)) {
				targetVelocity *= speed * shiftMultiplier;
			} else {
				targetVelocity *= speed;
			}

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigid.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigid.AddForce(velocityChange, ForceMode.VelocityChange);

			// Jump
			if (canJump && Input.GetButton("Jump")) {
				rigid.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				canJump = false;
			}
		}

		// We apply gravity manually for more tuning control
		rigid.AddForce(new Vector3 (0, -gravity * rigid.mass, 0));

		grounded = false;
	}

	void OnCollisionStay (Collision collision) {
		Vector3 normal = collision.contacts [0].normal;
		Vector3 vel = rigid.velocity;
		if (Vector3.Angle (vel, normal) > 60) {
			grounded = true;  
			canJump = true;
		}
		else 
		{
			rigid.velocity = new Vector3 (rigid.velocity.x * -0.5f, rigid.velocity.y, rigid.velocity.z * -0.5f);
		}

	}

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		if (Input.GetKey(KeyCode.LeftShift)) {
			return (Mathf.Sqrt (2 * jumpHeight * gravity * shiftMultiplier));
		} else {
			return Mathf.Sqrt (2 * jumpHeight * gravity);
		}
	}
}