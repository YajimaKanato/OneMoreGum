using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gum : MonoBehaviour
{
    [SerializeField] GumDefault _gumDefault;
    [SerializeField] Light _light;
    GumSpawner _spawner;
    int _id;
    public int ID => _id;

    public GumDefault GumDefault => _gumDefault;
    public GumSpawner Spawner => _spawner;

    public void SpawnSetting(GumSpawner spawner, int id)
    {
        if (tag != "Gum")
        {
            tag = "Gum";
        }
        _spawner = spawner;
        _id = id;
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
