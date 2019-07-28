using UnityEngine;

//script que aplica la fuerza a los nucleones que los mantiene pegados
public class NucleonForce : MonoBehaviour
{
    //rigidbody propio
    public Rigidbody rigidbody;

    //este método se ejecuta cada un cierto tiempo.
    public void FixedUpdate()
    {
        //obtengo la posición del padre. (Atom)
        Vector3 parentPosition = this.transform.parent.gameObject.transform.position;
        //calculo la dirección
        Vector3 direction = parentPosition - this.transform.position;
        //la distancia
        float distance = direction.magnitude;
        //si estoy en el mismo punto no hay fuerza y vuelve
        if (distance == 0f)
            return;
        //obtengo la fuerza con el vector dirección normalizado * una constante que definimos nosotros
        Vector3 force = direction.normalized * 1.4f;
        //aplicamos la fuerza al nucleon
        this.rigidbody.AddForce(force);
    }
}
