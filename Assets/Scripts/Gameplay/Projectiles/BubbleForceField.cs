using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BubbleForceField : MonoBehaviour
{
    public float upForce;
    private Rigidbody _rigidBody;
    public int timer;
    [HideInInspector] public GameObject objectInside;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(Pop());

    }
    private void FixedUpdate()
    {
        //force to float
        _rigidBody.AddForce(Physics.gravity * -1 + new Vector3(0, upForce, 0));

        //this keeps the object in the center
        objectInside.transform.SetParent(transform);
        objectInside.transform.localPosition = Vector3.zero;

        //this keeps the object inside from moving
        Rigidbody b = objectInside.gameObject.GetComponent<Rigidbody>();
        if (b != null)
        {
            b.velocity = Vector3.zero;
            b.angularVelocity = Vector3.zero;
            b.useGravity = false;
        }

    }

    //a corutine to pop the bubble 
    //i didn't want to use destroy after time because i had to set
    //the object inside parent to null and activate the rigid body
    private IEnumerator Pop()
    {
        yield return new WaitForSeconds(timer); 

        //returns the object back to normal
        objectInside.transform.position = transform.position;
        objectInside.transform.SetParent(null);

        Rigidbody b = objectInside.gameObject.GetComponent<Rigidbody>();
        if (b != null)
        {
            b.useGravity = true;
        }

        //destroys the bubble
        Destroy(this.gameObject);

    }
}
