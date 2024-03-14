using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplaySlider : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private Slider _slider;

    [SerializeField] private Gameplay _gameplay;

    [SerializeField] private Levels _levels;

    private float _delay = 0.02f;

    private Coroutine _coroutine;

    private void Start()
    {
        OnChanged();
    }

    private void OnEnable()
    {
        _score.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _score.Changed -= OnChanged;
    }

    private void OnChanged() =>
        _coroutine = StartCoroutine(SlideDisplay(_levels.GetValue()));

    private IEnumerator SlideDisplay(float value)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_slider.value != value)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, value, 1f);
            yield return wait;
        }
    }
}
