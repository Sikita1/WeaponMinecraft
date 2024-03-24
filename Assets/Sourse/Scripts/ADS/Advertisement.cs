using UnityEngine;
using UnityEngine.Events;
using YG;

public class Advertisement : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Gameplay _gameplay;
    [SerializeField] private Audio _audio;

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

    public void ShowReward()
    {
        _audio.Pause();
        YandexGame.RewVideoShow(0);
    }

    private void Rewarded(int obj)
    {
        _gameplay.UpLelev();
        _gameplay.PauseGame();
        _audio.UnPause();
        Time.timeScale = 1f;
    }

    private void OpenCallback()
    {
        IsADS = true;
        _audio.Pause();
    }

    private void CloseCallback()
    {
        IsADS = false;
        CommercialsAreOver?.Invoke();
        _audio.UnPause();
    }
}
