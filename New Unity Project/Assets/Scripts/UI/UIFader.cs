using System.Collections;
using UnityEngine;

//Aparecer y desaparecer la tabla periódica
public class UIFader : MonoBehaviour
{
    //para comunicar accion en la corutina!
    private bool active = false;
    private bool inactive = false;
    private bool activeRaycast = false;
    private bool inactiveRaycast = false;

    /**
    * Override para especificar que elemento CANVAS GROUP mostrar
    */
    public void FadeIn(GameObject ElementWithCanvasGroup)
    {
        StartCoroutine(fadeCanvasGroup(ElementWithCanvasGroup.GetComponent<CanvasGroup>(), ElementWithCanvasGroup.GetComponent<CanvasGroup>().alpha, 1));
    }

    /**
    * Override para especificar que elemento CANVAS GROUP a ocultar
    */
    public void FadeOut(GameObject ElementWithCanvasGroup)
    {
        //Inicia una corutina (thread) para el efecto sobre el panel
        StartCoroutine(fadeCanvasGroup(ElementWithCanvasGroup.GetComponent<CanvasGroup>(), ElementWithCanvasGroup.GetComponent<CanvasGroup>().alpha, 0));
    }

    /*este metodo se usa en varios go que estan desactivados y hacen efecto fade y luego activan!*/
    public void FadeInAndOutWithActive(GameObject ElementWithCanvasGroup)
    {
        active = true;

        if (ElementWithCanvasGroup.GetComponent<CanvasGroup>().alpha == 0)
        {
            ElementWithCanvasGroup.SetActive(true);

            FadeIn(ElementWithCanvasGroup);
        }
        else
            FadeOut(ElementWithCanvasGroup);
    }

    //Se separan en metodos para hacer mas entendible desde el llamado de la UI
    public void FadeInAndOutWithDesactive(GameObject ElementWithCanvasGroup)
    {
        inactive = true;

        if (ElementWithCanvasGroup.GetComponent<CanvasGroup>().alpha == 0)
        {
            ElementWithCanvasGroup.SetActive(false);

            FadeIn(ElementWithCanvasGroup);
        }
        else
            FadeOut(ElementWithCanvasGroup);
    }

    /**
     * especificar que elemento CANVAS GROUP ocultar/mostrar sin interaccion de activar o desactivar el objeto
     */
    public void FadeInAndOut(GameObject ElementWithCanvasGroup)
    {
        if (ElementWithCanvasGroup.GetComponent<CanvasGroup>().alpha == 0)
        {
            FadeIn(ElementWithCanvasGroup);
        }
        else
            FadeOut(ElementWithCanvasGroup);
    }

    /*este metodo se usa para GO que tienen rayscast desactivados desactivados y hacen efecto fade y luego activan!*/
    public void FadeInAndOutWithActiveRaycast(GameObject ElementWithCanvasGroup)
    {
        activeRaycast = true;

        if (ElementWithCanvasGroup.GetComponent<CanvasGroup>().alpha == 0)
        {
            ElementWithCanvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;

            FadeIn(ElementWithCanvasGroup);
        }
        else
            FadeOut(ElementWithCanvasGroup);
    }


    public void FadeInAndOutWithInactiveRaycast(GameObject ElementWithCanvasGroup)
    {
        inactiveRaycast = true;

        if (ElementWithCanvasGroup.GetComponent<CanvasGroup>().alpha == 0)
        {
            ElementWithCanvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;

            FadeIn(ElementWithCanvasGroup);
        }
        else
            FadeOut(ElementWithCanvasGroup);
    }


    /*metodo para generar el efecto de fade in and out*/
    public IEnumerator fadeCanvasGroup(CanvasGroup cg, float start, float end)
    {
        float lerpTime = 0.5f;//tiempo de retardo en los frames.. para generar el efecto fade
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float porcentajeComplete = timeSinceStarted / lerpTime;

        while (true)
        {

            timeSinceStarted = Time.time - _timeStartedLerping;
            porcentajeComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, porcentajeComplete);

            cg.alpha = currentValue;

            if (porcentajeComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        //validar si hay que activar o desactivar el objeto contenedor del canvasgroup
        if (end == 0)
        {
            //cuando termina el efecto tiene que volver a desactivarlo
            if (active)
            {
                cg.gameObject.SetActive(false);
            }
            //cuando termina el efecto tiene que volver a activarlo (por ahora no se esta usando... se deja el metodo igual)
            if (inactive)
            {
                cg.gameObject.SetActive(true);
            }
            //cuando termina el efeto y tiene que DESBLOQUEAR el raycast 
            if (activeRaycast)
            {
                cg.blocksRaycasts = false;
            }

            if (inactiveRaycast)
            {
                cg.blocksRaycasts = true;
            }
        }
    }
}
