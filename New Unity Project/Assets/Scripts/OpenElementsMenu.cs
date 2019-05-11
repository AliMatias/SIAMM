using UnityEngine;
using UnityEngine.UI;

public class OpenElementsMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;

    public void OpenPanel()
    {
        Animator animator = panel.GetComponent<Animator>();
        
        if(animator != null)
        {
            bool opened = animator.GetBool("open");
            animator.SetBool("open", !opened);
        }
        if(button != null)
        {
            Text buttonText = button.GetComponentInChildren<Text>();
            if(buttonText.text == "<")
            {
                buttonText.text = ">";
            }
            else
            {
                buttonText.text = "<";
            }
        }
    }
}
