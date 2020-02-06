using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public int timeLeft = 90;
    public int pause = 0;
    
    public Text countdownText;

    // Use this for initialization
    void Start() {
        timeLeft = 90;
        pause = 0;
       StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        if(pause == 0)
            countdownText.text = ("Timp rămas: " + timeLeft);

        if (Input.GetKeyDown(KeyCode.N) && timeLeft == 0)
            Start();   
        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = " Ups! Timpul s-a scurs. \n  Apasă tasta 'N' pentru a vâna din nou fantome. ";

        }
        if(Input.GetKeyDown(KeyCode.F1) && pause == 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = ("Pauză!");
            pause = 1;
        }
        if(Input.GetKeyDown(KeyCode.Space) && pause == 1)
        {
            StartCoroutine("LoseTime");
            pause = 0;
        }

    }
 
   

    IEnumerator LoseTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
              

        }
    }
}
