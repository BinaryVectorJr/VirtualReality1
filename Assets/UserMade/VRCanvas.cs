using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvas : MonoBehaviour
{

    public GazeableButton currentActiveButton;

    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetActiveButton(GazeableButton activeButton)
    {

        //If there was previously an active button, disable it.
        if (currentActiveButton != null)
        {
            currentActiveButton.SetButtonColor(unselectedColor);
        }

        if (activeButton != null && currentActiveButton != activeButton)
        {
            currentActiveButton = activeButton;
            currentActiveButton.SetButtonColor(selectedColor);
        }

        else
        {
            Debug.Log("Resetting");
            currentActiveButton = null;
            Player.instance.ActiveMode = InputMode.NONE;
        }
    }

}
