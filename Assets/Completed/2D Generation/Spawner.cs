using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
  
  public enum Beings
  {
    Enemy
  }

  public Room[] rooms;
  public Corridor[] corridors;

  private int numEntities;

  public GameObject[] entities; //The sprites of the enemies
  private BoardCreator boardCreator;
  public static Spawner spawner = null;

  // Use this for initialization
  void Start () {
    //GameObject BoardHolder = GameObject.Find("BoardHolder");
    GameObject gameManager = GameObject.Find("GameManager(Clone)");
    boardCreator = gameManager.GetComponent<BoardCreator>();
    
    rooms = boardCreator.GetRooms();
    corridors = boardCreator.GetCorridors();


    //TODO: Almost there, currently does not work as expected
    float height = rooms[0].getHeight();
    float width = rooms[0].getWidth();
    //Tile myTile = rooms[0].tiles[0];

    int enemiesToPlace = entities.Length - 1; //Store the number of entities to be placed in enemiesToPlace
    for (int i = 1; (i < rooms.Length) && (enemiesToPlace >= 1); i++)//As long as there are rooms and there are enemies to place
    {      
      Vector3 enemyPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
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
