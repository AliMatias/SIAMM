using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//Esta clase se va a encargar de desactivar y activar los diferentes elementos de la UI cuando corresponda
public class CombinationManager : MonoBehaviour
{
    //lista de botones que van a ser apagados cuando se entre en el modo combinación
    private List<Button> buttonsToToggle = new List<Button>();
    private bool combineMode = false;
    private AtomManager atomManager;
    private DBManager DBManager;
    public Button combineButton;
    public Button combineModeButton;

    void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        DBManager = FindObjectOfType<DBManager>();
        //encuentro y asigno a mi lista los botones a apagar
        GameObject[] btns = GameObject.FindGameObjectsWithTag("toToggle");
        foreach (GameObject btn in btns)
        {
            buttonsToToggle.Add(btn.GetComponent<Button>());
        }
        combineButton.interactable = false;
    }

    public void SwitchCombineMode()
    {
        combineMode = !combineMode;
        //apago los botones
        foreach (Button btn in buttonsToToggle)
        {
            btn.interactable = !combineMode;
        }
        combineButton.interactable = !combineButton.interactable;
        //le aviso al atom manager que cambié de modo
        atomManager.SwitchCombineMode();
        //obtengo el texto del boton y lo cambio
        Text text = combineModeButton.GetComponentInChildren<Text>();
        if (combineMode)
        {
            text.text = "Modo normal";
        }
        else
        {
            text.text = "Modo combinación";
        }
    }

    //Acá tiene que ir a la bd a buscar la combinación
    public void CombineAtoms()
    {
        List<int> selectedAtoms = atomManager.SelectedAtoms;
        List<int> elementNumbers = new List<int>();
        foreach (int index in selectedAtoms)
        {
            int numeroElemento = atomManager.FindAtomInList(index).ElementNumber;
            if(numeroElemento != 0)
            {
                elementNumbers.Add(numeroElemento);
            }
        }

        if (elementNumbers != null && elementNumbers.Count > 0)
        {
            List<List<int>> possibleCombinations = new List<List<int>>();
            IEnumerable<(int ElementId, int Count)> combinedElements = from element in elementNumbers
                                   group element by element into elementOccurrences
                                   select (
                                      ElementId: elementOccurrences.Key, Count: elementOccurrences.Count()
                                   );

            foreach ((int elementId, int count) in combinedElements)
            {
                List<int> molecules = DBManager.GetMoleculesByAtomNumberAndQuantity(elementId, count);
                possibleCombinations.Add(molecules);
            }

            if (possibleCombinations != null && possibleCombinations.Count > 0)
            {
                List<int> intersection = Intersect(possibleCombinations);
                if (intersection.Count > 0)
                {
                    bool found = false;
                    foreach(int moleculaId in intersection)
                    {
                        int elementCount = DBManager.GetUniqueElementCountInMoleculeById(moleculaId);
                        if (elementCount == combinedElements.ToList().Count)
                        {
                            MoleculeData moleculeData = DBManager.GetMoleculeById(moleculaId);
                            Debug.Log("Molecula Formada: " + moleculeData.ToString);
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        Debug.Log("No se encontro ninguna combinacion posible");
                    }
                } 
                else
                {
                    Debug.Log("No se encontro ninguna combinacion posible");
                }
            }
            else
            {
                Debug.Log("No se encontraron moleculas que contengan esos atomos");
            }
        }
        else
        {
            Debug.Log("No hay atomos validos seleccionados");
        }
    }

    static List<int> Intersect(List<List<int>> lists)
    {
        return lists.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
    }
}
