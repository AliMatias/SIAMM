using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//Carga tabla periódica
public class LoadTper : MonoBehaviour
{

    #region atributos
    private Button button;
    private DBManager DBManager;
    private GridLayoutGroup glg;
    private RectTransform parent;
    //estos parametros son estaticos en mi modelo! son estaticos
    private int row = 12;
    private int col = 23;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //instancio la clase de metodos sobre la base de datos
        DBManager = FindObjectOfType<DBManager>();

        ResizeCells();

        //Recorro todas las celdas que tienen un game object
        for (int i = 0; i < glg.transform.childCount; i++)
        {
            button = glg.transform.GetChild(i).GetComponent<Button>();

            //si no es NULL quiere decir que MAPEO un boton ahi tengo que ir a la base de datos
            if (button != null)
            {
                ResizeFont(button);
                LoadData(button);
            }
        }
    }

    /*Metodo para el seteo de los objetos TEXT de cada boton de la tabla periodica*/
    private void LoadData (Button elem)
    {
        ElementTabPer element = new ElementTabPer();
       
        //obtiene datos del elemento según cantidad de protones
        element = DBManager.GetElementFromNro(getNroAtomicoId(elem));

        //obtengo la lista de objetos o coleccion de objetos de tipo TEXT que estan en los botones
        Text[] textosObj = elem.GetComponentsInChildren<Text>();
       
        //recorro todos los game object que contiene el boton, se podria hacer por orden de objetos, como estan creados en el boton
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

    /*metodo para el dynamic size de las celdas del grid layout*/
    private void ResizeCells()
    {
        //obtengo el objeto "rectangulo" para poder tomar las medias del PANEL!! ya que este script es componente del mismo
        parent = gameObject.GetComponent<RectTransform>();

        //obtengo la referencia al objeto grid layout -> el this es referencia sobre al PANEL
        glg = this.GetComponent<GridLayoutGroup>();

        //rezise de acuerdo a resolucion del PENEL! sobre el objeto grid layout
        glg.cellSize = new Vector2(parent.rect.width / col, parent.rect.height / row);
    }

    /*metodo para el tamaño de las fuentes de los botones de la tabla periodica, se utiliza una proporcion de acuerdo
    al tamaño ORIGINAL usado para el tamaño de una resoluion de pantalla de 15" */
    private void ResizeFont(Button elem)
    {
        /*busco una proporcion aprox!!!*/
        int xOriginal = 70;
        int sizeActual;    
        int sizeProporcionCell = Convert.ToInt32(glg.cellSize.x);

        elem.GetComponent<RectTransform>().sizeDelta = new Vector2(glg.cellSize.y, glg.cellSize.x);

        //obtengo la lista de objetos o coleccion de objetos de tipo TEXT que estan en los botones
        Text[] textosObj = elem.GetComponentsInChildren<Text>();

        //recorro todos los game object que contiene el boton, se podria hacer por orden de objetos, como estan creados en el boton
        for (int j = 0; j < textosObj.Length; j++)
        {
            sizeActual = textosObj[j].fontSize;
            textosObj[j].fontSize = (sizeProporcionCell * sizeActual) / xOriginal;
        }
    }

    /*Obtiene el nro atomico a partir del "string" que tiene en el boton como txtNroAtomico*/
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

    //trae de la DB la info básica
    public ElementInfoBasic LoadInfoBasica(string elementName)
    {
        ElementInfoBasic elementInfoBasic = new ElementInfoBasic();

        elementInfoBasic = DBManager.GetElementInfoBasica(elementName);

        return elementInfoBasic;
    }

}
