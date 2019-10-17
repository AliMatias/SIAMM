using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipsObject : MonoBehaviour, IPointerClickHandler
{
    //para luego definirle donde estara posicionado
    private Transform parent;
    private TipsManager TipManager;
    private float lifetime = 20; //in seconds

    public GameObject toolTipGlobe;
    public GameObject toolTipInitial;

    private int idTip;
    
    public int IdTip { get => idTip; set => idTip = value; }

    public Transform Parent { get => parent; set => parent = value; }

    private void Awake()
    {
        TipManager = FindObjectOfType<TipsManager>();
        Invoke("DestroyMe", lifetime); /*el GO se destruye solo al lapso de tiempo configurado */
    }

    private void CancelAutoDestroy()
    {
      CancelInvoke("DestroyMe");
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }


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
            //manejado por el scrip LEAN DRAG.
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            CloseAsistent();
        }
    }


    public void setText (string text)
    {
        toolTipGlobe.GetComponentInChildren<Text>().text = text;   
    }

    /**
     * metodo principal para el manejo de mostrar el tooltip con el TIP
     */
    public void ShowTip()
    {
        Debug.Log("MUESTRO TIP"); //el tipito esta mostrandose en pantalla

        //si hago click luego de mostrar.. o destruyo.. o vuelvo al estado anterior y muestro el tooltip original
        //que hacer con el tiempo.... que tiene para cerrarse solo...
        CancelAutoDestroy();

        if (toolTipInitial.activeSelf)
        {
            //tengo que esconder el original "sabias que"
            toolTipInitial.SetActive(false);
            toolTipGlobe.GetComponent<UIFader>().FadeInAndOut(toolTipGlobe);
        }
        else
        {
            toolTipGlobe.GetComponent<UIFader>().FadeInAndOut(toolTipGlobe);
            toolTipInitial.SetActive(true);           
        }
    }


    /**
     * metodo principal para el manejo de cerrar el tooltip con el TIP
     */
    public void CloseAsistent()
    {
        Debug.Log("CIERRO TIP Y ASISTENTE");//BOTON DERECHO
        //tendria que parar el tiempo
        CancelAutoDestroy();

        gameObject.AddComponent<UIAction>();
        GetComponent<UIAction>().OptionsTips(); 
    }


    private void setInactiveRayCast(CanvasGroup objActivar)
    {
        objActivar.blocksRaycasts = false;
    }

    private void setActiveRayCast(CanvasGroup objActivar)
    {
        objActivar.blocksRaycasts = true;
    }

}
