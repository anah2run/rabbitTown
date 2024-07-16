using UnityEngine;
using System.Collections.Generic;

public class TileSystem : MonoBehaviour
{
    public int gridWidth = 20;
    public int gridHeight = 20;
    public float tileSize = 1f;

    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector2Int gridPos = new Vector2Int(x, z);
                Vector3 worldPos = GridToWorldPosition(gridPos);
                Tile newTile = new Tile(TileType.Empty, gridPos, worldPos);
                tiles.Add(gridPos, newTile);
            }
        }
    }

    public Vector3 GridToWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * tileSize, 0, gridPos.y * tileSize);
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / tileSize),
            Mathf.FloorToInt(worldPos.z / tileSize)
        );
    }

    public bool PlaceBuilding(Vector2Int gridPos, TileType buildingType)
    {
        if (tiles.TryGetValue(gridPos, out Tile tile))
        {
            if (tile.type == TileType.Empty)
            {
                tile.type = buildingType;
                // 여기에 실제 건물 게임 오브젝트를 생성하는 코드를 추가하세요
                return true;
            }
        }
        return false;
    }
}

public class Tile
{
    public TileType type;
    public Vector2Int gridPosition;
    public Vector3 worldPosition;

    public Tile(TileType type, Vector2Int gridPos, Vector3 worldPos)
    {
        this.type = type;
        this.gridPosition = gridPos;
        this.worldPosition = worldPos;
    }
}

public enum TileType
{
    Empty,
    Residential,
    Commercial,
    Industrial,
    // 필요한 다른 타일 유형들을 추가하세요
}