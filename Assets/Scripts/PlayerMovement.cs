using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float playerSpeed;
    public float jumpForce;
    public float coyoteTime;
    public LayerMask groundMask;
    public AudioSource jumpSound;
    public AudioSource killSound;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float coyoteTimeTimer = 0;
    private bool inGround = false;
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
        checkGround();
        if (Input.GetButtonDown("Jump") && (inGround || coyoteTimeTimer < coyoteTime)) {
            rigidBody.velocity = new Vector2(0, jumpForce);
            jumpSound.Play();
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

    private bool checkGround() {
        inGround = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .25f, groundMask);
        if (inGround) {
            coyoteTimeTimer = 0f;
        } else {
            coyoteTimeTimer += Time.deltaTime;
        }

        return inGround;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            rigidBody.velocity = new Vector2(0, 12f);
            killSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
