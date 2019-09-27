using UnityEngine;
using UnityEngine.UI;

//script que abre el panel de tabla periódica
public class OpenMenus : MonoBehaviour
{
    private AtomManager atomManager;
    private MoleculeManager moleculeManager;

    //para administrar los 3 paneles
    private CanvasGroup infoPanelElements;
    private CanvasGroup infoPanelMolecule;
    private CanvasGroup infoPanelMaterial;

    public CanvasGroup[] tabbedMenuMolecules;
    private MaterialManager materialManager;

    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        materialManager = FindObjectOfType<MaterialManager>();
        infoPanelElements = transform.Find("InfoContainerElementos").GetComponent<CanvasGroup>();
        infoPanelMolecule = transform.Find("InfoContainerMoleculas").GetComponent<CanvasGroup>();
        //infoPanelMaterial = transform.Find("InfoContainerMaterial").GetComponent<CanvasGroup>();
    }

    /*
    * metodo principal para el manejo de la apertura del panel INFOPANEL padre de los otros paneles de informacion
    * infocontainer de atomos, moleculas y materiales
    */
    public void OpenBottomPanel()
    {
        /*este fix es para que siempre si se cierra el panel vuelva a mostrar el tab nomclatures, esto es asi porque originalmente se hacia con ACTIVE.. y con alpha.. complica
        * si se deja como inactive / active los datos no pueden ser cargados desde la base de datos.         
        */
        restoreAlphasTabbedMenu();

        // traigo todos los atomos seleccionados
        var selectedAtoms = atomManager.GetSelectedAtoms();
        var selectedMolecules = moleculeManager.GetSelectedMolecules();
        var selectedMaterials = materialManager.GetSelectedMaterials();

        // si hay un solo atomo seleccionado y ninguna molecula o material, muestro info panel
        if (selectedAtoms.Count == 1 && selectedMolecules.Count == 0 && selectedMaterials.Count == 0 && infoPanelElements.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
        }

        else if (infoPanelElements.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);
        }

        // si hay una sola molecula seleccionada y ningun atomo o material, muestro info panel
        else if (selectedAtoms.Count == 0 && selectedMolecules.Count == 1 && selectedMaterials.Count == 0 && infoPanelMolecule.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
        }

        else if (infoPanelMolecule.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);
        }

        
    }

    /*
     * metodo principal para el manejo del cierre del panel INFOPANEL padre de los otros paneles de informacion
     * infocontainer de atomos, moleculas y materiales
     */
    public void CloseBottomPanel()
    {
        /*este fix es para que siempre si se cierra el panel vuelva a mostrar el tab nomclatures, esto es asi porque originalmente se hacia con ACTIVE.. y con alpha.. complica
        * si se deja como inactive / active los datos no pueden ser cargados desde la base de datos.         
        */
        restoreAlphasTabbedMenu();

        if (infoPanelElements.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);
        }


        if (infoPanelMolecule.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);
        }

       
    }


    private void restoreAlphasTabbedMenu()
    {
        foreach (CanvasGroup tab in tabbedMenuMolecules)
        {
            if (tab.gameObject.name == "Content1")
                tab.alpha = 1;
            else
                tab.alpha = 0;
        }
    }
}
