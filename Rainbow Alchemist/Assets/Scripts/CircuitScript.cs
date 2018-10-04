using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitScript : MonoBehaviour {

    //public CircuitScript[] nearbyCircuits;

    //private CircuitScript[] circuits;
    //private Animator anim;
    //private GameObject source;
    //private bool active;
    //private List<int> index;
    //private bool mixed;

    //public bool Active
    //{
    //    get { return active; }
    //    set
    //    {
    //        active = value;
    //        anim.SetBool("Inactive", !active);
    //    }
    //}

    //public int Index
    //{
    //    get { return index[index.Count - 1]; }
    //    set
    //    {
    //        index.Add(value);
    //        anim.SetInteger("Color", Index);
    //    }
    //}

    //public bool Mixed
    //{
    //    get { return mixed; }
    //}

    //public void Start()
    //{
    //    circuits = nearbyCircuits;
    //    anim = GetComponent<Animator>();
    //    Active = false;
    //}

    //public void Activate(GameObject mySource, int lastIndex)
    //{
    //    //source = mySource;
    //    //Index = lastIndex;
    //    //Active = true;
    //    //for(int i = 0; i < circuits.Length; i++)
    //    //{
    //    //    if (circuits[i].gameObject != source && !circuits[i].Active) circuits[i].Activate(gameObject, Index);
    //    //}
    //    source = mySource;
    //    if (active && CanMix(lastIndex))
    //    {
    //        Mix(lastIndex);
    //    }
    //    else if (active && IsRoot(lastIndex, Index))
    //    {
    //        Index = lastIndex;
    //    }
    //    else if (!active)
    //    {
    //        Active = true;
    //        Index = lastIndex;
    //    }
    //    for (int i = 0; i < circuits.Length; i++)
    //    {
    //        if (circuits[i].gameObject != source) circuits[i].Activate(gameObject, Index);
    //    }
    //}

    //public void Deactivate(int lastIndex)
    //{
    //    for (int i = 0; i < circuits.Length; i++)
    //    {
    //        if (circuits[i].gameObject != source) circuits[i].Deactivate(Index);
    //    }
    //    source = null;
    //}

    //public bool CanMix(int otherIndex)
    //{
    //    if ((Index == 0 && otherIndex == 2) || (Index == 2 && otherIndex == 0) || (Index == 0 && otherIndex == 4) || (Index == 4 && otherIndex == 0) || (Index == 2 && otherIndex == 4) || (Index == 4 && otherIndex == 2) || (Index == 1 && otherIndex == 4) || (Index == 4 && otherIndex == 1) || (Index == 0 && otherIndex == 3) || (Index == 3 && otherIndex == 0) || (Index == 5 && otherIndex == 2) || (Index == 2 && otherIndex == 5))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //public void Mix(int otherIndex)
    //{
    //    if ((Index == 0 && otherIndex == 2) || (Index == 2 && otherIndex == 0))
    //    {
    //        Index = 3;
    //    }
    //    else if ((Index == 0 && otherIndex == 4) || (Index == 4 && otherIndex == 0))
    //    {
    //        Index = 5;
    //    }
    //    else if ((Index == 2 && otherIndex == 4) || (Index == 4 && otherIndex == 2))
    //    {
    //        Index = 3;
    //    }
    //    else if ((Index == 1 && otherIndex == 4) || (Index == 4 && otherIndex == 1) || (Index == 0 && otherIndex == 3) || (Index == 3 && otherIndex == 0) || (Index == 5 && otherIndex == 2) || (Index == 2 && otherIndex == 5))
    //    {
    //        Index = 6;
    //    }
    //    else
    //    {
    //        throw new System.Exception();
    //    }
    //    mixed = true;
    //}

    //public bool IsRoot(int firstColor, int nextColor)
    //{
    //    switch (firstColor)
    //    {
    //        case 1:

    //            return (nextColor == 0 || nextColor == 2);

    //        case 3:

    //            return (nextColor == 2 || nextColor == 4);

    //        case 5:

    //            return (nextColor == 0 || nextColor == 4);

    //        case 6:

    //            return (nextColor != 6);
    //    }

    //    return false;
    //}


    public bool startActive;
    public List<CircuitScript> nearbyCircuits;
    public List<GameObject> interactives;

    private bool active;
    private int index;
    private List<int> indices;
    private bool mixed;
    private List<GameObject> sources;
    private GameObject wall;
    private Animator anim;
    private List<int> indexTracker;
    private int timer;
    public int delay;

    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            anim.SetBool("Inactive", !active);
        }
    }

    public int Index
    {
        get { return index; }
        set
        {
            index = value;
            anim.SetInteger("Color", index);
        }
    }

    public List<GameObject> Sources
    {
        get { return sources; }
    }

    public void Start()
    {
        index = 10;
        anim = GetComponent<Animator>();
        Active = startActive;
        mixed = false;
        sources = new List<GameObject>();
        indices = new List<int>();
        timer = 10;
    }

    public void Update()
    {
        
    }

    //public void Update()
    //{
    //    if (Active)
    //    {
    //        indexTracker = new List<int>();
    //        for (int i = 0; i < sources.Count; i++)
    //        {
    //            if ((sources[i].GetComponent<CircuitScript>() != null && !sources[i].GetComponent<CircuitScript>().Active && !mixed))
    //            {
    //                Active = false;
    //                indexTracker.Add(i);
    //            }
    //            else if (sources[i].GetComponent<CircuitScript>() != null && sources.Count > 1 && mixed)
    //            {
    //                indices.Remove(sources[i].GetComponent<CircuitScript>().Index);
    //                indexTracker.Add(i);
    //            }
    //            //else if (sources[i].GetComponent<PedestalScript>() != null && sources.Count > 1 && mixed)
    //            //{
    //            //    indices.Remove(sources[i].GetComponent<PedestalScript>().Held.GetComponent<PickupScript>().Index);
    //            //    indexTracker.Add(i);
    //            //}
    //        }
    //        for (int i = 0; i < interactives.Count; i++)
    //        {
    //            Interact(interactives[i]);
    //        }
    //        for (int i = indexTracker.Count - 1; i >= 0; i--)
    //        {
    //            sources.RemoveAt(indexTracker[i]);
    //        }
    //        if (sources.Count == 0)
    //        {
    //            Deactivate();
    //        }
    //    }
    //}

    public void Activate(GameObject source, int lastIndex)
    {
        sources.Add(source);
        Active = true;
        indices.Add(lastIndex);
        UpdateIndex();
        for(int i = 0; i < interactives.Count; i++)
        {
            Interact(interactives[i]);
        }
        for (int i = 0; i < nearbyCircuits.Count; i++)
        {
            if (!sources.Contains(nearbyCircuits[i].gameObject)) nearbyCircuits[i].Activate(gameObject, lastIndex);
        }
    }

    //public void Deactivate()
    //{
    //    sources = new List<GameObject>();
    //    Active = false;
    //    indices = new List<int>();
    //    for (int i = 0; i < interactives.Count; i++)
    //    {
    //        if (interactives[i].GetComponent<WallScript>() != null)
    //        {
    //            interactives[i].SetActive(true);
    //        }
    //    }
    //    for (int i = 0; i < interactives.Count; i++)
    //    {
    //        Reverse(interactives[i]);
    //    }
    //}

    //public void Deactivate(GameObject gameObj)
    //{
    //    sources.Remove(gameObj);
    //    indices.Remove(GetIndex(gameObj));
    //    UpdateIndex();
    //    for (int i = 0; i < interactives.Count; i++)
    //    {
    //        Reverse(interactives[i]);
    //    }
    //    if (sources.Count == 0)
    //    {
    //        Active = false;
    //    }
    //    for (int i = 0; i < nearbyCircuits.Count; i++)
    //    {
    //        if (!sources.Contains(nearbyCircuits[i].gameObject) && (nearbyCircuits[i].Active))
    //        {
    //            nearbyCircuits[i].Deactivate(gameObject);
    //        }
    //    }
    //}

    public void Deactivate(GameObject gameObj, int lastIndex)
    {
        sources.Remove(gameObj);
        indices.Remove(lastIndex);
        UpdateIndex();
        for (int i = 0; i < interactives.Count; i++)
        {
            Reverse(interactives[i]);
        }
        if (sources.Count == 0)
        {
            Active = false;
        }
        for (int i = 0; i < nearbyCircuits.Count; i++)
        {
            if (!sources.Contains(nearbyCircuits[i].gameObject) && (nearbyCircuits[i].Active))
            {
                nearbyCircuits[i].Deactivate(gameObject, lastIndex);
            }
        }
    }

    private void UpdateIndex()
    {
        if (indices.Count == 0)
        {
            mixed = false;
            Active = false;
            Index = 10;
        }
        if (indices.Count == 1)
        {
            mixed = false;
            Index = indices[0];
        }
        else
        {
            if ((indices.Contains(0) && indices.Contains(2) && indices.Contains(4)) || (indices.Contains(0) && indices.Contains(3)) || (indices.Contains(1) && indices.Contains(4)) || (indices.Contains(5) && indices.Contains(2)))
            {
                mixed = true;
                Index = 6;
            }
            else if (indices.Contains(6))
            {
                Index = 6;
            }
            else if (indices.Contains(0) && indices.Contains(4))
            {
                mixed = true;
                Index = 5;
            }
            else if (indices.Contains(2) && indices.Contains(4))
            {
                mixed = true;
                Index = 3;
            }
            else if (indices.Contains(0) && indices.Contains(2))
            {
                mixed = true;
                Index = 1;
            }
        }
    }

    public int GetIndex(GameObject gameObj)
    {
        if (gameObj.GetComponent<PickupScript>() != null)
        {
            return gameObj.GetComponent<PickupScript>().Index;
        }
        else if (gameObj.GetComponent<CircuitScript>() != null)
        {
            return gameObj.GetComponent<CircuitScript>().Index;
        }
        else if (gameObj.GetComponent<PedestalScript>() != null)
        {
            return gameObj.GetComponent<PedestalScript>().Held.GetComponent<PickupScript>().Index;
        }

        throw new System.Exception();
    }

    public void Interact(GameObject gameObj)
    {
        if (gameObj.GetComponent<WallScript>() != null && gameObj.GetComponent<WallScript>().Color == index)
        {
            gameObj.SetActive(false);
        }
    }

    public void Reverse(GameObject gameObj)
    {
        if (gameObj.GetComponent<WallScript>() != null && gameObj.GetComponent<WallScript>().Color != index)
        {
            gameObj.SetActive(true);
        }
    }

    //public void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (!collision.isTrigger)
    //    {
    //        if (active)
    //        {
    //            if (source != null && ((source.GetComponent<CircuitScript>() != null && !source.GetComponent<CircuitScript>().Active) || (source.GetComponent<PedestalScript>() != null && source.GetComponent<PedestalScript>().Held != null)))
    //            {
    //                Active = false;
    //                source = null;
    //            }
    //            else if (collision.CompareTag("Circuit") && !collision.GetComponent<CircuitScript>().Active && collision.gameObject != source)
    //            {
    //                collision.gameObject.GetComponent<CircuitScript>().Source = gameObject;
    //                collision.gameObject.GetComponent<CircuitScript>().Active = true;
    //                collision.gameObject.GetComponent<CircuitScript>().Index = index;
    //            }
    //            else if (collision.CompareTag("Circuit") && collision.GetComponent<CircuitScript>().Active && CanMix(collision.GetComponent<CircuitScript>().Index))
    //            {
    //                collision.GetComponent<CircuitScript>().Mix(collision.GetComponent<CircuitScript>().Index);
    //            }
    //            else if (collision.CompareTag("Wall") && index == collision.GetComponent<WallScript>().Color)
    //            {
    //                wall = collision.gameObject;
    //                wall.SetActive(false);
    //            }
    //        }
    //        else if (!active)
    //        {
    //            if (wall != null)
    //            {
    //                wall.SetActive(true);
    //            }
    //        }
    //    }
    //}

    public bool CanMix(int otherIndex)
    {
        if ((index == 0 && otherIndex == 2) || (index == 2 && otherIndex == 0) || (index == 0 && otherIndex == 4) || (index == 4 && otherIndex == 0) || (index == 2 && otherIndex == 4) || (index == 4 && otherIndex == 2) || (index == 1 && otherIndex == 4) || (index == 4 && otherIndex == 1) || (index == 0 && otherIndex == 3) || (index == 3 && otherIndex == 0) || (index == 5 && otherIndex == 2) || (index == 2 && otherIndex == 5))
        {
            return true;
        }
        return false;
    }

    public void Mix(int otherIndex)
    {
        if ((index == 0 && otherIndex == 2) || (index == 2 && otherIndex == 0))
        {
            mixed = true;
            Index = 1;
        }
        else if ((index == 0 && otherIndex == 4) || (index == 4 && otherIndex == 0))
        {
            mixed = true;
            Index = 5;
        }
        else if ((index == 2 && otherIndex == 4) || (index == 4 && otherIndex == 2))
        {
            mixed = true;
            Index = 3;
        }
        else if ((index == 1 && otherIndex == 4) || (index == 4 && otherIndex == 1) || (index == 0 && otherIndex == 3) || (index == 3 && otherIndex == 0) || (index == 5 && otherIndex == 2) || (index == 2 && otherIndex == 5))
        {
            mixed = true;
            Index = 6;
        }
        else
        {
            throw new System.Exception();
        }
    }


}
