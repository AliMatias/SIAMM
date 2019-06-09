using UnityEngine;

//script que se encarga de orbitar el electrón alrededor del núcleo.
public class ElectronOrbit : MonoBehaviour
{
    
    void FixedUpdate()
    {
        //obtengo la posición del padre (Atom)
        Vector3 parentPosition = this.transform.parent.gameObject.transform.position;
        //rota sobre el eje vertical de la posición del padre
        transform.RotateAround(parentPosition, Vector3.up, 90 * Time.deltaTime);
    }
}
