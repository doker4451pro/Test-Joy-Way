using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] InputActionReference _moveAction;
    [SerializeField] CharacterController _controller;
    [SerializeField, Range(0, 100)] int _speed;

    private void Update()
    {
        var moveV2 = _moveAction.action.ReadValue<Vector2>();

        var move = transform.right * moveV2.x + transform.forward * moveV2.y;

        _controller.Move(move * _speed * Time.deltaTime);
    }
}
