using UnityEngine;
using UnityEngine.UI;

/* esto se va a borrar más adelante porque no se ingresa de un input
 * tanto el botón como el input también se van a borrar
 * pero algo así va a hacer la tabla para llamar al spawner*/
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputField input;
    [SerializeField]
    private ParticleSpawner spawner;

    public void Spawn()
    {
        //obtengo text del input field
        string elementName = input.text;
        //no hago nullcheck, porque el método spawnFromPeriodicTable ya lo hace
        spawner.SpawnFromPeriodicTable(elementName);
    }
}
