using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : BaseWeapon
{
    [SerializeField] private int _damage = 20;
    protected override void StartFire(InputAction.CallbackContext context)
    {
        var scatecrow = Searcher.Instance.GetObjectFront<Scarecrow>();
        if (scatecrow != null)
            scatecrow.TakeDamage(_damage);
    }
}
