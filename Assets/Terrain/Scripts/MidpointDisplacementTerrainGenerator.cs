using UnityEngine;

public class MidpointDisplacementTerrainGenerator : MonoBehaviour
{
    public int size = 513; // 2^n + 1 형태여야 합니다. 예: 33, 65, 129, 257, 513
    public float roughness = 0.6f;
    public float minHeight = 0f;
    public float maxHeight = 100f;

    private Terrain terrain;
    private TerrainData terrainData;

    void Start()
    {
        InitializeTerrain();
        GenerateTerrain();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }

    void InitializeTerrain()
    {
        terrain = GetComponent<Terrain>();
        if (terrain == null)
        {
            terrain = gameObject.AddComponent<Terrain>();
        }

        terrainData = new TerrainData();
        terrainData.heightmapResolution = size;
        terrainData.size = new Vector3(size, maxHeight, size);
        terrain.terrainData = terrainData;
    }

    void GenerateTerrain()
    {
        float[,] heightMap = new float[size, size];

        // 초기 코너 높이 설정
        heightMap[0, 0] = Random.Range(minHeight, maxHeight);
        heightMap[0, size - 1] = Random.Range(minHeight, maxHeight);
        heightMap[size - 1, 0] = Random.Range(minHeight, maxHeight);
        heightMap[size - 1, size - 1] = Random.Range(minHeight, maxHeight);

        MidpointDisplacement(heightMap, 0, 0, size - 1, size - 1, roughness);

        // 높이맵을 0-1 범위로 정규화
        NormalizeHeightMap(heightMap);

        // 지형 데이터에 높이맵 적용
        terrainData.SetHeights(0, 0, heightMap);
    }

    void MidpointDisplacement(float[,] heightMap, int x1, int y1, int x2, int y2, float roughness)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;

        if (dx <= 1 && dy <= 1) return;

        int midX = (x1 + x2) / 2;
        int midY = (y1 + y2) / 2;

        // 중점 계산
        float avgHeight = (heightMap[x1, y1] + heightMap[x1, y2] + heightMap[x2, y1] + heightMap[x2, y2]) / 4f;
        float displacement = Random.Range(-roughness, roughness) * dx;

        heightMap[midX, midY] = avgHeight + displacement;

        // 변의 중점 계산
        if (heightMap[x1, midY] == 0) heightMap[x1, midY] = (heightMap[x1, y1] + heightMap[x1, y2]) / 2f + Random.Range(-roughness, roughness) * dx;
        if (heightMap[x2, midY] == 0) heightMap[x2, midY] = (heightMap[x2, y1] + heightMap[x2, y2]) / 2f + Random.Range(-roughness, roughness) * dx;
        if (heightMap[midX, y1] == 0) heightMap[midX, y1] = (heightMap[x1, y1] + heightMap[x2, y1]) / 2f + Random.Range(-roughness, roughness) * dx;
        if (heightMap[midX, y2] == 0) heightMap[midX, y2] = (heightMap[x1, y2] + heightMap[x2, y2]) / 2f + Random.Range(-roughness, roughness) * dx;

        // 재귀 호출
        MidpointDisplacement(heightMap, x1, y1, midX, midY, roughness);
        MidpointDisplacement(heightMap, midX, y1, x2, midY, roughness);
        MidpointDisplacement(heightMap, x1, midY, midX, y2, roughness);
        MidpointDisplacement(heightMap, midX, midY, x2, y2, roughness);
    }

    void NormalizeHeightMap(float[,] heightMap)
    {
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        // 최소값과 최대값 찾기
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                minValue = Mathf.Min(minValue, heightMap[x, y]);
                maxValue = Mathf.Max(maxValue, heightMap[x, y]);
            }
        }

        // 정규화
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                heightMap[x, y] = (heightMap[x, y] - minValue) / (maxValue - minValue);
            }
        }
    }
}