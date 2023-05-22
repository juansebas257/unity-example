using UnityEngine;
using UnityEngine.UI;

public class PlayerItemCollector : MonoBehaviour {

    public Text orangesText;

    private int orangeCounter = 0;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Orange")) {
            orangeCounter ++;
            Destroy(collision.gameObject);
            orangesText.text = "Oranges: " + orangeCounter;
        }
    }
}
