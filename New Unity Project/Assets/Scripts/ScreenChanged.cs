using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenChanged : MonoBehaviour
{
    #region Declaraciones
    public Animator animator;
    private int screenToLoad;
    public Image image;
    #endregion
    
    void Start()
    {
        image.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       //solo debug para apretar el mouse y ahi haga la transicion, asi esta en el video
        // if (Input.GetMouseButtonDown(0))
        //    FadeToScreen(1);
    }

    /*para poder hacer la transicion automatica sin el codigo
    del metodo del update en los frames*/
    public void finishTime()
    {
        FadeToScreen(1);
    }
    
    /*dispara el evento trigger que se coloco en el objeto
    animations screenchanged*/
    public void FadeToScreen(int indexScreen)
    {
        screenToLoad = indexScreen;
        animator.SetTrigger("Fade");
    }

    /*Metodo que se usa en el animation al final del fadeout
    para pasar a la escena principal*/
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(screenToLoad);
    }
}
