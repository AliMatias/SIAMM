using UnityEngine;
using UnityEngine.UI;

public class OpenMenus : MonoBehaviour
{
    #region Atributos

    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    private MaterialManager materialManager;

    //para administrar los 3 paneles
    private CanvasGroup infoPanelElements;
    private CanvasGroup infoPanelMolecule;
    private CanvasGroup infoPanelMaterial;

    public CanvasGroup[] tabbedMenuMolecules;
    public CanvasGroup[] tabbedMenuMateriales;

    #endregion

    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        materialManager = FindObjectOfType<MaterialManager>();
        infoPanelElements = transform.Find("InfoContainerElementos").GetComponent<CanvasGroup>();
        infoPanelMolecule = transform.Find("InfoContainerMoleculas").GetComponent<CanvasGroup>();
        infoPanelMaterial = transform.Find("InfoContainerMateriales").GetComponent<CanvasGroup>();
    }

    #region Metodos


    /*
    * metodo principal para el manejo de la apertura del panel INFOPANEL padre de los otros paneles de informacion
    * infocontainer de atomos, moleculas y materiales
    */
    public void OpenBottomPanel()
    {
        restoreAlphasTabbedMenu();

        // traigo todos los atomos seleccionados
        var selectedAtoms = atomManager.GetSelectedAtoms();
        var selectedMolecules = moleculeManager.GetSelectedMolecules();
        var selectedMaterials = materialManager.GetSelectedMaterials();

        // si hay un solo atomo seleccionado y ninguna molecula o material, muestro info panel
        if (selectedAtoms.Count == 1 && selectedMolecules.Count == 0 && selectedMaterials.Count == 0 && infoPanelElements.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
            setInactiveRayCast(infoPanelMolecule);
            setInactiveRayCast(infoPanelMaterial);
        }
    
        // si hay una sola molecula seleccionada y ningun atomo o material, muestro info panel
        else if (selectedAtoms.Count == 0 && selectedMolecules.Count == 1 && selectedMaterials.Count == 0 && infoPanelMolecule.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
            setInactiveRayCast(infoPanelMaterial);
        }


        // si hay un solo material seleccionada y ningun atomo o molecula, muestro info panel
        else if (selectedAtoms.Count == 0 && selectedMolecules.Count == 0 && selectedMaterials.Count == 1 && infoPanelMaterial.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
        }

        //verifica que quiza tenga que cerrar algun panel
        CloseBottomPanel();
    }

    /*
     * metodo principal para el manejo del cierre del panel INFOPANEL padre de los otros paneles de informacion
     * infocontainer de atomos, moleculas y materiales
     */
    public void CloseBottomPanel()
    {
        restoreAlphasTabbedMenu();

        if (infoPanelElements.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);
            setActiveRayCast(infoPanelMolecule);
            setActiveRayCast(infoPanelMaterial);
        }

        if (infoPanelMolecule.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);
            setActiveRayCast(infoPanelMaterial);
        }

        if (infoPanelMaterial.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);
        }
    }

    //que si algun panel esta en 1 quiere decir que el usuario en algun momento LO ACTIVO.. por lo tanto... tendria que ver ver la forma de hacer una combinacion
    public void CloseBottomPanelCombine()
    {
        restoreAlphasTabbedMenu();

        // traigo todos los atomos seleccionados
        var selectedAtoms = atomManager.GetSelectedAtoms();
        var selectedMolecules = moleculeManager.GetSelectedMolecules();
        var selectedMaterials = materialManager.GetSelectedMaterials();

        //son 6 combinaciones puesto que tengo 3 paneles con 2 condiciones por cada uno
        if (infoPanelElements.alpha == 1 && infoPanelMolecule.alpha == 0 && selectedMolecules.Count == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//quita elementos
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//muestra moleculas

            setActiveRayCast(infoPanelElements);
            setActiveRayCast(infoPanelMolecule);
            setInactiveRayCast(infoPanelMaterial);
        }

        if (infoPanelElements.alpha == 1 && infoPanelMaterial.alpha == 0 && selectedMaterials.Count == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//quita elementos
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//muestra materiales

            setActiveRayCast(infoPanelElements);
            setActiveRayCast(infoPanelMolecule);
            setActiveRayCast(infoPanelMaterial);
        }

        if (infoPanelMolecule.alpha == 1 && infoPanelElements.alpha == 0 && selectedAtoms.Count == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//quita moleculas
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//muestra elementos

            setActiveRayCast(infoPanelElements);
            setInactiveRayCast(infoPanelMolecule);
            setInactiveRayCast(infoPanelMaterial);
        }

        if (infoPanelMolecule.alpha == 1 && infoPanelMaterial.alpha == 0 && selectedMaterials.Count == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//quita moleculas
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//muestra materiales

            setActiveRayCast(infoPanelElements);
            setActiveRayCast(infoPanelMolecule);
            setActiveRayCast(infoPanelMaterial);
        }

        if (infoPanelMaterial.alpha == 1 && infoPanelElements.alpha == 0 && selectedAtoms.Count == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//muestra elementos
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//quita materiales

            setActiveRayCast(infoPanelElements);
            setInactiveRayCast(infoPanelMolecule);
            setInactiveRayCast(infoPanelMaterial);
        }

        if (infoPanelMaterial.alpha == 1 && infoPanelMolecule.alpha == 0 && selectedMolecules.Count == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//muestra moleculas
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//quita materiales

            setActiveRayCast(infoPanelElements);
            setActiveRayCast(infoPanelMolecule);
            setInactiveRayCast(infoPanelMaterial);
        }
    }


    /* este fix es para que siempre si se cierra el panel vuelva a mostrar el tab nomclatures, esto es asi porque originalmente se hacia con ACTIVE.. y con alpha.. complica
     * si se deja como inactive / active los datos no pueden ser cargados desde la base de datos.   
     */
    private void restoreAlphasTabbedMenu()
    {
        foreach (CanvasGroup tab in tabbedMenuMolecules)
        {
            if (tab.gameObject.name == "Content1")
                tab.alpha = 1;
            else
                tab.alpha = 0;
        }

        foreach (CanvasGroup tab in tabbedMenuMateriales)
        {
            if (tab.gameObject.name == "Content1")
                tab.alpha = 1;
            else
                tab.alpha = 0;
        }
    }


    /* Este fix tiene que ver con la "herencia y/o orden" de los GO en el canvas, como este hilo contenedor tiene 3 paneles el ultimo, siempre esta activo
     * dandole prioridad sobre los otros anteriores, aqui hay 3 paneles osea que hay 2 inactivos, una opcion es utilizar SETACTIVE nativo de unity pero
     * el problema es que habria que ir activando y desactivando a cada rato cada vez que tiene que ir a buscar  la base de datos porque sino no carga los datos
     * sobre un objeto INACTIVO. Por eso usamos CANVASGROUP y se evalua eso sin usar ACTIVE O INACTIVE 
     */
    private void setInactiveRayCast(CanvasGroup objActivar)
    {
        objActivar.blocksRaycasts = false;
    }

    private void setActiveRayCast(CanvasGroup objActivar)
    {
        objActivar.blocksRaycasts = true;
    }

    #endregion
}
