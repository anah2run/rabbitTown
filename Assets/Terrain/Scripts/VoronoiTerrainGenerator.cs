using UnityEngine;

public class VoronoiTerrainGenerator : MonoBehaviour
{
    public int mapSize = 129; // ������ �ػ�, 2�� �ŵ����� + 1
    public int numPoints = 10; // ���γ��� ���̾�׷��� ����� ���� ����
    public float heightScale = 20f; // ������ ���� ������

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
        // �� �����Ӹ��� numPoints ���� ����Ǿ����� Ȯ���Ͽ� ������ ������Ʈ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        float[,] heights = new float[mapSize, mapSize];

        // ���γ��� ���̾�׷��� �����Ͽ� ������ ���� ���� ����
        Vector2[] points = GeneratePoints(numPoints);

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                // �� �������� ���� ����� ���� ã�� ���̸� ����
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

        // Terrain�� ����
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
