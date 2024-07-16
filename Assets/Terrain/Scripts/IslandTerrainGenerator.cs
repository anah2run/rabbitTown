using UnityEngine;

public class IslandTerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public float islandSize = 0.3f; // ���� ũ�� ����
    public float seaLevel = 0.2f; // �ٴ��� ���� ����
    public float islandStrength = 2f; // ���� ���� ����

    private void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrainData(terrain.terrainData);
    }

    TerrainData GenerateTerrainData(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, 50, height); // Terrain�� ���̸� 50���� ���� (���� ����)
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                // Perlin Noise �� ���
                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);

                // �� ��� �ο�
                float islandFactor = Mathf.Pow(Mathf.PerlinNoise(xCoord * islandStrength, yCoord * islandStrength), islandSize);

                // �ٴٿ� �� ��� ����
                float heightValue = Mathf.Lerp(seaLevel, perlinValue, islandFactor);

                heights[x, y] = heightValue;
            }
        }

        return heights;
    }
}
