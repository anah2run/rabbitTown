using UnityEngine;

public class VoronoiTerrainGenerator : MonoBehaviour
{
    public int mapSize = 129; // 지형의 해상도, 2의 거듭제곱 + 1
    public int numPoints = 10; // 보로노이 다이어그램에 사용할 점의 개수
    public float heightScale = 20f; // 지형의 높이 스케일

    private Terrain terrain;
    private TerrainData terrainData;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        GenerateTerrain();
    }

    void Update()
    {
        // 매 프레임마다 numPoints 값이 변경되었는지 확인하여 지형을 업데이트
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        float[,] heights = new float[mapSize, mapSize];

        // 보로노이 다이어그램을 생성하여 지형의 높이 맵을 설정
        Vector2[] points = GeneratePoints(numPoints);

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                // 각 지점에서 가장 가까운 점을 찾아 높이를 설정
                float minDistance = float.MaxValue;
                for (int i = 0; i < points.Length; i++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), points[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        heights[x, y] = distance / mapSize * heightScale;
                    }
                }
            }
        }

        // Terrain에 적용
        terrainData.heightmapResolution = mapSize;
        terrainData.SetHeights(0, 0, heights);
    }

    Vector2[] GeneratePoints(int numPoints)
    {
        Vector2[] points = new Vector2[numPoints];
        for (int i = 0; i < numPoints; i++)
        {
            points[i] = new Vector2(Random.Range(0, mapSize), Random.Range(0, mapSize));
        }
        return points;
    }
}
