using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{

    public GameObject startingObject;
    public List<CircuitScript> activated;
    public List<CircuitScript> nearbyCircuits;

    private GameObject held;
    private bool active;

    public GameObject Held
    {
        get { return held; }
    }

    public void Start()
    {
        activated = new List<CircuitScript>();
        if (startingObject != null)
        {
            held = startingObject;
            held.transform.position = transform.position;
            active = true;
        }
        else
        {
            active = false;
        }
    }

    public void TakeObject(GameObject gameObj)
    {
        if (!active)
        {
            active = true;
            held = gameObj;
            held.transform.position = transform.position;
            for (int i = 0; i < nearbyCircuits.Count; i++)
            {
                nearbyCircuits[i].Activate(gameObject, held.GetComponent<PickupScript>().Index);
            }
        }
    }

    public GameObject ReturnObject()
    {
        if (active && held != null)
        {
            for (int i = 0; i < nearbyCircuits.Count; i++)
            {
                if (nearbyCircuits[i].Sources.Contains(gameObject)) nearbyCircuits[i].Deactivate(gameObject, held.GetComponent<PickupScript>().Index);
            }

            GameObject temp = held;
            held = null;
            active = false;
            
            //Deactivate();
            //for (int i = 0; i < activated.Count; i++)
            //{
            //    activated[i].Active = false;
            //    activated[i].Source = null;
            //}
            return temp;
        }
        throw new System.Exception();
    }

    //public void Deactivate()
    //{
    //    for (int i = 0; i < nearbyCircuits.Count; i++)
    //    {
    //        if (nearbyCircuits[i].Sources.Contains(gameObject)) nearbyCircuits[i].Deactivate();
    //    }
    //}

    //public void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (!collision.isTrigger)
    //    {
    //        if (active && held != null && collision.CompareTag("Circuit") && !collision.GetComponent<CircuitScript>().Active)
    //        {
    //            Debug.Log("I have a held thing.");
    //            collision.gameObject.GetComponent<CircuitScript>().Source = gameObject;
    //            collision.gameObject.GetComponent<CircuitScript>().Active = true;
    //            collision.gameObject.GetComponent<CircuitScript>().Index = held.GetComponent<PickupScript>().Index;
    //        }
    //        else if (!active && held == null && collision.CompareTag("Circuit") && collision.GetComponent<CircuitScript>().Active)
    //        {
    //            Debug.Log("You took my held thing.");
    //            collision.gameObject.GetComponent<CircuitScript>().Source = null;
    //            collision.gameObject.GetComponent<CircuitScript>().Active = false;
    //        }
    //    }
    //}
}
