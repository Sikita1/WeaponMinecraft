using UnityEngine;
using UnityEngine.Events;
using YG;

public class Advertisement : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Gameplay _gameplay;

    public bool IsADS { get; private set; }

    public event UnityAction CommercialsAreOver;

    private void OnEnable()
    {
        _timer.TimerEnd += Show;
        YandexGame.OpenFullAdEvent += OpenCallback;
        YandexGame.CloseFullAdEvent += CloseCallback;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        _timer.TimerEnd -= Show;
        YandexGame.OpenFullAdEvent -= OpenCallback;
        YandexGame.CloseFullAdEvent -= CloseCallback;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    public void Show() =>
        YandexGame.FullscreenShow();

    public void ShowReward() =>
        YandexGame.RewVideoShow(0);

    private void Rewarded(int obj) =>
        _gameplay.Pause();

    private void OpenCallback() =>
        IsADS = true;

    private void CloseCallback()
    {
        IsADS = false;
        CommercialsAreOver?.Invoke();
    }
}
