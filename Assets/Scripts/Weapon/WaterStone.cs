using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterStone : BaseWeapon
{
    [SerializeField] private string _nameBullet="WaterBall";
    [SerializeField] private float _forseStartBullet;
    [SerializeField] private Transform _startBullet;
    protected override void StartFire(InputAction.CallbackContext context)
    {
        var bullet=ObjectPooler.Instance.GetPoolObjectByName(_nameBullet);
        SetBulletToStartPosition(bullet);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward*_forseStartBullet);
    }

    private void SetBulletToStartPosition(GameObject bullet) 
    {
        bullet.transform.position = _startBullet.position;
        bullet.transform.rotation = _startBullet.rotation;
    }
}
