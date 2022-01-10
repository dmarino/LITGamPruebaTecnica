using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DefaultGun : MonoBehaviour
{
    //this is so i can store all guns easily
    public static List<DefaultGun> GunList = new List<DefaultGun>();

    [HideInInspector] public BaseGunSO GunSO; //is public cause this is set with the loader, if i could drag the SO then it wouldn´t be hid

    [Header("UI feedback")]
    [SerializeField] private GameObject _tagertFeedBack;
    [SerializeField] private TextMeshProUGUI _nameLabel;

    [Header("Shooting")]
    [SerializeField] private Transform _shootingPoint;

    [Header("Audio")]

    [SerializeField] AudioSource _shootSound;
    [SerializeField] AudioSource _reloadSound;


    private Rigidbody _rigidBody;
    private Collider _collider;

    private int _bulletsLeft;

    private bool _reloading;

    private void Awake()
    {
        GunList.Add(this);
        _tagertFeedBack.SetActive(false);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _bulletsLeft = GunSO._magazineSize;
        _nameLabel.text = GunSO.gunName;
    }

    public void Shoot()
    {

        if (_bulletsLeft > 0)
        {
            _shootSound.Play();
            GunSO.Shoot(_shootingPoint);
            _bulletsLeft--;

            if(_bulletsLeft==0 && _reloading==false)
            {
                StartCoroutine(Reload());
            }
        }

    }

    private IEnumerator Reload()
    {
        _reloading = true;
        yield return new WaitForSeconds(GunSO._cooldownTime);
        _bulletsLeft = GunSO._magazineSize;
        _reloadSound.Play();
        _reloading = false;

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
        return $"{_bulletsLeft}/{GunSO._magazineSize}";
    }
}
