using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DefaultProjectile : MonoBehaviour 
{ 
    [SerializeField] private ParticleSystem _explotion;
    protected virtual void OnCollisionEnter(Collision other)
    {
        _explotion.transform.SetParent(null);
        _explotion.Play();
        Destroy(gameObject);
    }

}
