using System;
using TMPro;
using UnityEngine;

//this class manages all gun info of the player
// i could have done this in the player controller 
//but i think this way is more organized
public class GunPickUpController : MonoBehaviour
{
    [SerializeField] private float _pickUpDistance = 4f;
    [SerializeField] private float _pickUpLookAngle = 0.9f;

    [SerializeField] private Transform gunPosition;

    [SerializeField] private TextMeshProUGUI _ammoLabel;

    private DefaultGun _closestGun;
    private DefaultGun _currentGun;


    //i actually don't like doing this on the update but the poin of 
    //this label was to show when the gun reloaded and that occurs in a courutine
    //i would prefer to use events next time
    private void Update()
    {
        if(_ammoLabel==null) return;

        _ammoLabel.text = "";
        if(_currentGun!=null)
        {
            _ammoLabel.text = _currentGun.GiveAmmoFeedback();
        }
        
    }

    public void UpdateClosestGun()
    {
        //if i already have a gun i don't need to do this
        if(_currentGun != null) return;

        DefaultGun closest = null;
        float closestDistance = _pickUpDistance;

        //for all guns created in the scene
        foreach (DefaultGun gun in DefaultGun.GunList)
        {
            //if they can be a target (player is looking in that direction they are in range)
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
                if (hit.collider && hit.collider.gameObject == gun.gameObject)
                {
                    return true;

                }

            }
        }

        return false;
    }

    //pick the gun
    public void PickUp()
    {
        //if i already have a gun i won't pick up another until i drop this one
        //if there isn't a gun near i won't do anything
        if(_currentGun!= null || _closestGun==null) return;

        _currentGun = _closestGun;
        _currentGun.PickUp(gunPosition);

        
    }

    //drop the gun
    public void Drop()
    {
        if(_currentGun==null) return;

        _currentGun.Drop();
        _currentGun = null;

        _ammoLabel.text = "";
    }

    //shoot the current gun
    public void Shoot(Transform camera)
    {
        if(_currentGun==null) return;

        _currentGun.Shoot(camera);
    }
}
