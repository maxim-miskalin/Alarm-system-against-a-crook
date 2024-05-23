using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _siren;
    [SerializeField] private float _increasingVolume = 5f;

    private bool _isActive = false;
    private Coroutine _coroutine;

    private float _minValue = 0f;
    private float _maxValue = 1f;

    public bool IsActive => _isActive;

    public event Action Activate;

    private void Start()
    {
        _siren.volume = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Criminal>(out Criminal criminal))
        {
            _isActive = true;
            Activate?.Invoke();
            _coroutine = StartCoroutine(TurnVolume(_maxValue));
        }
    }

    private void OnTriggerExit()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutine(TurnVolume(_minValue));
        _isActive = false;
    }

    private IEnumerator TurnVolume(float targetValue)
    {
        if (targetValue == _maxValue)
            _siren.Play();

        while (_siren.volume != targetValue)
        {
            _siren.volume = Mathf.MoveTowards(_siren.volume, targetValue, _increasingVolume * Time.deltaTime);
            yield return null;
        }

        if (targetValue == _minValue)
            _siren.Stop();
    }
}
