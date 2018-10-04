using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {

    public GameObject target;

    //Sets the given target object to be active or inactive depending on what it was before this method was called.
    public void FadeTarget()
    {
        if(!target.activeSelf) target.SetActive(true);
        else
        {
            target.SetActive(false);
        }
    }
}
