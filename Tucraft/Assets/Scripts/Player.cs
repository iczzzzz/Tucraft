using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	
	public float speedF = 5f, speedSprint = 10f, speedB = 2f, speedS = 3f;
	public float gravity = -9.81f * 2;
	public float jumpHeight = 1.5f;
	public float groundDistance = 0.3f;
	
	public CharacterController controller;
	public Transform groundCheck;
	public LayerMask groundMask;
	
	Vector3 velocity;
	bool isGrounded, isSprinting;
	float lives = 10f;

    // Update is called once per frame
    void Update()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		isSprinting = Input.GetButton("Sprint") && (isGrounded || isSprinting);
		
		Move();
		
		if (isGrounded) {
			float fallDamage = FallDamage();
			if (fallDamage > 0) {
				StopFalling();
				Hit(fallDamage);
			}
			if (Input.GetButton("Jump")) {
				Jump();
			}
		}
		else {
			Fall();
		}
    }
	
	float FallDamage() {
		float vel = Mathf.Abs(velocity.y);
		float damage = vel/5 - 3.5f;
		return damage<0 ? 0 : (damage);
	}
	
	void StopFalling() {
		velocity.y = 0;
	}
	
	float RoundPointFive(float n) {
		return ((float)Mathf.Round(n*2)) / 2.0f;
	}
	
	public void Hit(float damage = 1) {
		lives -= RoundPointFive(damage);
		HeartsManager.instance.SetNHearts(lives);
		Debug.Log("Lives: " + lives);
		if (lives <= 0) {
			lives = 0;
			Die();
		}
	}
	
	void Die() {
		//Debug.Log("Dead");
	}
	
	void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
		float speed = CalculateSpeed(x, z);
		
		Vector3 move = transform.right * x + transform.forward * z;
		controller.Move(move * speed * Time.deltaTime);
	}
		
	
	void Jump(float height = -1) {
		if (height < 0) {	// Normal height
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
			controller.Move(velocity * Time.deltaTime);
		}
		else {	// Input height
			velocity.y = Mathf.Sqrt(height * -2f * gravity);
			controller.Move(velocity * Time.deltaTime);
		}
	}
	
	void Fall() {
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
	
	float CalculateSpeed(float x, float z) {
		if (Mathf.Abs(x) > Mathf.Abs(z)) return speedS;	// Left or Right
		if (z < 0) return speedB;	// Backwards
		if (isSprinting) return speedSprint;	// Sprinting
		return speedF;	// Normal
	}
}
