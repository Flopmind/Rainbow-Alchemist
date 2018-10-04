using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    public int startIndex;
    private int index;
    private bool mixed;
    private Animator anim;
    private List<int> lastIndices;
    private List<GameObject> otherOrbs;

    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    public bool Mixed
    {
        get { return mixed; }
    }

    public int LastIndex
    {
        get { return lastIndices[lastIndices.Count - 1]; }
    }

    void Start()
    {
        index = startIndex;
        mixed = false;
        anim = gameObject.GetComponent<Animator>();
        lastIndices = new List<int>();
        otherOrbs = new List<GameObject>();
    }

    //Changes the color of the object and declares it mixed if appropriate.
    public void ChangeColor(int newIndex)
    {
        lastIndices.Add(index);
        index = newIndex;
        if(index != 7) mixed = true;
        UpdateColor();

    }

    //Updates the color by calling animator methods.
    public void UpdateColor()
    {
        if (index == 7)
        {
            anim.SetBool("Inactive", true);
        }
        else
        {
            anim.SetInteger("Color", index);
            anim.SetBool("Inactive", false);
        }
    }

    //Returns the string based on the current color.
    public string GetColor()
    {
        switch (index)
        {
            //Red
            case 0:
                return "red";

            //Orange
            case 1:
                return "orange";

            //Yellow
            case 2:
                return "yellow";

            //Green
            case 3:
                return "green";

            //Blue
            case 4:
                return "blue";

            //Purple
            case 5:
                return "purple";

            //Black
            case 6:
                return "black";

            //White
            case 7:
                return "white";
        }
        return null;
    }

    //Changes the color of this gameobject based on the index of the given object.
    //Also adds the given gameobject to the list of "Orbs" to track mixed in objects. 
    public void Mix(GameObject otherOne)
    {
        PickupScript other = otherOne.GetComponent<PickupScript>();
        if ((index == 0 && other.Index == 2) || (index == 2 && other.Index == 0))
        {
            ChangeColor(1);
            otherOrbs.Add(otherOne);
        }
        else if((index == 0 && other.Index == 4) || (index == 4 && other.Index == 0))
        {
            ChangeColor(5);
            otherOrbs.Add(otherOne);
        }
        else if ((index == 2 && other.Index == 4) || (index == 4 && other.Index == 2))
        {
            ChangeColor(3);
            otherOrbs.Add(otherOne);
        }
        else if((index == 1 && other.Index == 4) || (index == 4 && other.Index == 1) || (index == 0 && other.Index == 3) || (index == 3 && other.Index == 0) || (index == 5 && other.Index == 2) || (index == 2 && other.Index == 5))
        {
            ChangeColor(6);
            otherOrbs.Add(otherOne);
        }
        else
        {
            throw new System.Exception();
        }
    }

    //Reverses the mixing process by reverting the color of the gameobject and returning the last mixed in gameobject.
    public GameObject Unmix()
    {
        GameObject returnable = otherOrbs[otherOrbs.Count - 1];
        otherOrbs.RemoveAt(otherOrbs.Count - 1);
        Revert();
        return returnable;
    }

    //Checks to see if this gameobject and another given gameobject can mix and returns the corresponding boolean. True if they can mix and false if they can't.
    public bool CanMix(PickupScript other)
    {
        if ((index == 0 && other.Index == 2) || (index == 2 && other.Index == 0) || (index == 0 && other.Index == 4) || (index == 4 && other.Index == 0) || (index == 2 && other.Index == 4) || (index == 4 && other.Index == 2) || (index == 1 && other.Index == 4) || (index == 4 && other.Index == 1) || (index == 0 && other.Index == 3) || (index == 3 && other.Index == 0) || (index == 5 && other.Index == 2) || (index == 2 && other.Index == 5))
        {
            return true;
        }
        return false;
    }

    //Reverts the gameobject back to its previous color.
    public void Revert()
    {
        index = lastIndices[lastIndices.Count - 1];
        lastIndices.RemoveAt(lastIndices.Count - 1);
        anim.SetBool("Inactive", false);
        if (index == startIndex) mixed = false;
    }
}
