using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerItemCollector : MonoBehaviour {

    public Text orangesText;
    public Text finishText;
    public AudioSource collectSound;
    public AudioSource finishSound;

    private int orangeCounter = 0;

    private void Update() {
        if (Input.GetButtonDown("Jump") && orangeCounter == 4) {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Orange")) {
            orangeCounter ++;
            collectSound.Play();
            Destroy(collision.gameObject);
            orangesText.text = "Oranges: " + orangeCounter + "/4";

            if(orangeCounter == 4) {
                finishSound.Play();
                finishText.text = "You did it!\nThanks for Playing";
                Time.timeScale = 0;
            }
        }
    }
}
