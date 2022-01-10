using UnityEngine;


public class MagneticProjectile : DefaultProjectile
{
    public LayerMask _magneticLayers;
    public float _magneticForce; 
    public float _magneticRadius;
    public float _magneticMinDistance;
    private Rigidbody _rigidBody;
    private Collider _collider;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    //i found it better to do the attraction in the fixed update rather than in 
    //a trigger Enter kinda situation
    //maybe the other way is more efficient tho
    private void FixedUpdate()
    {
        Attract();
    }

    private void Attract()
    {
        //i get all the colliders in the layers marked as magnetic
        Collider[] colliders = Physics.OverlapSphere(transform.position, _magneticRadius, _magneticLayers);
        foreach (Collider collider in colliders)
        {
            //if the object is farther than the min distance then i add an attraction force
            //i decided to do it this way rather than using an orbit formula because 
            //in orbit atracction (ej. planets) both objects put force in one another
            //this would've made the projectile deviate from the path 
            if (Vector3.Distance(transform.position, collider.transform.position) > _magneticMinDistance)
            {
                Vector3 attractionForce = (collider.transform.position - transform.position).normalized * _magneticForce * -1;
                collider.GetComponent<Rigidbody>().AddForce(attractionForce);

            }
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        //if the object is magnetic ignore the collition
        //this way the projectile doesn't go in a random direction
        if (other.gameObject.tag == "magnetic")
        {
            Physics.IgnoreCollision(other.collider, _collider);
        }
        else
        {
            base.OnCollisionEnter(other);
        }
    }


    //testing the overlap sphere
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _magneticRadius);
    }
}
