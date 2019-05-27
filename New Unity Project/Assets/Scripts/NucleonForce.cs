using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleonForce : MonoBehaviour
{
    public Rigidbody rigidbody;

    public void FixedUpdate()
    {
        Vector3 parentPosition = this.transform.parent.gameObject.transform.position;
        Vector3 direction = parentPosition - this.transform.position;
        float distance = direction.magnitude;
        if (distance == 0f)
            return;
        Vector3 force = direction.normalized * 1.4f;
        this.rigidbody.AddForce(force);
    }
}
