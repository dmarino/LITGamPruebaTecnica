
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GunPickUpController))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private float _cameraMovement = 100f;
    [SerializeField] private Camera _camera;


    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 8f;


    private Vector2 _detectedRotation;
    private Vector2 _detectedMovement;

    private CharacterController _characterController;
    private GunPickUpController _pickUpController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _characterController = GetComponent<CharacterController>();
        _pickUpController = GetComponent<GunPickUpController>();

        //this is just in case someone forgets to attach the camera in the editor
        if (_camera == null)
        {
            _camera = GetComponentInChildren<Camera>();
        }


    }

    private void Update()
    {
        Move(Time.deltaTime);
        Turn(Time.deltaTime);
    }

    private void Move(float deltaTime)
    {
        Vector3 move = transform.right * _detectedMovement.x + transform.forward * _detectedMovement.y;
        _characterController.Move(move * deltaTime * _moveSpeed);

    }

    private void Turn(float deltaTime)
    {
        float movementX = _detectedRotation.x * deltaTime * _cameraMovement;
        float movementY = _detectedRotation.y * deltaTime * _cameraMovement;

        transform.Rotate(0f, movementX, 0f);  //rotate the player to rotate on X
        _camera.transform.Rotate(-movementY, 0f, 0f); //rotate the camera to rotate on Y

        //clamping the rotation of the camara so it won't pass the top of the player or the bottom
        //this could probably be easier but basically i'm checking if the euler angles are not passing 
        //the min and the max deppending if the player is in the bottom of the camera or in the top of the camera (x < 270)
        var min = _camera.transform.localEulerAngles.x < 270 ? 0 : 271;
        var max = _camera.transform.localEulerAngles.x < 270 ? 89 : 360;
        _camera.transform.localEulerAngles = new Vector3(Mathf.Clamp(_camera.transform.localEulerAngles.x, min, max), 0f, 0f);

    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        _detectedRotation = context.ReadValue<Vector2>();

        if (context.performed)
        {
            _pickUpController.UpdateClosestGun();
        }
        
    }

    public void OnPlayerMove(InputAction.CallbackContext context)
    {
        _detectedMovement = context.ReadValue<Vector2>();

        if (context.performed)
        {
            _pickUpController.UpdateClosestGun();
        }

    }

    public void OnPickUpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _pickUpController.PickUp();
        }
    }

    public void OnDropPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _pickUpController.Drop();
        }
    }

    public void OnShootPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _pickUpController.Shoot(_camera.transform);
        }
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
