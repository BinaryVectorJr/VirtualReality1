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
        else if (Player.instance.ActiveMode == InputMode.FURNITURE)
        {
            //Create the furniture
            GameObject PlacedFurniture = GameObject.Instantiate(Player.instance.ActiveFurniturePrefab) as GameObject;
            //GameOnjcet.Instantiate returns just an object, however we can write "as GameObject" to specify to the function that once executed, "save" it as of the type GameObject

            //Place the furniture on the floor wherever the raycast hits the collider of the floor
            PlacedFurniture.transform.position = HitInfo.point;
        }
    }
}
