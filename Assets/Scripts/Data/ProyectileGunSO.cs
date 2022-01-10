using UnityEngine;

[CreateAssetMenu(fileName ="New Proyectile Gun", menuName ="Gun System/Guns/ProyectileGun")]
public class ProyectileGunSO : BaseGunSO
{

    public DefaultProjectile projectilePrefab;
    public override void Shoot(Transform muzzle, Transform camera)
    {
        DefaultProjectile projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        Vector3 direction = camera.transform.forward.normalized + _shootingForce;
        projectile.GetComponent<Rigidbody>().AddRelativeForce(direction);

        Destroy(projectile.gameObject, _proyectileLifeSpan);
    }
}
