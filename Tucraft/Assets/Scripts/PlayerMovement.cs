using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    const float SPEED_FRONT = 5f, SPEED_SPRINT = 10f, SPEED_BACK = 2f, SPEED_SIDE = 3f;
    const float WEIGHT = -9.81f * 3, JUMP_HEIGHT = 1.5f, GROUND_DISTANCE = 0.35f;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded, isSprinting;
	PlayerLives livesManager;
	
	void Start() {
		livesManager = GetComponent<PlayerLives>();
	}

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, GROUND_DISTANCE, groundMask);
        isSprinting = Input.GetButton("Sprint") && (isGrounded || isSprinting);

        Move();

        if (isGrounded)  {
            float fallDamage = FallDamage();
            if (fallDamage > 0) {
                StopFalling();
                livesManager.DecreaseLives(fallDamage);
            }
            if (Input.GetButton("Jump")) {
                Jump();
            }
        }
        else {
            Fall();
        }
    }

    void Move() { // Makes the player move depending on the input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float speed = CalculateSpeed(x, z);

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Jump(float height = -1) { // Makes the player jump, if height is positive, will jump this height, if negative, will jump default height
        if (height < 0)
        {   // Normal height
            velocity.y = Mathf.Sqrt(JUMP_HEIGHT * -2f * WEIGHT);
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {   // Input height
            velocity.y = Mathf.Sqrt(height * -2f * WEIGHT);
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Fall() { // Makes the player fall
        velocity.y += WEIGHT * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    float FallDamage() { // Returns the falling damage depending on the actual velocity
        float vel = Mathf.Abs(velocity.y);
        float damage = vel / 5 - 3.5f;
        return damage < 0 ? 0 : (damage);
    }

    void StopFalling() { // Player stops falling
        velocity.y = 0;
    }

    float CalculateSpeed(float x, float z) { // Returns the speed depending on the direction that is going and if it is sprinting
        if (Mathf.Abs(x) > Mathf.Abs(z)) return SPEED_SIDE; // Left or Right
        if (z < 0) return SPEED_BACK;   // Backwards
        if (isSprinting) return SPEED_SPRINT;   // Sprinting
        return SPEED_FRONT; // Normal
    }
}
