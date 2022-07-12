using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private InputActionReference _pickUpLeftWeapon;
    [SerializeField] private InputActionReference _pickUpRightWeapon;

    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _pickUpLeftWeapon.action.started += SetWeaponInLeftHand;
        _pickUpRightWeapon.action.started += SetWeaponInRightHand;
    }

    private void OnDisable()
    {
        _pickUpLeftWeapon.action.started -= SetWeaponInLeftHand;
        _pickUpRightWeapon.action.started -= SetWeaponInRightHand;
    }

    private void SetWeaponInLeftHand(InputAction.CallbackContext callback) => SetWeapon(callback, true);
    private void SetWeaponInRightHand(InputAction.CallbackContext callback) => SetWeapon(callback, false);
    
    private void SetWeapon(InputAction.CallbackContext callback, bool inLeftHand)
    {

        var weapon = Searcher.Instance.GetObjectFront<BaseWeapon>();

        if (weapon != null && (inLeftHand ? _player.LeftWeapon:_player.RightWeapon)==null)
        {
            SetWeaponInHand(inLeftHand ? _leftHand : _rightHand, weapon);
        }

        _player.SetWeapon(weapon, inLeftHand);
    }
    private void SetWeaponInHand(Transform hand,BaseWeapon weapon) 
    {
        weapon.transform.position = hand.transform.position;
        weapon.transform.rotation = hand.transform.rotation;

        weapon.transform.SetParent(hand);
    }
}
