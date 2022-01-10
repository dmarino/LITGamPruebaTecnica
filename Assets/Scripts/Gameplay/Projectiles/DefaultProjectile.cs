using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//just handles the particles of the projectiles
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
