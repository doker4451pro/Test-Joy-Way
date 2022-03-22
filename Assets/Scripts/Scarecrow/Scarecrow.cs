using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    public event Action<int> OnHPChanged;
    public event Action<bool> OnFire;
    public event Action<float> OnWetStateChanged;

    [SerializeField] private ScarecrowData _scarecrowData;
    [SerializeField] private int _deltaFireDamage = 10;
    [SerializeField] private int _deltaWetDamage = 10;
    [SerializeField] private int _deltaWetState = 10;

    private int _HP;
    private int _wetState;
    private bool _isFire;
    private Coroutine _fireCorutine;
    private int _timeToFire;


    public int HP 
    { 
        get { return _HP; }
        private set 
        {
            if (value <= 0)
            {
                Death();
                return;
            }
            _HP = value;
            OnHPChanged?.Invoke(_HP);
        }
    }
    public bool FireState 
    {
        get { return _isFire; }
        private set 
        {
            _isFire = value;
            OnFire?.Invoke(value);
            if (value)
                _fireCorutine = StartCoroutine(Fire());
            else
                StopCoroutine(_fireCorutine);
        }
    }

    public int WetState 
    {
        get { return _wetState; }
        set 
        {
            _wetState= Mathf.Clamp(value, 0, _scarecrowData.MaxWet);
            OnWetStateChanged?.Invoke((float)_wetState / _scarecrowData.MaxWet);
        }
    }

    public void TakeDamage(int damage=0, DamageType type=DamageType.None) 
    {
        if (type == DamageType.None)
        {
            if (FireState)
                HP -= (damage + _deltaFireDamage);
            else if (_wetState != 0)
                HP -= (damage - _deltaWetDamage);
            else
                HP -= damage;
        }
        else if (type == DamageType.Water)
        {
            if (FireState)
            {
                FireState = false;
            }
            WetState += _deltaWetState;
        }
        else 
        {
            if (FireState)
            {
                HP -= 1;
                //restart Cotoutine
                _timeToFire = 0;
            }
            else 
            {
                if (WetState == 0)
                {
                    FireState = true;
                    HP -=1;
                }
                else 
                {
                    WetState -= 1;
                }
            }
        }
    }

    private IEnumerator Fire() 
    {
        while (_timeToFire <= 10) 
        {
            HP -= 5;
            yield return new WaitForSecondsRealtime(1);
            _timeToFire+= 1;
        }
    }

    private void Death() 
    {
        gameObject.SetActive(false);
    }

    private void SetDefaultValue() 
    {
        HP = _scarecrowData.MaxHP;
        WetState = 0;
        _isFire = false;
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
}
