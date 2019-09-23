using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MainInfoPanel : MonoBehaviour
{
    //objeto con el que interactúo para acceder a la DB
    private QryElementos qryElement;
    private QryMoleculas qryMolecule;
    //labels donde muestra info
    public TextMeshProUGUI nameLbl;
    public TextMeshProUGUI nameLblMolecule;
    public GameObject[] suggestionButtons;
    //contenedor para la info de atomos luego ira cada uno de los otros
    public CanvasGroup infoContainer;
    public CanvasGroup infoContainerMolecule;
    //imagen del boton
    public GameObject elementBtn;
    //diccionario que mapea categoría de la tabla, con color
    private Dictionary<string, Color32> categories = new Dictionary<string, Color32>();
    //para el proceso de carga de informacion de acuerdo al tipo de info elem, mole, mat..)
    private PanelInfoLoader PanelInfoLoader;

    private void Awake()
    {
        //se instancia las clases para querys
        GameObject go = new GameObject();
        go.AddComponent<QryElementos>();
        qryElement = go.GetComponent<QryElementos>();

        GameObject go1 = new GameObject();
        go1.AddComponent<QryElementos>();
        qryMolecule = go1.GetComponent<QryMoleculas>();

        InitializeCategoryDictionary();
        PanelInfoLoader = FindObjectOfType<PanelInfoLoader>();
    }

    #region elementos
    //diccionario de categoría_grupo -> color
    private void InitializeCategoryDictionary()
    {
        categories.Add("Sin Grupo", new Color32(174,174,174,255));
        categories.Add("Gas Inerte", new Color32(195,136,10,255));
        categories.Add("Alcalino", new Color32(68,139,214,255));
        categories.Add("Alcalino Terreo", new Color32(203,231,56,255));
        categories.Add("Metaloide", new Color32(250,154,0,255));
        categories.Add("No Metal", new Color32(248,46,89,255));
        categories.Add("Halogeno", new Color32(219,70,24,255));
        categories.Add("Pobre", new Color32(231,204,47,255));
        categories.Add("De Transicion", new Color32(75,178,75,255));
        categories.Add("Lantanido", new Color32(204,92,198,255));
        categories.Add("Actinidos", new Color32(214,88,123,255));
    }

    //método para obtener el material
    private Color32 GetMaterialIndexFromDictionary(string cat)
    {
        return categories[cat];
    }

    public void SetInfo(Atom atom)
    {
        if (atom.TypeAtom == TypeAtomEnum.atom)
        {

            ElementTabPer element = qryElement.GetElementFromNro(atom.ElementNumber);

            if (element != null && element.Nroatomico != 0)
            {

                for (int i = 0; i < infoContainer.transform.childCount; i++)
                {
                    infoContainer.transform.GetChild(i).gameObject.SetActive(true);
                }

                nameLbl.text = element.Name;
                SetElementColor(element);
                SetButtonTexts(element);
                //carga los datos especiales del elemento
                SetInfoElementSelected(atom.ElementNumber);
            }  
        }
        else //por ahora aca NO MUESTRA NADA Y DESACTIVA TODO
        {
            for (int i = 0; i < infoContainer.transform.childCount; i++)
            {
               infoContainer.transform.GetChild(i).gameObject.SetActive(false);      
            }
        }
    }

    private void SetButtonTexts(ElementTabPer element){
        Text[] texts = elementBtn.GetComponentsInChildren<Text>();
        texts[0].text = element.Simbol;
        texts[1].text = Convert.ToString(element.Nroatomico);
        texts[2].text = Convert.ToString(element.PesoAtomico);
        texts[3].text = element.ConfElectronica;
    }

    private void SetElementColor(ElementTabPer element)
    {
        if(element.Nroatomico != 0){
            //obtengo el material según la clasif
            elementBtn.GetComponent<Image>().color = GetMaterialIndexFromDictionary(element.ClasificacionGrupo);
            FillSuggestions(element.Nroatomico);
        }
    }

    private void FillSuggestions(int elementId){
            //seteo sugerencias
            List<Suggestion> suggestions = qryElement.GetSuggestionForElement(elementId);
            List<ElementTabPer> suggestedElements = new List<ElementTabPer>();
        
            foreach(GameObject sugg in suggestionButtons){
                sugg.SetActive(false);
            }

            if(suggestions.Count > 0 && suggestions.Count < 4){
                foreach(Suggestion suggestion in suggestions){
                    suggestedElements.Add(qryElement.GetElementFromNro(suggestion.IdSugerido));
                }
                for(int i=0; i < suggestedElements.Count; i++){
                    suggestionButtons[i].SetActive(true);
                    suggestionButtons[i].GetComponentInChildren<Text>().text = suggestedElements[i].Simbol;
                    suggestionButtons[i].GetComponent<Image>().color = 
                        GetMaterialIndexFromDictionary(suggestedElements[i].ClasificacionGrupo);
                }
            }
    }

    //trae de la base los 6 campos para la informacion especial
    private void SetInfoElementSelected(int elementId)
    {
        ElementInfoPanelInfo element = qryElement.GetElementInfoPanelSuggestion(elementId);
        //llamo al metodo que carga la info en los text box del panel
        PanelInfoLoader.SetPanelInfo(element);
    }
    #endregion

    #region Moleculas

    public void SetInfoMolecule(Molecule mol)
    {

        MoleculeData molecule = qryMolecule.GetMoleculeById(mol.MoleculeId);
        
        if (molecule != null)
        {

            nameLblMolecule.text = molecule.TraditionalNomenclature;
            //carga los datos especiales del elemento
            //SetInfoElementSelected(atom.ElementNumber);
        }

    }

    //trae de la base los campos para la informacion especial
    private void SetInfoMoleculeSelected(int moleculeId)
    {
        ElementInfoPanelInfo element = qryElement.GetElementInfoPanelSuggestion(moleculeId);
        //llamo al metodo que carga la info en los text box del panel
        PanelInfoLoader.SetPanelInfo(element);
    }

    #endregion
}
