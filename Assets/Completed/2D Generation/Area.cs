using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Area : MonoBehaviour {

  public List<Tile> tiles = new List<Tile>();
  public int maxWidth;              // maximum tiles wide the area is.
  public int maxHeight;             // maximum tiles high the area is.
  public int minWidth;              // minimum tiles wide the area is.
  public int minHeight;             // minimum tiles high the area is.    
  public int xPos;                  // The x coordinate of the lower left tile of the area.
  public int yPos;                  // The y coordinate of the lower left tile of the area.


  public Area JoinAreas(Area addedArea)
  {
    //TODO
    
    return addedArea;
  }

  protected void FillTiles()
  {
    //TODO: Attempt to fill tiles in a complete square, may need to be done more intelligently in future
    for (float x = xPos; x <= maxWidth + xPos - 1; x++)
    {
      for (float y = yPos; y <= maxHeight + yPos - 1; y++)
      {
        Tile thisTile = new Tile();
        thisTile.vectorTile = new Vector3(x, y, 0f);
        tiles.Add(thisTile);
      }
    }
  }

  //TODO: Consolidate
  public int GetHeight()
  {
    float maxY = 0f;
    float minY = tiles[0].vectorTile.y;
    foreach(Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      maxY = vectorTile.y > maxY ? vectorTile.y : maxY;//If the tiles y value is greater than maxY then set the maxY to the new max of vectorTile
      minY = vectorTile.y < minY ? vectorTile.y : minY;//If the tiles y value is less than minY then set the minY to the new min of vectorTile
    }
    return (int)(maxY - minY) + 1;//Subtract the lowest part of the room from the highest part and then +1 to offset 0 index
  }

  public int GetMaxHeight()
  {
    float maxY = 0f;
    float minY = tiles[0].vectorTile.y;
    foreach (Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      maxY = vectorTile.y > maxY ? vectorTile.y : maxY;//If the tiles y value is greater than maxY then set the maxY to the new max of vectorTile
    }
    return (int)maxY + 1;
  }

  public int GetMinHeight()
  {
    float maxY = 0f;
    float minY = tiles[0].vectorTile.y;
    foreach (Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      maxY = vectorTile.y > maxY ? vectorTile.y : maxY;//If the tiles y value is greater than maxY then set the maxY to the new max of vectorTile
    }
    return (int)maxY + 1;
  }

  public int GetWidth()
  {
    float maxX = 0f;
    float minX = tiles[0].vectorTile.x;
    foreach (Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      maxX = vectorTile.x > maxX ? vectorTile.x : maxX;//If the tiles x value is greater than maxX then set the maxX to the new max of vectorTile
      minX = vectorTile.x < minX ? vectorTile.x : minX;//If the tiles x value is less than minX then set the minX to the new min of vectorTile
    }
    return (int)(maxX - minX) +1;//Subtract the leftmost part of the room from the rightmost part and then +1 to offset 0 index
  }

  public int GetMaxWidth()
  {
    float maxX = 0f;
    float minX = tiles[0].vectorTile.x;
    foreach (Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      minX = vectorTile.x < minX ? vectorTile.x : minX;//If the tiles x value is less than minX then set the minX to the new min of vectorTile
    }
    return (int)minX + 1;//Subtract the leftmost part of the room from the rightmost part and then +1 to offset 0 index
  }

  public int GetMinWidth()
  {
    float maxX = 0f;
    float minX = tiles[0].vectorTile.x;
    foreach (Tile tile in tiles)
    {
      Vector3 vectorTile = tile.vectorTile;
      maxX = vectorTile.x > maxX ? vectorTile.x : maxX;//If the tiles x value is greater than maxX then set the maxX to the new max of vectorTile
      minX = vectorTile.x < minX ? vectorTile.x : minX;//If the tiles x value is less than minX then set the minX to the new min of vectorTile
    }
    return (int)minX + 1;//Subtract the leftmost part of the room from the rightmost part and then +1 to offset 0 index
  }


  public Tile GetCenterTile()
  {
    //Find the average of min and max and then find the center of that
    int halfWidth = ((maxWidth+minWidth)/2) / 2;
    int halfHeight = ((maxHeight+minHeight)/2) / 2;

    return GetTile(halfWidth + xPos, halfHeight + yPos);
  }

  public Tile GetTile(float x, float y)
  {
    foreach(Tile tile in tiles)
    {
      if (tile.vectorTile.x  == x && tile.vectorTile.y == y)
        return tile;
    }
    return null;
  }

  private void RecalculateArea()
  {
    maxWidth = GetMaxWidth();
    minWidth = GetMinWidth();
    maxHeight = GetMaxHeight();
    minHeight = GetMinHeight();
  }

}
