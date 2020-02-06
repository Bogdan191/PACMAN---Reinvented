using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Buttons : MonoBehaviour {

    public Buttons Play;
    int highScore = 0;
    public Text P;
    int pause = 0;
	// Use this for initialization
	void Start () {
        Play = null;
        StreamWriter d = new StreamWriter("@Scores");
        P = null;
	}
	
	// Update is called once per frame
	void Update () {

        Pause();
       
              
		
	}

    public void Play_Click()
    {

        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if(Input.GetKeyDown(KeyCode.F1) && pause == 0)
        {
            pause = 1;
            P.text = ("Pause!");
        }
        if (Input.GetKeyDown(KeyCode.Space) && pause == 1)
        {
            pause = 0;
            P.text = ("");
        }

    }

    

}
