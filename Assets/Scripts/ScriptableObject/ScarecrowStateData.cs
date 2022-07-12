using UnityEngine;

[CreateAssetMenu(fileName = "New ScarecrowStateData", menuName = "ScarecrowState Data", order = 51)]
public class ScarecrowStateData : ScriptableObject
{
    [SerializeField] private int _maxWet;
    [SerializeField] private int _timeToFire;

    public int MaxWet
    {
        get { return _maxWet; }
    }

    public int TimeToFire
    {
        get { return _timeToFire; }
    }
}
