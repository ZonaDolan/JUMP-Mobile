using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Enums.MoveDirection playerDirection = Enums.MoveDirection.MoveRight;
    public float speed = 3;
    public float jumpForce = 10;
    public float rayLength = 0.3f;
    public LayerMask collisionMask;

    private Rigidbody2D playerRigidBody;
    private Vector3 movement;
    private Vector2 raySource;
    private Animator animator;

    private bool onGround;
    private bool canJump;

	// Use this for initialization
	void Start () {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

    void FixedUpdate() {
        MovePlayer();
    }

	// Update is called once per frame
	void Update () {
        bool jump = Input.GetKeyDown(KeyCode.Space);

        if(jump && canJump) {
            // TODO force jump
            playerRigidBody.AddForce(Vector2.up * jumpForce * 50);
            canJump = false;
        }

        UpdateRaySource();
        CheckGround();
	}

    private void MovePlayer() {
        if(playerDirection == Enums.MoveDirection.MoveRight) {
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
            if(playerRigidBody.velocity.y < 0)
                canJump = true;
        }  else {
            animator.SetBool("onGround", false);
        }
    }

    public void TurnPlayer(Enums.MoveDirection direction) {
        playerDirection = direction;
    }
}
