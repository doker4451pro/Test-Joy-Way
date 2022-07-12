using System;
using TMPro;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private ScarecrowHP _scarecrowHP;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Camera _cameraAtLook;
    
    private void Update()
    {
        TextLookAtCamera();
    }

    private void TextLookAtCamera()
    {
        _text.transform.rotation = Quaternion.LookRotation(_text.transform.position - _cameraAtLook.transform.position);
    }
    
    private void OnEnable()
    {
        _scarecrowHP.OnHPChanged += ChangeHPTextValue;
    }

    private void OnDisable()
    {
        _scarecrowHP.OnHPChanged -= ChangeHPTextValue;
    }
    
    private void ChangeHPTextValue(int HP) 
    {
        _text.text = HP.ToString();
    }
}
