using UnityEngine;

public class cameraController : MonoBehaviour {

    public Transform playerTransofrm;

    void Update() {
        //gets the transform with the namespace
        //float minCameraY = -3f;
        //float posY = playerTransofrm.position.y + 2 < minCameraY? minCameraY : playerTransofrm.position.y + 2;

        transform.position = new Vector3(playerTransofrm.position.x, playerTransofrm.position.y, transform.position.z);
    }
}
