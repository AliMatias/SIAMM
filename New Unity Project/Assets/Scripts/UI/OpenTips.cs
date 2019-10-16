using UnityEngine;
using UnityEngine.UI;

public class OpenTips : MonoBehaviour
{
    #region Atributos

    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    private MaterialManager materialManager;

    private CanvasGroup toolTipGlobe;
    private CanvasGroup SiammTip;

    #endregion

    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        materialManager = FindObjectOfType<MaterialManager>();
    }

    #region Metodos

    /**
     * metodo principal para el manejo de mostrar el tooltip con el TIP
     */
    public void ShowTip()
    {
        Debug.Log("MUESTRO TIP"); //el tipito esta mostrandose en pantalla
    }


    /**
     * metodo principal para el manejo de cerrar el tooltip con el TIP
     */
    public void CloseAsistent()
    {
        Debug.Log("CIERRO TIP Y ASISTENTE");//BOTON DERECHO
        //gameObject.GetComponent<UIFader>().FadeInAndOut(gameObject);
    }


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
