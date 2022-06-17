
using UnityEngine;

public static class ExtUtil {

    public static Vector2 ToVector2(this Vector3 vector) {
        return new Vector2(vector.x, vector.y);
    }
}