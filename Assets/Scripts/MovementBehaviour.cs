using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementBehaviour : NetworkBehaviour {

    [Range(1, 10)]
    public float movementSpeed;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private float adjustedMovementSpeed;
    
    private static readonly int Walking = Animator.StringToHash("Walking");

    private void OnValidate() {
        if (GetComponentInChildren<Animator>() == null) {
            throw new ArgumentException("Missing Animator in children");
        }
    }

    private void Start() {
        animator = GetComponentInChildren<Animator>();
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
        animator.SetBool(Walking, rigidBody.velocity.magnitude > 0);
    }
}
