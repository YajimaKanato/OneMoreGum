using System.Collections;
using UnityEngine;

public class SearchUI : MonoBehaviour
{
    [SerializeField] Transform _cameraPos;
    [SerializeField] GumInfo _gumInfo;
    [SerializeField] LineRenderer _line;
    [SerializeField] float _crossLineRange = 1;
    [SerializeField] float _horizontalLineRange = 0.5f;
    [SerializeField] int _separateCount = 100;
    [SerializeField] float _height = 4;
    Coroutine _coroutine;

    private void Start()
    {
        _line.positionCount = 0;
        _gumInfo.gameObject.SetActive(false);
    }

    public void DrawLine(Gum gum, Vector3 boxPos)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(DrawLineCoroutine(gum, boxPos));
        }
    }

    public void EraseLine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _line.positionCount = 0;
            _gumInfo.gameObject.SetActive(false);
        }
    }

    IEnumerator DrawLineCoroutine(Gum gum, Vector3 boxPos)
    {
        //斜めの線
        var theta = Random.Range(Mathf.PI / 12, 5 * Mathf.PI / 12);
        var cameraHeight = _cameraPos.position.y - 0.5f;
        var gumPos = gum.transform.position * (_height / cameraHeight);
        gumPos.y = _height;
        var direction = gumPos - boxPos;
        var crossLinePos = gumPos + new Vector3(Mathf.Cos(theta) * (direction.x / Mathf.Abs(direction.x)), 0, Mathf.Sin(theta) * (-direction.z / Mathf.Abs(direction.z))) * _crossLineRange * (_height / cameraHeight);
        //線の折れ目
        var positions = new Vector3[] { gumPos, crossLinePos, crossLinePos + (Vector3.right * direction.x / Mathf.Abs(direction.x)) * _horizontalLineRange * (_height / cameraHeight) };
        var currentPos = positions[0];
        var positionIndex = 0;
        _line.positionCount++;
        _line.SetPosition(positionIndex, currentPos);

        var wait = new WaitForSeconds(1 / _separateCount);
        var addPos = (positions[1] - positions[0]) / _separateCount;
        while (Vector3.SqrMagnitude(currentPos - positions[1]) > 0.05f)
        {
            currentPos += addPos;
            positionIndex++;
            _line.positionCount++;
            _line.SetPosition(positionIndex, currentPos);
            yield return wait;
        }

        addPos = (positions[2] - positions[1]) / _separateCount;
        while (Vector3.SqrMagnitude(currentPos - positions[2]) > 0.05f)
        {
            currentPos += addPos;
            positionIndex++;
            _line.positionCount++;
            _line.SetPosition(positionIndex, currentPos);
            yield return wait;
        }
        _gumInfo.gameObject.SetActive(true);
        _gumInfo.InfoUpdate(gum.HitGumRate, gum.Score);
        _gumInfo.transform.position = Camera.main.WorldToScreenPoint(currentPos);
    }
}
