using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour
{
    public bool IsTranformable = false;

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

    }

    public virtual void GazeRotate(RaycastHit HitInfo)
    {

    }

    public virtual void GazeScale(RaycastHit HitInfo)
    {

    }
}
