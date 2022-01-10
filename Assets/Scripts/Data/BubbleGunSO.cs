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

    public override void Shoot(Transform pos)
    {
        BubbleProjectile projectile = Instantiate(projectilePrefab, pos.position, pos.rotation);
        projectile._bubbleTime = bubbleTimer;
        projectile._bubbleForce = bubbleUpForce;
        projectile.GetComponent<Rigidbody>().AddRelativeForce(_shootingForce);

        Destroy(projectile.gameObject, _proyectileLifeSpan);
    }
}
