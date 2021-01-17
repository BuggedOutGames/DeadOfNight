using Mirror;
using UnityEngine;

public class PlayerBehaviour : NetworkBehaviour {

    
    private void Start() {
        if (isLocalPlayer) {
            var cameraMountPoint = transform.Find("CameraMountPoint");
            var cameraTransform = Camera.main.gameObject.transform;
            cameraTransform.parent = cameraMountPoint.transform;
            cameraTransform.position = cameraMountPoint.transform.position;
            cameraTransform.rotation = cameraMountPoint.transform.rotation;
        } else {
            var playerVision = transform.Find("Vision");
            playerVision.gameObject.SetActive(false);
        }
    }
}
