using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelClick : MonoBehaviour
{
    public bool entrando = false;
    private Vector2 currentPosition;

    public bool Entrando { get => entrando; set => entrando = value; }

    // Update is called once per frame
    void Update()
    {
        if (entrando)
        {
            clickMenuOpenClose();
        }
        else
        {
            clickMenuOpenClose();
        }
    }

    public void clickMenuOpenClose()
    {
        var oldVector = default(Vector2);
        var target = gameObject.GetComponent<Lean.Gui.LeanDrag>().TargetTransform;

        var newVector = default(Vector2);

        var anchoredPosition = target.anchoredPosition;

        currentPosition += newVector - oldVector;

        if (gameObject.GetComponent<Lean.Gui.LeanDrag>().Horizontal == true)
        {
            anchoredPosition.x = currentPosition.x;
        }

        if (gameObject.GetComponent<Lean.Gui.LeanDrag>().Vertical == true)
        {
            anchoredPosition.y = currentPosition.y;
        }

        //dependiendo de la accion a realizar
        if (entrando)
        {
            ClampPositionOpen(ref anchoredPosition);
        }
        else
        {
            ClampPositionClose(ref anchoredPosition);
        }

        // Offset the anchored position by the difference
        target.anchoredPosition = anchoredPosition;
    }

    //metodo para el moviemiento de apertura (los valores los objeto de la clase LEAN GUI DRAG a traves de los SET correspondientes)
    private void ClampPositionOpen(ref Vector2 anchoredPosition)
    {
        if (gameObject.GetComponent<Lean.Gui.LeanDrag>().HorizontalClamp == true)
        {
            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, gameObject.GetComponent<Lean.Gui.LeanDrag>().HorizontalMin, gameObject.GetComponent<Lean.Gui.LeanDrag>().HorizontalMax);
        }

        if (gameObject.GetComponent<Lean.Gui.LeanDrag>().VerticalClamp == true)
        {
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, gameObject.GetComponent<Lean.Gui.LeanDrag>().VerticalMin, gameObject.GetComponent<Lean.Gui.LeanDrag>().VerticalMax);
        }
    }

    //metodo para el moviemiento de cierre (los valores los objeto de la clase LEAN GUI DRAG a traves de los SET correspondientes)
    private void ClampPositionClose(ref Vector2 anchoredPosition)
    {
        if (gameObject.GetComponent<Lean.Gui.LeanDrag>().HorizontalClamp == true)
        {
            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, gameObject.GetComponent<Lean.Gui.LeanDrag>().HorizontalMax, gameObject.GetComponent<Lean.Gui.LeanDrag>().HorizontalMin);
        }

        if (gameObject.GetComponent<Lean.Gui.LeanDrag>().VerticalClamp == true)
        {
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, gameObject.GetComponent<Lean.Gui.LeanDrag>().VerticalMax, gameObject.GetComponent<Lean.Gui.LeanDrag>().VerticalMin);
        }
    }

    //metodo para UIAction
    public void CloseMenu()
    {
        Entrando = false;
        Destroy(this, 0.5f);
    }

    //metodo para UIAction
    public void OpenMenu()
    {
        Entrando = true;
    }
}
