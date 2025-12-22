using System.Collections;
using UnityEngine;

public class SearchUI : MonoBehaviour
{
    [SerializeField] GumInfo _gumInfo;
    [SerializeField] LineRenderer _line;
    [SerializeField] float _crossLineRange = 1;
    [SerializeField] float _horizontalLineRange = 0.5f;
    [SerializeField] int _separateCount = 100;
    [SerializeField] Vector3 _lineHeight = Vector3.up * 7;
    Coroutine _coroutine;

    private void Start()
    {
        _line.positionCount = 0;
        _gumInfo.gameObject.SetActive(false);
    }

    public void DrawLine(Vector3 pos, Vector3 boxPos)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(DrawLineCoroutine(pos, boxPos));
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

    IEnumerator DrawLineCoroutine(Vector3 pos, Vector3 boxPos)
    {
        var theta = Random.Range(Mathf.PI / 6, Mathf.PI / 3);
        var direction = pos - boxPos;
        var crossLinePos = pos + new Vector3(Mathf.Cos(theta) * (direction.x / Mathf.Abs(direction.x)), 0, Mathf.Sin(theta) * (-direction.z / Mathf.Abs(direction.z))) * _crossLineRange + _lineHeight;
        var positions = new Vector3[] { pos, crossLinePos, crossLinePos + (Vector3.right * direction.x / Mathf.Abs(direction.x)) * _horizontalLineRange };
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
        //_gumInfo.InfoUpdate();
        _gumInfo.transform.position = Camera.main.WorldToScreenPoint(currentPos);
    }
}
