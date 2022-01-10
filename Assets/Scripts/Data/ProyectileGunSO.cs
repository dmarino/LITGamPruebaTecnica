using UnityEngine;

[CreateAssetMenu(fileName ="New Proyectile Gun", menuName ="Gun System/Guns/ProyectileGun")]
public class ProyectileGunSO : BaseGunSO
{

    public DefaultProjectile projectilePrefab;
    public override GameObject InstanciateProjectile(Transform muzzle)
    {
        DefaultProjectile projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        return projectile.gameObject;
    }
}
