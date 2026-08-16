using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tilesData;
    private readonly Vector3Int[] forwardDirections =
    {
        Vector3Int.right,   // East
        Vector3Int.left,    // West
        Vector3Int.up,      // North
        Vector3Int.down     // South
    };

    private Dictionary<TileBase, TileData> dataFromTiles;

    protected override void Awake()
    {
        base.Awake();
        dataFromTiles = new Dictionary<TileBase, TileData>();
        
        foreach(var tileData in tilesData)
        {
            foreach(var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    void Update()
    {

    }

    public List<Vector3Int> GetWalkableCells(Vector3 worldPos, int radius)
    {
        List<Vector3Int> result = new List<Vector3Int>();

        Vector3Int centerCell = map.WorldToCell(worldPos);

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector3Int cell = centerCell + new Vector3Int(x, y, 0);

                if (map.HasTile(cell))
                {
                    if (IsWalkable(cell))
                    {
                        result.Add(cell);
                    }
                }
            }
        }

        return result;
    }


    public bool IsWalkable(Vector3Int gridPos)
    {
        TileBase tile = map.GetTile(gridPos);

        if (tile == null)
            return false;

        if (!dataFromTiles.TryGetValue(tile, out TileData data))
            return false;

        return data.isWalkable;
    }

    public List<Vector3Int> FindPatrolCells(Vector3 worldPos, int allyCount, int radius)
    {
        Vector3Int towerCell = map.WorldToCell(worldPos);

        foreach (Vector3Int forward in forwardDirections)
        {
            List<Vector3Int> result = CheckDirection(towerCell, forward, allyCount, radius);

            if (result != null)
                return result;
        }

        return null;
    }

    private List<Vector3Int> CheckDirection(Vector3Int towerCell, Vector3Int forward, int allyCount, int radius)
    {
        Vector3Int right = GetRight(forward);

        int start = -(allyCount / 2);

        for (int distance = 1; distance <= radius; distance++)
        {
            Vector3Int center = towerCell + forward * distance;

            List<Vector3Int> cells = new List<Vector3Int>();

            bool valid = true;

            for (int i = 0; i < allyCount; i++)
            {
                Vector3Int cell = center + right * (start + i);

                if (!IsWalkable(cell))
                {
                    valid = false;
                    break;
                }

                cells.Add(cell);
            }
            if (valid)
            {
                return cells;
            }
        }
        return null;
    }

    private Vector3Int GetRight(Vector3Int forward)
    {
        if (forward == Vector3Int.right)
            return Vector3Int.up;

        if (forward == Vector3Int.left)
            return Vector3Int.down;

        if (forward == Vector3Int.up)
            return Vector3Int.right;

        return Vector3Int.left;
    }

    public Vector3 GetCellCenterWorld(Vector3Int cell)
    {
        return map.GetCellCenterWorld(cell);
    }
}
