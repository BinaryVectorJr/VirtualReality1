using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
    NONE,
    TELEPORT,
    WALK,
    FLOAT,
    FURNITURE,
    TRANSLATE,
    ROTATE,
    SCALE,
    DRAG
}

public class Player : MonoBehaviour {

    public static Player instance;  //creates a player instance that can be called anywhere in the program

    public Object ActiveFurniturePrefab;

    public InputMode ActiveMode = InputMode.NONE;

    [SerializeField]
    private float Speed = 3.0f;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject forwardWall;
    public GameObject backWall;
    public GameObject ceiling;
    public GameObject floor;


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
	void Update ()
    {
        Walk();
	}

    public void Walk()
    {
        if (Input.GetMouseButton(0) && ActiveMode == InputMode.WALK)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 NewPos = transform.position + forward * Time.deltaTime * Speed;

            if (//NewPos.y > rightWall.transform.position.y && NewPos.y < leftWall.transform.position.y &&
                NewPos.y < ceiling.transform.position.y && NewPos.y > floor.transform.position.y)
                //NewPos.z > backWall.transform.position.z && NewPos.z < forwardWall.transform.position.z)
            {
                transform.position = NewPos;    //need to sort the confinement using colliders
            }
        }
        else if (ActiveMode == InputMode.FLOAT)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 NewPos = transform.position + forward * Time.deltaTime * (Speed/5);

            if (//NewPos.y > rightWall.transform.position.y && NewPos.y < leftWall.transform.position.y &&
                NewPos.y < ceiling.transform.position.y && NewPos.y > floor.transform.position.y)
            //NewPos.z > backWall.transform.position.z && NewPos.z < forwardWall.transform.position.z)
            {
                transform.position = NewPos;    //need to sort the confinement using colliders
            }
        }
    }
}
