using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<InputActionReference> _listActions;
    [SerializeField] private Scarecrow _scarecrow;
    [SerializeField] private InputActionReference _restartScarecrow;

    private void Start()
    {
        foreach (var item in _listActions)
        {
            item.action.Enable();
        }
        _restartScarecrow.action.started += RestartScarecrow;
    }
    private void RestartScarecrow(InputAction.CallbackContext context) 
    {
        _scarecrow.gameObject.SetActive(false);
        _scarecrow.gameObject.SetActive(true);
    }
}
