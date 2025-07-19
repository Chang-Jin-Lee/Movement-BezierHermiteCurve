using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject splinePrefab;         // CatmullRomSpline이 붙은 프리팹
    public void Initialize(List<Vector3> controlPoints)
    {
        GameObject pointsParent = new GameObject("Points");
        List<Transform> pointTransforms = new();

        for (int i = 0; i < controlPoints.Count; i++)
        {
            GameObject p = new GameObject($"p{i}");
            p.transform.position = controlPoints[i];
            p.transform.parent = pointsParent.transform;
            pointTransforms.Add(p.transform);
        }

        GameObject splineObj = Instantiate(splinePrefab);
        splineObj.name = "MovingSpline";
        splineObj.transform.position = pointTransforms[0].position;

        CatmullRomSpline spline = splineObj.GetComponent<CatmullRomSpline>();
        spline.pointsParent = pointsParent.transform;

        // 움직임 연결 등은 spline 내부에서 처리
    }
    
}