using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureButton : GazeableButton {

    public Object prefab;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        //Set Player mode to PlaceFurniture
        Player.instance.ActiveMode = InputMode.FURNITURE;

        //Set the current prefab to the selected prefab
        Player.instance.ActiveFurniturePrefab = prefab;
    }
}
