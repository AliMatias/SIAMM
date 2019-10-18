using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTooltip : MonoBehaviour
{
    private Animator tooltip;
    // Start is called before the first frame update
    void Start()
    {
        tooltip = GetComponent<Animator>();
        tooltip.SetBool("Scaling",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
