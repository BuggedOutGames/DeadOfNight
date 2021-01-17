using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class FlashlightBehaviour : NetworkBehaviour {
    
    private Transform flashlight;
    
    private void Start() {
        flashlight = transform.Find("FlashlightAnchor");
    }

    private void Update() {
        if (isLocalPlayer) {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AimFlashlightAtPosition(mousePosition);
        }
    }

    private void AimFlashlightAtPosition(Vector3 targetPosition) {
        var direction = (transform.position - targetPosition).normalized;
        flashlight.rotation = quaternion.LookRotation(direction, Vector3.back);
        // // only rotate about the z-axis
        flashlight.eulerAngles = new Vector3(0, 0, flashlight.eulerAngles.z);
    }
}
