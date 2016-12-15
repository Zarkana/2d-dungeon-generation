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

  //Returns the quickest direction 
  //public Direction getDirectionTo(Area destination)
  //{            

  //  return 0;
  //}
  public bool isUp(Area destination) {return destination.GetBottomCoord() > GetTopCoord();}
  public bool isLeft(Area destination) { return destination.GetRightCoord() < GetLeftCoord(); }
  public bool isRight(Area destination) { return destination.GetLeftCoord() > GetRightCoord(); }
  public bool isDown(Area destination) { return destination.GetTopCoord() < GetBottomCoord(); }

  //private int getDistanceUp(Area destination) { return destination.GetBottomCoord() - GetTopCoord(); }
  //private int getDistanceLeft(Area destination) { return destination.GetRightCoord() - GetLeftCoord(); }
  //private int getDistanceRight(Area destination) { return  GetRightCoord() - destination.GetLeftCoord(); }
  //private int getDistanceDown(Area destination) { return  GetBottomCoord() - destination.GetTopCoord(); }

  //private bool isWithinLR(Area destination){ return !isLeft(destination) && !isRight(destination); }
  //private bool isWithinUD(Area destination) { return !isUp(destination) && !isDown(destination); }
  //private bool isOutsideLR(Area destination) { return isLeft(destination) && isRight(destination); }
  //private bool isOutsideUD(Area destination) { return isLeft(destination) && isRight(destination); }


  //Finds the closest direction from the middle of an area to the middle of another area
  public Direction closestDirection( Area destination)
  {
    if(isUp(destination))//If up
    {
      if(isLeft(destination) && isRight(destination))
      {
        //If destination is to the left and right of origin than no matter what going straight up is the quickest
        return Direction.Up;
      }
      else if (isLeft(destination) && ( GetDistance(destination.GetBottomCenter(), GetLeftCenter()) < GetDistance(destination.GetRightCenter(), GetTopCenter()) ))
      {        
        //If going left from origin into the bottom of destination is quicker than going up from origin into the right of destination 
        return Direction.Left;
      }
      else if (isRight(destination) && ( GetDistance(destination.GetBottomCenter(), GetRightCenter()) < GetDistance(destination.GetLeftCenter(), GetTopCenter()) ))
      {
        //If going right from origin into the bottom of destination is quicker than going up from origin into the left of destination 
        return Direction.Right;
      }
      else
      {
        //It will be quicker to go up and then turn towards the area
        return Direction.Up;
      }
    }
    else if(isDown(destination))
    {
      if (isLeft(destination) && isRight(destination))
      {
        //If destination is to the left and right of origin than no matter what going straight down is the quickest
        return Direction.Down;
      }
      else if (isLeft(destination) && ( GetDistance(destination.GetTopCenter(), GetLeftCenter()) < GetDistance(destination.GetRightCenter(), GetBottomCenter()) ))
      {
          //If going left from origin into the top of destination is quicker than going down from origin into the right of destination 
          return Direction.Left;
      }
      else if (isRight(destination) && ( GetDistance(destination.GetTopCenter(), GetRightCenter()) < GetDistance(destination.GetLeftCenter(), GetBottomCenter()) ))
      {
          //If going right from origin into the bottom of destination is quicker than going up from origin into the left of destination 
          return Direction.Right;
      }
      else
      {
        //It will be quicker to go up and then turn towards the area
        return Direction.Down;
      }
    }
    else if(isLeft(destination))
    {
      //At this point destination cannot be up or down and so it is only directly left 
      return Direction.Left;
    }
    else
    {
      //At this point destination cannot be up or down and so it is only directly right
      return Direction.Right;
    }
  }


  // Enum to specify the direction is heading.
  public enum Direction
  {
    Up, Right, Down, Left,
  }

  public int GetDistance(Tile tileA, Tile tileB)
  {
    int distanceX = (int)(Math.Abs(tileA.vectorTile.x - tileB.vectorTile.x));
    int distanceY = (int)(Math.Abs(tileA.vectorTile.y - tileB.vectorTile.y));
    int totalDistance = distanceX + distanceY;  
    return totalDistance;
  }

  public Tile GetTile(float x, float y)
  {
    return tiles.Find(tile => tile.vectorTile.x == x && tile.vectorTile.y == y);
  }

  public Tile GetCenter()
  {
    return tiles.Find(tile => tile.vectorTile.x == (xPos + width/2) && tile.vectorTile.y == (yPos + height/2));
  }

  public Tile GetBottomLeft()
  {
    return tiles.Find(tile => tile.vectorTile.x == xPos && tile.vectorTile.y == yPos );     
  }

  public Tile GetBottomCenter()
  {
    return tiles.Find(tile => tile.vectorTile.x == (xPos + width/2) && tile.vectorTile.y == yPos);
  }

  public Tile GetBottomRight()
  {
    return tiles.Find(tile => tile.vectorTile.x == (xPos + width) && tile.vectorTile.y == yPos);
  }

  public Tile GetRightCenter()
  {
    return tiles.Find(tile => tile.vectorTile.x == (xPos + width) && tile.vectorTile.y == (yPos + height/2));
  }

  public Tile GetTopRight()
  {
    return tiles.Find(tile => tile.vectorTile.x == (xPos + width) && tile.vectorTile.y == (yPos + height));
  }

  public Tile GetTopCenter()
  {
    return tiles.Find(tile => tile.vectorTile.x == (xPos + width / 2) && tile.vectorTile.y == (yPos + height));
  }

  public Tile GetTopLeft()
  {
    return tiles.Find(tile => tile.vectorTile.x == xPos && tile.vectorTile.y == (yPos + height));
  }

  public Tile GetLeftCenter()
  {
    return tiles.Find(tile => tile.vectorTile.x == xPos && tile.vectorTile.y == (yPos + height/2));
  }

  public bool isOnTopRightDiagonal(Area destination)
  {
    Tile topRight = GetTopRight();
    foreach(Tile tile in destination.tiles)//Look at every tile in the destination area
    {
      if (tile.vectorTile.x - topRight.vectorTile.x == tile.vectorTile.y - topRight.vectorTile.y)//If on the topright diagonal
      {
        return true;
      }
    }
    return false;
  }

  public bool isOnTopLeftDiagonal(Area destination)
  {
    Tile topLeft = GetTopLeft();
    foreach (Tile tile in destination.tiles)//Look at every tile in the destination area
    {
      if (topLeft.vectorTile.x - tile.vectorTile.x == tile.vectorTile.y - topLeft.vectorTile.y)//If on the topleft diagonal
      {
        return true;
      }
    }
    return false;
  }

  public bool isOnBottomLeftDiagonal(Area destination)
  {
    Tile bottomLeft = GetBottomLeft();
    foreach (Tile tile in destination.tiles)//Look at every tile in the destination area
    {
      if (bottomLeft.vectorTile.x - tile.vectorTile.x == bottomLeft.vectorTile.y - tile.vectorTile.y)//If on the bottomleft diagonal
      {
        return true;
      }
    }
    return false;
  }

  public bool isOnBottomRightDiagonal(Area destination)
  {
    Tile bottomLeft = GetBottomLeft();
    foreach (Tile tile in destination.tiles)//Look at every tile in the destination area
    {
      if (tile.vectorTile.x - bottomLeft.vectorTile.x == bottomLeft.vectorTile.y - tile.vectorTile.y)//If on the bottomright diagonal
      {
        return true;
      }
    }
    return false;
  }

  private void RecalculateArea()
  {
    width = GetWidth();
    //innerWidth = GetMinWidth();
    height = GetHeight();
    //innerHeight = GetMinHeight();
  }

}
