using TMPro;
using UnityEngine;

public class LabelRotation : MonoBehaviour
{
    private Camera camera;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
    }

    private void FixedUpdate()
    {
        transform.LookAt(camera.transform);
        transform.Rotate(0, 180, 0);
    }
}
