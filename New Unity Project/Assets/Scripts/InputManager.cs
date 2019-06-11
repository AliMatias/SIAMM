using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    #region Atributos
    private ParticleSpawner spawner;
    public GameObject parent;
    private LoadTper loadTPer;
    #endregion

    /*Metodo para instanciar una clase en unity*/
    private void Awake()
    {
        spawner = FindObjectOfType<ParticleSpawner>();
        loadTPer = FindObjectOfType<LoadTper>();
    }

    /*Va a crear un objeto elemento a partir de apretar el boton izq del mouse*/
    public void Spawn()
    {
        /*no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace, ademas 
          no hace falta porque siempre va a existir un boton que contendra un objeto text, 
          en este caso el objeto text del boton se esta trayendo el 1ro de la coleccion*/
        
        Text text = parent.GetComponentInChildren<Text>();
        spawner.SpawnFromPeriodicTable(text.text);
    }

    /*va a ejecutar el proceso para mostrar informacion basica a partir de apretar el boton der del mouse*/
    public void GetInfoBasic()
    {
        Text text = parent.GetComponentInChildren<Text>();
        loadTPer.LoadInfoBasica(text.text);
    }

}



