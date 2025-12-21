using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gum : MonoBehaviour
{
    [SerializeField] GumDefault _gumDefault;
    GumSpawner _spawner;
    int _id;
    public int ID => _id;

    public GumDefault GumDefault => _gumDefault;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tag != "Gum")
        {
            tag = "Gum";
        }
    }

    public void SpawnerSetting(GumSpawner spawner, int id)
    {
        _spawner = spawner;
        _id = id;
    }

    public GumDefault.Lotto OpenLotto()
    {
        _spawner.ReleaseToPool(this, _id);
        Debug.Log(_gumDefault.LottoType.ToString());
        return _gumDefault.LottoType;
    }
}
