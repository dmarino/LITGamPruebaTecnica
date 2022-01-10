using UnityEngine;

public abstract class BaseGunSO : ScriptableObject
{
    [Header("Gun Info")]

    public string gunName;
    public DefaultGun gunPrefab;
    public Vector3 spawnPosition;
    public Vector3 _shootingForce;

    [Range(0,5)] public int _cooldownTime;

    [Range(1,15)] public int _magazineSize;
    

    [Header("Projectile Info")]
    [Range(2,10)] public int _proyectileLifeSpan;

    //method that instanciates the projectile and sets all it's parameters
    public abstract GameObject InstanciateProjectile(Transform muzzle);
}
