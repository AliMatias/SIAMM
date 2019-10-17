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

    private bool disableTips;

    public bool DisableTips { get => disableTips; set => disableTips = value; }

    #endregion

    void Start()
    {
        //se instancia las clases para querys
        GameObject go = new GameObject();
        go.AddComponent<QryTips>();
        qryTip = go.GetComponent<QryTips>();

        //estado inicial! tips activos! NO persistente en el tiempo solo en runtime
        disableTips = false;

        //carga un struct del tipo id - cantapariciones
        InitializeCounter();
    }

    /*
     * este es llamado desde otras clases al momento de crear un tip
     * el tip mas actual! reemplaza el existente destruye si hay uno ya y crea uno nuevo
     */
    public void LaunchTips(int id)
    {
        if (!disableTips)
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

            //aparecera el tip.. a lo sumo luego de "x" iteracciones (para evitar que aparezca siempre)
            discountCounter(id);         
        }
    }

    /*
     * spawn asistente u obtiene la instancia ya creada y la borra si esta existe
     */
    public TipsObject SpawnTipsCharacter()
    {
        TipsObject newTip = FindObjectOfType<TipsObject>();//esta instanciado?

        //CREA el tip GO SI NO ESTA CREADO
        if (newTip == null)
        {
            newTip = GetTipPos();
        }
        else
        {
            DeleteTip(newTip);
            newTip = GetTipPos();//otra instancia nueva.. le da efecto sobre el asistente tambien
        }

        return newTip;
    }


    /*
     * La posicion esta "harcodeada" en el propio prefab y necesita la referencia del transfor del canvas
     */
    private TipsObject GetTipPos()
    {
        TipsObject newTip = Instantiate<TipsObject>(TipSiammPrefab);
        newTip.transform.SetParent(canvas.transform, false);
        /*los valores de position estan seteados directos en el prefab que se puede ver la referencia en el canvas en forma grafica*/
        return newTip;
    }

    /*
     * Quitar tip
     */
    public void DeleteTip(TipsObject tip)
    {     
        //lo destruyo
        Destroy(tip);
    }

    /*
     * inicializa la esctructura id,cant iteraciones
     */
    private void InitializeCounter()
    {
        List<int> tipId = qryTip.GetTipsIds();

        foreach (int id in tipId)
        {
            counterIdTip.Add(id, counterShow);// del tipo 1,5 / 2,5 / 3,5...
        }
    }

    /*
     * si de deshabilita los tips que vuelva a resetear los contadores
     */
    private void ResetCounter()
    {
        counterIdTip.Clear();
        InitializeCounter();
    }

    /*
     * logica para contador de que cada ciertas iteracciones de un mismo IDTIP vuelva a reaparecer el asistente
     * para evitar que aparezca a cada instante
     */
    private void discountCounter(int id)
    {
        int valorActual = GetIndexFromDictionary(id);

        if (valorActual != 1)
        {
            SetIndexFromDictionary(id, valorActual-1);
        }
        else //ya llego al tope, restauro
        {
            SetIndexFromDictionary(id, counterShow);
        }

    }
   
    /*
     * método para obtener el valor
     */
    private int GetIndexFromDictionary(int id)
    {
        return counterIdTip[id];
    }

    //metodo para setear el valor de una key determinada
    private void SetIndexFromDictionary(int id, int valor)
    {
        counterIdTip[id] = valor;
    }


    /*
     * metodo para desactivar llamado desde el menu
     */
    public void setDisabledAllTips()
    {
        disableTips = true;
        ResetCounter();//reseteo los contadores
    }

    /*
     * metodo para REactivar llamado desde el menu
     */
    public void setEnabledAllTips()
    {
        disableTips = false;
        ResetCounter();//reseteo los contadores
    }

}
