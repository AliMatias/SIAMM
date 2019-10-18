using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipsObject : MonoBehaviour, IPointerClickHandler
{

    #region atributos
    
    //para luego definirle donde estara posicionado
    private Transform parent;
    private TipsManager TipManager;
    private float lifetime = 20; //in seconds

    public GameObject toolTipGlobe;
    public GameObject toolTipInitial;

    private int idTip;
    
    public int IdTip { get => idTip; set => idTip = value; }

    public Transform Parent { get => parent; set => parent = value; }

    #endregion

    private void Awake()
    {
        TipManager = FindObjectOfType<TipsManager>();
        Invoke("DestroyMe", lifetime); /*el GO se destruye solo al lapso de tiempo configurado */
    }

    /*
     * metodos para manejo de timer y para cancelarlo
     */
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
            //manejado por el scrip LEAN DRAG. Se deja el evento para el doble posicionamiento del pointer
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            CloseAsistent();
        }
    }

    /*
     * metodo para que desde el manager setee el texto a mostrar
     */
    public void setText (string text)
    {
        toolTipGlobe.GetComponentInChildren<Text>().text = text;   
    }

    /*
     * metodo principal para el manejo de mostrar el tooltip con el TIP
     */
    public void ShowTip()
    {
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
            //vuelvo a estado original
            toolTipGlobe.GetComponent<UIFader>().FadeInAndOut(toolTipGlobe);
            toolTipInitial.SetActive(true);

            //de nuevo activo el timer.. 
            Invoke("DestroyMe", lifetime); /*el GO se destruye solo al lapso de tiempo configurado */
        }
    }


    /*
     * metodo principal para el manejo de cerrar el tooltip con el TIP
     */
    public void CloseAsistent()
    {
        //tendria que parar el tiempo por si se cierra antes de la interaccion!
        CancelAutoDestroy();

        //podria mostrar una notificacion.. que "diga ocultando...." y por lo menos le avisa al usuario

        TipManager.DeleteTip(this);
    }
}
