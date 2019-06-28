﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/*Carga la info básica de los elementos*/
public class BasicInfoLoader : MonoBehaviour
{
    #region atributos
    //panel padre (asignado por interfaz)
    public GameObject panel;
    //array de textos a modificar
    private TextMeshProUGUI[] texts;

    //book asignado por interface
    public Book FlipBookActivo;

    private Button buttonimg;
    public Button ButtonimgTPER { get => buttonimg; set => buttonimg = value; }
    private Button[] btnInBook;

    public Sprite[] estructCristalina;
    private Image[] estCristInBook;
    #endregion

    private void Awake()
    {
        //obtengo array de textos para después modificar en todos los hijos del panel padre busca
        //no importa si tengo como en este caso panel de panel...
        texts = panel.GetComponentsInChildren<TextMeshProUGUI>();
        //obtengo los botones que haya en el panel de infotper TODOS
        btnInBook = panel.GetComponentsInChildren<Button>();
        //obtengo las fotos del panel las que haya
        estCristInBook = panel.GetComponentsInChildren<Image>();
    }

    //setea la info básica y muestra el panel
    public void SetBasicInfo(ElementInfoBasic elementInfoBasic)
    {
        foreach (TextMeshProUGUI a in texts)
        {

            if (a.name == "txtNombre")
                a.text = elementInfoBasic.Name;
            if (a.name == "txtNroAtomico")
            {
                a.text = "Nro atómico: " + elementInfoBasic.Nroatomico;
                setButtonConfig();
            }
            if (a.name == "txtSimbol")
                a.text = "Símbolo: " + elementInfoBasic.Simbol;
            if (a.name == "txtPeso")
                a.text = "Peso atómico: " + elementInfoBasic.PesoAtomico;
            if (a.name == "txtPeriodo")
                a.text = "Período: " + elementInfoBasic.Periodo;
            if (a.name == "txtColor")
                a.text = "Color: " + elementInfoBasic.Color;
            if (a.name == "txtClasificacion")
                a.text = "Clasificación: " + elementInfoBasic.Clasificacion;
            if (a.name == "txtClasificacionGrupo")
                a.text = "Clasificación Grupo: " + elementInfoBasic.Clasificacion_grupo;
            if (a.name == "txtValencia")
                a.text = "Valencia: " + elementInfoBasic.Valencia;
            if (a.name == "txtEstadoNatural")
                a.text = "Estado Natural: " + elementInfoBasic.Estado_natural;
            if (a.name == "txtEstructuraCrist")
            {
                a.text = "Estructura Cristalina: " + elementInfoBasic.EstructuraCristalina;
                setImageEstCrist(elementInfoBasic.EstructuraCristalina);
            }
            if (a.name == "txtNroOxi")
                a.text = "Números Oxidación: " + elementInfoBasic.NumerosOxidacion;
            if (a.name == "txtConfElectronica")
                a.text = "Configuración Electrónica: " + elementInfoBasic.ConfElectronica;
            if (a.name == "txtPtoFusion")
                a.text = "Punto de Fusión: " + elementInfoBasic.PuntoFusion;
            if (a.name == "txtPtoEbullicion")
                a.text = "Punto de Ebullicón: " + elementInfoBasic.PuntoEbullicion;
            if (a.name == "txtResumen")
                a.text = elementInfoBasic.Resumen;

        }
        panel.SetActive(true);
    }

    //Cierra panel de info básica
    public void CloseBasicInfoPanel()
    {
        //desactivo el panel principal
        panel.SetActive(false);
        //reseteo de variables del flip BOOK porque sino quedaba en la pagina que habia dejado aunque venga otro nuevo elemento
        FlipBookActivo.currentPage = 0;
        //metodo que se hizo publico para poder manejar la actulizacion de los sprite sin tener que interactuar con un drag o boton sobre el book
        FlipBookActivo.UpdateSprites();
    }

