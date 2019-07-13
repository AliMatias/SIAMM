using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour
{
    #region Declaraciones
    public float letterTime;
    public string message;
    public TextMesh messageText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        messageText.GetComponent<Text>();
        message = messageText.text;
        messageText.text = "";
        StartCoroutine(TextAnimated());
    }


    /*toma letra por letra de la cadena y la escribe dejando un tiempo delay entre cada una*/
    IEnumerator TextAnimated()
    {
        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterTime);//genera un retardo en los frames

        }
    }
}
