using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapMode : GazeableObject {

    public MapMode ActiveMapMode;

    public enum MapMode
    {
        MINIMAP,
        MENU
    }

    public Canvas ActiveCanvas;
    public Canvas InactiveCanvas;


    // Use this for initialization
    void Start () {

        Canvas ActiveCanvas = GetComponent<Canvas>();
        Canvas InactiveCanvas = GetComponent<Canvas>();

        ActiveMapMode = MapMode.MINIMAP;

        ActiveCanvas.gameObject.SetActive(true);
        InactiveCanvas.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnPress(RaycastHit HitInfo)
    {
        base.OnPress(HitInfo);

        ActiveMapMode = MapMode.MINIMAP;

        ActiveCanvas.gameObject.SetActive(false);
        InactiveCanvas.gameObject.SetActive(true);

    }
}
