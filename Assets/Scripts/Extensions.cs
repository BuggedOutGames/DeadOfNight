using System.Linq;
using UnityEngine;

public static class Extensions {

    public static Transform FindChildByTag(this Transform parent, string tag) {
        return parent
            .Cast<Transform>()
            .FirstOrDefault(child => child.CompareTag(tag));
    }
}