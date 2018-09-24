using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : GazeableObject
{
    private VRCanvas parentPanel;

    private Transform originalParent;

    private InputMode savedInputMode = InputMode.NONE;

    // Use this for initialization
    void Start()
    {
        parentPanel = GetComponentInParent<VRCanvas>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPress(RaycastHit HitInfo)
    {
        base.OnPress(HitInfo);

        //Make the entire canvas a child of the camera to move with it.
        originalParent = parentPanel.transform.parent;
        parentPanel.transform.parent = Camera.main.transform;   //here the panel becomes a child of the camera

        //Save the old input mode and set the current mode to drag.
        savedInputMode = Player.instance.ActiveMode;
        Player.instance.ActiveMode = InputMode.DRAG;
    }

    public override void OnRelease(RaycastHit HitInfo)
    {
        base.OnRelease(HitInfo);

        //Reapply the old values.
        parentPanel.transform.parent = originalParent;
        Player.instance.ActiveMode = savedInputMode;
    }
}
