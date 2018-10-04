using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shifter
{
    public delegate void ColorShift();
    public static event ColorShift Shift;

    public static void ShiftColors()
    {
        Shift();
    }
}

public class ShiftingGateScript : MonoBehaviour
{

    public GateScript gate;
    public int index;

    private bool shifted;
    private Animator anim;

    public void Start()
    {
        shifted = false;
        Shifter.Shift += Shift;
        anim = GetComponent<Animator>();
    }

    public void Shift()
    {
        if (gate.Index != 7 && !shifted)
        {
            gate.ChangeColor(index);
            anim.SetInteger("Color", gate.Index);
            shifted = true;
            Debug.Log("Go " + gate.Index);
        }
        else if (gate.Index != 7 && shifted)
        {
            shifted = false;
            gate.Revert();
            anim.SetInteger("Color", gate.Index);
            Debug.Log("Back " + gate.Index);
        }
    }

}
