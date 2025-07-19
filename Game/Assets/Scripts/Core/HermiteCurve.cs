using UnityEngine;

public static class HermiteCurve {
    public static Vector3 HermiteCurvePoint(float t, Vector3[] points, Vector3[] tangents) {
        float t2 = t * t;
        float t3 = t2 * t;
        Vector3 p0 = points[0];
        Vector3 p1 = points[1];
        Vector3 m0 = tangents[0];
        Vector3 m1 = tangents[1];

        float h00 = 2 * t3 - 3 * t2 + 1;
        float h10 = t3 - 2 * t2 + t;
        float h01 = -2 * t3 + 3 * t2;
        float h11 = t3 - t2;

        Debug.DrawRay(p0, m0, Color.green);
        Debug.DrawRay(p1, m1, Color.green);
        return h00 * p0 + h10 * m0 + h01 * p1 + h11 * m1;
    }

    public static Vector3 HermiteTangentPoint(float t, Vector3[] points, Vector3[] tangents) {
        float t2 = t * t;
        float t3 = t2 * t;
        Vector3 p0 = points[0];
        Vector3 p1 = points[1];
        Vector3 m0 = tangents[0];
        Vector3 m1 = tangents[1];

        float h00 = 6f * t2 - 6f * t;
        float h10 = 3f * t2 - 4f * t + 1f;
        float h01 = -6f * t2 + 6f * t;
        float h11 = 3f * t2 - 2f * t;

        return h00 * p0 + h10 * m0 + h01 * p1 + h11 * m1;
    }
}