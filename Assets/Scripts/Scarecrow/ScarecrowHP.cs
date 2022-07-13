using System;
using UnityEngine;

public class ScarecrowHP : MonoBehaviour
{
    public event Action<int> OnHPChanged;
    public bool IsAlive { get;protected set; } = true;

    [SerializeField] private ScarecrowHPData _scarecrowHPData;
    
    private int _HP;
    
    public void SetDefaultValue()
    {
        _HP = _scarecrowHPData.MaxHP;
        OnHPChanged?.Invoke(_HP);
    }
    
    public void TakeDamage(int damage)
    {
        if (IsAlive)
        {
            _HP -= damage;
            if (_HP <= 0)
            {
                Death();
                return;
            }
            OnHPChanged?.Invoke(_HP);
        }
    }
    
    private void Death()
    {
        IsAlive = false;
        gameObject.SetActive(false);
    }
}
