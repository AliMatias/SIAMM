using UnityEngine;

//clase que se encarga de darle el efecto de selección al átomo
[RequireComponent(typeof(MeshRenderer))]
public class HighlightObject : MonoBehaviour
{
    //tiempo de animación
    public float animationTime = 1f;
    //límite de brillo
    public float threshold = 1.5f;
    
    private Material material;
    private Color normalColor;
    private Color selectedColor;

    private void Awake()
    {
        //agarra el material del componente
        material = GetComponent<MeshRenderer>().material;
        normalColor = material.color;
        //color seleccionado. Es el color normal + un threshold
        selectedColor = new Color(
          Mathf.Clamp01(normalColor.r * threshold),
          Mathf.Clamp01(normalColor.g * threshold),
          Mathf.Clamp01(normalColor.b * threshold)
        );
    }

    //inicia una animación de selección
    public void StartHighlight()
    {
        iTween.ColorTo(gameObject, iTween.Hash(
          "color", selectedColor,
          "time", animationTime,
          "easetype", iTween.EaseType.linear,
          "looptype", iTween.LoopType.pingPong
        ));
    }

    //finaliza la animación de selección
    public void StopHighlight()
    {
        iTween.Stop(gameObject);
        material.color = normalColor;
    }
}