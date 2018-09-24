using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour
{
    public bool IsTranformable = false;

    private int ObjectLayer;
    private const int IGNORE_RAYCAST_LAYER = 2;

    private Vector3 InitialCameraRotation;
    private Vector3 InitialObjectRotation;

    private Vector3 InitialObjectScale;

    public void Start()
    {
        GetComponentInChildren<cakeslice.Outline>().enabled = false;    //For the outlining when looked at part
    }

    public virtual void OnGazeEnter(RaycastHit HitInfo)
    {
        Debug.Log("Gaze entered on " + gameObject.name);

        //If this object is furniture and the player is in a transformation mode, enable outline.
        if (IsTranformable && (Player.instance.ActiveMode == InputMode.TRANSLATE || Player.instance.ActiveMode == InputMode.ROTATE || Player.instance.ActiveMode == InputMode.SCALE))
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = true;
        }

    }

    public virtual void OnGazeHold(RaycastHit HitInfo)
    {
        Debug.Log("Gaze held on " + gameObject.name);
    }

    public virtual void OnGazeLeave() //No more key information to pass so no hit info needed
    {
        Debug.Log("Gaze left from " + gameObject.name);

        //To disable outline once the player stops looking at it.
        if (IsTranformable)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = false;
        }
    }

    public virtual void OnPress(RaycastHit HitInfo)
    {
        Debug.Log("Button pressed!");

        if (IsTranformable)
        {
            ObjectLayer = gameObject.layer;
            gameObject.layer = IGNORE_RAYCAST_LAYER;

            //Get initial rotation values
            InitialCameraRotation = Camera.main.transform.rotation.eulerAngles;
            InitialObjectRotation = transform.rotation.eulerAngles;

            //Get initial scale
            InitialObjectScale = transform.localScale;
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
        float RotationSpeed = 5.0f;

        Vector3 CurrentCameraRotation = Camera.main.transform.rotation.eulerAngles; //Unity stores rotations in default Quarternian format - gotta convert to Vector3 for real life x,y,z values
        Vector3 CurrentObjectRotation = transform.rotation.eulerAngles;

        //Calculate how many degrees we've moved our head after pressing button
        Vector3 DeltaRotation = CurrentCameraRotation - InitialCameraRotation;

        //Now to set the rotation of object
        Vector3 NewRotation = new Vector3(CurrentObjectRotation.x, InitialObjectRotation.y + (DeltaRotation.y * RotationSpeed), CurrentObjectRotation.z);

        //Set the rotation in Quarternian terms
        transform.rotation = Quaternion.Euler(NewRotation);
    }

    public virtual void GazeScale(RaycastHit HitInfo)
    {
        float ScaleSpeed = 0.1f;

        float ScaleFactor = 1.0f;

        Vector3 CurrentCameraRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 DeltaRotation = CurrentCameraRotation - InitialCameraRotation;

        if (DeltaRotation.x < 0 && DeltaRotation.x > -180.0f || DeltaRotation.x > 180.0f && DeltaRotation.x < 360.0f)
        {
            if (DeltaRotation.x > 180.0f)   //To strictly put the range between -180 and 180
            {
                DeltaRotation.x = 360.0f - DeltaRotation.x;
            }

            //Looking up
            ScaleFactor = 1.0f + Mathf.Abs(DeltaRotation.x) * ScaleSpeed;   //To keep the range between 0 and 180 degrees

        }
        else
        {
            if (DeltaRotation.x < -180.0f)
            {
                DeltaRotation.x = 360.0f + DeltaRotation.x;
            }

            //Looking down
            //(Mathf.Abs(DeltaRotation.x) * ScaleSpeed) gives value between 0 and 180; we divide that by 180 to get
            //a normalized value b/w 0 and 1, so that 1-x returns a positive number
            //Then we compare whichever one between 0.1 and the calculated value is larger, as we do not want to scale
            //below 0.1 i.e. negative values

            //1/Scalespeed used coz scalespeed is a small no. and after a point the shrinking becomes to slow to be noticeable
            ScaleFactor = Mathf.Max(0.1f, 1.0f - ((Mathf.Abs(DeltaRotation.x) * (1.0f / ScaleSpeed)) / 180.0f));
        }

        transform.localScale = ScaleFactor * InitialObjectScale;    //Finally set the scale of the object

    }
}
