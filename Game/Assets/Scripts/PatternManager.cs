using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoSingleton<PatternManager>
{
    [Header("Setup")]
    [SerializeField] private GameObject obstacleSpawnerPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform goal;

    [Header("Pattern Settings")]
    [SerializeField] private int numCurves = 10;
    [SerializeField] private int pointsPerCurve = 6;
    [SerializeField] private float difficulty = 0.5f; // 0 (easy) ~ 1 (hard)
    [SerializeField] private float curveWidth = 5f;
    [SerializeField] private float curveHeightMin = 3f;
    [SerializeField] private float curveHeightMax = 6f;

    private Vector2 start;
    private Vector2 end;

    void Start()
    {
        start = player.position;
        end = goal.position; 
        ApplyDifficultySettings();
        GeneratePattern();
    }
    
    void ApplyDifficultySettings()
    {
        // 난이도에 따라 값 보정
        // 예: difficulty 0.0 -> 3개, 1.0 -> 10개
        numCurves = Mathf.RoundToInt(Mathf.Lerp(6, 24, difficulty));
        curveWidth = Mathf.Lerp(3f, 10f, difficulty);
        curveHeightMax = Mathf.Lerp(curveHeightMin, 15f, difficulty);
    }

    void GeneratePattern()
{
    int seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Second * 1000;
    Random.InitState(seed);

    Camera cam = Camera.main;
    Vector3 viewMin = cam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, cam.nearClipPlane));
    Vector3 viewMax = cam.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, cam.nearClipPlane));
    Vector2 center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));

    for (int i = 0; i < numCurves; i++)
    {
        int patternType = Random.Range(0, 5); // 0~4: 총 5가지 패턴

        Vector2 randomStart = Vector2.zero;
        Vector2 randomEnd = Vector2.zero;

        switch (patternType)
        {
            case 0: // 아래 → 위
                randomStart = new Vector2(Random.Range(viewMin.x, viewMax.x), viewMin.y - 5f);
                randomEnd = new Vector2(Random.Range(viewMin.x, viewMax.x), viewMax.y + 5f);
                break;
            case 1: // 오른 → 왼
                randomStart = new Vector2(viewMax.x + 5f, Random.Range(viewMin.y, viewMax.y));
                randomEnd = new Vector2(viewMin.x - 5f, Random.Range(viewMin.y, viewMax.y));
                break;
            case 2: // 왼 → 오른
                randomStart = new Vector2(viewMin.x - 5f, Random.Range(viewMin.y, viewMax.y));
                randomEnd = new Vector2(viewMax.x + 5f, Random.Range(viewMin.y, viewMax.y));
                break;
            case 3: // 대각선
                Vector2[] corners = {
                    new Vector2(viewMin.x - 5f, viewMin.y - 5f),
                    new Vector2(viewMax.x + 5f, viewMin.y - 5f),
                    new Vector2(viewMin.x - 5f, viewMax.y + 5f),
                    new Vector2(viewMax.x + 5f, viewMax.y + 5f)
                };
                randomStart = corners[Random.Range(0, 4)];
                randomEnd = new Vector2(
                    Random.Range(viewMin.x, viewMax.x),
                    Random.Range(viewMin.y, viewMax.y)
                );
                break;
            case 4: // 중앙에서 시작 → 바깥 방향으로 나아감
                randomStart = center;
                float angle = Random.Range(0f, 2f * Mathf.PI);
                float radius = 8f;
                randomEnd = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                break;
        }

        Vector2 mustPass = new Vector2(
            Random.Range(viewMin.x, viewMax.x),
            Random.Range(viewMin.y, viewMax.y)
        );

        List<Vector3> controlPoints = new();
        controlPoints.Add(randomStart);

        for (int j = 1; j < pointsPerCurve - 1; j++)
        {
            float t = j / (float)(pointsPerCurve - 1);
            Vector2 baseVector = Vector2.Lerp(randomStart, randomEnd, t);
            float noise = Mathf.PerlinNoise(i * 0.5f, j * 0.5f);
            Vector2 offset = new Vector2(
                Mathf.Sin(t * Mathf.PI) * curveWidth * (noise - 0.5f),
                Mathf.Cos(t * Mathf.PI) * curveHeightMax * (noise - 0.5f)
            );

            Vector2 point = baseVector + offset;

            if (j == pointsPerCurve / 2)
                point = mustPass;

            controlPoints.Add(new Vector3(point.x, point.y, 0f));
        }

        controlPoints.Add(randomEnd);

        GameObject spawnerGO = Instantiate(obstacleSpawnerPrefab);
        ObstacleSpawner spawner = spawnerGO.GetComponent<ObstacleSpawner>();
        spawner.Initialize(controlPoints);
    }
}


}