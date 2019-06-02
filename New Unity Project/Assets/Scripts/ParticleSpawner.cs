using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script que se encarga de spawnear las partículas, manejarlas y saber que estoy formando.
public class ParticleSpawner : MonoBehaviour
{
    //Lista de prefabs de partículas, posición 0->proton, 1->neutron, 2->electron
    public GameObject[] particlePrefabs;
    //objeto padre, que va a representar el átomo en sí
    [SerializeField]
    private Transform parent;
    public DBManager DBManager;
    //label que indica elemento en construcción.
    public TextMeshProUGUI elementLabel;
    //listas para controlar las partículas agregadas
    private Queue<GameObject> protonQueue = new Queue<GameObject>();
    private Queue<GameObject> neutronQueue = new Queue<GameObject>();
    private Queue<GameObject> electronQueue = new Queue<GameObject>();
    //contadores de partículas
    private int protonCounter = 0;
    private int neutronCounter = 0;
    private int electronCounter = 0;

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
        
        //posicion random para que no queden todos en fila, aún no quedan bien
        float randomNumber = Random.Range(0f, 0.2f);
        Vector3 randomPosition = new Vector3(randomNumber, randomNumber, randomNumber);
        spawn.transform.localPosition = randomPosition;
        
        //encolar y aumentar contadores según partícula creada
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

    /*Metodo Valida si es un elemento de tabla periodica, si es isotopo, y cation-anion*/
    private void UpdateElement(int protons, int neutrons, int electrons)
    {
        ElementData element = new ElementData();
        string elementText = string.Empty;

        Debug.Log("protones: " + protons + " neutrones:" + neutrons + " electrones:" + electrons);
        //string elementName = DBManager.GetElementFromParticles(protons, neutrons, electrons);

        //resetea valor a by default
        if (protons == 0 && neutrons == 0 && electrons == 0)
            elementText = "";
        else
        {
            //obtiene datos del elemento según cantidad de protones
            element = DBManager.GetElementFromProton(protons);
            //si es null o no lo encontré
            if (element == null || element.Name == null)
            {
                elementText = "no encontrado.";
            }
            else
            {
                //seteo nombre y símbolo
                elementText = element.Name + " (" + element.Simbol + ")";

                //si no coinciden los neutrones es un isótopo de ese material (falta límites inf y sup)
                if (element.Neutrons != neutrons)
                {
                    elementText = "isótopo de " + elementText;
                }
                //si mi modelo tiene mas electrones que el de la tabla, es anión (-)
                if (element.Electrons < electrons)
                {
                    elementText = elementText + ", anión.";
                }
                //sino, si el modelo tiene menos electrones que el de la tabla, es catión (+)
                else if (element.Electrons > electrons)
                {
                    elementText = elementText + ", catión.";
                }
                //sino, significa que es la misma cantidad, y tiene carga neutra
            }
        }

        Debug.Log(elementText);
        elementLabel.SetText("Elemento: " + elementText);
    }

    //por ahora borra todas sus partículas y empieza a spawnear las nuevas hasta llegar a la cantidad indicada
    //estoy hay que cambiarlo cuando se maneje con más de un átomo porque tiene que ser en el onCreate o algo así.
    //y ya sabemos que van a estar las 3 partículas en 0
    public void SpawnFromPeriodicTable()
    {
        //acá tendría que recibir el nombre por param y averiguar estos 3 valores en la db
        int protons = 10;
        int neutrons = 10;
        int electrons = 10;
        //chequea lo actual y lo borra
        IterateCounterAndDeleteParticles(ref protonCounter, ref protonQueue);
        IterateCounterAndDeleteParticles(ref neutronCounter, ref neutronQueue);
        IterateCounterAndDeleteParticles(ref electronCounter, ref electronQueue);

        IterateCounterAndCreateParticles(protons, neutrons, electrons);
    }

    private void IterateCounterAndDeleteParticles(ref int counter, ref Queue<GameObject> queue)
    {
        while(counter > 0)
        {
            GameObject toDelete = queue.Dequeue();
            Destroy(toDelete);
            counter--;
        }
    }

    private void IterateCounterAndCreateParticles(int protons, int neutrons, int electrons)
    {
        while(protons > 0)
        {
            SpawnNucleon(true);
            protons--;
            Delay(0.5f);
        }
        while(neutrons > 0)
        {
            SpawnNucleon(false);
            neutrons--;
            Delay(0.5f);
        }
        while(electrons > 0)
        {
            SpawnElectron();
            electrons--;
            Delay(0.5f);
        }
    }
    
    private void Delay(float delay)
    {
        float limitTime = Time.time + delay;
        //si acá pongo un while Time.time < limitTime, se tilda el unity
        Debug.Log("time: " + Time.time + ". limit: " + limitTime);
        return;
    }

}
