using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightBulb : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private Color _firstColor = Color.red;
    [SerializeField] private Color _secondColor = Color.blue;
    [SerializeField, Range(0f, 2f)] private float _switchingTime = 1f;

    private Light _light;
    private Color _defualtColor;
    private WaitForSeconds _wait;

    private void Start()
    {
        _light = GetComponent<Light>();
        _defualtColor = _light.color;
        _wait = new(_switchingTime);
    }

    private void OnEnable()
    {
        _alarm.Activation += TurnOnFlashing;
    }

    private void OnDisable()
    {
        _alarm.Activation -= TurnOnFlashing;
    }

    private void TurnOnFlashing()
    {
        StartCoroutine(SwitchColors());
    }

    private IEnumerator SwitchColors()
    {
        while(_alarm.IsActive)
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
