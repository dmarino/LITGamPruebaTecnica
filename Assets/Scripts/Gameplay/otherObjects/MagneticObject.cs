using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//i think i don't need this class anymore because now i'm using a layer instead of the tag 
//to do the magnetic effect

[RequireComponent(typeof(Rigidbody))]
public class MagneticObject : MonoBehaviour
{
    private void Awake()
    {
        gameObject.tag = "magnetic";
    }
}
