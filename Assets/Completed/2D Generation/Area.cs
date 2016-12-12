using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Area : MonoBehaviour {

  public List<Tile> tiles = new List<Tile>();
  public int width;              // maximum tiles wide the area is.
  public int height;             // maximum tiles high the area is.
  public int innerWidth;              // minimum tiles wide the area is.
  public int innerHeight;             // minimum tiles high the area is.    
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
    for (float x = xPos; x <= width + xPos - 1; x++)
    {
      for (float y = yPos; y <= height + yPos - 1; y++)
      {
        Tile thisTile = new Tile();
        thisTile.vectorTile = new Vector3(x, y, 0f);
        tiles.Add(thisTile);
      }
    }
  }


  public int GetHeight()
  {
    //float maxY = 0f;
    //float minY = tiles[0].vectorTile.y;
    //foreach (Tile tile in tiles)
    //{
    //  Vector3 vectorTile = tile.vectorTile;
    //  maxY = vectorTile.y > maxY ? vectorTile.y : maxY;//If the tiles y value is greater than maxY then set the maxY to the new max of vectorTile
    //}

    //return (int)maxY + 1;
    return (int)tiles.MaxBy(tile => tile.vectorTile.y).vectorTile.y;
  }
    
  public int GetWidth()
  {
    //float maxX = 0f;
    //float minX = tiles[0].vectorTile.x;
    //foreach (Tile tile in tiles)
    //{
    //  Vector3 vectorTile = tile.vectorTile;
    //  minX = vectorTile.x < minX ? vectorTile.x : minX;//If the tiles x value is less than minX then set the minX to the new min of vectorTile
    //}
    //return (int)minX + 1;//Subtract the leftmost part of the room from the rightmost part and then +1 to offset 0 index

    return (int)tiles.MaxBy(tile => tile.vectorTile.x).vectorTile.x;
  }

  public int GetEdgeCoord(Direction edgeToFind)
  {
    if (edgeToFind == Direction.Up)
      return yPos + height;
    else if (edgeToFind == Direction.Right)
      return xPos + width;
    else if (edgeToFind == Direction.Down)
      return yPos;
    else //If Left
      return xPos;
  }

  public int GetTopCoord() {return yPos + height;}
  public int GetRightCoord() { return xPos + width; }
  public int GetBottomCoord() { return yPos;  }
  public int GetLeftCoord() { return xPos; }

  public Direction getDirectionTo(Area destination)
  {        

    if (isUp(destination))
    {
      //If destination is thinner and in between origin in both directions
      if (!isLeft(destination) && !isRight(destination))
      {
        return Direction.Up;
      }

      //If destination is wider and in expands beyond origin in both directions
      if (!isLeft(destination) && !isRight(destination))
      {
        return Direction.Up;
      }

      if (isLeft(destination))
      {
        //destination is up and to the left of origin
        if (getDistanceLeft(destination) > getDistanceUp(destination))
          return Direction.Left;
        else
          return Direction.Up;
      }
      else if (isRight(destination))
      {
        //destination is up and to the right of origin
        if (getDistanceRight(destination) > getDistanceUp(destination))
          return Direction.Right;
        else
          return Direction.Up;
      }
      else
      {
        //destination is above origin

        
      }
    }
    else if (isDown(destination))//If destination is completely below the origin
    {
      //stuff
      if (isLeft(destination))//If destination is completely to the left of origin
      {
        //destination is down and to the left of origin


      }
      else if (isRight(destination))//If destination is completely to the right of origin
      {
        //destination is down and to the right of origin

        
      }
    }
    
    return 0;
  }
  private bool isUp(Area destination) {return destination.GetBottomCoord() > GetTopCoord();}
  private bool isLeft(Area destination) { return destination.GetRightCoord() < GetLeftCoord(); }
  private bool isRight(Area destination) { return destination.GetLeftCoord() > GetRightCoord(); }
  private bool isDown(Area destination) { return destination.GetTopCoord() < GetBottomCoord(); }

  private int getDistanceUp(Area destination) { return destination.GetBottomCoord() - GetTopCoord(); }
  private int getDistanceLeft(Area destination) { return destination.GetRightCoord() - GetLeftCoord(); }
  private int getDistanceRight(Area destination) { return  GetRightCoord() - destination.GetLeftCoord(); }
  private int getDistanceDown(Area destination) { return  GetBottomCoord() - destination.GetTopCoord(); }

  private Direction isBetweenLeftRight()
  {
    
  }

  public Direction closestDirection( Area destination)
  {
    if ()
    {

    }


      return Direction.Down;
  }




  // Enum to specify the direction is heading.
  public enum Direction
  {
    Up, Right, Down, Left,
  }

  public Tile GetCenter()
  {
    //Find the average of min and max and then find the center of that
    int halfWidth = ((width+innerWidth)/2) / 2;
    int halfHeight = ((height+innerHeight)/2) / 2;

    return GetTile(halfWidth + xPos, halfHeight + yPos);
  }

  public Tile GetTile(float x, float y)
  {
    Tile center = tiles.Find(tile => tile.vectorTile.x == x && tile.vectorTile.y == y);
    return center;
  }

  public Tile GetBottomLeft()
  {
    Tile bottomLeft = tiles.Find(tile => tile.vectorTile.x == xPos && tile.vectorTile.y == yPos );
    return bottomLeft;
  }

  public Tile GetBottomRight()
  {
    Tile bottomRight = tiles.Find(tile => (tile.vectorTile.x + width) == xPos && tile.vectorTile.y == yPos);
    return bottomRight;
  }

  public Tile GetTopLeft()
  {
    Tile bottomLeft = tiles.Find(tile => tile.vectorTile.x == xPos && (tile.vectorTile.y + height) == yPos);
    return bottomLeft;
  }

  public Tile GetTopRight()
  {
    Tile bottomRight = tiles.Find(tile => (tile.vectorTile.x + width) == xPos && (tile.vectorTile.y + height) == yPos);
    return bottomRight;
  }

  private void RecalculateArea()
  {
    width = GetMaxWidth();
    innerWidth = GetMinWidth();
    height = GetMaxHeight();
    innerHeight = GetMinHeight();
  } 

}
