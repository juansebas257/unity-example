using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rigidBody;

    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Deadly")) {
            die();
        }
    }

    private void die() {
        animator.SetTrigger("playerDead");
        rigidBody.bodyType = RigidbodyType2D.Static;
        //wait()
        Invoke("restartLevel", 2);
    }

    private void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
