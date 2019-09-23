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

    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
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
        // traigo todos los atomos seleccionados
        var selectedAtoms = atomManager.GetSelectedAtoms();
        var selectedMolecules = moleculeManager.GetSelectedMolecules();

        // si hay un solo atomo seleccionado y ninguna molecula, muestro info panel
        if (selectedAtoms.Count == 1 && selectedMolecules.Count == 0 && infoPanelElements.alpha == 0)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
            //cgInfoPanel.alpha = 1;
        }


        //ACA DEBERIA MOSTRAR MOLECULAS!!!
        //else if (selectedAtoms.Count == 1 && selectedMolecules.Count == 0 && infoPanelElements.alpha == 0)
        //{
        //    gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);//el uifader lo tiene instanciado el padre de todos los panels
        //    //cgInfoPanel.alpha = 1;
        //}



        else if (infoPanelElements.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);
            //cgInfoPanel.alpha = 0;
        }
    }

    /*
     * metodo principal para el manejo del cierre del panel INFOPANEL padre de los otros paneles de informacion
     * infocontainer de atomos, moleculas y materiales
     */
    public void CloseBottomPanel()
    {
        if (infoPanelElements.alpha == 1)
        {
            gameObject.GetComponent<UIFader>().FadeInAndOut(infoPanelElements.gameObject);
            //cgInfoPanel.alpha = 0;
        }

    }
}
