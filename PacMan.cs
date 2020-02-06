using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PacMan : MonoBehaviour
{

    public float speed = 4.0f;

    public Vector2 orientation;

    public bool StopGame = false;
    public int timeLeft = 100;
    public int pause = 0;

    public Vector2 Location;
    public Sprite idleSprite;
    
    private Vector2 direction = Vector2.zero;
    private Vector2 nextDirection;


    private Node currentNode, previousNode, targetNode, initialNode;

    // Use this for initialization
    public void Start()
    {
        direction = Vector2.zero;
        Node node = GetNodeAtPosition(transform.localPosition);

        StartCoroutine("LoseTime1");

        if (node != null)
        {
            currentNode = node;
            initialNode = node;
            Location = node.transform.position;
            Debug.Log(currentNode);
        }
        direction = Vector2.left;
        orientation = Vector2.left;
        ChangePosition(direction);

    }
  
    // Update is called once per frame
    void Update()
    {

        CheckInput();

        Move();

        UpdateOrientation();

        UpdateAnimationState();

        ConsumePelllet();

        ChangeSpeed();

        PRESS();

        Pause();

        GoToMainMenu();


        if (timeLeft <= 0)
        {
            StopGame = true;
            GetComponent<Animator>().enabled = false;
        }
            //Debug.Log(timeLeft);
        
    }
    IEnumerator LoseTime1()
    {
        while (true)
        {  
            yield return new WaitForSeconds(18);
            timeLeft--;


        }
    }

    void ChangeSpeed()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            speed = 6.0f;
           
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            speed = 4.0f;
            
        }

    }

    public void PRESS()
    {
        if (StopGame == true && Input.GetKeyDown(KeyCode.N))

            SceneManager.LoadScene("Level1");

        
    }

    public void Pause()
    {
        if(Input.GetKeyDown(KeyCode.F1) && pause == 0)
        {
            pause = 1;
            StopCoroutine("LoseTime1");
            GetComponent<Animator>().enabled = false;
            StopGame = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && pause == 1)
        {
            pause = 0;
            StartCoroutine("LoseTime1");
            GetComponent<Animator>().enabled = true;
            StopGame = false;

        }

    }

    public void GoToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
            ChangePosition(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

           
            ChangePosition(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

           
            ChangePosition(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            
            ChangePosition(Vector2.down);
        }
    }

    void  MoveToNode(Vector2 d)
    {
        Node moveToNode = CanMove(d);
        if(moveToNode != null)
        {
            transform.localPosition = moveToNode.transform.position;
            currentNode = moveToNode;
        }
    }
    void MoveToInitialNode(Vector2 d)
    {
        transform.localPosition = initialNode.transform.position;
        currentNode = initialNode;
    }
    
   

    void ChangePosition(Vector2 d)
    {
        if (d != direction)
            nextDirection  = d;

        if(currentNode != null)
        {
            Node moveToNode = CanMove(d);
            if(moveToNode != null)
            {
                direction = d;
                targetNode = moveToNode;
                previousNode = currentNode;
                currentNode = null;

            }
        }
    }

    public void Move()
    {   
         

       if(StopGame == false)
        {
            if (targetNode != currentNode && targetNode != null)
            {
                if (nextDirection == direction * -1)
                {
                    direction *= -1;

                    Node tempNode = targetNode;

                    targetNode = previousNode;
                    previousNode = tempNode;
                }

                if (OverShotTarget())
                {
                    currentNode = targetNode;

                    transform.localPosition = currentNode.transform.position;

                    GameObject otherPortal = GetPortal(currentNode.transform.position);

                    if (otherPortal != null)
                    {
                        transform.localPosition = otherPortal.transform.position;

                        currentNode = otherPortal.GetComponent<Node>();
                    }

                    Node moveToNode = CanMove(nextDirection);

                    if (moveToNode != null)
                        direction = nextDirection;

                    if (moveToNode == null)
                        moveToNode = CanMove(direction);

                    if (moveToNode != null)
                    {
                        targetNode = moveToNode;
                        previousNode = currentNode;
                        currentNode = null;
                    }
                    else
                    {
                        direction = Vector2.zero;

                    }

                }
                else
                {
                    transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
                }
            }
        }
        
    }

    void UpdateOrientation()
    {

        if (direction == Vector2.left)
        {
            orientation = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        else if (direction == Vector2.right)
        {
            orientation = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        else if (direction == Vector2.up)
        {
            orientation = Vector2.up;
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);

        }
        else if (direction == Vector2.down)
        {
            orientation = Vector2.down;
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
    }

   public  void UpdateAnimationState()
    {
        if(direction == Vector2.zero)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = idleSprite;

        }
        else if(pause == 0)
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    void ConsumePelllet()
    {
        GameObject o = GetTileAtPosition(transform.position);

        if(o != null)
        {
            Tile tile = o.GetComponent<Tile>();

            if(tile != null)
            {
                if(!tile.didConsume && (tile.isPellet || tile.isSuperPellet))
                {
                    o.GetComponent<SpriteRenderer>().enabled = false;
                    tile.didConsume = true;
                }
            }
         
        }
    }

    Node CanMove(Vector2 d)
    {
        Node moveToNode = null;

        for(int i = 0; i < currentNode.vecini.Length; i++)
        {
            if(currentNode.directiiValide[i] == d)
            {
                moveToNode = currentNode.vecini[i];
                break;
            }
        }
        return moveToNode;
    }

    GameObject GetTileAtPosition(Vector2 pos)
    {
        int tileX = Mathf.RoundToInt(pos.x);
        int tileY = Mathf.RoundToInt(pos.y);

        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[tileX, tileY];

        if(tile != null)
        {
            return tile;
        }
        return null;
    }

    Node GetNodeAtPosition(Vector2 pos)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];
        if(tile != null)
        {
            return tile.GetComponent<Node>();
        }

        return null;
    }
    bool OverShotTarget()
    {
        float nodeToTarget = LengthFromNode(targetNode.transform.position);
        float NodeToSelf = LengthFromNode(transform.localPosition);

        return NodeToSelf > nodeToTarget;
    }
    float LengthFromNode(Vector2 targetPosition)
    {
        Vector2 vec = targetPosition - (Vector2)previousNode.transform.position;
        return vec.sqrMagnitude;
    }

    GameObject GetPortal(Vector2 pos)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];
        if(tile != null)
        {   
           if(tile.GetComponent<Tile>() != null)
            if(tile.GetComponent<Tile>().isPortal)
            {
                GameObject otherPortal = tile.GetComponent<Tile>().portalReceiver;
                return otherPortal;
            }
        }

        return null;
    }

    
}


