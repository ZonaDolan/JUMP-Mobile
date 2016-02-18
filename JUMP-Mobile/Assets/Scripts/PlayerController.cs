using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Enums.MoveDirection playerDirection = Enums.MoveDirection.MoveRight;
    public float speed = 3;
    public float jumpForce = 10;
    public int jumpLimit = 1;
    public float rayLength = 0.3f;
    public LayerMask collisionMask;

    private Rigidbody2D playerRigidBody;
    private Vector3 movement;
    private Vector2 raySource;
    private Animator animator;

    private bool onGround;
    private bool canJump;
    private int jumpCount;

	// Use this for initialization
	void Start () {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = 0;

        animator.SetBool("onGround", true);
        canJump = true;
	}

    void FixedUpdate() {
        MovePlayer();
    }

	// Update is called once per frame
	void Update () {
		bool jump = Input.GetKeyDown(KeyCode.W);

        if(jump && canJump && (jumpCount < jumpLimit)) {
            // TODO force jump
            playerRigidBody.AddForce(Vector2.up * jumpForce * 50);
            
            jumpCount++;
            if (jumpCount >= jumpLimit)
                canJump = false;
        }

        UpdateRaySource();
        CheckGround();
	}

    private void MovePlayer() {
        playerRigidBody.velocity = Vector2.ClampMagnitude(playerRigidBody.velocity, 10f);

        if (playerDirection == Enums.MoveDirection.MoveRight) {
            movement.x = 1f;
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            movement.x = -1f;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        transform.position += movement * speed * Time.deltaTime;
    }

    private void UpdateRaySource() {
        Bounds bound = GetComponent<BoxCollider2D>().bounds;
        raySource = new Vector2(bound.center.x, bound.min.y);
    }

    private void CheckGround() {
        RaycastHit2D hit = Physics2D.Raycast(raySource, Vector2.down, rayLength, collisionMask);
        Debug.DrawLine(raySource, raySource + (Vector2.down * rayLength), Color.green);

        if(hit) {
            animator.SetBool("onGround", true);
            if (playerRigidBody.velocity.y < 0) {
                canJump = true;
                jumpCount = 0;
            }
        }  else {
            animator.SetBool("onGround", false);
        }
    }

    public void TurnPlayer(Enums.MoveDirection direction) {
        playerDirection = direction;
    }
}
