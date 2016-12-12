using UnityEngine;

[System.Serializable]
public class Room : Area
{

  public Corridor topCorridor;
  public Corridor leftCorridor;
  public Corridor rightCorridor;
  public Corridor bottomCorridor;

  public Direction enteringCorridor;    // The direction of the corridor that is entering this room.

  // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
  public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
  {
    SetDimensions(widthRange, heightRange);

    // Set the x and y coordinates so the room is roughly in the middle of the board.
    xPos = Mathf.RoundToInt(columns / 2f - width / 2f);
    yPos = Mathf.RoundToInt(rows / 2f - height / 2f);

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
        height = Mathf.Clamp(height, 1, rows - corridor.height);

        // The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
        yPos = corridor.height;

        // The x coordinate can be random but the left-most possibility is no further than the width
        // and the right-most possibility is that the end of the corridor is at the position of the room.
        xPos = Random.Range(corridor.width - width + 1, corridor.width);

        // This must be clamped to ensure that the room doesn't go off the board.
        xPos = Mathf.Clamp(xPos, 0, columns - width);
        break;
      case Direction.East:
        width = Mathf.Clamp(width, 1, columns - corridor.width);
        xPos = corridor.width;

        yPos = Random.Range(corridor.height - height + 1, corridor.height);
        yPos = Mathf.Clamp(yPos, 0, rows - height);
        break;
      case Direction.South:
        height = Mathf.Clamp(height, 1, corridor.height);
        yPos = corridor.height - height + 1;

        xPos = Random.Range(corridor.width - width + 1, corridor.width);
        xPos = Mathf.Clamp(xPos, 0, columns - width);
        break;
      case Direction.West:
        width = Mathf.Clamp(width, 1, corridor.width);
        xPos = corridor.width - width + 1;

        yPos = Random.Range(corridor.height - height + 1, corridor.height);
        yPos = Mathf.Clamp(yPos, 0, rows - height);
        break;
    }

    FillTiles();
  }

  private void SetDimensions(IntRange widthRange, IntRange heightRange)
  {
    // Set a random width and height.
    width = widthRange.Random;
    innerWidth = width;
    height = heightRange.Random;
    innerHeight = height;
  }
}