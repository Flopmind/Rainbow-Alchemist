using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Direction { Right, Left, Up, Down }

public enum Action { Normal, Dialogue, Options}

public class OptionsManager
{
    private int currentOption;
    private int optionsNum;
    private string[] options;

    public int CurrentOption
    {
        get { return currentOption; }
    }

    public string[] Options
    {
        get { return options; }
        set { options = value; }
    }

    public OptionsManager(string[] opts)
    {
        currentOption = 0;
        options = opts;
    }

    public void ChangeSelection(Direction direct)
    {
        if(direct == Direction.Left)
        {
            currentOption--;
            if(currentOption < 0)
            {
                currentOption = options.Length - 1;
            }
        }
        else if(direct == Direction.Right)
        {
            currentOption++;
            if (currentOption >= options.Length)
            {
                currentOption = 0;
            }
        }
    }

    public string Select()
    {
        return options[currentOption];
    }

    public override string ToString()
    {
        string build = "";

        for(int i = 0; i < options.Length; i++)
        {
            if(i == currentOption)
            {
                build += ">> " + options[i] + " <<" + "\t";
            }
            else
            {
                build += "     " + options[i] + "     " + "\t";
            }
        }

        return build;
    }

}

public class PlayerScript : MonoBehaviour
{

    public GameManagerScript manager;
    public DialogueManagerScript dialogueManager;
    public float xMove;
    public float yMove;
    public float delay;

    private GameObject held;
    private Direction direct;
    private float timer;
    private Action myAction;
    private OptionsManager myOptions;
    private GameObject optionsObject;
    private bool moving;

    void Start()
    {
        timer = delay;
        myAction = Action.Normal;
        moving = false;
    }

    void Update()
    {
        if (!moving)
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3((int)(transform.position.x) + 0.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3((int)(transform.position.x) - 0.5f, transform.position.y, transform.position.z);
            }
        }
        
