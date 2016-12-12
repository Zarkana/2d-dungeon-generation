using UnityEngine;
using System.Collections;
using Completed;

public class Spawner : MonoBehaviour {
  
  public enum Beings
  {
    Enemy
  }

  public Room[] rooms;
  public Corridor[] corridors;

  private int numEntities;

  public GameObject[] entities; //The sprites of the enemies
  private Cell cell;
  public static Spawner spawner = null;

  // Use this for initialization
  void Start () {
    //GameObject cellHolder = GameObject.Find("CellHolder");
    //GameObject gameManager = GameObject.Find("GameManager(Clone)");
    GameManager gameManager = GameManager.instance;
    cell = gameManager.GetComponent<Cell>();
    
    rooms = cell.GetRooms();
    corridors = cell.GetCorridors();

    float height = rooms[0].GetHeight();
    float width = rooms[0].GetWidth();

    int enemiesToPlace = entities.Length - 1; //Store the number of entities to be placed in enemiesToPlace
    for (int i = 0; (i < rooms.Length -1) && (enemiesToPlace >= 1); i++)//As long as there are rooms and there are enemies to place
    {
      //Vector3 enemyPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
      Vector3 enemyPos = rooms[i].GetCenter().vectorTile;
      Instantiate(entities[enemiesToPlace], enemyPos, Quaternion.identity);
      enemiesToPlace--;
    }

  }
	

  //Entities, Variance, Area
  private void Spray()
  {
    //TODO
  }

  private void Line()
  {
    //TODO
  }

  private void Rectangle()
  {
    //TODO
  }

  private void Sphere()
  {
    //TODO
  }

  //
  private void joinArea()
  {
    //TODO
  }

  private void isSameTile()
  {
    //TODO
  }


}
