using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerSkill _skill;

    public void Init(PlayerSkill skill)
    {
        _skill = skill;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.gameObject.tag == "Gum")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var gum = hit.collider.gameObject.GetComponent<Gum>();
                    if (_skill.PurchaseGum(gum.GumDefault.GumValue))
                    {
                        if (gum.OpenLotto() == GumDefault.Lotto.Hit)
                        {
                            _skill.GetHitGum();
                        }
                        else
                        {
                            _skill.GetMissGum();
                        }
                        PlayerActionManager.Instance.StatusUpdate();
                        Debug.Log("Purchase Success");
                    }
                    else
                    {
                        Debug.Log("Purchase Failed");
                    }
                    Debug.Log($"Money => {_skill.CurrentMoney}");
                    Debug.Log($"HitCount => {_skill.HitCount}");
                }
            }
        }
    }
}
