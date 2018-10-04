using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{

    public int startIndex;
    public bool activateShift;
    public List<CircuitScript> nearbyCircuits;

    private int index;
    private List<int> lastIndices;
    private Animator anim;

    public int Index
    {
        get { return index; }
    }

    public int LastIndex
    {
        get { return lastIndices[lastIndices.Count - 1]; }
    }

    public bool ActivateShift
    {
        get { return activateShift; }
    }

    void Start()
    {
        index = startIndex;
        lastIndices = new List<int>();
        anim = gameObject.GetComponent<Animator>();
    }

    //Changes the color of the object and declares it mixed if appropriate. Also updates the color by calling animator methods.
    public void ChangeColor(int newIndex)
    {
        lastIndices.Add(index);
        index = newIndex;

        if(index == 7)
        {
            anim.SetBool("Inactive", true);
        }
        else
        {
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

    //Reverts to the previous color.
    public void Revert()
    {
        index = lastIndices[lastIndices.Count - 1];
        lastIndices.RemoveAt(lastIndices.Count - 1);
        anim.SetBool("Inactive", false);
    }

    public void Activate()
    {
        for (int i = 0; i < nearbyCircuits.Count; i++)
        {
            nearbyCircuits[i].Activate(gameObject, Index);
        }
    }

    public void Deactivate()
    {
        for (int i = 0; i < nearbyCircuits.Count; i++)
        {
            nearbyCircuits[i].Deactivate(gameObject, Index);
        }
    }

}
