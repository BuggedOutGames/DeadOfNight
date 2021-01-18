using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementBehaviour : NetworkBehaviour {

    [Range(1, 10)]
    public float movementSpeed;

    private Rigidbody2D rigidBody;
    private float adjustedMovementSpeed;
    
    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        adjustedMovementSpeed = movementSpeed * 75f;
    }

    private void FixedUpdate() {
        if (isLocalPlayer) {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            MoveTowards(new Vector2(horizontal, vertical));
        }
    }

    private void MoveTowards(Vector2 direction) {
        rigidBody.velocity = direction * (adjustedMovementSpeed * Time.deltaTime);
    }
}
