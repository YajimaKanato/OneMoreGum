using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gum : MonoBehaviour
{
    [SerializeField] GumDefault _gumDefault;
    GumSpawner _spawner;
    int _id;
    public int ID => _id;

    public GumDefault GumDefault => _gumDefault;

    public void SpawnSetting(GumSpawner spawner, int id)
    {
        if (tag != "Gum")
        {
            tag = "Gum";
        }
        _spawner = spawner;
        _id = id;
    }

    public GumDefault.Lotto OpenLotto()
    {
        _spawner.ReleaseToPool(this, _id);
        Debug.Log(_gumDefault.LottoType.ToString());
        return _gumDefault.LottoType;
    }

    public void Observing()
    {

    }

    public void EscapedObserving()
    {

    }
}
