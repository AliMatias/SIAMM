using UnityEngine;

public class ElectronOrbit : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 90 * Time.deltaTime);
    }
}
