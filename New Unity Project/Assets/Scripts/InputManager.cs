using UnityEngine;
using UnityEngine.UI;

/* esto se va a borrar más adelante porque no se ingresa de un input
 * tanto el botón como el input también se van a borrar
 * pero algo así va a hacer la tabla para llamar al spawner*/
public class InputManager : MonoBehaviour
{
    private ParticleSpawner spawner;
    public GameObject parent;
    private LoadTper loadTPer;

    private void Awake()
    {
        spawner = FindObjectOfType<ParticleSpawner>();
        loadTPer = FindObjectOfType<LoadTper>();
    }

    public void Spawn()
    {
        //no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace
        Text text = parent.GetComponentInChildren<Text>();
        spawner.SpawnFromPeriodicTable(text.text);
    }

    public void GetInfoBasic()
    {
        Text text = parent.GetComponentInChildren<Text>();
        loadTPer.LoadInfoBasica(text.text);
    }

}



