using UnityEngine;

public class UIPopupQuestionNewProy : MonoBehaviour
{

    private bool showPopUp = false;
    private string popUpMessage = "";
    private string popUpTitle = "";

    private AtomManager atom;
    private MoleculeManager mol;
    private MaterialManager mat;

    public bool ShowPopUp { get => showPopUp; set => showPopUp = value; }
    public string PopUpMessage { get => popUpMessage; set => popUpMessage = value; }
    public string PopUpTitle { get => popUpTitle; set => popUpTitle = value; }

    public void MostrarPopUp(string title, string message)
    {
        PopUpTitle = title;
        PopUpMessage = message;
        showPopUp = true;
    }

    public void OcultarPopUp()
    {
        showPopUp = false;
    }

    public void OnGUI()
    {
        if (showPopUp)
        {
            // Arma el pop up
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 150), ShowGUI, popUpTitle);
        }
    }

    //INSTANCIA UNA VEZ para la interfaz que lo referencia por eso hay MAS DE UNA clase uipopup
    public void ShowGUI(int windowID)
    {
        //no ejecuta el awake por eso se coloca aca las referencias
        atom = FindObjectOfType<AtomManager>();
        mol = FindObjectOfType<MoleculeManager>();
        mat = FindObjectOfType<MaterialManager>();

        // Label que muestra mensaje
        GUI.Label(new Rect(65, 40, 200, 50), popUpMessage);

        // Boton OK para cerrar
        if (GUI.Button(new Rect(50, 100, 75, 30), "OK"))
        {
            OcultarPopUp();
            atom.DeleteAllAtoms();
            mol.DeleteAllMolecules();
            mat.DeleteAllMaterials();
        }
        else if (GUI.Button(new Rect(200, 100, 75, 30), "Cancelar"))
        {
            OcultarPopUp();
        }
    }
}
