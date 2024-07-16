using UnityEngine;

public class FourierTerrainGenerator : MonoBehaviour
{
    public int mapSize = 129; // 지형의 해상도, 2의 거듭제곱 + 1
    public float heightScale = 20f; // 지형의 높이 스케일
    public float frequency = 1.0f; // 주파수
    public float amplitude = 1.0f; // 진폭

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
        // 매 프레임마다 frequency, amplitude 값이 변경되었는지 확인하여 지형을 업데이트
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        float[,] heights = new float[mapSize, mapSize];

        // Fourier Transform을 사용하여 지형의 높이 맵을 생성
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

        // Terrain에 적용
        terrainData.heightmapResolution = mapSize;
        terrainData.SetHeights(0, 0, heights);
    }
}
