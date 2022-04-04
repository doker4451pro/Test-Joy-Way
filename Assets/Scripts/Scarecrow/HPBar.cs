using TMPro;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Scarecrow _scarecrow;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Camera _cameraAtLook;

    private void Awake()
    {
        _scarecrow.OnHPChanged += ChangeHPTextValue;
    }

    private void ChangeHPTextValue(int HP) 
    {
        _text.text = HP.ToString();
    }

    private void Update()
    {
        TextLookAtCamera();
    }

    private void TextLookAtCamera()
    {
        _text.transform.rotation = Quaternion.LookRotation(_text.transform.position - _cameraAtLook.transform.position);
    }

    private void OnDestroy()
    {
        _scarecrow.OnHPChanged -= ChangeHPTextValue;
    }
}
