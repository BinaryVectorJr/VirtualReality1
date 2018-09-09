﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButton : GazeableButton
{
    [SerializeField]
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
