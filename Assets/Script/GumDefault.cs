using UnityEngine;

[CreateAssetMenu(fileName = "GumDefault", menuName = "Scriptable Objects/GumDefault")]
public class GumDefault : ScriptableObject
{
    public enum Lotto
    {
        [InspectorName("当たり")] Hit,
        [InspectorName("ハズレ")] Miss
    }
    [SerializeField] int _gumValue = 10;
    [SerializeField] int _gumScore = 10;
    [SerializeField] Lotto _lottoType;

    public int GumValue => _gumValue;
    public int GumScore => _gumScore;
    public Lotto LottoType => _lottoType;
}
