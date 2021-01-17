using UnityEngine;

public static class Utils {
    public static RaycastHit2D CastRayFromScreenToWorld(Camera camera, Vector2 screenPosition) {
        var worldPosition = camera.ScreenToWorldPoint(screenPosition);
        return Physics2D.Raycast(worldPosition, Vector2.zero);
    }

    public static Vector2 GetPositionInWorld(Camera camera, Vector2 screenPosition) {
        var worldRayCast = CastRayFromScreenToWorld(camera, screenPosition);
        return worldRayCast.point;
    }
}
