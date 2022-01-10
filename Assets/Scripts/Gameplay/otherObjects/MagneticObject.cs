using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MagneticObject : MonoBehaviour
{
    private void Awake()
    {
        gameObject.tag = "magnetic";
    }
}
