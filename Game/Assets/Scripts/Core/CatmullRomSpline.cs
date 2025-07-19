using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class CatmullRomSpline : MonoBehaviour
{
    public Transform pointsParent;
    private List<Vector3[]> piecewiseHermiteCurves = new List<Vector3[]>();
    private float ellipsedTime;
    public float randomSpeedMinValue = 1.0f;
    public float randomSpeedMaxValue = 8.0f;
    public float speedWeight = 1.0f;

    private LineRenderer lineRenderer;
    private List<float> curveSpeeds = new List<float>();
    int resolution = 200;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InitializeCatmullRomSplineData(); // 곡선 데이터 초기화
        DrawCatmullRomSplinePath(); // 디버그용
    }

    private void Update()
    {
        UpdateCurve();
    }

    private void UpdateCurve()
    {
        ellipsedTime += Time.deltaTime;

        float totalDuration = 0f;
        foreach (var speed in curveSpeeds)
            totalDuration += speed;

        float time = ellipsedTime % totalDuration;

        int curveIndex = 0;
        float accumTime = 0f;
        while (curveIndex < curveSpeeds.Count && accumTime + curveSpeeds[curveIndex] < time)
        {
            accumTime += curveSpeeds[curveIndex];
            curveIndex++;
        }

        if (curveIndex >= piecewiseHermiteCurves.Count)
            return;

        float localT = (time - accumTime) / curveSpeeds[curveIndex];

        Vector3[] curve = piecewiseHermiteCurves[curveIndex];
        Vector3[] curvePoints = new Vector3[] { curve[0], curve[1] };
        Vector3[] curveTangents = new Vector3[] { curve[2], curve[3] };

        transform.position = HermiteCurve.HermiteCurvePoint(localT, curvePoints, curveTangents);

        Vector3 tangent = HermiteCurve.HermiteTangentPoint(localT, curvePoints, curveTangents).normalized;
        float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void InitializeCatmullRomSplineData()
    {
        piecewiseHermiteCurves.Clear();
        curveSpeeds.Clear();

        int count = pointsParent.childCount;
        if (count < 2)
        {
            Debug.LogError("2개 이상의 점이 필요합니다.");
            return;
        }

        Vector3[] tangents = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
                tangents[i] = pointsParent.GetChild(i + 1).position - pointsParent.GetChild(i).position;
            else if (i == count - 1)
                tangents[i] = pointsParent.GetChild(i).position - pointsParent.GetChild(i - 1).position;
            else
                tangents[i] = (pointsParent.GetChild(i + 1).position - pointsParent.GetChild(i - 1).position) / 2f;
        }

        for (int i = 0; i < count - 1; i++)
        {
            Vector3[] curve = new Vector3[4];
            curve[0] = pointsParent.GetChild(i).position;
            curve[1] = pointsParent.GetChild(i + 1).position;
            curve[2] = tangents[i];
            curve[3] = tangents[i + 1];
            piecewiseHermiteCurves.Add(curve);

            float randomSpeed = Random.Range(randomSpeedMinValue, randomSpeedMaxValue);
            curveSpeeds.Add(randomSpeed * speedWeight);
        }
    }

    private void DrawCatmullRomSplinePath()
    {
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        List<Vector3> curvePoints = new List<Vector3>();

        foreach (var curve in piecewiseHermiteCurves)
        {
            Vector3[] pts = new Vector3[] { curve[0], curve[1] };
            Vector3[] tangents = new Vector3[] { curve[2], curve[3] };

            for (int i = 0; i < resolution; i++)
            {
                float t = i / (float)(resolution - 1);
                curvePoints.Add(HermiteCurve.HermiteCurvePoint(t, pts, tangents));
            }
        }

        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }
}
