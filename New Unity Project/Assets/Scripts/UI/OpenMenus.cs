using UnityEngine;
using UnityEngine.UI;

//script que abre el panel de tabla periódica
public class OpenMenus : MonoBehaviour
{
    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    CanvasGroup cgInfoPanel;

    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        cgInfoPanel = gameObject.GetComponent<CanvasGroup>();
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
        if (selectedAtoms.Count == 1 && selectedMolecules.Count == 0 && cgInfoPanel.alpha == 0)
        {
            //gameObject.GetComponent<UIFader>().FadeInAndOut(cgInfoPanel);
            cgInfoPanel.alpha = 1;
        }
        else if (cgInfoPanel.alpha == 1)
        {
            //gameObject.GetComponent<UIFader>().FadeInAndOut(cgInfoPanel);
            cgInfoPanel.alpha = 0;
        }
    }

    /*
     * metodo principal para el manejo del cierre del panel INFOPANEL padre de los otros paneles de informacion
     * infocontainer de atomos, moleculas y materiales
     */
    public void CloseBottomPanel()
    {
        if (cgInfoPanel.alpha == 1)
        {
            //gameObject.GetComponent<UIFader>().FadeInAndOut(cgInfoPanel);
            cgInfoPanel.alpha = 0;
        }

    }
}
