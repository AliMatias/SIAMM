using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadTper : MonoBehaviour
{
    private Button button;
    //tener en cuenta que este componente lo envia desde la interface! sino da error null pointer
    public DBManager DBManager;

    //estos parametros son estaticos en mi modelo 
    private int row = 12;
    private int col = 23;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform parent = gameObject.GetComponent<RectTransform>();

        GridLayoutGroup glg = this.GetComponent<GridLayoutGroup>();

        //rezise de acuerdo a resolucion
        glg.cellSize = new Vector2(parent.rect.width / col, parent.rect.height / row);

        //Recorro todas las celdas que tienen un game object
        for (int i = 0; i < glg.transform.childCount; i++)
        {
            button = glg.transform.GetChild(i).GetComponent<Button>();

            //si no es NULL quiere decir que MAPEO un boton ahi tengo que ir a la base de datos
            if (button != null)
            {
                //Debug.Log(button.name);
                LoadData(button);
            }
        }
    }

    private void LoadData (Button elem)
    {
        ElementTabPer element = new ElementTabPer();

        //obtiene datos del elemento según cantidad de protones
        element = DBManager.GetElementFromNro(getNroAtomicoId(elem));

        Text[] textosObj = elem.GetComponentsInChildren<Text>();

        //recorro todos los game object que contiene el boton
        for (int j = 0; j < textosObj.Length; j++)
        {
            if (textosObj[j].name == "txtDistElect")
                textosObj[j].text = element.ConfElectronica;
            if (textosObj[j].name == "txtPeso")
                textosObj[j].text = Convert.ToString(element.PesoAtomico);
            if (textosObj[j].name == "txtNombre")
                textosObj[j].text = element.Simbol;
        }
    }


    public int getNroAtomicoId (Button elem)
    {
        int nroAtomico = 0;

        Text[] textosObj = elem.GetComponentsInChildren<Text>();

        for (int j = 0; j < textosObj.Length; j++)
        {
            if (textosObj[j].name == "txtNroAtomico")         
                nroAtomico = Convert.ToInt32(textosObj[j].text);        
        }

        return nroAtomico;
    }

    public void LoadInfoBasica(string elementName)
    {
        ElementInfoBasic elementoInfoBasic = new ElementInfoBasic();

        elementoInfoBasic = DBManager.GetElementInfoBasica(elementName);

        Debug.Log("TRAIGO INFO!! " + elementoInfoBasic.Name);
    }

}
