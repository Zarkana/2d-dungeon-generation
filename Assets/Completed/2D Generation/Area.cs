using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Area : MonoBehaviour {

  public List<Tile> tiles = new List<Tile>();

  public Area joinAreas(Area addedArea)
  {
    //TODO
    return addedArea;
  }

  public float getHeight()
  {
    float maxY = 0f;
    float minY = 0f;
    foreach(Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      maxY = vectorTile.y > maxY ? vectorTile.y : maxY;//If the tiles y value is greater than maxY then set the maxY to the new max of vectorTile
      minY = vectorTile.y < minY ? vectorTile.y : minY;//If the tiles y value is less than minY then set the minY to the new min of vectorTile
    }
    return maxY - minY;
  }

  public float getWidth()
  {
    float maxX = 0f;
    float minX = 0f;
    foreach (Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      maxX = vectorTile.x > maxX ? vectorTile.x : maxX;//If the tiles x value is greater than maxX then set the maxX to the new max of vectorTile
      minX = vectorTile.x < minX ? vectorTile.x : minX;//If the tiles x value is less than minX then set the minX to the new min of vectorTile
    }
    return maxX - minX;
  }
}
