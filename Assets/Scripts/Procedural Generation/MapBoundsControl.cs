using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundsControl : MonoBehaviour
{
    [Header("Camera Border")]
    public Vector2 cameraBorderOffset;
    public Vector2 playerBoundsOffset;

    private MapGenerator mapGen;

    public void SetBounds()
    {
        mapGen = gameObject.GetComponent<MapGenerator>();

        SetCameraBounds();
        SetPlayerBounds();
    }
    public void SetCameraBounds()
    {
        PolygonCollider2D cmConfinerCollider = mapGen.GetComponentInChildren<PolygonCollider2D>();
        List<Vector2> points = GetPoints(mapGen.mapWidth, mapGen.mapHeight, cameraBorderOffset);
        cmConfinerCollider.SetPath(0, points);
    }
    public void SetPlayerBounds()
    {
        EdgeCollider2D playerBoundsCollider = mapGen.GetComponentInChildren<EdgeCollider2D>();

        List<Vector2> points = GetPoints(mapGen.mapWidth, mapGen.mapHeight, playerBoundsOffset);
        points.Add(points[0]);
        playerBoundsCollider.SetPoints(points);
    }
    private List<Vector2> GetPoints(int mapWidth, int mapHeight,  Vector2 boundsOffset)
    {
        float halfWidth = mapWidth / 2 - boundsOffset.x;
        float halfHeight = mapHeight / 2 - boundsOffset.y;
        List<Vector2> points = new List<Vector2>{
                new Vector2(halfWidth, -halfHeight),
                new Vector2(halfWidth, halfHeight),
                new Vector2(-halfWidth, halfHeight),
                new Vector2(-halfWidth, -halfHeight)
            };

        return points;
    }
}
