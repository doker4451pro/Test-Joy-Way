using UnityEngine;

[CreateAssetMenu(fileName = "New ScarecrowHPData", menuName = "ScarecrowHP Data", order = 51)]
public class ScarecrowHPData : ScriptableObject
{
    [SerializeField] private int _maxHP;
    
    public int MaxHP { get { return _maxHP; } }
}
