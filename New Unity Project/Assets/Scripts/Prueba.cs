using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{

    private TipsManager t;

    void Start()
    {
        t = FindObjectOfType<TipsManager>();
    }

    public void exec()
    {
        t.GetTips(1);
    }
}
