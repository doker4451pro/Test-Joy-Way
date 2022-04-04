using UnityEngine;

public class ScarecrowMaterial : MonoBehaviour
{
    [SerializeField] private Scarecrow _scarecrow;
    [SerializeField] private ScarecrowMaterialData _data;
    [SerializeField] private Material _material;

    private void Start()
    {
        _scarecrow.OnFire += ChangeFireMaterial;
        _scarecrow.OnWetStateChanged += ChangeWetMaterial;
    }
    private void OnApplicationQuit()
    {
        _material.color = _data.DefaultColor;
    }

    private void ChangeWetMaterial(float value) 
    {
        _material.color = Color.Lerp(_data.DefaultColor, _data.WetColor, value);
    }
    private void ChangeFireMaterial(bool flag) 
    {
        if (flag)
            _material.color = _data.FireColor;
        else
            _material.color = _data.DefaultColor;
    }
    private void OnDestroy()
    {
        _scarecrow.OnFire -= ChangeFireMaterial;
        _scarecrow.OnWetStateChanged -= ChangeWetMaterial;
    }
}
