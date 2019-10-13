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
    //contenedor para la info de atomos luego ira cada uno de los otros
    public CanvasGroup infoContainer;

    //objeto con el que interactúo para acceder a la DB
    private QryTips qryTip;

    #endregion

    private void Awake()
    {
        //se instancia las clases para querys
        GameObject go = new GameObject();
        go.AddComponent<QryTips>();
        qryTip = go.GetComponent<QryTips>();
    } 


    /*Utilizado desde el Material Manager*/
    public void SetInfoTips(int id)
    {

        //MaterialData material = qryMaterial.GetMaterialById(mapping.MaterialId);

        //if (material != null)
        //{
        //    nameLblMaterial.text = material.Name;
        //    //carga los datos especiales de la molecula en el panel especial
        //    PanelInfoLoader.SetPanelInfoMaterial(material);
        //}
    }

}
