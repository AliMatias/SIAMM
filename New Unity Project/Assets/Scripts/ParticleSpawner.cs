using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject[] nucleonPrefabs;
    [SerializeField]
    private Transform parent;
    public DBManager DBManager;
    public TextMeshProUGUI elementLabel;
    private Queue<GameObject> protonQueue = new Queue<GameObject>();
    private Queue<GameObject> neutronQueue = new Queue<GameObject>();
    private Queue<GameObject> electronQueue = new Queue<GameObject>();
    private int protonCounter = 0;
    private int neutronCounter = 0;
    private int electronCounter = 0;

    public void SpawnNucleon(bool proton)
    {
        int index = 1;
        if (proton)
        {
            index = 0;
        }
        GameObject prefab = nucleonPrefabs[index];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
        
        //posicion random para que no queden todos en fila
        float randomNumber = Random.Range(0f, 0.2f);
        Vector3 randomPosition = new Vector3(randomNumber, randomNumber, randomNumber);
        spawn.transform.localPosition = randomPosition;
        
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
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

    public void SpawnElectron()
    {
        GameObject prefab = nucleonPrefabs[2];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
        spawn.transform.localPosition = new Vector3(1f,0f,0f);
        electronQueue.Enqueue(spawn);
        electronCounter++;
        UpdateElement(protonCounter, neutronCounter, electronCounter);
    }

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

    /*Metodo Valida si es un elemento de tabla periodica, si isotopo, cation-anion o propio*/
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
            element = DBManager.GetElementFromProton(protons);

            if (element == null || element.Name == null)
            {
                elementText = "no encontrado.";
            }
            else
            {
                elementText = element.Name + " (" + element.Simbol + ")";

                if (element.Neutrons != neutrons)
                {
                    elementText = "isótopo de " + elementText;
                }
                if (element.Electrons < electrons)
                {
                    elementText = elementText + ", catión.";
                }
                else if (element.Electrons > electrons)
                {
                    elementText = elementText + ", anión.";
                }
            }
        }

        Debug.Log(elementText);
        elementLabel.SetText("Elemento: " + elementText);
    }

}
