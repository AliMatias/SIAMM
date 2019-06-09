using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//script que se encarga de spawnear las part�culas, manejarlas y saber que estoy formando.
public class ParticleSpawner : MonoBehaviour
{
    #region atributos
    //Lista de prefabs de part�culas, posici�n 0->proton, 1->neutron, 2->electron
    public GameObject[] particlePrefabs;
    //objeto padre, que va a representar el �tomo en s�
    [SerializeField]
    private Transform parent;
    //objeto con el que interact�o para acceder a la DB
    public DBManager DBManager;
    //label que indica elemento en construcci�n.
    public TextMeshProUGUI elementLabel;
    //listas para controlar las part�culas agregadas
    private Queue<GameObject> protonQueue = new Queue<GameObject>();
    private Queue<GameObject> neutronQueue = new Queue<GameObject>();
    private Queue<GameObject> electronQueue = new Queue<GameObject>();
    //contadores de part�culas
    private int protonCounter = 0;
    private int neutronCounter = 0;
    private int electronCounter = 0;
#endregion


    #region spawn
    //crea un nucleon, true -> crea proton, false -> crea neutron
    public void SpawnNucleon(bool proton)
    {
        int index = 1;
        if (proton)
        {
            index = 0;
        }
        //selecciono el prefab y lo instancio
        GameObject prefab = particlePrefabs[index];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
        
        //posicion random para que no queden todos en fila, a�n no quedan bien
        float randomNumber = Random.Range(0f, 0.4f);
        Vector3 randomPosition = new Vector3(randomNumber, randomNumber, randomNumber);
        spawn.transform.localPosition = randomPosition;
        
        //encolar y aumentar contadores seg�n part�cula creada
        if (proton)
        {
            protonQueue.Enqueue(spawn);
            protonCounter++;
        }
        else
        {
            neutronQueue.Enqueue(spawn);
            neutronCounter++;
        }
        //actualizar el label que indica el elemento.
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //crear un electron
    public void SpawnElectron()
    {
        //selecciono el prefab y lo instancio
        GameObject prefab = particlePrefabs[2];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
        //lo pongo a un radio de 1 en el eje X
        spawn.transform.localPosition = new Vector3(1f,0f,0f);
        //agrego a la cola y aumento contador
        electronQueue.Enqueue(spawn);
        electronCounter++;
        //actualizo label
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }
#endregion

    #region borradores

    //borrar neutron
    public void RemoveNeutron()
    {
        if (neutronCounter != 0)
        {
            GameObject toDelete = neutronQueue.Dequeue();
            Destroy(toDelete);
            neutronCounter--;
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //borrar proton
    public void RemoveProton()
    {
        if (protonCounter != 0)
        {
            GameObject toDelete = protonQueue.Dequeue();
            Destroy(toDelete);
            protonCounter--;
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    //borrar electron
    public void RemoveElectron()
    {
        if (electronCounter != 0)
        {
            GameObject toDelete = electronQueue.Dequeue();
            Destroy(toDelete);
            electronCounter--;
        }
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }
#endregion

    /*Metodo Valida si es un elemento de tabla periodica, si es isotopo, y cation-anion
     y luego lo escribe en el label del elemento*/
    private void UpdateElement(int protons, int neutrons, int electrons)
    {
        ElementData element = new ElementData();
        string elementText = string.Empty;

        Debug.Log("protones: " + protons + " neutrones:" + neutrons + " electrones:" + electrons);

        //resetea valor a by default
        if (protons == 0 && neutrons == 0 && electrons == 0)
            elementText = "";
        else
        {
            //obtiene datos del elemento seg�n cantidad de protones
            element = DBManager.GetElementFromProton(protons);
            //si es null o no lo encontr�
            if (IsNullOrEmpty(element))
            {
                elementText = "no encontrado.";
            }
            else
            {
                //seteo nombre y s�mbolo
                elementText = element.Name + " (" + element.Simbol + ")";

                //si no coinciden los neutrones es un is�topo de ese material (falta l�mites inf y sup)
                if (element.Neutrons != neutrons)
                {
                    elementText = "is�topo de " + elementText;
                }
                //si mi modelo tiene mas electrones que el de la tabla, es ani�n (-)
                if (element.Electrons < electrons)
                {
                    elementText = elementText + ", ani�n.";
                }
                //sino, si el modelo tiene menos electrones que el de la tabla, es cati�n (+)
                else if (element.Electrons > electrons)
                {
                    elementText = elementText + ", cati�n.";
                }
                //sino, significa que es la misma cantidad, y tiene carga neutra
            }
        }

        Debug.Log(elementText);
        elementLabel.SetText("Elemento: " + elementText);
    }

    #region crear desde tabla periodica
    /*Por ahora borra todas sus part�culas y empieza a spawnear las nuevas hasta llegar a la cantidad indicada
    esto hay que cambiarlo cuando se maneje con m�s de un �tomo porque tiene que ser en el onCreate o algo as�.
    y ya sabemos que van a estar las 3 part�culas en 0
    Adem�s, el elementName por ahora viene definido en el m�todo del bot�n*/
    public void SpawnFromPeriodicTable(string elementName)
    {
        //nullcheck del nombre
        if(IsNullOrEmpty(elementName))
        {
            Debug.Log("Element name null or empty");
            return;
        }

        //obtengo la data del elemento de la DB
        ElementData element = DBManager.GetElementFromName(elementName);
        //nullcheck por si no encontr� en la DB
        if (IsNullOrEmpty(element))
        {
            Debug.Log("Element not found.");
            return;
        }

        //chequea lo actual y lo borra, esto es lo que seguro hay que borrar m�s adelante
        IterateCounterAndDeleteParticles(ref protonCounter, ref protonQueue);
        IterateCounterAndDeleteParticles(ref neutronCounter, ref neutronQueue);
        IterateCounterAndDeleteParticles(ref electronCounter, ref electronQueue);
        //crea la cantidad de part�culas indicadas
        IterateCounterAndCreateParticles(element.Protons, element.Neutrons, element.Electrons);
    }

    //le paso la lista y el contador correspondiente por referencia para hacer un solo m�todo para las 3 part�culas
    private void IterateCounterAndDeleteParticles(ref int counter, ref Queue<GameObject> queue)
    {
        while(counter > 0)
        {
            GameObject toDelete = queue.Dequeue();
            Destroy(toDelete);
            counter--;
        }
    }

    //Este m�todo lanza las 3 co rutinas que spawnean las part�culas indicadas por par�metro
    private void IterateCounterAndCreateParticles(int protons, int neutrons, int electrons)
    {
        //Empiezo las 3 co rutinas que se van a ejecutar en paralelo
        StartCoroutine(SpawnGivenNumberOfNucleons(protons,true));
        StartCoroutine(SpawnGivenNumberOfNucleons(neutrons, false));
        StartCoroutine(SpawnGivenNumberOfElectrons(electrons));
    }
    
    //Co rutina que spawnea N nucleones (true = proton, false = neutron), cada X segundos
    IEnumerator SpawnGivenNumberOfNucleons(int counter, bool proton)
    {
        while (counter > 0)
        {
            SpawnNucleon(proton);
            counter--;
            //esta l�nea espera x segundos antes de seguir ejecutando
            yield return new WaitForSeconds(0.25f);
        }
    }

    //Co rutina que spawnea N electrones cada X segundos
    IEnumerator SpawnGivenNumberOfElectrons(int counter)
    {
        while (counter > 0)
        {
            SpawnElectron();
            counter--;
            //esta l�nea espera x segundos antes de seguir ejecutando
            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion


    #region nullchecks
    //nullcheck de string, averiguar si existe alguna librer�a que ya haga esto.
    private bool IsNullOrEmpty(string s)
    {
        if (s == null || s == "")
            return true;
        return false;
    }

    //nullcheck de ElementData, averiguar si existe alguna librer�a que ya haga esto.
    private bool IsNullOrEmpty(ElementData e)
    {
        if (e == null || e.Name == null || e.Name == "")
            return true;
        return false;
    }
    #endregion
}
