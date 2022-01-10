using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Bubble Gun ", menuName ="Gun System/Guns/BubbleGun")]
public class BubbleGunSO : BaseGunSO
{
    public BubbleProjectile projectilePrefab;
    
    [Range(0.5f,0.15f)]
    public float bubbleUpForce;
    public int bubbleTimer;

    public override void Shoot(Transform muzzle, Transform camera)
    {
        BubbleProjectile projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        projectile._bubbleTime = bubbleTimer;
        projectile._bubbleForce = bubbleUpForce;

        Vector3 direction = camera.transform.forward.normalized + _shootingForce;
        projectile.GetComponent<Rigidbody>().AddRelativeForce(direction);

        Destroy(projectile.gameObject, _proyectileLifeSpan);
    }
}
