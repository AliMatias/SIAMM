using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRaycastControllerCam : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    private CameraManager cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cam.enabled = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cam.enabled = true;//solo cuando el mouse sale del panel de lista se activa la cam
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        cam.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        cam.enabled = false;
    }
}
