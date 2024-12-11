using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    [Header("Map Display")]
    public Tilemap mapRenderer;
    public Vector3Int centerPosition;
    public Sprite tileSprite;
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        DrawNoiseMap(noiseMap);
    }

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        int halfWidth = Mathf.FloorToInt(width / 2);
        int halfHeight = Mathf.FloorToInt(height / 2);
        Vector3Int currentPosition = centerPosition - new Vector3Int(halfWidth, - halfHeight);
        for (int y = 0; y < height; y++)
        {
            currentPosition.x = centerPosition.x - halfWidth;
            for (int x = 0; x < width; x++)
            {
                Debug.Log(currentPosition);

                Tile colorTile = ScriptableObject.CreateInstance<Tile>();
                colorTile.sprite = tileSprite;
                colorTile.color = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
                mapRenderer.SetTile(currentPosition, colorTile);

                currentPosition += Vector3Int.right;
            }
            currentPosition += Vector3Int.down;
        }       
    }
}
