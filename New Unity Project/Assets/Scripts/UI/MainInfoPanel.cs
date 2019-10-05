using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MainInfoPanel : MonoBehaviour
{

    #region atributos
    //contenedor para la info de atomos luego ira cada uno de los otros
    public CanvasGroup infoContainer;
    public CanvasGroup infoContainerMolecule;
    public CanvasGroup infoContainerMaterial;
    public CanvasGroup infoContainerIsotopos;

    //objeto con el que interactúo para acceder a la DB
    private QryElementos qryElement;
    private QryMoleculas qryMolecule;
    private QryMaterials qryMaterial;

    //labels donde muestra info
    private TextMeshProUGUI nameLbl;
    private TextMeshProUGUI nameLblMolecule;
    private TextMeshProUGUI nameLblMaterial;
    private TextMeshProUGUI nameLblIsotopos;

    public GameObject[] suggestionButtons;
    //imagen del boton
    public GameObject elementBtn;
    //diccionario que mapea categoría de la tabla, con color
    private Dictionary<string, Color32> categories = new Dictionary<string, Color32>();
    //para el proceso de carga de informacion de acuerdo al tipo de info elem, mole, mat..)
    private PanelInfoLoader PanelInfoLoader;
    #endregion

    private void Awake()
    {
        //se instancia las clases para querys
        GameObject go = new GameObject();
        go.AddComponent<QryElementos>();
        qryElement = go.GetComponent<QryElementos>();

        GameObject go1 = new GameObject();
        go1.AddComponent<QryMoleculas>();
        qryMolecule = go1.GetComponent<QryMoleculas>();

        GameObject go2 = new GameObject();
        go2.AddComponent<QryMaterials>();
        qryMaterial = go2.GetComponent<QryMaterials>();

        InitializeCategoryDictionary();
        PanelInfoLoader = FindObjectOfType<PanelInfoLoader>();

        nameLblMolecule = infoContainerMolecule.GetComponentInChildren<TextMeshProUGUI>();
        nameLbl = infoContainer.GetComponentInChildren<TextMeshProUGUI>(); //aunque hay 2 lbl el 1ro es el name
        nameLblMaterial= infoContainerMaterial.GetComponentInChildren<TextMeshProUGUI>();
        nameLblIsotopos = infoContainerIsotopos.GetComponentInChildren<TextMeshProUGUI>();
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
        ElementTabPer element = new ElementTabPer();
        IsotopoAllData isotopo = new IsotopoAllData();

        if (atom.TypeAtom == TypeAtomEnum.atom)
        {

            element = qryElement.GetElementFromNro(atom.ElementNumber);

            if (element != null && element.Nroatomico != 0)
            {

                ActivatePanelsAtoms();

                nameLbl.text = element.Name;
                SetElementColor(element);
                SetButtonTexts(element);
                //carga los datos especiales del elemento
                SetInfoElementSelected(atom.ElementNumber);
            }
        }

        else if (atom.TypeAtom == TypeAtomEnum.isotopo)
        {

            element = qryElement.GetElementFromNro(atom.ElementNumber);
            isotopo = qryElement.GetAllDataIsotopo(atom.IsotopoNumber);

            if (isotopo != null && isotopo.NumeroAtomico != 0)
            {

                ActivatePanelsAtoms();

                nameLblIsotopos.text = isotopo.Isotopo + " de " + element.Name;
                //carga los datos especiales del isotopo
                SetInfoIsotopoSelected(isotopo);
            }
        }

        else //NO MUESTRA NADA Y DESACTIVA TODO SI FUERA NO ENCONTRADO! no analiza si uno de los 2 paneles estaba o no activo...
        {
            DesactivatePanelsAtoms();
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
        PanelInfoLoader.SetPanelInfoElement(element);
    }
    #endregion

    #region Moleculas

    public void SetInfoMolecule(Molecule mol)
    {
        MoleculeData molecule = qryMolecule.GetMoleculeById(mol.MoleculeId);

        if (molecule != null)
        {
            nameLblMolecule.text = molecule.TraditionalNomenclature;
            //carga los datos especiales de la molecula en el panel especial
            PanelInfoLoader.SetPanelInfoMolecule(molecule);
        }
    }

    #endregion

    #region Materiales

    /*utilizado desde el selection manager*/
    public void SetInfoMaterial(MaterialObject mapping)
    {
        MaterialData material = qryMaterial.GetMaterialById(mapping.MaterialId);

        if (material != null)
        {
            nameLblMaterial.text = material.Name;
            //carga los datos especiales de la molecula en el panel especial
            PanelInfoLoader.SetPanelInfoMaterial(material);
        }
    }

    /*Utilizado desde el Material Manager*/
    public void SetInfoMaterial(MaterialData material)
    {      
        if (material != null)
        {
            nameLblMaterial.text = material.Name;
            //carga los datos especiales de la molecula en el panel especial
            PanelInfoLoader.SetPanelInfoMaterial(material);
        }
    }


    #endregion

    #region isotopos

    private void SetInfoIsotopoSelected(IsotopoAllData isotopoData)
    {
        //llamo al metodo que carga la info en los text box del panel
        PanelInfoLoader.SetPanelInfoIsotopos(isotopoData);
    }

    #endregion


    private void DesactivatePanelsAtoms()
    {
        for (int i = 0; i < infoContainer.transform.childCount; i++)
        {
            infoContainer.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < infoContainerIsotopos.transform.childCount; i++)
        {
            infoContainerIsotopos.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ActivatePanelsAtoms()
    {
        for (int i = 0; i < infoContainer.transform.childCount; i++)
        {
            infoContainer.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < infoContainerIsotopos.transform.childCount; i++)
        {
            infoContainerIsotopos.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
