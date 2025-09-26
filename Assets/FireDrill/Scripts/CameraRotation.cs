using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    public PlayerInput Input { get; private set; }
    public float mouseSensitive;
    private float rx;
    private float ry;

    private void Awake()
    {
        Input = GetComponentInParent<PlayerInput>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        //Vector2 mouseDelta = Input.PlayerActions.Mouse.ReadValue<Vector2>();
        transform.eulerAngles = new Vector3(rx, ry, 0);
        //rx -= mouseDelta.y * mouseSensitive * Time.deltaTime;
        //ry += mouseDelta.x * mouseSensitive * Time.deltaTime;
        rx = Mathf.Clamp(rx, -70.0f, 50.0f);
        transform.eulerAngles = new Vector3(rx, ry, 0);

        //Debug.Log($"{Input.PlayerActions.Mouse.ReadValue<Vector2>()}");
        #region inputSystem
        //float mx = Input.GetAxis("Mouse X");
        //float my = Input.GetAxis("Mouse Y");

        //rx -= my * mouseSensitive * Time.deltaTime;
        //ry += mx * mouseSensitive * Time.deltaTime;

        //rx = Mathf.Clamp(rx, -70.0f, 50.0f);
        ////transform.rotation = Quaternion.Euler(rx, ry, 0);
        //transform.eulerAngles = new Vector3(rx, ry, 0);
        #endregion
    }


}
