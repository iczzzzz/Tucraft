using UnityEngine;

public class Player : MonoBehaviour
{

    const float SPEED_FRONT = 5f, SPEED_SPRINT = 10f, SPEED_BACK = 2f, SPEED_SIDE = 3f;
    const float WEIGHT = -9.81f * 3, JUMP_HEIGHT = 1.5f, GROUND_DISTANCE = 0.35f;
    const float MAX_LIVES = 10f;
    const float INTERACT_RADIUS = 5f;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded, isSprinting;
    float lives;
    Camera cam;


    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        lives = MAX_LIVES;
        HeartsManager.instance.SetNHearts(lives, false);
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
                Hit(fallDamage);
            }
            if (Input.GetButton("Jump")) {
                Jump();
            }
        }
        else {
            Fall();
        }

        Interactable interactable;
        if (RaycastWithDistance(out interactable) && interactable != null) {
            if (Input.GetButtonDown("Interact")) {
                interactable.Interact();
            }
            else {
                interactable.SetHighlighted(interactable);
            }

        }

        if (Input.GetButtonDown("Action")) {
            DoAction();
        }

    }

    bool RaycastWithDistance(out Interactable inter)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (Vector3.Distance(transform.position, hit.point) <= INTERACT_RADIUS)
            {
                inter = hit.collider.GetComponent<Interactable>();
                return true;
            }
        }

        inter = null;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, INTERACT_RADIUS);
    }

    void DoAction()
    {
        Inventory.instance.UseSelected();
    }



    /*****   MOVEMENT   *****/ 

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float speed = CalculateSpeed(x, z);

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Jump(float height = -1)
    {
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

    void Fall()
    {
        velocity.y += WEIGHT * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    float FallDamage()
    {
        float vel = Mathf.Abs(velocity.y);
        float damage = vel / 5 - 3.5f;
        return damage < 0 ? 0 : (damage);
    }

    void StopFalling()
    {
        velocity.y = 0;
    }




    /*****   LIVES   *****/

    void Eat(float kal)
    {
        float prevLives = lives;
        lives = Mathf.Clamp(lives + RoundPointFive(kal), 0f, MAX_LIVES);
        if (prevLives < MAX_LIVES) {
            HeartsManager.instance.SetNHearts(lives, true);
        }
        //Debug.Log("Lives: " + lives);
    }

    void Hit(float damage)
    {
        float prevLives = lives;
        lives = Mathf.Clamp(lives - RoundPointFive(damage), 0f, MAX_LIVES);
        if (prevLives > 0f) {
            HeartsManager.instance.SetNHearts(lives, true);
            if (lives <= 0) {
                Die();
            }
        }
        //Debug.Log("Lives: " + lives);
    }

    void Die()
    {
        Debug.Log("Dead");
    }




    /*****   OTHERS   *****/

    float CalculateSpeed(float x, float z)
    {
        if (Mathf.Abs(x) > Mathf.Abs(z)) return SPEED_SIDE; // Left or Right
        if (z < 0) return SPEED_BACK;   // Backwards
        if (isSprinting) return SPEED_SPRINT;   // Sprinting
        return SPEED_FRONT; // Normal
    }

    float RoundPointFive(float n)
    {
        return ((float)Mathf.Round(n * 2)) / 2.0f;
    }
}
