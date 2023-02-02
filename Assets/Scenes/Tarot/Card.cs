using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] Transform _asset;
    private Vector2 _initPos;
    [SerializeField] Vector3 _endPos;
    [SerializeField] float _maxTime = 0.25f;
    [SerializeField] float _extraScale = 0.1f;

    Vector3 _currScale;
    IEnumerator _routine;
    BoxCollider _col;

    private void Start()
    {
        TryGetComponent(out _col);

        _currScale = _asset.localScale;
        _col.enabled = false;
        _initPos = _asset.localPosition;
    }

    private void OnMouseDown()
    {
        Tarot.instance.CardChose(id);
    }
    private void OnMouseEnter()
    {
        if (_routine != null) StopCoroutine(_routine);
        StartCoroutine(_routine = Animation(true));
    }
    private void OnMouseExit()
    {
        if (_routine != null) StopCoroutine(_routine);
        StartCoroutine(_routine = Animation(false));
    } 

    IEnumerator Animation(bool enter)
    {
        float t = 0;
        Vector3 actual = _asset.localPosition;
        Vector3 actScale = _asset.localScale;
        Vector3 endScale = _currScale + Vector3.one * _extraScale;
        float time = enter ? _maxTime : _maxTime / 4;
        while (t < time)
        {
            t += Time.deltaTime;
            if (enter)
            {
                _asset.localPosition = Vector3.Lerp(actual, _endPos, t / time);
                _asset.localScale = Vector3.Lerp(actScale, endScale, t / time);
            }
            else
            {
                _asset.localPosition = Vector3.Lerp(actual, _initPos, t / time);
                _asset.localScale = Vector3.Lerp(actScale, _currScale, t / time);
            }
            yield return null;
        }
    }

    public void Enabled(bool s)
    { 
        _col.enabled = s;
    }
} 