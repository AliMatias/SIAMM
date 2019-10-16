using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MainInfoTips : MonoBehaviour
{

    #region atributos

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


    /*Utilizado desde el Material Manager*/
    public void SetInfoTips(int id)
    {
        Debug.Log("CARGA TIP DE DB Y MUESTRA ASISTENTE VALIDANDO SI..");
      
        TipsData tipsData = qryTip.GetTipById(id);

        //if (material != null)
        //{
        //    nameLblMaterial.text = material.Name;
        //    //carga los datos especiales de la molecula en el panel especial
        //    PanelInfoLoader.SetPanelInfoMaterial(material);
        //}
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
