using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject[] interactives;

    public GameObject[] Interactives
    {
        get { return interactives; }
    }

    //Checks to see if the given Collider2D matches one of the ones in the interactives list.
    public bool CheckEqual(Collider2D coll)
    {
        for (int i = 0; i < interactives.Length; i++)
        {
            if (interactives[i] == null) return false;
            if (interactives[i].GetComponent<Collider2D>() != null && interactives[i].GetComponent<Collider2D>() == coll) return true;
        }
        return false;
    }
    
    //Returns the gameobject with the matching Collider2D to the one given.
    public GameObject GetEqual(Collider2D coll)
    {
        for (int i = 0; i < interactives.Length; i++)
        {
            if (interactives[i].GetComponent<Collider2D>() != null && interactives[i].GetComponent<Collider2D>() == coll) return interactives[i];
        }
        return null;
    }
}
