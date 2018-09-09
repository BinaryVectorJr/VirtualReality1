using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
    NONE,
    TELEPORT,
    WALK
}

public class Player : MonoBehaviour {

    public static Player instance;

    public InputMode ActiveMode = InputMode.NONE;

    void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance.gameObject);    //There can only be one player
        }

        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
