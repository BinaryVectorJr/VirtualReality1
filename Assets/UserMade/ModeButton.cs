using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButton : GazeableButton
{
    [SerializeField]    //Acccording to this option, it makes the private variables appear in the editor. kinda like UProperty
    private InputMode Mode;

    public override void OnPress(RaycastHit HitInfo)
    {
        base.OnPress(HitInfo);

        if (parentPanel.currentActiveButton != null)
        {
            Player.instance.ActiveMode = Mode;
        }
    }
}
