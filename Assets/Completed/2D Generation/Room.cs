using UnityEngine;

[System.Serializable]
public class Room : Area
{
  public Direction enteringCorridor;    // The direction of the corridor that is entering this room.

  // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
  public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
  {
    SetDimensions(widthRange, heightRange);

    // Set the x and y coordinates so the room is roughly in the middle of the board.
    xPos = Mathf.RoundToInt(columns / 2f - maxWidth / 2f);
    yPos = Mathf.RoundToInt(rows / 2f - maxHeight / 2f);

    FillTiles();
  }


  // This is an overload of the SetupRoom function and has a corridor parameter that represents the corridor entering the room.
  public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
  {
    // Set the entering corridor direction.
    enteringCorridor = corridor.direction;

    SetDimensions(widthRange, heightRange);

    switch (corridor.direction)
    {
      // If the corridor entering this room is going north...
      case Direction.North:
        // ... the height of the room mustn't go beyond the board so it must be clamped based
        // on the height of the board (rows) and the end of corridor that leads to the room.
        maxHeight = Mathf.Clamp(maxHeight, 1, rows - corridor.EndPositionY);

        // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
        yPos = corridor.EndPositionY;

        // The x coordinate can be random but the left-most possibility is no further than the width
        // and the right-most possibility is that the end of the corridor is at the position of the room.
        xPos = Random.Range(corridor.EndPositionX - maxWidth + 1, corridor.EndPositionX);

        // This must be clamped to ensure that the room doesn't go off the board.
        xPos = Mathf.Clamp(xPos, 0, columns - maxWidth);
        break;
      case Direction.East:
        maxWidth = Mathf.Clamp(maxWidth, 1, columns - corridor.EndPositionX);
        xPos = corridor.EndPositionX;

        yPos = Random.Range(corridor.EndPositionY - maxHeight + 1, corridor.EndPositionY);
        yPos = Mathf.Clamp(yPos, 0, rows - maxHeight);
        break;
      case Direction.South:
        maxHeight = Mathf.Clamp(maxHeight, 1, corridor.EndPositionY);
        yPos = corridor.EndPositionY - maxHeight + 1;

        xPos = Random.Range(corridor.EndPositionX - maxWidth + 1, corridor.EndPositionX);
        xPos = Mathf.Clamp(xPos, 0, columns - maxWidth);
        break;
      case Direction.West:
        maxWidth = Mathf.Clamp(maxWidth, 1, corridor.EndPositionX);
        xPos = corridor.EndPositionX - maxWidth + 1;

        yPos = Random.Range(corridor.EndPositionY - maxHeight + 1, corridor.EndPositionY);
        yPos = Mathf.Clamp(yPos, 0, rows - maxHeight);
        break;
    }

    FillTiles();
  }

  private void SetDimensions(IntRange widthRange, IntRange heightRange)
  {
    // Set a random width and height.
    maxWidth = widthRange.Random;
    minWidth = maxWidth;
    maxHeight = heightRange.Random;
    minHeight = maxHeight;
  }
}