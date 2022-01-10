using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Magnetic Gun", menuName ="Gun System/Guns/MagneticGun")]
public class MagneticGunSO : BaseGunSO
{

    public MagneticProjectile projectilePrefab;

    public float MagneticMinDistance;
    public float MagneticRange;
    public float MagneticForce;

    public override void Shoot(Transform pos)
    {
        MagneticProjectile projectile = Instantiate(projectilePrefab, pos.position, pos.rotation);
        projectile._magneticRadius = MagneticRange;
        projectile._magneticForce = MagneticForce;
        projectile._magneticMinDistance = MagneticMinDistance;

        projectile.GetComponent<Rigidbody>().AddRelativeForce(_shootingForce);
        Destroy(projectile.gameObject, _proyectileLifeSpan);
    }
}
