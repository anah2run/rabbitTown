using System.Drawing;
using UnityEngine;

public class DiamondSquareTerrainGenerator : MonoBehaviour
{
    public int size = 129; // 2^n + 1 형태여야 합니다. 예: 5, 9, 17, 33, 65, 129
    public float roughness = 0.5f;
    public float minHeight = 0f;
    public float maxHeight = 1f;
    private Terrain terrain;
    private TerrainData terrainData;

    private float[,] heightMap;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        GenerateTerrain();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        heightMap = new float[size, size];
        DiamondSquare();
        CreateMesh();
    }

    void DiamondSquare()
    {
        // 모서리 초기화
        heightMap[0, 0] = Random.Range(minHeight, maxHeight);
        heightMap[0, size - 1] = Random.Range(minHeight, maxHeight);
        heightMap[size - 1, 0] = Random.Range(minHeight, maxHeight);
        heightMap[size - 1, size - 1] = Random.Range(minHeight, maxHeight);

        int step = size - 1;
        float scale = 1f;

        while (step > 1)
        {
            int halfStep = step / 2;

            // Diamond 단계
            for (int y = halfStep; y < size; y += step)
            {
                for (int x = halfStep; x < size; x += step)
                {
                    DiamondStep(x, y, halfStep, scale);
                }
            }

            // Square 단계
            for (int y = 0; y < size; y += halfStep)
            {
                for (int x = (y + halfStep) % step; x < size; x += step)
                {
                    SquareStep(x, y, halfStep, scale);
                }
            }

            step /= 2;
            scale *= Mathf.Pow(2, -roughness);
        }
    }

    void DiamondStep(int x, int y, int size, float scale)
    {
        float avg = (heightMap[x - size, y - size] +
                     heightMap[x + size, y - size] +
                     heightMap[x - size, y + size] +
                     heightMap[x + size, y + size]) / 4f;

        heightMap[x, y] = avg + Random.Range(-scale, scale);
    }

    void SquareStep(int x, int y, int size, float scale)
    {
        float avg = 0f;
        int count = 0;

        if (x - size >= 0) { avg += heightMap[x - size, y]; count++; }
        if (x + size < this.size) { avg += heightMap[x + size, y]; count++; }
        if (y - size >= 0) { avg += heightMap[x, y - size]; count++; }
        if (y + size < this.size) { avg += heightMap[x, y + size]; count++; }

        avg /= count;
        heightMap[x, y] = avg + Random.Range(-scale, scale);
    }

    void CreateMesh()
    {
        int width = size;
        int height = size;

        Vector3[] vertices = new Vector3[width * height];
        int[] triangles = new int[(width - 1) * (height - 1) * 6];
        Vector2[] uv = new Vector2[width * height];

        for (int i = 0, y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++, i++)
            {
                vertices[i] = new Vector3(x, heightMap[x, y] * 10, y);
                uv[i] = new Vector2((float)x / width, (float)y / height);
            }
        }

        int tris = 0;
        for (int y = 0; y < height - 1; y++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                int i = y * width + x;

                triangles[tris++] = i;
                triangles[tris++] = i + width + 1;
                triangles[tris++] = i + width;

                triangles[tris++] = i;
                triangles[tris++] = i + 1;
                triangles[tris++] = i + width + 1;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heightMap);
    }
}