using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TipsManager : MonoBehaviour
{

    #region atributos
    public TipsObject TipSiammPrefab;
    public GameObject canvas;// the canvas get transform 

    //objeto con el que interactúo para acceder a la DB
    private QryTips qryTip;
    private Dictionary<int, int> counterIdTip = new Dictionary<int, int>();
    private const int counterShow = 5;

    #endregion

    void Start()
    {
        //se instancia las clases para querys
        GameObject go = new GameObject();
        go.AddComponent<QryTips>();
        qryTip = go.GetComponent<QryTips>();

        //carga un struct del tipo id - cantapariciones
        InitializeCounter();
    }

    //este es llamado desde otras clases al momento de crear un tip
    public void SpawnTipsCharacter(int id)
    {
        //CREA el tip GO
        TipsObject newTip = GetTipPos();

        Debug.Log("CARGA TIP DE DB Y MUESTRA ASISTENTE VALIDANDO SI..");

       
        TipsData tipsData = qryTip.GetTipById(id);

        if (tipsData != null)
        {
            //    nameLblMaterial.text = material.Name;
            //    //carga los datos especiales de la molecula en el panel especial
            //    PanelInfoLoader.SetPanelInfoMaterial(material);
            Debug.Log(tipsData.Descripcion);
        }
    }



    /*La posicion por ahora esta "harcodeada"*/
    private TipsObject GetTipPos()
    {
        TipsObject newTip = Instantiate<TipsObject>(TipSiammPrefab);
        newTip.transform.SetParent(canvas.transform, false);
        newTip.transform.localScale = new Vector3(1, 1, 1);
        //newTip.transform.localPosition = new Vector3(480, -185, 14);
        newTip.transform.localPosition = new Vector3(-0.13f * Screen.width, 0.04f * Screen.height, 0);

        return newTip;
    }

    //Quitar tip
    public void DeleteTip(TipsObject tip)
    {
        //lo destruyo
        Destroy(tip);
    }


    private void InitializeCounter()
    {
        List<int> tipId = qryTip.GetTipsIds();

        foreach (int id in tipId)
        {
            counterIdTip.Add(id, counterShow);// del tipo 1,5 / 2,5 / 3,5...
        }
    }


    //método para obtener el material
    private int GetIndexFromDictionary(int id)
    {
        return counterIdTip[id];
    }

    private void SetIndexFromDictionary(int id, int valor)
    {
        counterIdTip[id] = valor;
    }

}
