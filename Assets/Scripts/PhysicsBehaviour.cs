﻿using UnityEngine;

public class PhysicsBehaviour : MonoBehaviour {

    private const string PlayerLayer = "Meeples";
    
    private void Start() {
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer(PlayerLayer),
            LayerMask.NameToLayer(PlayerLayer)
        );
    }
}
