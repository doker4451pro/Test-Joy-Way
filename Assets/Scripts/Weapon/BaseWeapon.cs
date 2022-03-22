using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseWeapon : MonoBehaviour
{
    public void StartUse(InputAction action) 
    {
        action.started += StartFire;
        action.canceled += EndFire;
    }
    public void EndUse(InputAction action) 
    {
        action.started -= StartFire;
        action.canceled -= EndFire;
    }
    protected virtual void StartFire(InputAction.CallbackContext context) 
    {

    }
    protected virtual void EndFire(InputAction.CallbackContext context) 
    {

    }
}
