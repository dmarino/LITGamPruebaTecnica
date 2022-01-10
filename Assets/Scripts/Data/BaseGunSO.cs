using UnityEngine;

public abstract class BaseGunSO : ScriptableObject
{
    [Header("Gun Info")]

    public string gunName;
    public DefaultGun gunPrefab;
    public Vector3 spawnPosition;
    [SerializeField] protected Vector3 _shootingForce;

    [Range(0,5)] public int _cooldownTime;

    [Range(1,15)] public int _magazineSize;
    

    [Header("Projectile Info")]

    [Range(2,10)] [SerializeField] protected int _proyectileLifeSpan;
    public abstract void Shoot(Transform pos);
}
