using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField,Range(0,100)] private int _mouseSensitivity=100;
    [SerializeField] private InputActionReference _mouseDelta;
    [SerializeField] private Transform _bodyTransform;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        var mouseX = _mouseDelta.action.ReadValue<Vector2>().x * _mouseSensitivity * Time.deltaTime;
        var mouseY = _mouseDelta.action.ReadValue<Vector2>().y * _mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _bodyTransform.Rotate(Vector3.up * mouseX);
    }
}
