using System;
using UnityEngine;

public class ScarecrowMaterial : MonoBehaviour
{
    [SerializeField] private Scarecrow _scarecrow;
    [SerializeField] private ScarecrowMaterialData _data;
    [SerializeField] private Material _material;

    private void OnEnable()
    {
        _scarecrow.OnFire += ChangeFireMaterialTo;
        _scarecrow.OnWetStateChanged += ChangeWetMaterialTo;
    }

    private void OnDisable()
    {
        _scarecrow.OnFire -= ChangeFireMaterialTo;
        _scarecrow.OnWetStateChanged -= ChangeWetMaterialTo;
    }
    
    private void ChangeFireMaterialTo(bool isFire) 
    {
        if (isFire)
            _material.color = _data.FireColor;
        else
            _material.color = _data.DefaultColor;
    }
    
    private void ChangeWetMaterialTo(float value) 
    {
        _material.color = Color.Lerp(_data.DefaultColor, _data.WetColor, value);
    }
    
    private void OnApplicationQuit()
    {
        _material.color = _data.DefaultColor;
    }
}
