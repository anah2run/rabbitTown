using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TerrainSystem : MonoBehaviour
{
    public int gridWidth = 20;
    public int gridLength = 20;
    public float tileSize = 1f;
    public float minHeight = 0f;
    public float maxHeight = 5f;

    public float scale = 20f;
    [Range(0f, 1f)]
    public float islandSize = 0.3f; // 섬의 크기 비율
    public float seaLevel = 0.2f; // 바다의 높이 비율
    public float islandStrength = 2f; // 섬의 강도 조절

    private Dictionary<Vector2Int, TerrainTile> terrainTiles = new Dictionary<Vector2Int, TerrainTile>();

    [SerializeField] private GameObject tilePrefab;

    private void Update()
    {
        GenerateTerrain();
    }
    void GenerateTerrain()
    {
        var children = transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if(child != this.transform)
                Destroy(child.gameObject);
        }
        terrainTiles.Clear();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridLength; z++)
            {
                Vector2Int gridPos = new Vector2Int(x, z);
                float height = GenerateHeight(gridPos);
                Vector3 worldPos = GridToWorldPosition(gridPos, height);

                TerrainTile newTile = new TerrainTile(TerrainType.Grass, gridPos, worldPos, height);
                terrainTiles.Add(gridPos, newTile);

                CreateTileObject(newTile);
            }
        }
    }

    float GenerateHeight(Vector2Int gridPos)
    {
        // 여기에 원하는 높이 생성 알고리즘을 구현하세요.
        // 예: Perlin 노이즈를 사용한 높이 생성
        float perlinValue = Mathf.PerlinNoise((float)gridPos.x * scale, (float)gridPos.y * scale);
        // 섬 모양 부여
        float islandFactor = Mathf.Pow(Mathf.PerlinNoise(gridPos.x * islandStrength, gridPos.y * islandStrength), islandSize);
        // 바다와 섬 경계 설정
        float heightValue = Mathf.Lerp(seaLevel, perlinValue * 10, islandFactor);

        // return Mathf.Lerp(minHeight, maxHeight, perlinValue);
        return heightValue;
    }

    Vector3 GridToWorldPosition(Vector2Int gridPos, float height)
    {
        return new Vector3(gridPos.x * tileSize, height, gridPos.y * tileSize);
    }

    Vector2Int WorldToGridPosition(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / tileSize),
            Mathf.FloorToInt(worldPos.z / tileSize)
        );
    }

    void CreateTileObject(TerrainTile tile)
    {
        GameObject tileObject = Instantiate(tilePrefab, tile.worldPosition - tile.height / 2 * Vector3.up, Quaternion.identity, transform);
        tileObject.transform.localScale = new Vector3(tileSize, tile.height, tileSize);
        // 타일의 외관을 설정하는 추가 로직을 여기에 구현하세요
    }

    public bool ModifyTerrain(Vector2Int gridPos, float heightChange)
    {
        if (terrainTiles.TryGetValue(gridPos, out TerrainTile tile))
        {
            float newHeight = Mathf.Clamp(tile.height + heightChange, minHeight, maxHeight);
            tile.height = newHeight;
            tile.worldPosition = GridToWorldPosition(gridPos, newHeight);

            // 타일 게임 오브젝트 업데이트
            UpdateTileObject(tile);

            return true;
        }
        return false;
    }

    void UpdateTileObject(TerrainTile tile)
    {
        // 해당 타일의 게임 오브젝트를 찾아 높이와 위치를 업데이트합니다
        // 이 부분은 타일 오브젝트를 어떻게 관리하느냐에 따라 구현이 달라질 수 있습니다
    }
}

public class TerrainTile
{
    public TerrainType type;
    public Vector2Int gridPosition;
    public Vector3 worldPosition;
    public float height;

    public TerrainTile(TerrainType type, Vector2Int gridPos, Vector3 worldPos, float height)
    {
        this.type = type;
        this.gridPosition = gridPos;
        this.worldPosition = worldPos;
        this.height = height;
    }
}

public enum TerrainType
{
    Grass,
    Sand,
    Rock,
    Water
    // 필요한 다른 지형 유형들을 추가하세요
}