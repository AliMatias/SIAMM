using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    #region Atributos
    private AtomManager atomManager;
    private UIFader UIFader;  
    private LoadTper loadTPer;
    private BasicInfoLoader BasicInfoLoader;
    //asigna por interfaz
    public GameObject parent;
    public Button buttonPref;
    #endregion

    /*Metodo para instanciar una clase en unity*/
    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        loadTPer = FindObjectOfType<LoadTper>();
        UIFader = FindObjectOfType<UIFader>();
        // seteo el CanvasGroup (tabla periodica) que voy a ocultar cuando llame al UIFader.FadeInAndOut
        UIFader.uiElement = parent.transform.parent.gameObject.GetComponent<CanvasGroup>();
        BasicInfoLoader = FindObjectOfType<BasicInfoLoader>();
    }

    /*Va a crear un objeto elemento a partir de apretar el boton izq del mouse*/
    public void Spawn()
    {
        /*no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace, ademas 
          no hace falta porque siempre va a existir un boton que contendra un objeto text, 
          en este caso el objeto text del boton se esta trayendo el 1ro de la coleccion*/ 
        Text text = parent.GetComponentInChildren<Text>();

        atomManager.SpawnFromPeriodicTable(text.text);

        //aca se puede utilizar el metodo del fadeinout porque es el panel de la tabla que contiene el objeto CANVAS GROUP
        UIFader.FadeInAndOut();
    }

    /*va a ejecutar el proceso para mostrar informacion basica a partir de apretar el boton der del mouse*/
    public void GetInfoBasic()
    {

        /*no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace, ademas 
          no hace falta porque siempre va a existir un boton que contendra un objeto text, 
          en este caso el objeto text del boton se esta trayendo el 1ro de la coleccion*/
        Text text = parent.GetComponentInChildren<Text>();

        ElementInfoBasic elementInfo = loadTPer.LoadInfoBasica(text.text);
        
        //envio el boton que fue presionado para obtener luego su diseño
        BasicInfoLoader.ButtonimgTPER = buttonPref;

        //llamo al metodo que carga la info en los text box del panel
        BasicInfoLoader.SetBasicInfo(elementInfo);
    }

}



