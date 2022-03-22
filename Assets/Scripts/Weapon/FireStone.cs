using UnityEngine;
using UnityEngine.InputSystem;

public class FireStone : BaseWeapon
{
    [SerializeField] ParticleSystem _particle;
    protected override void StartFire(InputAction.CallbackContext context)
    {
        _particle.Play();
    }
    protected override void EndFire(InputAction.CallbackContext context)
    {
        _particle.Stop();
    }
}
