using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{
    private WebCamTexture CamTexture = null;

    void Start()
    {
        CamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = CamTexture;
        CamTexture.Play();
    }

    void Update()
    {

    }

}
