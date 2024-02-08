using UnityEngine;
using UnityEngine.Events;
using YG;

public class Advertisement : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    public bool IsADS { get; private set; }

    public event UnityAction CommercialsAreOver;

    private void OnEnable()
    {
        _timer.TimerEnd += Show;
        YandexGame.OpenFullAdEvent += OpenCallback;
        YandexGame.CloseFullAdEvent += CloseCallback;
    }

    private void OnDisable()
    {
        _timer.TimerEnd -= Show;
        YandexGame.OpenFullAdEvent -= OpenCallback;
        YandexGame.CloseFullAdEvent -= CloseCallback;
    }

    public void Show() =>
        YandexGame.FullscreenShow();

    private void OpenCallback() =>
        IsADS = true;

    private void CloseCallback()
    {
        IsADS = false;
        CommercialsAreOver?.Invoke();
    }
}
