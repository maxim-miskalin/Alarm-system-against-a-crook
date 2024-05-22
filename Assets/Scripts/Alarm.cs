using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _siren;
    [SerializeField] private float _increasingVolume = 0.05f;
    [SerializeField] private float _smoothnessOfSoundChange = 0.01f;

    private WaitForSeconds _wait;
    private Collider _collider;
    private bool _isActive = false;

    public bool IsActive => _isActive;

    public event Action Activation;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _wait = new(_smoothnessOfSoundChange);
        _siren.volume = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Criminal>() != null)
        {
            _isActive = true;
            Activation?.Invoke();
            StartCoroutine(TurnUpVolume(collider));
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        StopAllCoroutines();
        StartCoroutine(TurnDownVolume());
        _isActive = false;
    }

    private IEnumerator TurnUpVolume(Collider collider)
    {
        _siren.Play();

        while (_siren.volume < 1)
        {
            _siren.volume += _increasingVolume;
            yield return _wait;
        }

        yield return null;
    }

    private IEnumerator TurnDownVolume()
    {
        while (_siren.volume > 0)
        {
            _siren.volume -= _increasingVolume;
            yield return _wait;
        }

        _siren.Pause();

        yield return null;
    }
}
