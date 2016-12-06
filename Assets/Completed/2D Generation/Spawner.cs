﻿using UnityEngine;
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

    int enemiesToPlace = entities.Length - 1; //Store the number of entities to be placed in enemiesToPlace
    for (int i = 1; (i < rooms.Length) && (enemiesToPlace >= 1); i++)//As long as there are rooms and there are enemies to place
    {      
      Vector3 enemyPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
      Instantiate(entities[enemiesToPlace], enemyPos, Quaternion.identity);
      enemiesToPlace--;
    }


  }
	
	// Update is called once per frame
	void Update () {

  }
}
