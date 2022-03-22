using UnityEngine;

[CreateAssetMenu(fileName = "New ScarecrowMaterialData", menuName = "Scarecrow Material Data", order = 51)]
public class ScarecrowMaterialData : ScriptableObject
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _wetColor;
    [SerializeField] private Color _fireColor;

    public Color DefaultColor { get { return _defaultColor; } }
    public Color WetColor { get { return _wetColor; } }
    public Color FireColor { get { return _fireColor; } }
}
