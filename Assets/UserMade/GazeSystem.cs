using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour {

    public GameObject reticle;

    //Set the color for the reticle
    public Color InactiveReticleColor = Color.grey;
    public Color ActiveReticleColor = Color.green;

    private GazeableObject CurrentGazeObject;
    private GazeableObject CurrentSelectedObject;

    private RaycastHit LastHit;

	// Use this for initialization
	void Start () {

        SetReticleColor(InactiveReticleColor);  //Initialize the reticle to grey color at start of play
	}
	
	// Update is called once per frame
	void Update () {

        ProcessGaze();
        CheckForInput(LastHit);
		
	}

    public void ProcessGaze()
    {
        Ray RaycastRay = new Ray(transform.position, transform.forward);
        RaycastHit HitInfo;

        Debug.DrawRay(RaycastRay.origin, RaycastRay.direction * 100); //Draws a straight line to wherever the camera is pointing and reticle at end

        if (Physics.Raycast(RaycastRay, out HitInfo))
        {
            //Get gameobject from hitinfo - this is why colliders are required to have a functioning raycast system
            GameObject HitObject = HitInfo.collider.gameObject;

            //Get the GazeableObject component from the object hit
            GazeableObject GazeObject = HitObject.GetComponentInParent<GazeableObject>();

            if (GazeObject != null) //If an object has GazeableObject component
            {
                if (GazeObject != CurrentGazeObject) //If its the first time we are looking at the GazeObject
                {
                    ClearCurrentObject();
                    CurrentGazeObject = GazeObject;
                    CurrentGazeObject.OnGazeEnter(HitInfo);
                    SetReticleColor(ActiveReticleColor);
                }
                else
                {
                    CurrentGazeObject.OnGazeHold(HitInfo);
                }
            }
            else
            {
                ClearCurrentObject(); //No GazeableObject component present so we don't need to worry about it
            }

            LastHit = HitInfo;

        }
        else
        {
            ClearCurrentObject();   //No physics component present so we don't need to worry about it
        }
    }

    //To set the color of the reticle
    private void SetReticleColor(Color ReticleColor)
    {
        reticle.GetComponent<Renderer>().material.SetColor("_Color", ReticleColor);
    }

    private void CheckForInput(RaycastHit HitInfo)
    {
        if (Input.GetMouseButtonDown(0) && CurrentGazeObject != null)   //Check for button press
        {
            CurrentSelectedObject = CurrentGazeObject;
            CurrentSelectedObject.OnPress(HitInfo);
        }

        else if (Input.GetMouseButton(0) && CurrentSelectedObject != null)  //Check for button press and hold
        {
            CurrentSelectedObject.OnHold(HitInfo);
        }

        else if (Input.GetMouseButtonUp(0) && CurrentSelectedObject != null)    //Check for button release
        {
            CurrentSelectedObject.OnRelease(HitInfo);
            CurrentSelectedObject = null;
        }
    }

    private void ClearCurrentObject()
    {
        if (CurrentGazeObject != null)
        {
            CurrentGazeObject.OnGazeLeave();
            SetReticleColor(InactiveReticleColor);
            CurrentGazeObject = null;
        }
    }
}
