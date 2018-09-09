using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGazeable : GazeableObject
{
    public override void OnPress(RaycastHit HitInfo)
    {
        base.OnPress(HitInfo);

        if (Player.instance.ActiveMode == InputMode.TELEPORT)
        {
            Vector3 DestLoc = HitInfo.point;    //get the x,y,z coords of wherever we are looking at akak the line from camera hits
            DestLoc.y = Player.instance.transform.position.y;   //this keeps height same, and moves only in x and z axis aka the floor

            Player.instance.transform.position = DestLoc;
        }
    }
}
