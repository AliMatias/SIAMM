using UnityEngine;

//script que se encarga de orbitar el electrón alrededor del núcleo.
public class ElectronOrbit : MonoBehaviour
{
    
    void FixedUpdate()
    {
        //rota sobre el eje vertical
        transform.RotateAround(Vector3.zero, Vector3.up, 90 * Time.deltaTime);
    }
}
