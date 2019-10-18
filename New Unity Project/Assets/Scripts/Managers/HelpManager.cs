using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject help1;
    public GameObject help2;
    private int shown = 0;

    public void switchHelp(){
        switch(shown){
            case 0:
                helpPanel.SetActive(true);
                help1.SetActive(true);
                shown = 1;
                break;
            case 1:
                help1.SetActive(false);
                help2.SetActive(true);
                shown = 2;
                break;
            case 2:
                help2.SetActive(false);
                helpPanel.SetActive(false);
                shown = 0;
                break;
        }
    }
}
