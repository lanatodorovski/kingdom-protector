using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public bool generateOnLoad = false;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0f,1f)]
    public float persistance;
    public float lacunarity;

    [Header("Seed")]
    public int seed;
    public bool randomiseSeed;
    public Vector2 offset;

    public TerrainType[] regions;

    public enum DrawMode { NoiseMap, ColorMap}
    [Header("Map Display")]
    public Tilemap mapRenderer;
    public Tilemap resourceRenderer;
    public Vector3Int centerPosition;
    public Sprite tileSpriteBasic;
    public DrawMode drawMode;

    [Header("Resource Generation")]
    public ResourceInformation[] spawnInfo;
    public float spawnFrequency = 0.4f;
    public Vector2Int borderResourceSpawnGap;
    public bool canSpawnResources = true;

    public void Start()
    {
        if (generateOnLoad) GenerateMap();
    }
    public void GenerateMap()
    {
        if (randomiseSeed) seed = UnityEngine.Random.Range(-100000, 100000);
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset) ;



        mapRenderer.ClearAllTiles();
        for (int i = resourceRenderer.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(resourceRenderer.transform.GetChild(i).gameObject);
        }
        if (drawMode == DrawMode.NoiseMap)
        {
            DrawNoiseMap(noiseMap);
        }
        else if(drawMode == DrawMode.ColorMap)
        {
            DrawTileMap(noiseMap, canSpawnResources);
        }
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
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = tileSpriteBasic;
                tile.color = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
                mapRenderer.SetTile(currentPosition, tile);

                currentPosition += Vector3Int.right;
            }
            currentPosition += Vector3Int.down;
        }       
    }

    public void DrawTileMap(float[,] noiseMap, bool spawnEnvironment)
    {
        int halfWidth = Mathf.FloorToInt(mapWidth / 2);
        int halfHeight = Mathf.FloorToInt(mapHeight / 2);
        Vector3Int startPosition = centerPosition - new Vector3Int(halfWidth, halfHeight);
        

        TileBase[] tileMap = new TileBase[mapHeight * mapWidth];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        tileMap[y * mapWidth + x] = regions[i].tile;


                        bool borderX = borderResourceSpawnGap.x <= x && borderResourceSpawnGap.x <= mapWidth - x;
                        bool borderY = borderResourceSpawnGap.y <= y && borderResourceSpawnGap.y <= mapHeight - y;
                        if (spawnEnvironment && borderX && borderY)
                        {
                            GameObject resource = getResource(regions[i].name);
                            if (resource != null) Instantiate(resource, (Vector3)startPosition + new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, resourceRenderer.transform);
                        }
                        break;
                    }
                }
            }
        }       
        
        BoundsInt bounds = new BoundsInt(startPosition, new Vector3Int(mapWidth, mapHeight, 1));

        mapRenderer.SetTilesBlock(bounds, tileMap);
    }


    public GameObject getResource(string regionName)
    {
        float randomFrequency = UnityEngine.Random.Range(0f, 1f);
        if (randomFrequency <= spawnFrequency)
        {
            float spawnStrength = UnityEngine.Random.Range(0f, 1f);
            ResourceInformation resource = Array.Find(spawnInfo, resource => {
                return resource.spawnStrength > spawnStrength && resource.terrainTypes.Contains(regionName); 
            });
            return resource.ResourceGO;
        }
        return null;
    }


    public string FindRegionName(TileBase tile)
    {
        foreach(TerrainType terrain in regions)
        {
            if(tile == terrain.tile)
            {
                return terrain.name;
            }
        }

        return null;
    }

    private void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }
    }
}

[Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public TileBase tile;
}

[Serializable]
public struct ResourceInformation
{
    public GameObject ResourceGO;
    public Vector2Int collisionSize;
    [Range(0f, 1f)]public float spawnStrength;
    public string[] terrainTypes;    
}
