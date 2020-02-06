using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node [] vecini;
    public Vector2[] directiiValide;


	

	// functie folosita pentru initializare
	void Start ()
    {
        directiiValide = new Vector2[vecini.Length];
        
        for(int i = 0; i < vecini.Length; i++)
        {
            Node vecin = vecini[i];
            Vector2 tempVector = vecin.transform.localPosition - transform.localPosition;

            directiiValide[i] = tempVector.normalized;
        }


	}
}