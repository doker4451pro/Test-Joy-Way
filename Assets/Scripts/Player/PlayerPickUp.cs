using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private InputActionReference _pickUpLeftWeapon;
    [SerializeField] private InputActionReference _pickUpRightWeapon;

    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    [SerializeField] private Player _player;

    private void Start()
    {
        _pickUpLeftWeapon.action.started += ctx => SetWeapon(ctx, true);
        _pickUpRightWeapon.action.started += ctx => SetWeapon(ctx, false);

    }
    private void SetWeapon(InputAction.CallbackContext callback, bool inLeftHand)
    {

        var weapon = Searcher.Instance.GetObjectFront<BaseWeapon>();

        if (weapon != null && (inLeftHand ? _player.LeftWeapon:_player.RightWeapon)==null)
        {
            SetWeaponInHend(inLeftHand ? _leftHand : _rightHand, weapon);
        }

        _player.SetWeapon(weapon, inLeftHand);
    }
    private void SetWeaponInHend(Transform hand,BaseWeapon weapon) 
    {
        weapon.transform.position = hand.transform.position;
        weapon.transform.rotation = hand.transform.rotation;

        weapon.transform.SetParent(hand);
    }
}
