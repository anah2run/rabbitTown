using UnityEngine;

public class IslandTerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public float islandSize = 0.3f; // 섬의 크기 비율
    public float seaLevel = 0.2f; // 바다의 높이 비율
    public float islandStrength = 2f; // 섬의 강도 조절

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
        terrainData.size = new Vector3(width, 50, height); // Terrain의 높이를 50으로 설정 (조정 가능)
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

                // Perlin Noise 값 계산
                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);

                // 섬 모양 부여
                float islandFactor = Mathf.Pow(Mathf.PerlinNoise(xCoord * islandStrength, yCoord * islandStrength), islandSize);

                // 바다와 섬 경계 설정
                float heightValue = Mathf.Lerp(seaLevel, perlinValue, islandFactor);

                heights[x, y] = heightValue;
            }
        }

        return heights;
    }
}
