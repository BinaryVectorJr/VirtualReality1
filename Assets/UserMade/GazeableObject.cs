using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour
{
    public bool IsTranformable = false;

    private int ObjectLayer;
    private const int IGNORE_RAYCAST_LAYER = 2;

    public virtual void OnGazeEnter(RaycastHit HitInfo)
    {
        Debug.Log("Gaze entered on " + gameObject.name);
    }

    public virtual void OnGazeHold(RaycastHit HitInfo)
    {
        Debug.Log("Gaze held on " + gameObject.name);
    }

    public virtual void OnGazeLeave() //No more key information to pass so no hit info needed
    {
        Debug.Log("Gaze left from " + gameObject.name);
    }

    public virtual void OnPress(RaycastHit HitInfo)
    {
        Debug.Log("Button pressed!");

        if (IsTranformable)
        {
            ObjectLayer = gameObject.layer;

            gameObject.layer = IGNORE_RAYCAST_LAYER;
        }
    }

    public virtual void OnHold(RaycastHit HitInfo)
    {
        Debug.Log("Button held!");
        if (IsTranformable)
        {
            GazeTransform(HitInfo);
        }
    }

    public virtual void OnRelease(RaycastHit HitInfo)
    {
        Debug.Log("Button released!");

        if (IsTranformable)
        {
            gameObject.layer = ObjectLayer;
        }

    }

    public virtual void GazeTransform(RaycastHit HitInfo)
    {
        switch (Player.instance.ActiveMode)
        {
            case InputMode.TRANSLATE:
                GazeTranslate(HitInfo);
                break;

            case InputMode.ROTATE:
                GazeRotate(HitInfo);
                break;

            case InputMode.SCALE:
                GazeScale(HitInfo);
                break;
        }
    }

    public virtual void GazeTranslate(RaycastHit HitInfo)
    {
        //Move the object to some other position on the floor
        if (HitInfo.collider != null && HitInfo.collider.GetComponent<FloorGazeable>())
        {
            transform.position = HitInfo.point;
        }
        else
        {
            Debug.LogError("Cannot move!");
        }
    }

    public virtual void GazeRotate(RaycastHit HitInfo)
    {

    }

    public virtual void GazeScale(RaycastHit HitInfo)
    {

    }
}
