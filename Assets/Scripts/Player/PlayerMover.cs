using System;
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

    private Transform _thisTransform;

    private void Awake()
    {
        _thisTransform = transform;
    }

    private void Update()
    {
        var moveV2 = _moveAction.action.ReadValue<Vector2>();

        var move = _thisTransform.right * moveV2.x + _thisTransform.forward * moveV2.y;

        _controller.Move(move * (_speed * Time.deltaTime));
    }
}
