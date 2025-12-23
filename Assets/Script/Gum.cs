using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gum : MonoBehaviour
{
    [SerializeField] GumDefault _gumDefault;
    [SerializeField] Light _light;
    GumSpawner _spawner;
    int _hitGumRate;
    int _id;
    public int HitGumRate => _hitGumRate;
    public int ID => _id;

    public GumDefault GumDefault => _gumDefault;

    public void SpawnSetting(GumSpawner spawner, int id, int rate)
    {
        if (tag != "Gum")
        {
            tag = "Gum";
        }
        _spawner = spawner;
        _id = id;
        _hitGumRate = rate;
        _light.gameObject.SetActive(false);
    }

    public GumDefault.Lotto OpenLotto(bool isCertainHit)
    {
        _spawner.ReleaseToPool(this, _id);
        Debug.Log(_gumDefault.LottoType.ToString());
        return isCertainHit ? GumDefault.Lotto.Hit : _gumDefault.LottoType;
    }

    public void Reveal()
    {
        _light.gameObject.SetActive(true);
    }

    public void Observing()
    {

    }

    public void EscapedObserving()
    {

    }
}
