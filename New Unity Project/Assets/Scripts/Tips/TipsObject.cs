using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TipsObject : MonoBehaviour, IPointerClickHandler
{
    //para luego definirle donde estara posicionado
    private Transform parent;

    private TipsManager TipManager;

    private CanvasGroup toolTipGlobe;
    private CanvasGroup SiammTip;

    private void Awake()
    {
        TipManager = FindObjectOfType<TipsManager>();
    }

    public Transform Parent { get => parent; set => parent = value; }

/*  cuando se destruye la instancia de este script, tengo que destruir
*   manualmente el gameObject al cual está asignado este script
*/
void OnDestroy()
    {
        Destroy(gameObject);
    }


    //Según el botón apretado del mouse llama al evento indicado
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShowTip();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            CloseAsistent();
        }
    }


    #region Metodos

    /**
     * metodo principal para el manejo de mostrar el tooltip con el TIP
     */
    public void ShowTip()
    {
        Debug.Log("MUESTRO TIP"); //el tipito esta mostrandose en pantalla
    }


    /**
     * metodo principal para el manejo de cerrar el tooltip con el TIP
     */
    public void CloseAsistent()
    {
        Debug.Log("CIERRO TIP Y ASISTENTE");//BOTON DERECHO
        //gameObject.GetComponent<UIFader>().FadeInAndOut(gameObject);
        //TipManager.DeleteTip(this);
    }


    private void setInactiveRayCast(CanvasGroup objActivar)
    {
        objActivar.blocksRaycasts = false;
    }

    private void setActiveRayCast(CanvasGroup objActivar)
    {
        objActivar.blocksRaycasts = true;
    }


    #endregion

}
