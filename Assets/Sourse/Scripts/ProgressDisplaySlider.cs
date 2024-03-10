using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplaySlider : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private Slider _slider;

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

    private void OnChanged()
    {
        _coroutine = StartCoroutine(SlideDisplay(_score.GetValue()));
    }

    private IEnumerator SlideDisplay(float health)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_slider.value != health)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, health, 1f);
            yield return wait;
        }
    }
}
