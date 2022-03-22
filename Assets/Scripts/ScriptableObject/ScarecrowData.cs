using UnityEngine;

[CreateAssetMenu(fileName = "New ScarecrowData", menuName = "Scarecrow Data", order = 51)]
public class ScarecrowData : ScriptableObject
{
    [SerializeField] private int _maxHP;
    [SerializeField] private int _maxWet;

    public int MaxHP { get { return _maxHP; } }
    public int MaxWet { get { return _maxWet; } }
}
