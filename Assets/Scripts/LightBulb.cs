using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightBulb : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private Color _firstColor = Color.red;
    [SerializeField] private Color _secondColor = Color.blue;
    [SerializeField, Range(0f, 2f)] private float _switchingTime = 0.5f;

    private Light _light;
    private Color _defualtColor;
    private WaitForSeconds _wait;

    private void OnEnable()
    {
        _alarmTrigger.Activate += TurnOnFlashing;
    }

    private void OnDisable()
    {
        _alarmTrigger.Activate -= TurnOnFlashing;
    }

    private void Start()
    {
        _light = GetComponent<Light>();
        _defualtColor = _light.color;
        _wait = new(_switchingTime);
    }

    private void TurnOnFlashing()
    {
        StartCoroutine(SwitchColors());
    }

    private IEnumerator SwitchColors()
    {
        while (_alarmTrigger.IsActive)
        {
            _light.color = _firstColor;
            yield return _wait;
            _light.color = _secondColor;
            yield return _wait;
        }

        _light.color = _defualtColor;

        yield return null;
    }
}