        //Determines input and actions based on the player's current conditions.
        switch (myAction)
        {
            case Action.Normal:

                //Movement Section
                if (timer >= delay)
                {
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        moving = true;
                        direct = Direction.Right;
                        if (!CheckBarrier(HitScan()))
                        {
                            gameObject.transform.position = new Vector3(gameObject.transform.position.x + xMove * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
                            timer = 0;
                        }
                        else if (CheckBarrier(HitScan()))
                        {
                            HandleGate(HitScan());
                            timer = 0;
                        }
                    }
                    else if (Input.GetAxis("Horizontal") < 0)
                    {
                        direct = Direction.Left;
                        if (!CheckBarrier(HitScan()))
                        {
                            gameObject.transform.position = new Vector3(gameObject.transform.position.x - xMove * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
                            timer = 0;
                        }
                        else if (CheckBarrier(HitScan()))
                        {
                            HandleGate(HitScan());
                            timer = 0;
                        }
                    }
                    else if (Input.GetAxis("Vertical") > 0)
                    {
                        direct = Direction.Up;
                        if (!CheckBarrier(HitScan()))
                        {
                            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yMove * Time.deltaTime, gameObject.transform.position.z);
                            timer = 0;
                        }
                        else if (CheckBarrier(HitScan()))
                        {
                            HandleGate(HitScan());
                            timer = 0;
                        }
                    }
                    else if (Input.GetAxis("Vertical") < 0)
                    {
                        direct = Direction.Down;
                        if (!CheckBarrier(HitScan()))
                        {
                            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - yMove * Time.deltaTime, gameObject.transform.position.z);
                            timer = 0;
                        }
                        else if (CheckBarrier(HitScan()))
                        {
                            HandleGate(HitScan());
                            timer = 0;
                        }
                    }
                    else
                    {
                        moving = false;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                }

                //"Interaction" Section

                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                {
                    HandleInteractionTags(HitScan());
                }


                //"Examination" Section

                if (Input.GetMouseButtonDown(1))
                {
                    HandleExaminationTags(HitScan());
                }

                //Level reset input

                if (Input.GetButtonDown("Fire3"))
                {
                    myOptions = new OptionsManager(new string[] { "Reset", "Cancel" });
                    dialogueManager.MainText.text = "Would you like to reset the level?";
                    myAction = Action.Options;
                    dialogueManager.Show();
                }

                break;

            case Action.Options:

                if(myOptions != null && timer >= delay)
                {
                    if (dialogueManager != null) dialogueManager.bottomText.text = myOptions.ToString();

                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        myOptions.ChangeSelection(Direction.Left);
                        if (dialogueManager != null) dialogueManager.bottomText.text = myOptions.ToString();
                        timer = 0;
                    }
                    else if (Input.GetAxis("Horizontal") > 0)
                    {
                        myOptions.ChangeSelection(Direction.Right);
                        if (dialogueManager != null) dialogueManager.bottomText.text = myOptions.ToString();
                        timer = 0;
                    }
                    else if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                    {
                        switch (myOptions.Select())
                        {
                            case "Pick Up":

                                held = optionsObject;
                                if (held != null) held.SetActive(false);

                                break;

                            case "Swap":

                                if (optionsObject != null && optionsObject.GetComponent<PedestalScript>() != null)
                                {
                                    GameObject tempHeld = held;
                                    held = optionsObject.GetComponent<PedestalScript>().ReturnObject();
                                    optionsObject.GetComponent<PedestalScript>().TakeObject(tempHeld);
                                }
                                else
                                {
                                    held.transform.position = optionsObject.transform.position;
                                    held.SetActive(true);
                                    held.GetComponent<PickupScript>().UpdateColor();
                                    held = optionsObject;
                                    if (held != null) held.SetActive(false);
                                }

                                break;

                            case "Mix":

                                if (held.GetComponent<PickupScript>().CanMix(optionsObject.GetComponent<PickupScript>()))
                                {
                                    held.GetComponent<PickupScript>().Mix(optionsObject);
                                    optionsObject.SetActive(false);
                                }

                                break;

                            case "Unmix":

                                GameObject temp = held.GetComponent<PickupScript>().Unmix();
                                switch (direct)
                                {
                                    case Direction.Right:
                                        temp.transform.position = new Vector3(gameObject.transform.position.x + xMove, gameObject.transform.position.y, gameObject.transform.position.z);
                                        break;

                                    case Direction.Left:
                                        temp.transform.position = new Vector3(gameObject.transform.position.x - xMove, gameObject.transform.position.y, gameObject.transform.position.z);
                                        break;

                                    case Direction.Up:
                                        temp.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yMove, gameObject.transform.position.z);
                                        break;

                                    case Direction.Down:
                                        temp.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - yMove, gameObject.transform.position.z);
                                        break;
                                }
                                temp.SetActive(true);

                                break;

                            case "Drop":

                                switch (direct)
                                {
                                    case Direction.Right:
                                        held.transform.position = new Vector3(gameObject.transform.position.x + xMove, gameObject.transform.position.y, gameObject.transform.position.z);
                                        break;

                                    case Direction.Left:
                                        held.transform.position = new Vector3(gameObject.transform.position.x - xMove, gameObject.transform.position.y, gameObject.transform.position.z);
                                        break;

                                    case Direction.Up:
                                        held.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yMove, gameObject.transform.position.z);
                                        break;

                                    case Direction.Down:
                                        held.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - yMove, gameObject.transform.position.z);
                                        break;
                                }
                                held.SetActive(true);
                                held.GetComponent<PickupScript>().UpdateColor();
                                held = null;

                                break;

                            case "Pull":

                                optionsObject.GetComponent<LeverScript>().FadeTarget();

                                break;

                            case "Place Item":

                                optionsObject.GetComponent<PedestalScript>().TakeObject(held);
                                held = null;

                                break;

                            case "Take Item":
                                
                                held = optionsObject.GetComponent<PedestalScript>().ReturnObject();

                                break;

                            case "Reset":

                                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                                break;
                        }

                        myOptions = null;
                        optionsObject = null;
                        myAction = Action.Normal;
                        timer = 0;
                        dialogueManager.BottomText.text = "";
                        dialogueManager.Close();
                    }
                }
                else if(timer < delay)
                {
                    timer += Time.deltaTime;
                }

                break;

        }
    }

    //Scans in front of the character and returns and array of the hits.
    public RaycastHit2D[] HitScan()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        switch (direct)
        {
            case Direction.Right:
                gameObject.GetComponent<Collider2D>().Raycast(new Vector2(1, 0), hits, 0.75f * xMove);
                break;

            case Direction.Left:
                gameObject.GetComponent<Collider2D>().Raycast(new Vector2(-1, 0), hits, 0.75f * xMove);
                break;

            case Direction.Up:
                gameObject.GetComponent<Collider2D>().Raycast(new Vector2(0, 1), hits, 0.75f * yMove);
                break;

            case Direction.Down:
                gameObject.GetComponent<Collider2D>().Raycast(new Vector2(0, -1), hits, 0.75f * yMove);
                break;
        }
        return hits;
    }

    //Checks to see if there is an obstacle where the player is next going to move based on the given array.
    public bool CheckBarrier(RaycastHit2D[] hits)
    {
        if (IsRaycastEmpty(hits)) return false;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && !hits[i].collider.isTrigger && (hits[i].collider.CompareTag("Gate") || hits[i].collider.CompareTag("Pickup") || hits[i].collider.CompareTag("Obstacle") || hits[i].collider.CompareTag("Lever") || hits[i].collider.CompareTag("Circuit") || hits[i].collider.CompareTag("Wall") || hits[i].collider.CompareTag("Pedestal"))) return true;
        }
        return false;
    }

    //Handles whether or not the player should pass through the gate and other related changes, such as the color of the gate and the held object.
    public void HandleGate(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.CompareTag("Gate") && manager.CheckEqual(hits[i].collider))
            {
                GameObject gate = manager.GetEqual(hits[i].collider);

                //Case where the player simply passes through with held object if applicable
                if ((gate.GetComponent<GateScript>().Index == 7 && (held == null || (held != null && held.GetComponent<PickupScript>().Index != 7))) || (held != null && held.GetComponent<PickupScript>().Index == 7 && gate.GetComponent<GateScript>().Index == 7 && held.GetComponent<PickupScript>().LastIndex != gate.GetComponent<GateScript>().LastIndex))
                {
                    PassThrough(gate);
                }

                //Case where gate and held object turn white
                else if (held != null && gate.GetComponent<GateScript>().Index == held.GetComponent<PickupScript>().Index && held.GetComponent<PickupScript>().Index != 7 && gate.GetComponent<GateScript>().Index != 7)
                {
                    gate.GetComponent<GateScript>().Activate();
                    held.GetComponent<PickupScript>().ChangeColor(7);
                    gate.GetComponent<GateScript>().ChangeColor(7);
                    PassThrough(gate);
                    if (gate.GetComponent<GateScript>().ActivateShift) Shifter.ShiftColors();
                }

                //Case where gate and held object are white and revert back to previous color.
                else if (held != null && held.GetComponent<PickupScript>().Index == 7 && gate.GetComponent<GateScript>().Index == 7 && held.GetComponent<PickupScript>().LastIndex == gate.GetComponent<GateScript>().LastIndex)
                {
                    PassThrough(gate);
                    if (gate.GetComponent<GateScript>().ActivateShift) Shifter.ShiftColors();
                    held.GetComponent<PickupScript>().Revert();
                    gate.GetComponent<GateScript>().Revert();
                    gate.GetComponent<GateScript>().Deactivate();
                }
            }
        }
    }

    //Passes through, or over, the given gate object depending on the direction the player is facing.
    public void PassThrough(GameObject gate)
    {
        switch (direct)
        {
            case Direction.Right:
                gameObject.transform.position = new Vector3(gate.transform.position.x + (int)(1 * xMove), gameObject.transform.position.y, gameObject.transform.position.z);
                break;

            case Direction.Left:
                gameObject.transform.position = new Vector3(gate.transform.position.x - (int)(1 * xMove), gameObject.transform.position.y, gameObject.transform.position.z);
                break;

            case Direction.Up:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gate.transform.position.y + (int)(1 * yMove), gameObject.transform.position.z);
                break;

            case Direction.Down:
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gate.transform.position.y - (int)(1 * yMove), gameObject.transform.position.z);
                break;
        }
    }

    //Interacts with objects in various ways depending on tags and validity.
    public void HandleInteractionTags(RaycastHit2D[] hits)
    {
        if (IsRaycastEmpty(hits) && held != null)
        {
            if (held.GetComponent<PickupScript>().Mixed && held.GetComponent<PickupScript>().Index != 7) myOptions = new OptionsManager(new string[] { "Drop", "Unmix", "Cancel" });
            else myOptions = new OptionsManager(new string[] { "Drop", "Cancel" });
            dialogueManager.MainText.text = "You are holding an orb that is " + held.GetComponent<PickupScript>().GetColor() + ". What would you like to do?";
            dialogueManager.Show();
            myAction = Action.Options;
        }
        else
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null && hits[i].collider.CompareTag("Pickup") && manager.CheckEqual(hits[i].collider))
                {
                    if (held == null)
                    {
                        optionsObject = manager.GetEqual(hits[i].collider);
                        myOptions = new OptionsManager(new string[] { "Pick Up", "Cancel" });
                        dialogueManager.MainText.text = "You have selected an orb that is " + optionsObject.GetComponent<PickupScript>().GetColor() + ". What would you like to do?";
                        myAction = Action.Options;
                        dialogueManager.Show();
                    }
                    else
                    {
                        optionsObject = manager.GetEqual(hits[i].collider);
                        if(optionsObject.GetComponent<PickupScript>().Index != 7 && held.GetComponent<PickupScript>().Index != 7 && held.GetComponent<PickupScript>().CanMix(optionsObject.GetComponent<PickupScript>())) myOptions = new OptionsManager(new string[] { "Swap", "Mix", "Cancel" });
                        else myOptions = new OptionsManager(new string[] { "Swap", "Cancel" });
                        dialogueManager.MainText.text = "You are holding an orb that is " + held.GetComponent<PickupScript>().GetColor() + " and you have selected an orb that is " + optionsObject.GetComponent<PickupScript>().GetColor() + ". What would you like to do?";
                        dialogueManager.Show();
                        myAction = Action.Options;
                    }

                }
                else if(hits[i].collider != null && hits[i].collider.CompareTag("Lever") && manager.CheckEqual(hits[i].collider))
                {
                    myAction = Action.Options;
                    myOptions = new OptionsManager(new string[] { "Pull", "Cancel" });
                    dialogueManager.MainText.text = "You have selected a lever what would you like to do?";
                    dialogueManager.Show();
                    optionsObject = manager.GetEqual(hits[i].collider);

                }
                else if (hits[i].collider != null && !hits[i].collider.isTrigger && hits[i].collider.CompareTag("Pedestal") && manager.CheckEqual(hits[i].collider))
                {
                    if (held != null && manager.GetEqual(hits[i].collider).GetComponent<PedestalScript>().Held == null)
                    {
                        myAction = Action.Options;
                        myOptions = new OptionsManager(new string[] { "Place Item", "Cancel" });
                        dialogueManager.MainText.text = "You have selected a Pedestal what would you like to do?";
                        dialogueManager.Show();
                        optionsObject = manager.GetEqual(hits[i].collider);
                    }
                    else if (held == null && manager.GetEqual(hits[i].collider).GetComponent<PedestalScript>().Held != null)
                    {
                        myAction = Action.Options;
                        myOptions = new OptionsManager(new string[] { "Take Item", "Cancel" });
                        dialogueManager.MainText.text = "You have selected a Pedestal what would you like to do?";
                        dialogueManager.Show();
                        optionsObject = manager.GetEqual(hits[i].collider);
                    }
                    else if (held != null && manager.GetEqual(hits[i].collider).GetComponent<PedestalScript>().Held != null)
                    {
                        myAction = Action.Options;
                        myOptions = new OptionsManager(new string[] { "Swap", "Cancel" });
                        dialogueManager.MainText.text = "You have selected a Pedestal what would you like to do?";
                        dialogueManager.Show();
                        optionsObject = manager.GetEqual(hits[i].collider);
                    }
                }
            }
        }
    }

    //Displays a string in a dialouge box depending on the object in front of the player.
    public void HandleExaminationTags(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.CompareTag("Gate") && manager.CheckEqual(hits[i].collider))
            {
                myAction = Action.Options;
                myOptions = new OptionsManager(new string[] { "" });
                dialogueManager.MainText.text = "The gate in front of you is currently " + manager.GetComponent<GameManagerScript>().GetEqual(hits[i].collider).GetComponent<GateScript>().GetColor() + "." ;
                dialogueManager.Show();
                optionsObject = manager.GetEqual(hits[i].collider);
            }
        }
    }

    //Returns true if the array is empty; returns false otherwise.
    public bool IsRaycastEmpty(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null) return false;
        }
        return true;
    }
}
