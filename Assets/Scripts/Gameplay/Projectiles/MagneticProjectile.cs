using UnityEngine;


public class MagneticProjectile : DefaultProjectile
{
    public LayerMask _magneticLayers;
    [HideInInspector] public float _magneticForce;
    [HideInInspector] public float _magneticRadius;

    [HideInInspector] public float _magneticMinDistance;
    private Rigidbody _rigidBody;
    private Collider _collider;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

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
            if (Vector3.Distance(transform.position, collider.transform.position) > _magneticMinDistance)
            {
                Vector3 attractionForce = (collider.transform.position - transform.position).normalized * _magneticForce * -1;
                collider.GetComponent<Rigidbody>().AddForce(attractionForce);

            }
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
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
