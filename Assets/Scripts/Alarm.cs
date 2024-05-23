using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private float _increasingVolume = 0.5f;

    private AudioSource _siren;
    private Coroutine _coroutine;
    private float _minValue = 0f;
    private float _maxValue = 1f;

    private void OnEnable()
    {
        _alarmTrigger.Activate += RespondToAlarm;
    }

    private void OnDisable()
    {
        _alarmTrigger.Activate -= RespondToAlarm;
    }

    private void Start()
    {
        _siren = GetComponent<AudioSource>();
        _siren.volume = 0;
    }

    private void RespondToAlarm()
    {
        if (_alarmTrigger.IsActive)
            StartTurnVolume(_maxValue);

        if (!_alarmTrigger.IsActive)
            StartTurnVolume(_minValue);
    }

    private void StartTurnVolume(float valueVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(TurnVolume(valueVolume));
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
