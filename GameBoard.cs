using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    
    private static int boardWidth = 1380;
    
    private static int boardHeight = 1360;

    public GameObject[,] board = new GameObject[boardWidth, boardHeight];
   

    

    // Use this for initialization
    void Start()
    {
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach (GameObject o in objects)
        {
           
            Vector2 pos = o.transform.position;
            if (o.name != "PacMan" && o.name != "Maze" && o.name != "Pellets" && o.name != "Nodes" && o.name != "GhostsKilled")
            {
                board[(int)pos.x, (int)pos.y] = o;
            }

        }

    }

    
    // Update is called once per frame
    void Update()
    {

    }
}
