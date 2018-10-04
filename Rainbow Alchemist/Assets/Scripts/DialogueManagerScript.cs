using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerScript : MonoBehaviour {

    public GameObject dialogueBox;
    public Text mainText;
    public Text bottomText;
    private OptionsManager options;

    public OptionsManager Options
    {
        get { return options; }
        set { options = value; }
    }

    public Text MainText
    {
        get { return mainText; }
        set { mainText = value; }
    }

    public Text BottomText
    {
        get { return bottomText; }
        set { bottomText = value; }
    }

    void Start ()
    {
        Close();
	}

    //Displays the given dialogueBox object.
    public void Show()
    {
        dialogueBox.SetActive(true);
    }

    //Stops displaying the given dialogueBox object.
    public void Close()
    {
        dialogueBox.SetActive(false);
    }
}
