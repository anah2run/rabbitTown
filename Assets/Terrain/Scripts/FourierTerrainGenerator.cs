using UnityEngine;

public class FourierTerrainGenerator : MonoBehaviour
{
    public int mapSize = 129; // ������ �ػ�, 2�� �ŵ����� + 1
    public float heightScale = 20f; // ������ ���� ������
    public float frequency = 1.0f; // ���ļ�
    public float amplitude = 1.0f; // ����

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
        // �� �����Ӹ��� frequency, amplitude ���� ����Ǿ����� Ȯ���Ͽ� ������ ������Ʈ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        float[,] heights = new float[mapSize, mapSize];

        // Fourier Transform�� ����Ͽ� ������ ���� ���� ����
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                float u = (float)x / mapSize;
                float v = (float)y / mapSize;
                float noiseValue = Mathf.PerlinNoise(u * frequency, v * frequency) * amplitude;
                heights[x, y] = noiseValue * heightScale;
            }
        }

        // Terrain�� ����
        terrainData.heightmapResolution = mapSize;
        terrainData.SetHeights(0, 0, heights);
    }
}
