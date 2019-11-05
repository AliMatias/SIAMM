using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldBlockCam : MonoBehaviour
{
    private CameraManager cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    public void DisabledCam()
    {
        cam.enabled = false;
    }

    public void EnabledCam()
    {
        cam.enabled = true;
    }
}
