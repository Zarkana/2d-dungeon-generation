using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
  
  public enum Beings
  {
    Enemy
  }

  public Room[] floorTiles;
  public Corridor[] wallTiles;
  public string[] mystr;

  public int numEntities = 10; 
  public GameObject[] entities; //The sprites of the enemies
  private BoardCreator boardCreator;
  public static Spawner spawner = null;

  // Use this for initialization
  void Start () {
    //GameObject BoardHolder = GameObject.Find("BoardHolder");
    GameObject gameManager = GameObject.Find("GameManager(Clone)");
    boardCreator = gameManager.GetComponent<BoardCreator>();
    
    floorTiles = boardCreator.GetRooms();
  }
	
	// Update is called once per frame
	void Update () {

  }
}
