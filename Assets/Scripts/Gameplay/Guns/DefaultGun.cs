using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DefaultGun : MonoBehaviour
{
    //this is so i can store all guns easily
    public static List<DefaultGun> gunList = new List<DefaultGun>();

    [HideInInspector] public BaseGunSO gunSO; //is public cause this is set with the loader, if i could drag the SO then it wouldn´t be hid

    [SerializeField] private GameObject _tagertFeedBack;

    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private Transform _shootingPoint;


    private Rigidbody _rigidBody;
    private Collider _collider;

    private int _bulletsLeft;

    private void Awake()
    {
        gunList.Add(this);
        _tagertFeedBack.SetActive(false);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _bulletsLeft = gunSO._magazineSize;
        _nameLabel.text = gunSO.gunName;
    }

    public void Shoot()
    {

        if (_bulletsLeft > 0)
        {
            gunSO.Shoot(_shootingPoint);
            _bulletsLeft--;
        }
        else
        {
            StartCoroutine(Reload());
        }

    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(gunSO._cooldownTime);
        _bulletsLeft = gunSO._magazineSize;

    }

    public void Target()
    {
        _tagertFeedBack.SetActive(true);
    }


    public void UnTarget()
    {
        _tagertFeedBack.SetActive(false);
    }

    public void PickUp(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        _rigidBody.isKinematic = true;
        _collider.isTrigger = true;

        UnTarget();
    }

    public void Drop()
    {
        transform.SetParent(null);
        _rigidBody.isKinematic = false;
        _collider.isTrigger = false;
    }

    public string GiveAmmoFeedback()
    {
        return $"{_bulletsLeft}/{gunSO._magazineSize}";
    }
}
