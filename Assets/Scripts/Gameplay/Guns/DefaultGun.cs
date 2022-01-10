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

    public void Shoot(Transform camera)
    {

        if (_bulletsLeft > 0)
        {
            //play Sound
            _shootSound.Play();

            //instanciate projectile, i decided to do it on the Scriptable Object
            //because every gun has a different way to do it so instead of doing a 
            //switch here or creating more DefaultGuns and doing a double bind with SO and MonoBehavoiurs
            //i can just do it there
            GameObject projectile = GunSO.InstanciateProjectile(_shootingPoint);

            //adding force to the projectile
            Vector3 direction = camera.transform.forward.normalized + GunSO._shootingForce;
            projectile.GetComponent<Rigidbody>().AddRelativeForce(direction);

            //destroy projectile after lifespan ended
            Destroy(projectile, GunSO._proyectileLifeSpan);

            _bulletsLeft--;

            //if i got no bullets left reload
            if (_bulletsLeft == 0 && _reloading == false)
            {
                StartCoroutine(Reload());
            }
        }

    }

    //reloads gun
    private IEnumerator Reload()
    {
        _reloading = true;
        yield return new WaitForSeconds(GunSO._cooldownTime);
        _bulletsLeft = GunSO._magazineSize;
        _reloadSound.Play();
        _reloading = false;

    }

    //shows UI feedback
    public void Target()
    {
        _tagertFeedBack.SetActive(true);
    }

    //hides ui feedback
    public void UnTarget()
    {
        _tagertFeedBack.SetActive(false);
    }

    //picks up the gun
    public void PickUp(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        _rigidBody.isKinematic = true;
        _collider.isTrigger = true;

        UnTarget();
    }

    //drops the gun
    public void Drop()
    {
        transform.SetParent(null);
        _rigidBody.isKinematic = false;
        _collider.isTrigger = false;
    }

    //gives ammoleft/totalAmmo in string
    public string GiveAmmoFeedback()
    {
        return $"{_bulletsLeft}/{GunSO._magazineSize}";
    }
}
