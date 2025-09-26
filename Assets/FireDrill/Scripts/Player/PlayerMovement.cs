using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public PlayerInput Input { get; private set; }

    public float speed = 5f;
    public float gravity = -9.81f;
    private float verticalVelocity;
    private Vector3 impact = Vector3.zero;

    [SerializeField] private float drag = 0.3f;
    private Vector3 dampingVelocity;

    public float mouseSensitivity = 1f;
    public float minVerticalRotation = -90f;
    public float maxVerticalRotation = 40f;

    private float verticalRotation = 0f;

    public void Initialize(CharacterController controller)
    {
        this.controller = controller;
    }

    private void Awake()
    {
        Input = GetComponentInParent<PlayerInput>();
    }



    public Vector3 GetMoveVector()
    {
        Vector2 inputDir = Input.PlayerActions.Movement.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputDir.x, 0, inputDir.y);

        if (Camera.main != null)
        {
            moveDir = Camera.main.transform.TransformDirection(moveDir);
            moveDir.y = 0;
            moveDir.Normalize();
        }

        Vector3 velocity = moveDir * speed;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        velocity.y = verticalVelocity;

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
        velocity += impact;

        return velocity;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void ResetForces()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void Jump(float jumpForce)
    {
        if (controller.isGrounded)
            verticalVelocity = jumpForce;
    }

    public void CameraRotate(Transform playerTransform, Transform cameraTransform)
    {
        Vector2 lookInput = Input.PlayerActions.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        if (Mathf.Abs(mouseX) > 0.01f)
        {
            Quaternion horizontalRotation = Quaternion.Euler(0f, mouseX, 0f);
            playerTransform.rotation *= horizontalRotation;
        }
    }

}
