using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float playerSpeed;
    public float jumpForce;
    public LayerMask groundMask;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private enum AnimationState { idle, running, jumping, falling };

    // Start is called before the first frame update
    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update() {
        movementLogic();
        jumpLogic();
    }

    private void jumpLogic() {
        if (Input.GetButtonDown("Jump") && isGrounded()) {
            rigidBody.velocity = new Vector2(0, jumpForce);
        }
    }
    private void movementLogic() {
        //float axisX = Input.GetAxis("Horizontal"); // With acceleration
        float axisX = Input.GetAxisRaw("Horizontal"); // Without acceleration
        rigidBody.velocity = new Vector2(axisX * playerSpeed, rigidBody.velocity.y);

        // check sprite animation
        AnimationState animationState;
        if(axisX != 0f) {
            animationState = AnimationState.running;
        } else {
            animationState = AnimationState.idle;
        }

        if(rigidBody.velocity.y > 0.05) {
            animationState = AnimationState.jumping;
        } else if(rigidBody.velocity.y < -0.1) {
            animationState = AnimationState.falling;
        }

        animator.SetInteger("animationState", (int)animationState);

        // check sprite direction
        if (axisX > 0f) {
            spriteRenderer.flipX = false;
        } else if(axisX < 0f) {
            spriteRenderer.flipX = true;
        }
    }

    private bool isGrounded() {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundMask);
    }
}
