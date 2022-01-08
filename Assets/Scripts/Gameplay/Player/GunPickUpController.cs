using System;
using UnityEngine;

// i could have done this in the player controller 
//but i think this way is more organized
public class GunPickUpController : MonoBehaviour
{
    [SerializeField] private float _pickUpDistance = 4f;
    [SerializeField] private float _pickUpLookAngle = 0.9f;

    [SerializeField] private Transform gunPosition;

    private DefaultGun _closestGun;
    private DefaultGun _currentGun;

    public void UpdateClosestGun()
    {
        //if i already have a gun i don't need to do this
        if(_currentGun != null) return;

        DefaultGun closest = null;
        float closestDistance = _pickUpDistance;

        foreach (DefaultGun gun in DefaultGun.gunList)
        {
            if (IsGunATarget(gun) == true)
            {
                float distanceToGun = Vector3.Distance(transform.position, gun.transform.position);

                //if its the closest
                if (distanceToGun < closestDistance)
                {
                   
                    closestDistance = distanceToGun;
                    closest = gun;
                }
            }

            gun.UnTarget();
        }

        _closestGun = closest;
        if(_closestGun!= null)
        {
            _closestGun.Target();
        }
    }

    public bool IsGunATarget(DefaultGun gun)
    {
        float distanceToGun = Vector3.Distance(transform.position, gun.transform.position);

        //if the gun its in range
        if (distanceToGun <= _pickUpDistance)
        {
            //if i'm looking at it
            Vector3 dirToGun = (gun.transform.position - transform.position).normalized;
            if (Vector3.Dot(dirToGun, transform.forward) > _pickUpLookAngle)
            {
                //if there is nothing in between
                RaycastHit hit;
                Physics.Raycast(transform.position, gun.transform.position - transform.position, out hit, distanceToGun);
                if (hit.collider.gameObject == gun.gameObject)
                {
                    return true;

                }

            }
        }

        return false;
    }
    public void PickUp()
    {
        //if i already have a gun i won't pick up another until i drop this one
        //if there isn't a gun near i won't do anything
        if(_currentGun!= null || _closestGun==null) return;

        _currentGun = _closestGun;
        _currentGun.PickUp(gunPosition);
    }

    public void Drop()
    {
        if(_currentGun==null) return;

        _currentGun.Drop();
        _currentGun = null;
    }


    public void Shoot()
    {
        throw new NotImplementedException();
    }
}
