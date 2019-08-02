using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDraw : MonoBehaviour
{
    public float ThetaScale = 0.01f;
    public float radius { get; set; } = 1f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;

    void Create(float radius)
    {
        this.radius = radius;
    }

    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 2f);
        LineDrawer.positionCount = Size;

        // modificar el segundo parametro para cambiar el ancho de la linea
        LineDrawer.widthCurve = AnimationCurve.Linear(0, 0.01f, 0, 0f);

        // obtiene la posicion y del padre
        Vector3 parentLocalPosition = transform.parent.localPosition;
        float y = parentLocalPosition.y;

        // dibuja los puntos del circulo
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta) + parentLocalPosition.x;
            float z = radius * Mathf.Sin(Theta) + parentLocalPosition.z;
            LineDrawer.SetPosition(i, new Vector3(x, y, z));
        }
    }
}