    //setea los datos al boton del book para simular una copia del boton que fue presionado
    private void setButtonConfig()
    {
        //atributos que vienen del boton original
        string nombre = "";
        string nro = "";
        string peso = "";
        string dist = "";
        int tamNombre = 0;
        int tamNro = 0;
        int tamDist = 0;
        int tamPeso = 0;

        Text[] textosObjLlamados = buttonimg.GetComponentsInChildren<Text>();
        Color color = buttonimg.GetComponent<Image>().color;

        foreach (Text origen in textosObjLlamados)
        {
            if (origen.name == "txtNombre")
            {
                nombre = origen.text;
                tamNombre = origen.fontSize;
            }
            if (origen.name == "txtPeso")
            {
                peso = origen.text;
                tamPeso = origen.fontSize;
            }
            if (origen.name == "txtNroAtomico")
            {
                nro = origen.text;
                tamNro = origen.fontSize;
            }
            if (origen.name == "txtDistElect")
            {
                dist = origen.text;
                tamDist = origen.fontSize;
            }
        }
        ///////////////////////////////////
        //asigno a destino

        foreach (Button a in btnInBook)
        {
            //solo al boton prefact
            if (a.name == "btnElemImg")
            {
                //obtengo la lista de objetos o coleccion de objetos de tipo TEXT que estan en los botones
                Text[] textosObj = a.GetComponentsInChildren<Text>();

                foreach (Text destino in textosObj)
                {
                    if (destino.name == "txtNombre")
                    {
                        destino.text = nombre;
                        //destino.fontSize = tamNombre;
                    }
                    if (destino.name == "txtPeso")
                    {
                        destino.text = peso;
                        //destino.fontSize = tamPeso;
                    }
                    if (destino.name == "txtNroAtomico")
                    {
                        destino.text = nro;
                       //destino.fontSize = tamNro;
                    }
                    if (destino.name == "txtDistElect")
                    {
                        destino.text = dist;
                       //destino.fontSize = tamDist;
                    }
                }

                a.GetComponent<Image>().color = color;
                //ResizeFont(a);
            }
        }
    }

    //metodo para el tamaño actual del boton para este panel del book (por el momento no se usa)
    private void ResizeFont(Button elem)
    {
        /*busco una proporcion aprox!!!*/
        int xOriginal = 90;
        int sizeActual;
        int sizeProporcionCell = Convert.ToInt32(elem.GetComponent<RectTransform>().rect.height);

        //obtengo la lista de objetos o coleccion de objetos de tipo TEXT que estan en los botones
        Text[] textosObj = elem.GetComponentsInChildren<Text>();

        //recorro todos los game object que contiene el boton, se podria hacer por orden de objetos, como estan creados en el boton
        for (int j = 0; j < textosObj.Length; j++)
        {
            sizeActual = textosObj[j].fontSize;
            textosObj[j].fontSize = (sizeProporcionCell * sizeActual) / xOriginal;
        }
    }

    //como es informacion estatica se utiliza enviando los sprites por interface
    private void setImageEstCrist(string nombre)
    {
        foreach (Image a in estCristInBook)
        {
            if (a.name == "imgEstCristalina")
            {
                if (nombre == "Cúbica Centrada en el Cuerpo")
                    a.sprite = estructCristalina[0];
                else if (nombre == "Cúbica Centrada en las Caras")
                    a.sprite = estructCristalina[1];
                else if (nombre == "Cúbica Simple")
                    a.sprite = estructCristalina[2];
                else if (nombre == "Empacado Tetraédrico")
                    a.sprite = estructCristalina[3];
                else if (nombre == "Hexagonal Simple")
                    a.sprite = estructCristalina[4];
                else if (nombre == "Monoclínica Centrada en la Base")
                    a.sprite = estructCristalina[5];
                else if (nombre == "Monoclínica Simple")
                    a.sprite = estructCristalina[6];
                else if (nombre == "Ortorrómbica Centrada en la Base")
                    a.sprite = estructCristalina[7];
                else if (nombre == "Ortorrómbica Centrada en la Cara")
                    a.sprite = estructCristalina[8];
                else if (nombre == "Ortorrómbica Simple")
                    a.sprite = estructCristalina[9];
                else if (nombre == "Tetragonal Centrada")
                    a.sprite = estructCristalina[10];
                else if (nombre == "Triclínica Simple")
                    a.sprite = estructCristalina[11];
                else if (nombre == "Trigonal Simple")
                    a.sprite = estructCristalina[12];
            }
        }
    }
}
