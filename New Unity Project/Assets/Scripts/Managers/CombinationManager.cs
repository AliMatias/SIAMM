using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

//Esta clase se va a encargar de desactivar y activar los diferentes elementos de la UI cuando corresponda
public class CombinationManager : MonoBehaviour
{
    //lista de botones que van a ser apagados cuando se entre en el modo combinación
    private List<Button> buttonsToToggle = new List<Button>();
    private bool combineMode = false;
    private AtomManager atomManager;
    private QryMoleculas qryMolecule;
    private MoleculeManager moleculeManager;
    private SelectionManager selectionManager;
    public Button combineButton;
    public Button combineModeButton;
    private UIPopup popup;

    public bool CombineMode { get => combineMode; }

    void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        popup = FindObjectOfType<UIPopup>();

        GameObject go = new GameObject();
        go.AddComponent<QryMoleculas>();
        qryMolecule = go.GetComponent<QryMoleculas>();

        moleculeManager = FindObjectOfType<MoleculeManager>();
        selectionManager = FindObjectOfType<SelectionManager>();
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
        // le aviso al selection manager que cambié de modo
        selectionManager.SwitchCombineMode(combineMode);
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
        List<int> selectedAtoms = atomManager.GetSelectedAtoms();
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
                List<int> molecules = new List<int>();
                try
                {
                    molecules = qryMolecule.GetMoleculesByAtomNumberAndQuantity(elementId, count);
                    possibleCombinations.Add(molecules);
                }
                catch (Exception e)
                {
                    Debug.LogError("CombinationManager :: Ocurrio un error al buscar moleculas por elemento y cantidad: " + e.Message);
                    popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo moleculas por elemento y cantidad");
                    return;
                }
            }

            if (possibleCombinations != null && possibleCombinations.Count > 0)
            {
                List<int> intersection = Intersect(possibleCombinations);
                if (intersection.Count > 0)
                {
                    bool found = false;
                    foreach(int moleculaId in intersection)
                    {
                        int elementCount;
                        try
                        {
                            elementCount = qryMolecule.GetUniqueElementCountInMoleculeById(moleculaId);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("CombinationManager :: Ocurrio un error al buscar Elementos Componentes de una Molecula: " + e.Message);
                            popup.MostrarPopUp("Elementos Qry DB", "Error obteniendo Elementos Componentes de una Molecula");
                            return;
                        }


                        if (elementCount == combinedElements.ToList().Count)
                        {
                            MoleculeData moleculeData = null;
                            try
                            {
                                moleculeData = qryMolecule.GetMoleculeById(moleculaId);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError("CombinationManager :: Ocurrio un error al buscar Identificador de Molecula: " + e.Message);
                                popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Identificador de Molecula");
                                return;
                            }

                            List<AtomInMolPositionData> atomsPosition;
                            try
                            {
                                atomsPosition = qryMolecule.GetElementPositions(moleculaId);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError("CombinationManager :: Ocurrio un error al buscar posiciones de los Elementos Quimico: " + e.Message);
                                popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo posiciones de los Elementos Quimicos");
                                return;
                            }

                            //exito! muestro por pantalla 
                            popup.MostrarPopUp("Manager Combinación", "Molécula Formada: " + moleculeData.ToString);

                            found = true;
                            DeleteCombinedAtoms(selectedAtoms);
                            SpawnMolecule(atomsPosition, moleculeData.ToString);
                            break;
                        }
                    }
                    if(!found)
                    {
                        popup.MostrarPopUp("Manager Combinación", "No se encontró ninguna combinación posible");
                    }
                } 
                else
                {
                    popup.MostrarPopUp("Manager Combinación", "No se encontró ninguna combinación posible");
                }
            }
            else
            {
                popup.MostrarPopUp("Manager Combinación", "No se encontraron moléculas que contengan esos átomos");
            }
        }
        else
        {
            popup.MostrarPopUp("Manager Combinación", "No hay átomos válidos seleccionados");
        }
    }


    static List<int> Intersect(List<List<int>> lists)
    {
        return lists.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
    }

    //Borrar del espacio de trabajo, los átomos seleccionados.
    private void DeleteCombinedAtoms(List<int> selectedAtoms)
    {
        foreach(int atom in selectedAtoms)
        {
            atomManager.DeleteAtom(atom);
        }
    }

    private void SpawnMolecule(List<AtomInMolPositionData> atomsPosition, string name)
    {
        moleculeManager.SpawnMolecule(atomsPosition, name);
    }
}
