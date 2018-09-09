using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour
{
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
    }

    public virtual void OnRelease(RaycastHit HitInfo)
    {
        Debug.Log("Button released!");
    }

}
