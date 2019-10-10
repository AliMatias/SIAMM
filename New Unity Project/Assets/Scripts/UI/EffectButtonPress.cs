using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectButtonPress : MonoBehaviour
{
    public Text myText = null;
    private int counter = 0;
    public string newText;
    private string originalText;

    public void changeText()
    {
        originalText = myText.text;
        counter++;
        if (counter % 2 == 1)
        {
            myText.text = originalText;
        }
        else
        {
            myText.text = newText;
        }
    }
  
}
