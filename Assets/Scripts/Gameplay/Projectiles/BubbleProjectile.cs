using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleProjectile : DefaultProjectile
{

    [SerializeField] private BubbleForceField _fieldPrefab;
    public int _bubbleTime = 3;

    public float _bubbleForce;
    
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        //there is defenitly a better way to handle this exceptions
        if (other.gameObject.layer != LayerMask.NameToLayer("Terrain") && other.gameObject.tag != "Player" && other.gameObject.tag != "projectile")
        {
            //i ignore this collition so the thing doesn't move because of the bullet
            Physics.IgnoreCollision(other.collider, _collider);

            //set bubble info
            BubbleForceField bubble = Instantiate(_fieldPrefab, other.transform.position, other.transform.rotation);
            bubble.timer = _bubbleTime;
            bubble.objectInside = other.gameObject;

            Destroy(this.gameObject);
        }
    }
}
