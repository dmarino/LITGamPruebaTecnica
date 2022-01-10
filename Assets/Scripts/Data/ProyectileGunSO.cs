using UnityEngine;

[CreateAssetMenu(fileName ="New Proyectile Gun", menuName ="Gun System/Guns/ProyectileGun")]
public class ProyectileGunSO : BaseGunSO
{

    public DefaultProjectile proyectilePrefab;
    public override void Shoot(Transform pos)
    {
        DefaultProjectile proyectile = Instantiate(proyectilePrefab, pos.position, pos.rotation);
        proyectile.GetComponent<Rigidbody>().AddRelativeForce(_shootingForce);

        Destroy(proyectile.gameObject, _proyectileLifeSpan);
    }
}
