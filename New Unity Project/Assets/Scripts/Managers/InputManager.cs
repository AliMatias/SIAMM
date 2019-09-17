using UnityEngine;
using UnityEngine.UI;
using System;

public class InputManager : MonoBehaviour
{
    #region Atributos
    private AtomManager atomManager;
    private UIFader UIFader;  
    private LoadTper loadTPer;
    private BasicInfoLoader BasicInfoLoader;
    private DetailInfoLoader DetailInfoLoader;
    //asigna por interfaz
    public GameObject parent;
    public Button buttonPref;

    private int nroAtomico;

    #endregion

    /*Metodo para instanciar una clase en unity*/
    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        loadTPer = FindObjectOfType<LoadTper>();
        UIFader = FindObjectOfType<UIFader>();
        // seteo el CanvasGroup (tabla periodica) que voy a ocultar cuando llame al UIFader.FadeInAndOut
        //Este quedo asi para en algun momento modificarlo y utilice el override
        // UIFader.uiElement = parent.transform.parent.gameObject.GetComponent<CanvasGroup>();
        BasicInfoLoader = FindObjectOfType<BasicInfoLoader>();
        DetailInfoLoader = FindObjectOfType<DetailInfoLoader>();
    }

    #region Metodos

    /*Va a crear un objeto elemento a partir de apretar el boton izq del mouse*/
    public void Spawn()
    {
        UIToolTipControl.flagTooltip = true;//le digo al controlador que se activa un tooltip

        /*no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace, ademas 
          no hace falta porque siempre va a existir un boton que contendra un objeto text, 
          en este caso el objeto text del boton se esta trayendo el 1ro de la coleccion*/
        Text text = parent.GetComponentInChildren<Text>();

        atomManager.SpawnFromPeriodicTable(text.text);

        //ESTO EVITA QUE SE CIERRE LA TABLA POR CADA AGREGAR ATOMO
        //aca se puede utilizar el metodo del fadeinout porque es el panel de la tabla que contiene el objeto CANVAS GROUP
        //CanvasGroup tablaPeriodicaPanel = parent.transform.parent.gameObject.GetComponent<CanvasGroup>();
        //UIFader.FadeInAndOut(tablaPeriodicaPanel);
    }

    /*va a ejecutar el proceso para mostrar informacion basica a partir de apretar el boton der del mouse*/
    public void GetInfoBasic()
    {
        UIToolTipControl.flagTooltip = true;//le digo al controlador que se activa un tooltip

        nroAtomico = getNroAtomicoId();

        //llamo para completar la info detallada
        ElementInfoDetail elementInfoDet = loadTPer.LoadInfoDeatail(nroAtomico);
        DetailInfoLoader.SetDetailInfo(elementInfoDet);
     
        //envio el boton que fue presionado para obtener luego su diseño
        BasicInfoLoader.ButtonimgTPER = buttonPref;

        ElementInfoBasic elementInfo = loadTPer.LoadInfoBasica(nroAtomico);
        //llamo al metodo que carga la info en los text box del panel
        BasicInfoLoader.SetBasicInfo(elementInfo);
    }


    /*Obtiene el nro atomico a partir del "string" que tiene en el boton como txtNroAtomico*/
    private int getNroAtomicoId()
    {
        int nroAtomico = 0;

        /*no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace, ademas 
        no hace falta porque siempre va a existir un boton que contendra un objeto text, 
        en este caso el objeto text del boton se esta trayendo el 1ro de la coleccion*/
        Text[] textosObj = parent.GetComponentsInChildren<Text>();

        for (int j = 0; j < textosObj.Length; j++)
        {
            if (textosObj[j].name == "txtNroAtomico")
                nroAtomico = Convert.ToInt32(textosObj[j].text);
        }

        return nroAtomico;
    }

    #endregion
}



