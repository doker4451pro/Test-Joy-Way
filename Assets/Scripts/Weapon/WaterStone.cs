using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterStone : BaseWeapon
{
    [SerializeField] private float _forseStartBullet;
    [SerializeField] private Transform _startBullet;
    
    protected override void StartFire(InputAction.CallbackContext context)
    {
        var bullet=ObjectPooler.Instance.GetObjectOfType<WaterBall>();
        SetBulletToStartPosition(bullet.transform);
        bullet.AddForce(transform.forward * _forseStartBullet);
    }

    private void SetBulletToStartPosition(Transform bulletTransform) 
    {
        bulletTransform.position = _startBullet.position;
        bulletTransform.rotation = _startBullet.rotation;
    }
}
