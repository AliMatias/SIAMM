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
    private CanvasGroup infoPanelIsotopos;

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
        infoPanelIsotopos = transform.Find("InfoContainerIsotopos").GetComponent<CanvasGroup>();
    }

    #region Metodos


    /*
    * metodo principal para el manejo de la apertura del panel INFOPANEL padre de los otros paneles de informacion
    * infocontainer de atomos, moleculas y materiales
    */
    public void OpenBottomPanel()
    {
        // cierro todos los paneles
        CloseAll();
        restoreAlphasTabbedMenu();

        // traigo todos los atomos seleccionados
        var selectedAtoms = atomManager.GetSelectedAtoms();
        var selectedMolecules = moleculeManager.GetSelectedMolecules();
        var selectedMaterials = materialManager.GetSelectedMaterials();

        // si hay un solo atomo seleccionado y ninguna molecula o material, muestro info panel
        if (selectedAtoms.Count == 1)
        {
            //obtiene el tipo de elemento seleccionado si es un isotopo o no
            if (atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.atom)
            {
                if (selectedMolecules.Count == 0 && selectedMaterials.Count == 0 && infoPanelElements.alpha == 0)
                {
                    gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
                    // activo el blockRayCast para el panel que estoy abriendo y lo desactivo para el resto
                    setActiveRayCast(infoPanelElements);
                    setInactiveRayCast(infoPanelMolecule);
                    setInactiveRayCast(infoPanelMaterial);
                    setInactiveRayCast(infoPanelIsotopos);
                }
            }

            else if (atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.isotopo)
            {
                if (selectedMolecules.Count == 0 && selectedMaterials.Count == 0 && infoPanelIsotopos.alpha == 0)
                {
                    gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
                    // activo el blockRayCast para el panel que estoy abriendo y lo desactivo para el resto
                    setInactiveRayCast(infoPanelElements);
                    setInactiveRayCast(infoPanelMolecule);
                    setInactiveRayCast(infoPanelMaterial);
                    setActiveRayCast(infoPanelIsotopos);
                }
            }
        }

        // si hay una sola molecula seleccionada y ningun atomo o material, muestro info panel
        else if (selectedAtoms.Count == 0 && selectedMolecules.Count == 1 && selectedMaterials.Count == 0 && infoPanelMolecule.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
            // activo el blockRayCast para el panel que estoy abriendo y lo desactivo para el resto
            setInactiveRayCast(infoPanelElements);
            setActiveRayCast(infoPanelMolecule);
            setInactiveRayCast(infoPanelMaterial);
            setInactiveRayCast(infoPanelIsotopos);
        }


        // si hay un solo material seleccionada y ningun atomo o molecula, muestro info panel
        else if (selectedAtoms.Count == 0 && selectedMolecules.Count == 0 && selectedMaterials.Count == 1 && infoPanelMaterial.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
            // activo el blockRayCast para el panel que estoy abriendo y lo desactivo para el resto
            setInactiveRayCast(infoPanelElements);
            setInactiveRayCast(infoPanelMolecule);
            setActiveRayCast(infoPanelMaterial);
            setInactiveRayCast(infoPanelIsotopos);
        }
    }

    /**
     * Cierro todos los paneles abiertos y desactivo todos los blockRayCast
     */
    private void CloseAll()
    {
        if (infoPanelElements.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);
        }

        if (infoPanelMolecule.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);
        }

        if (infoPanelMaterial.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);
        }

        if (infoPanelIsotopos.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);
        }

        setInactiveRayCast(infoPanelElements);
        setInactiveRayCast(infoPanelMolecule);
        setInactiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    /*
     * Metodo publico para cerrar paneles
     */
    public void CloseBottomPanel()
    {
        CloseAll();
        restoreAlphasTabbedMenu();
    }

    //que si algun panel esta en 1 quiere decir que el usuario en algun momento LO ACTIVO.. por lo tanto... tendria que ver ver la forma de hacer una combinacion
    public void CloseBottomPanelCombine()
    {
        restoreAlphasTabbedMenu();

        // traigo todos los atomos seleccionados
        var selectedAtoms = atomManager.GetSelectedAtoms();
        var selectedMolecules = moleculeManager.GetSelectedMolecules();
        var selectedMaterials = materialManager.GetSelectedMaterials();

        //son 9 combinaciones puesto que tengo 3 paneles con 3 condiciones por cada uno
        //atoms
        if (infoPanelElements.alpha == 1)        
        {
            if (infoPanelMolecule.alpha == 0 && selectedMolecules.Count == 1)
            {
                CombineAtomMolecules();
            }

            if (infoPanelMaterial.alpha == 0 && selectedMaterials.Count == 1)
            {
                CombineAtomMaterials();
            }

            if (infoPanelIsotopos.alpha == 0 && selectedAtoms.Count == 1 && atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.isotopo)
            {
                CombineAtomIsotopos();
            }
        }

        //molecules
        if (infoPanelMolecule.alpha == 1)
        {
            if (infoPanelElements.alpha == 0 && selectedAtoms.Count == 1 && atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.atom)
            {
                CombineMoleculesAtom();
            }

            if (infoPanelMaterial.alpha == 0 && selectedMaterials.Count == 1)
            {
                CombineMoleculesMaterials();
            }

            if (infoPanelIsotopos.alpha == 0 && selectedAtoms.Count == 1 && atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.isotopo)
            {
                CombineMoleculesIsotopos();
            }
        }

        //materiales
        if (infoPanelMaterial.alpha == 1)
        {
            if (infoPanelElements.alpha == 0 && selectedAtoms.Count == 1 && atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.atom)
            {
                CombineMaterialsAtoms();
            }

            if (infoPanelMolecule.alpha == 0 && selectedMolecules.Count == 1)
            {
                CombineMaterialsMolecules();
            }

            if (infoPanelIsotopos.alpha == 0 && selectedAtoms.Count == 1 && atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.isotopo)
            {
                CombineMaterialsIsotopos();
            }
        }

        //isotopos
        if (infoPanelIsotopos.alpha == 1)
        {
            if (infoPanelMolecule.alpha == 0 && selectedMolecules.Count == 1)
            {
                CombineIsotoposMolecules();
            }

            if (infoPanelMaterial.alpha == 0 && selectedMaterials.Count == 1)
            {
                CombineIsotoposMaterials();
            }

            if (infoPanelElements.alpha == 0 && selectedAtoms.Count == 1 && atomManager.GetTypeSelectedAtoms() == TypeAtomEnum.atom)
            {
                CombineIsotoposAtoms();
            }
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

    #region Metodos Show Combinaciones 

    private void CombineAtomIsotopos()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//quita elementos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);//muestra isotopos

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setActiveRayCast(infoPanelMaterial);
        setActiveRayCast(infoPanelIsotopos);
    }

    private void CombineAtomMaterials()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//quita elementos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//muestra materiales

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setActiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineAtomMolecules()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//quita elementos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//muestra moleculas

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setInactiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineMoleculesAtom()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//quita moleculas
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//muestra elementos

        setActiveRayCast(infoPanelElements);
        setInactiveRayCast(infoPanelMolecule);
        setInactiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineMoleculesMaterials()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//quita moleculas
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//muestra materiales

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setActiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineMoleculesIsotopos()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//quita moleculas
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);//muestra isotopos

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setActiveRayCast(infoPanelMaterial);
        setActiveRayCast(infoPanelIsotopos);
    }


    private void CombineMaterialsAtoms()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//muestra elementos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//quita materiales

        setActiveRayCast(infoPanelElements);
        setInactiveRayCast(infoPanelMolecule);
        setInactiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineMaterialsMolecules()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//muestra moleculas
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//quita materiales

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setInactiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineMaterialsIsotopos()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);//muestra isotopos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//quita materiales

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setActiveRayCast(infoPanelMaterial);
        setActiveRayCast(infoPanelIsotopos);
    }

    private void CombineIsotoposAtoms()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);//quita isotopos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//muestra elementos

        setActiveRayCast(infoPanelElements);
        setInactiveRayCast(infoPanelMolecule);
        setInactiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineIsotoposMaterials()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);//quita isotopos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMaterial.gameObject);//muestra materiales

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setActiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    private void CombineIsotoposMolecules()
    {
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelIsotopos.gameObject);//quita isotopos
        gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelMolecule.gameObject);//muestra moleculas

        setActiveRayCast(infoPanelElements);
        setActiveRayCast(infoPanelMolecule);
        setInactiveRayCast(infoPanelMaterial);
        setInactiveRayCast(infoPanelIsotopos);
    }

    #endregion

    #endregion
}
