using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ScarecrowHP))]
[SelectionBase]
public class Scarecrow : MonoBehaviour
{
    public event Action<bool> OnFire;
    public event Action<float> OnWetStateChanged;

    [SerializeField] private ScarecrowStateData _scarecrowStateData;
    [SerializeField] private ScarecrowHP _scarecrowHp;
    [SerializeField] private int _deltaFireDamage = 10;
    [SerializeField] private int _deltaWetDamage = 10;
    [SerializeField] private int _deltaWetState = 10;
    [SerializeField] private int _damageFromFire = 5;
    
    private int _wetState;
    private bool _isFire;
    private Coroutine _fireCorutine;
    private int _timeToFire;

    public void TakeDamage(int damage = 0, DamageType type = DamageType.None)
    {
        if (type == DamageType.None)
        {
            TakeDamage(damage);
        }
        else if (type == DamageType.Water)
        {
            TakeWaterDamage();
        }
        else if (type == DamageType.Fire)
        {
            TakeFireDamage();
        }
    }

    private void TakeDamage(int damage)
    {
        if (_isFire)
            _scarecrowHp.TakeDamage(damage + _deltaFireDamage);
        else if (_wetState != 0)
            _scarecrowHp.TakeDamage(damage - _deltaWetDamage);
        else
            _scarecrowHp.TakeDamage(damage);
    }

    private void TakeWaterDamage()
    {
        if (_isFire)
        {
            ChangeFireStateTo(false);
        }
        ChangeWaterStateToDelta(_deltaWetState);
    }

    private void TakeFireDamage()
    {
        if (_isFire)
        {
            //restart Cotoutine
            _timeToFire = 0;
        }
        else 
        {
            if (_wetState == 0)
            {
                ChangeFireStateTo(true);
            }
            else 
            {
                ChangeWaterStateToDelta(-1);
            }
                
        }
        _scarecrowHp.TakeDamage(1);
    }

    private void ChangeWaterStateToDelta(int deltaWaterState)
    {
        _wetState += deltaWaterState;
        _wetState = Mathf.Clamp(_wetState, 0, _scarecrowStateData.MaxWet);
        OnWetStateChanged?.Invoke((float)_wetState / _scarecrowStateData.MaxWet);

    }
    
    private void ChangeFireStateTo(bool isFire)
    {
        _isFire = isFire;
        OnFire?.Invoke(isFire);
        if (isFire)
        {
            _fireCorutine = StartCoroutine(Fire());
        }
        else
        {
            if (_fireCorutine != null)
                StopCoroutine(_fireCorutine);
        }
    }
    
    private IEnumerator Fire()
    {
        var timeWaitBetweenDamage = 1;
        _timeToFire = 0;
        while (_timeToFire <= _scarecrowStateData.TimeToFire) 
        {
            _scarecrowHp.TakeDamage(_damageFromFire);
            
            yield return new WaitForSecondsRealtime(timeWaitBetweenDamage);
            _timeToFire+= timeWaitBetweenDamage;
        }
        ChangeFireStateTo(false);
    }

    #region UnityMethod

    private void Start()
    {
        SetDefaultValue();
    }

    private void OnEnable()
    {
        SetDefaultValue();
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(type: DamageType.Fire);
    }
    #endregion
    
    private void SetDefaultValue() 
    {
        _scarecrowHp.SetDefaultValue();
        ChangeWaterStateToDelta(-_scarecrowStateData.MaxWet); 
        ChangeFireStateTo(false);
    }
}
