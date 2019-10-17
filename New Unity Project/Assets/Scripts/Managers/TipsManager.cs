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

    private Dictionary<int, bool> EnableIdTip = new Dictionary<int, bool>();
    private bool disableTips;
    
    #endregion

    void Start()
    {
        //se instancia las clases para querys
        GameObject go = new GameObject();
        go.AddComponent<QryTips>();
        qryTip = go.GetComponent<QryTips>();

        disableTips = false;//estado inicial! tips activos!

        //carga un struct del tipo id - cantapariciones
        InitializeCounter();

        InitializeEnabledIdTip();
    }

    //este es llamado desde otras clases al momento de crear un tip
    //el tip mas actual! reemplaza el existente destruye si hay uno ya y crea uno nuevo
    public void GetTips(int id)
    {
        if (!disableTips)
        {
            bool estadoIdActual = GetIndexFromDictionaryEnable(id);

            if (estadoIdActual)//ese idtip esta activo
            {
                int valorActual = GetIndexFromDictionary(id);

                if (valorActual == counterShow)//si es igual al original estado.. ahi si muestro.. sino tiene que esperar a que resetee
                {
                    //CREA el tip GO SI NO ESTA CREADO o lo destruye antes si ya estaba generado
                    TipsObject GoTip = SpawnTipsCharacter();
                    TipsData tipsData = qryTip.GetTipById(id);

                    if (tipsData != null)
                    {
                        GoTip.IdTip = tipsData.Id;
                        GoTip.setText(tipsData.ToString);
                    }
                }

                //aparecera el tip.. a lo sumo luego de 5 interacciones (para evitar que aparezca siempre)
                discountCounter(id);
            }
        }
    }

    /*spawn asistente u obtiene la instancia ya creada y la borra si esta existe*/
    public TipsObject SpawnTipsCharacter()
    {
        TipsObject newTip = FindObjectOfType<TipsObject>();

        //CREA el tip GO SI NO ESTA CREADO
        if (newTip == null)
        {
            newTip = GetTipPos();
        }
        else
        {
            DeleteTip(newTip);
        }

        return newTip;
    }


    /*La posicion por ahora esta "harcodeada"*/
    private TipsObject GetTipPos()
    {
        TipsObject newTip = Instantiate<TipsObject>(TipSiammPrefab);
        newTip.transform.SetParent(canvas.transform, false);
        /*los valores de position estan seteados directos en el prefab que se puede ver la referencia en el canvas en forma grafica*/
        //newTip.transform.localScale = new Vector3(1, 1, 1);
        //newTip.transform.position = new Vector3(-100, 40, 14);
        //newTip.transform.localPosition = new Vector3(0.33f * Screen.width, -0.30f * Screen.height, 0);
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


    private void ResetCounter()
    {
        foreach (var item in counterIdTip.Keys)
        {
            SetIndexFromDictionary(item, counterShow);
        }
    }


    private void discountCounter(int id)
    {
        int valorActual = GetIndexFromDictionary(id);

        if (valorActual != 0)
        {
            SetIndexFromDictionary(id, valorActual-1);
        }
        else //ya llego al tope, restauro
        {
            SetIndexFromDictionary(id, counterShow);
        }

    }

    private void InitializeEnabledIdTip()
    {
        List<int> tipId = qryTip.GetTipsIds();

        foreach (int id in tipId)
        {
            EnableIdTip.Add(id, true);
        }
    }

    /*desactivar*/
    public void setDisabledTipId(int id)
    {
        SetIndexFromDictionaryEnable(id, false);
        SetIndexFromDictionary(id, counterShow);//reseteo el contador 
    }

    public void setDisabledAllTips()
    {
        disableTips = true;
        ResetCounter();//reseteo los contadores
    }


    //método para obtener el valor
    private int GetIndexFromDictionary(int id)
    {
        return counterIdTip[id];
    }

    private void SetIndexFromDictionary(int id, int valor)
    {
        counterIdTip[id] = valor;
    }


    //método para obtener el valor
    private bool GetIndexFromDictionaryEnable(int id)
    {
        return EnableIdTip[id];
    }

    private void SetIndexFromDictionaryEnable(int id, bool valor)
    {
        EnableIdTip[id] = valor;
    }


}
