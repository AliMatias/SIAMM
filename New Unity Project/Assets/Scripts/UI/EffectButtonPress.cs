using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectButtonPress : MonoBehaviour
{
    public Text myText;
    private bool original = true;
    private string originalText;
    private GameObject buttonAtomAdd;
    private UIToolTipControl openTper;

    void Start()
    {
        buttonAtomAdd = GameObject.Find("AddAtomBtn");//efecto especial cuando se abre la tabla periodica
        originalText = myText.text;//guarda el texto original
        openTper = FindObjectOfType<UIToolTipControl>();
    }

    public void changeButtonTextTper()
    {
        string newText = "Cerrar Tabla";

        if (original)
        {
            original = !original;
            myText.text = newText;

            activateDeactiveButtonAtomadd();
        }
        else
        {
            original = !original;
            myText.text = originalText;

            activateDeactiveButtonAtomadd();
        }
    }

    private void activateDeactiveButtonAtomadd()
    {
        Button bObj = buttonAtomAdd.GetComponentInChildren<Button>();
        bObj.interactable = !bObj.interactable;
    }
}
