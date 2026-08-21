using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private int columns = 16;
    [SerializeField] private int rows = 8;

    [Header("Tile")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform gridParent;

    [Header("Level Settings")]
    [SerializeField] private int diamondCount = 15;
    [SerializeField] private int lavaCount = 35;

    private readonly List<Tile> tiles = new List<Tile>();

    public int TotalTiles => columns * rows;

    public void GenerateGrid()
    {
        ClearGrid();

        List<TileType> tileTypes = CreateTileTypes();

        Shuffle(tileTypes);

        for (int i = 0; i < TotalTiles; i++)
        {
            Tile newTile = Instantiate(tilePrefab, gridParent);

            newTile.Setup(tileTypes[i]);

            tiles.Add(newTile);
        }

        // Debug.Log("Grid Generated: " + TotalTiles + " tiles");

        GameManager.Instance.SetTotalDiamonds(diamondCount);
    }

    private List<TileType> CreateTileTypes()
    {
        List<TileType> tileTypes = new List<TileType>();

        // Add diamonds
        for (int i = 0; i < diamondCount; i++)
        {
            tileTypes.Add(TileType.Diamond);
        }

        // Add lava
        for (int i = 0; i < lavaCount; i++)
        {
            tileTypes.Add(TileType.Lava);
        }

        // Remaining tiles are green
        int greenCount = TotalTiles - diamondCount - lavaCount;

        for (int i = 0; i < greenCount; i++)
        {
            tileTypes.Add(TileType.Green);
        }

        return tileTypes;
    }

    private void Shuffle(List<TileType> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            TileType temp = list[i];

            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        tiles.Clear();
    }

    public void DisableAllTiles()
    {
        foreach (Tile tile in tiles)
        {
            tile.DisableTile();
        }
    }
}