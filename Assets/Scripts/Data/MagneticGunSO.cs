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
    public override GameObject InstanciateProjectile(Transform muzzle)
    {
        //setting the info of the projectile
        MagneticProjectile projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        projectile._magneticRadius = MagneticRange;
        projectile._magneticForce = MagneticForce;
        projectile._magneticMinDistance = MagneticMinDistance;

        return projectile.gameObject;
    }
}
