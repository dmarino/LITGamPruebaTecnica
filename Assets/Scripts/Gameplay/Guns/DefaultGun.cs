using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class DefaultGun : MonoBehaviour
{

    public static List<DefaultGun> gunList = new List<DefaultGun>();

    private Rigidbody _rigidBody;
    private BoxCollider _collider;

    [SerializeField] private GameObject _tagertFeedBack;

    private void Awake()
    {
        gunList.Add(this);
        _tagertFeedBack.SetActive(false);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }

    public void Shoot()
    {

    }

    public void Target()
    {
        _tagertFeedBack.SetActive(true);
    }

    
    public void UnTarget()
    {
        _tagertFeedBack.SetActive(false);
    }

    public void PickUp(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        _rigidBody.isKinematic = true;
        _collider.isTrigger = true;

        UnTarget();
    }

    public void Drop()
    {
        transform.SetParent(null);
        _rigidBody.isKinematic = false;
        _collider.isTrigger = false;
    }
}
