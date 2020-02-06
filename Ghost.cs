using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Ghost : MonoBehaviour
{
    public Text Z;

   public int highScore, pressed = 0;
   private int nr = 0, nr1 = 0, nr2 = 0, nr3 = 0, nr4 = 0 ;
    public float moveSpeed = 3.9f;
    private bool StopGame = false;
    private int timeLeft;
    private int pause = 0;

    public Node startingPosition;
    public Node secondPosition;

    public Vector2 actualPosition;

    public int redGhostTime, blueGhostTime, pinkGhostTime, orangeGhostTime;
 

    public GameObject pacMan;

    private Node currentNode, targetNode, previousNode;
    private Vector2 direction, nextDirection;

  
    


    // Use this for initialization
    void Start()
    {
   
        timeLeft = 90;
        Node node = GetNodeAtPosition(transform.localPosition);
        redGhostTime = blueGhostTime = orangeGhostTime = pinkGhostTime = 0;
        StartCoroutine("LTime");

        if (node != null)
        {
            currentNode = node;

        }
        if(ghostType == GhostType.Red)
        {
            currentNode = startingPosition;
            previousNode = currentNode;
            targetNode = secondPosition;
            direction = Vector2.right;
           

        }

        if (ghostType == GhostType.Blue)
        {
            currentNode = startingPosition;
            previousNode = currentNode;
            targetNode = secondPosition;
            direction = Vector2.right;
        }

        if (ghostType == GhostType.Pink)
        {
            currentNode = startingPosition;
            previousNode = currentNode;
            targetNode = secondPosition;
            direction = Vector2.up;

        }

        if (ghostType == GhostType.Orange)
        {
            currentNode = startingPosition;
            previousNode = currentNode;
            targetNode = secondPosition;
            direction = Vector2.right;
        }
 
        
    }
    
    // Update is called once per frame
    void Update()
    {
       
      
        Move();
        
        NewGame();
        
        
        CatchRedGhost();
        CatchBlueGhost();
        CatchOrangeGhost();
        CatchPinkGhost();

        GameObject blueGhost = GameObject.FindGameObjectWithTag("ghost_blue");
        GameObject redGhost = GameObject.FindGameObjectWithTag("ghost_red");
        GameObject orangeGhost = GameObject.FindGameObjectWithTag("ghost_orange");
        GameObject pinkGhost = GameObject.FindGameObjectWithTag("ghost_pink");

        if (redGhost.GetComponent<SpriteRenderer>().enabled == false )
            nr++;

        if (blueGhost.GetComponent<SpriteRenderer>().enabled == false )
            nr++;
        if (orangeGhost.GetComponent<SpriteRenderer>().enabled == false)
                nr++;
        if (pinkGhost.GetComponent<SpriteRenderer>().enabled == false )
            nr++;

        if (Input.GetKeyDown(KeyCode.E) && pressed == 0)
        {
            nr -= 1000;
            pressed = 1;
        }


       //Z.text =("Scor: " + nr.ToString());
        
        RedAlive();
        BlueAlive();
        OrangeAlive();
        PinkAlive();

        Stop();

        Pause();
       
       

        
    }
   
  
     IEnumerator LTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            Debug.Log(timeLeft);
        }
    }
    void NewGame()
    {

        if (timeLeft <= 0)
        {
            if (nr >= 5000)
                Z.text = ("Scor:" + nr.ToString() + "\n\n Extraordinar! Scorul tău este imens!");

            if (nr > 4000 && nr < 5000)
                Z.text = ("Scor:" + nr.ToString() + "\n\n Scorul tău denotă adevărata valoare a ta!");

            if (2000 <= nr && nr <= 4000)
                Z.text = ("Scor:" + nr.ToString() + "\n\n Foarte bine! Ești pe cale să devii maestru la jocul ăsta!");

            if (nr < 2000 && nr > 1000)
                Z.text = ("Scor:" + nr.ToString() + "\n\n Ups! Poate data viitoare...");

            if (nr <= 1000)
                Z.text = ("Scor:" + nr.ToString() + "\n\n Nu te descuraja! Încearcă din nou!");




        }
        else Z.text = ("Scor:" + nr.ToString());

    }
  void Stop()
    {
        GameObject blueGhost = GameObject.FindGameObjectWithTag("ghost_blue");
        GameObject redGhost = GameObject.FindGameObjectWithTag("ghost_red");
        GameObject orangeGhost = GameObject.FindGameObjectWithTag("ghost_orange");
        GameObject pinkGhost = GameObject.FindGameObjectWithTag("ghost_pink");
       
        if (timeLeft <= 0)
        {
            StopGame = true;
            redGhost.GetComponent<SpriteRenderer>().enabled = true;
            blueGhost.GetComponent<SpriteRenderer>().enabled = true;
            orangeGhost.GetComponent<SpriteRenderer>().enabled = true;
            pinkGhost.GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }
   
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.F1) && pause == 0)
        {
            StopCoroutine("LTime");
            pause = 1;
            StopGame = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && pause == 1)
        {
            StartCoroutine("LTime");
            pause = 0;
            StopGame = false;

        }

    }



    Vector2 GetPositionOfBlueGhost()
    {
        GameObject blueGhost = GameObject.FindGameObjectWithTag("ghost_blue");
        Vector2 pos = blueGhost.transform.position;

        return pos;
    }
    Vector2 GetPositionOfRedGhost()
    {
        GameObject redGhost = GameObject.FindGameObjectWithTag("ghost_red");
        Vector2 pos = redGhost.transform.position;

        return pos;
    }
    Vector2 GetPositionOfOrangeGhost()
    {
        GameObject orangeGhost = GameObject.FindGameObjectWithTag("ghost_orange");
        Vector2 pos = orangeGhost.transform.position;

        return pos;
    }
    Vector2 GetPositionOfPinkGhost()
    {
        GameObject pinkGhost = GameObject.FindGameObjectWithTag("ghost_pink");
        Vector2 pos = pinkGhost.transform.position;

        return pos;
    }
  
    void CatchRedGhost()
    {
        GameObject pacMan = GameObject.FindGameObjectWithTag("PacMan");
        Vector2 pacManPosition = pacMan.transform.position;
        GameObject redGhost = GameObject.FindGameObjectWithTag("ghost_red");
        
        Vector2 redGhostPosition = GetPositionOfRedGhost();

        if(pacManPosition.x < redGhostPosition.x)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = redGhostPosition;
            redGhostPosition = aux;
        }

        if (pacManPosition.y < redGhostPosition.y)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = redGhostPosition;
            redGhostPosition = aux;
        }

        Vector2 orientation = pacMan.GetComponent<PacMan>().orientation;

        int pos1x = Mathf.RoundToInt(pacManPosition.x);
        int pos1y = Mathf.RoundToInt(pacManPosition.y);
        int pos2x = Mathf.RoundToInt(redGhostPosition.x);
        int pos2y = Mathf.RoundToInt(redGhostPosition.y);

        if (orientation == direction * -1 && pos1x - pos2x == 1  && pos1y == pos2y && redGhostTime == 0 && redGhost.GetComponent<SpriteRenderer>().enabled == true)
        {
            nr1++;
            redGhost.GetComponent<SpriteRenderer>().enabled = false;
            redGhostTime = 5;
            StartCoroutine(RedTimeOff());
           
         // Debug.Log("Ghosts X : " + );
        }

        if (orientation == direction * -1 && pos1y - pos2y == 0 && pos1x == pos2x && redGhostTime == 0 && redGhost.GetComponent<SpriteRenderer>().enabled == true)
        { 
            nr1++;
            redGhost.GetComponent<SpriteRenderer>().enabled = false;
            redGhostTime = 5;
            StartCoroutine(RedTimeOff());
          
          //  Debug.Log("Ghosts Y : " + ghostskilledY);
        }
    }

    void CatchBlueGhost()
    {
        GameObject pacMan = GameObject.FindGameObjectWithTag("PacMan");
        GameObject blueGhost = GameObject.FindGameObjectWithTag("ghost_blue");

        Vector2 pacManPosition = pacMan.transform.position;
        Vector2 blueGhostPosition = GetPositionOfBlueGhost();

        if (pacManPosition.x < blueGhostPosition.x)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = blueGhostPosition;
            blueGhostPosition = aux;
        }

        if (pacManPosition.y < blueGhostPosition.y)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = blueGhostPosition;
            blueGhostPosition = aux;
        }

        Vector2 orientation = pacMan.GetComponent<PacMan>().orientation;

        int pos1x = Mathf.RoundToInt(pacManPosition.x);
        int pos1y = Mathf.RoundToInt(pacManPosition.y);
        int pos2x = Mathf.RoundToInt(blueGhostPosition.x);
        int pos2y = Mathf.RoundToInt(blueGhostPosition.y);


        if (orientation == direction * -1 && pos1x - pos2x == 1 && pos1y == pos2y && blueGhostTime == 0 && blueGhost.GetComponent<SpriteRenderer>().enabled == true)
        {

            nr2++;
            blueGhost.GetComponent<SpriteRenderer>().enabled = false;
            blueGhostTime = 5;
            StartCoroutine(BlueTimeOff());
            
           // Debug.Log("Ghosts X : " + ghostskilledX);
        }

        if (orientation == direction * -1 && pos1y - pos2y == 1 && pos1x == pos2x && blueGhostTime == 0 && blueGhost.GetComponent<SpriteRenderer>().enabled == true)
        {
            nr2++;
            blueGhost.GetComponent<SpriteRenderer>().enabled = false;
            blueGhostTime = 5;
            StartCoroutine(BlueTimeOff());
            
            //Debug.Log("Ghosts Y : " + ghostskilledY);
        }
    }

    void CatchOrangeGhost()
    {
        GameObject pacMan = GameObject.FindGameObjectWithTag("PacMan");
        Vector2 pacManPosition = pacMan.transform.position;
        GameObject orangeGhost = GameObject.FindGameObjectWithTag("ghost_orange");

        Vector2 orangeGhostPosition = GetPositionOfOrangeGhost();

        if (pacManPosition.x < orangeGhostPosition.x)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = orangeGhostPosition;
            orangeGhostPosition = aux;
        }

        if (pacManPosition.y < orangeGhostPosition.y)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = orangeGhostPosition;
            orangeGhostPosition = aux;
        }

        Vector2 orientation = pacMan.GetComponent<PacMan>().orientation;

        int pos1x = Mathf.RoundToInt(pacManPosition.x);
        int pos1y = Mathf.RoundToInt(pacManPosition.y);
        int pos2x = Mathf.RoundToInt(orangeGhostPosition.x);
        int pos2y = Mathf.RoundToInt(orangeGhostPosition.y);


        if (orientation == direction * -1 && pos1x - pos2x == 1 && pos1y == pos2y && orangeGhostTime == 0 && orangeGhost.GetComponent<SpriteRenderer>().enabled == true)
        {
            nr3++;
            orangeGhost.GetComponent<SpriteRenderer>().enabled = false;
            orangeGhostTime = 5;
            StartCoroutine(OrangeTimeOff());
            
            //Debug.Log("Ghosts X : " + ghostskilledX);
        }

        if (orientation == direction * -1 && pos1y - pos2y == 1 && pos1x == pos2x && orangeGhostTime == 0 && orangeGhost.GetComponent<SpriteRenderer>().enabled == true)
        {
            nr3++;
            orangeGhost.GetComponent<SpriteRenderer>().enabled = false;
            orangeGhostTime = 5;
            StartCoroutine(OrangeTimeOff());
       
           // Debug.Log("Ghosts Y : " + ghostskilledY);
        }
    }

    void CatchPinkGhost()
    {
        GameObject pacMan = GameObject.FindGameObjectWithTag("PacMan");
        Vector2 pacManPosition = pacMan.transform.position;
        GameObject pinkGhost = GameObject.FindGameObjectWithTag("ghost_pink");

        Vector2 pinkGhostPosition = GetPositionOfPinkGhost();

        if (pacManPosition.x < pinkGhostPosition.x)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = pinkGhostPosition;
            pinkGhostPosition = aux;
        }

        if (pacManPosition.y < pinkGhostPosition.y)
        {
            Vector2 aux = pacManPosition;
            pacManPosition = pinkGhostPosition;
            pinkGhostPosition = aux;
        }

        Vector2 orientation = pacMan.GetComponent<PacMan>().orientation;

        int pos1x = Mathf.RoundToInt(pacManPosition.x);
        int pos1y = Mathf.RoundToInt(pacManPosition.y);
        int pos2x = Mathf.RoundToInt(pinkGhostPosition.x);
        int pos2y = Mathf.RoundToInt(pinkGhostPosition.y);

        if (orientation == direction * -1 && pos1x - pos2x == 1 && pos1y == pos2y && pinkGhostTime == 0 && pinkGhost.GetComponent<SpriteRenderer>().enabled == true)
        {
            nr4++;
            pinkGhost.GetComponent<SpriteRenderer>().enabled = false;
            pinkGhostTime = 5;
            StartCoroutine(PinkTimeOff());
            
            //Debug.Log("Ghosts X : " + ghostskilledX);
        }

        if (orientation == direction * -1 && pos1y - pos2y == 1 && pos1x == pos2x && pinkGhostTime == 0 && pinkGhost.GetComponent<SpriteRenderer>().enabled == true)
        {
            nr4++;
            pinkGhost.GetComponent<SpriteRenderer>().enabled = false;
            pinkGhostTime = 5;
            StartCoroutine(PinkTimeOff());
           
           // Debug.Log("Ghosts Y : " + ghostskilledY);
        }

    }
    IEnumerator RedTimeOff()
    {

        while (redGhostTime > -1)
        {
            yield return new WaitForSeconds(1);
            redGhostTime--;
            //Debug.Log(redGhostTime);
        }
    }
    IEnumerator BlueTimeOff()
    {
       
        while (blueGhostTime > -1)
        {
            yield return new WaitForSeconds(1);
            blueGhostTime--;
           
        }
    }
    IEnumerator OrangeTimeOff()
    {
        
        while (orangeGhostTime > -1)
        {
            yield return new WaitForSeconds(1);
            orangeGhostTime--;
            //Debug.Log(redGhostTime);
        }
    }
    IEnumerator PinkTimeOff()
    {
       
        while (pinkGhostTime > -1)
        {
            yield return new WaitForSeconds(1);
            pinkGhostTime--;
            //Debug.Log(redGhostTime);
        }
    }
    void Appear(string s)
    {
        GameObject DGhost = GameObject.FindGameObjectWithTag(s);
        DGhost.GetComponent<SpriteRenderer>().enabled = true;

    }
    void RedAlive()
    {
        if(redGhostTime < 0)
        {
            redGhostTime = 0;
            StopCoroutine(RedTimeOff());
            GameObject redGhost = GameObject.FindGameObjectWithTag("ghost_red");
            redGhost.GetComponent<SpriteRenderer>().enabled = true;
           
            //Debug.Log(redGhostTime);
        }
    }
    void BlueAlive()
    {
        if (blueGhostTime < 0)
        {
             blueGhostTime = 0;
            StopCoroutine(BlueTimeOff());
            GameObject blueGhost = GameObject.FindGameObjectWithTag("ghost_blue");
            blueGhost.GetComponent<SpriteRenderer>().enabled = true;
          
            //Debug.Log(redGhostTime);
        }
    }
    void OrangeAlive()
    {
        if (orangeGhostTime < 0)
        {
            orangeGhostTime = 0;
            StopCoroutine(OrangeTimeOff());
            GameObject orangeGhost = GameObject.FindGameObjectWithTag("ghost_orange");
            orangeGhost.GetComponent<SpriteRenderer>().enabled = true;
         
            //Debug.Log(redGhostTime);
        }
    }
    void PinkAlive()
    {
        if (pinkGhostTime < 0)
        {
            pinkGhostTime = 0;
            StopCoroutine(PinkTimeOff());
            GameObject pinkGhost = GameObject.FindGameObjectWithTag("ghost_pink");
            pinkGhost.GetComponent<SpriteRenderer>().enabled = true;
          
            //Debug.Log(redGhostTime);
        }
    }

    void Move()
    {
        if(targetNode != currentNode && targetNode != null && StopGame == false)
        {
            if(OverShootTarget())
            {
                currentNode = targetNode;
                transform.localPosition = currentNode.transform.position;

                GameObject otherPortal = GetPortal(currentNode.transform.position);

                if(otherPortal != null)
                {
                    transform.localPosition = otherPortal.transform.position;
                    currentNode = otherPortal.GetComponent<Node>();


                }
                targetNode = ChooseNextNode();

                previousNode = currentNode;
                currentNode = null;
            }
            else
            {
                transform.localPosition += (Vector3)direction * moveSpeed * Time.deltaTime;
            }
        }
    }
    public enum GhostType
    {
        Red,
        Pink,
        Orange,
        Blue,
        None
    }
    public GhostType ghostType= GhostType.Red;
  
    Node ChooseNextNode()
    {
        
        Node moveToNode = null;

        int length = targetNode.vecini.Length;
        int x = Random.Range(0, length);

        moveToNode = targetNode.vecini[x];
        direction = targetNode.directiiValide[x];

        return moveToNode;
    }
   
   
    
    Node GetNodeAtPosition(Vector2 pos)
    {
       
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];
        
        if(tile != null && tile.tag != "GhostsKilled")
        {
            if(tile.GetComponent<Node> () != null)
           return tile.GetComponent<Node>();    
        }
        return null;
    }
    
    GameObject GetPortal (Vector2  pos)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];

        if(tile != null && tile.tag != "GhostsKilled")
        {
            if (tile.GetComponent<Tile>() != null)
                if (tile.GetComponent<Tile>().isPortal)
                {
                   GameObject otherPortal = tile.GetComponent<Tile>().portalReceiver;
                   return otherPortal;
                }
        }

        return null;
    }

    float LengthFromNode(Vector2 targetPosition)
    {
        Vector2 vec = targetPosition - (Vector2)previousNode.transform.position;
        return vec.sqrMagnitude;
    }

    bool OverShootTarget ()
    {
        float nodeToTarget = LengthFromNode(targetNode.transform.position);
        float nodeToSelf = LengthFromNode(transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }

    float GetDistance(Vector2 posA, Vector2 posB)
    {
        float dx = posA.x - posB.x;
        float dy = posA.y - posB.y;

        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        return distance;
    }

}
