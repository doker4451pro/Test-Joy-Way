using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private BaseWeapon _leftWeapon;
    [SerializeField] private BaseWeapon _rightWeapon;

    [SerializeField] private InputActionReference _leftMouseAction;
    [SerializeField] private InputActionReference _rightMouseAction;


    public BaseWeapon LeftWeapon { get { return _leftWeapon; } }
    public BaseWeapon RightWeapon { get { return _rightWeapon; } }

    public void SetWeapon(BaseWeapon weapon, bool inLeftHand) 
    {
        if (weapon != null)
        {
            var currentWeapon = inLeftHand ? _leftWeapon : _rightWeapon;

            if (currentWeapon == null)
            {
                if (inLeftHand)
                {
                    weapon.StartUse(_leftMouseAction);
                    _leftWeapon = weapon;
                }
                else
                {
                    weapon.StartUse(_rightMouseAction);
                    _rightWeapon = weapon;
                }
            }
        }
        else
        {
            ThrowAwayWeapon(inLeftHand);
        }
    }

    private void ThrowAwayWeapon(bool inLefthand) 
    {
        var weapon = inLefthand ? _leftWeapon : _rightWeapon;
        if (weapon != null)
        {
            weapon.transform.SetParent(null);
            weapon.EndUse(inLefthand ? _leftMouseAction : _rightMouseAction);

            if (inLefthand)
                _leftWeapon = null;
            else
                _rightWeapon = null;
        }
    }
}
