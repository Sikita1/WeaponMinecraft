using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using YG;
using UnityEngine.EventSystems;

public class Gameplay : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private List<Sprite> _sprites;

    [SerializeField] private Image _active;
    [SerializeField] private Image _target;
    [SerializeField] private Score _score;
    [SerializeField] private Dancer _dancer;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Image _tap;

    [SerializeField] private ParticleSystem _systemLeft;
    [SerializeField] private ParticleSystem _systemRight;

    [SerializeField] private Audio _audio;

    [SerializeField] private Advertisement _advertisement;
    [SerializeField] private Timer _timerPause;

    private const string _saveKey = "imageSave";

    public event UnityAction Win;
    private Coroutine _coroutine;

    private bool _isSpinning;
    private int _activeImage = 0;

    private Tween _tween;
    private float _activeRotation;

    private float _upperLimit = 10f;
    private float _lowerLimit = 355f;
    private float _fullTurnover = 360f;

    private int _currentValueADS = 0;
    private int _timeToADS;

    private bool _isPanelTimerActive;

    private void Awake()
    {
        HideTimerPanel();
    }

    private void OnEnable()
    {
        _advertisement.CommercialsAreOver += HideTimerPanel;
    }

    private void OnDisable()
    {
        _advertisement.CommercialsAreOver -= HideTimerPanel;
    }

    private void Start()
    {
        _activeRotation = _active.transform.rotation.z;

        Load();
        ShowPictures(_activeImage);

        _tap.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isPanelTimerActive == false)
            Toggle();

        if (_advertisement.IsADS == true)
            _audio.Pause();
        else
            _audio.UnPause();

        Debug.Log("Работает");
    }

    public int GetSpritesCount() =>
        _sprites.Count;

    public void ResetButton()
    {
        _score.ResetValue();
        Pause();
    }

    private void HideTimerPanel()
    {
        _timerPause.gameObject.SetActive(false);
        _isPanelTimerActive = false;
        Time.timeScale = 1f;
    }

    private void Rotate()
    {
        _tween = _active.transform.DORotate(new Vector3(0, 0, _fullTurnover),
                                            SetSpeed(),
                                            RotateMode.FastBeyond360)
                                  .SetLoops(-1, LoopType.Restart)
                                  .SetRelative()
                                  .SetEase(Ease.Linear);
    }

    private void Toggle()
    {
        if (_tap.gameObject.activeSelf == true)
            _tap.gameObject.SetActive(false);

        if (_isSpinning == true)
            Pause();
        else
            UnPause();
    }

    public void Pause()
    {
        _tween.Kill();
        Time.timeScale = 0f;
        CheckWin();
        _isSpinning = false;
    }

    private void UnPause()
    {
        Time.timeScale = 1f;
        _isSpinning = true;
        Rotate();
    }

    private void SetPicture()
    {
        if (_sprites.Count > 0 && _activeImage < _sprites.Count - 1)
            _activeImage = _score.GetValue();

        Save();
        ShowPictures(_activeImage);
    }

    private void CheckWin()
    {
        if (_active.transform.localEulerAngles.z <= _upperLimit
         || _active.transform.localEulerAngles.z >= _lowerLimit)
            UpLelev();
    }

    private void UpLelev()
    {
        Winning();
        _dancer.Activ();

        Time.timeScale = 1f;
        _isSpinning = true;

        _tap.gameObject.SetActive(true);

        Win?.Invoke();
    }

    private void ResetRotationImage()
    {
        _activeRotation = 0f;
        _active.transform.rotation = Quaternion.identity;
    }

    private void SetColor(Image image)
    {
        float alfaColor = 0.3f;

        Color color = image.color;
        color.a = alfaColor;
        image.color = color;
    }

    private void ParticlesPlay()
    {
        _systemLeft.Play();
        _systemRight.Play();
        _audio.Win();
    }

    private float SetSpeed()
    {
        if (_score.GetValue() < 7)
            _rotationSpeed = 0.6f;

        if (_score.GetValue() >= 7 && _score.GetValue() < 20)
            _rotationSpeed = 0.5f;

        if (_score.GetValue() >= 20 && _score.GetValue() < 40)
            _rotationSpeed = 0.4f;

        if (_score.GetValue() >= 40 && _score.GetValue() <= GetSpritesCount())
            _rotationSpeed = 0.1f;

        return _rotationSpeed;
    }

    private void ShowPictures(int activeImage)
    {
        AssignPicture(_active, activeImage);
        AssignPicture(_target, activeImage);
        SetColor(_target);
    }

    private void Winning()
    {
        ResetRotationImage();
        SetPicture();
        ParticlesPlay();

        _currentValueADS++;
        ShowAdvertisement();
    }

    private void ShowAdvertisement()
    {
        int value = 5;

        if (_currentValueADS == value)
        {
            _timeToADS = YandexGame.Instance.infoYG.fullscreenAdInterval - (int)YandexGame.timerShowAd;

            if (_timeToADS <= 0)
            {
#if UNITY_EDITOR == false
            StartTimerADS();
#endif
            }

            _currentValueADS = 0;
        }
    }

    private void StartTimerADS()
    {
        _timerPause.gameObject.SetActive(true);
        _isPanelTimerActive = true;
        Time.timeScale = 0f;
    }

    private void AssignPicture(Image image, int activeImage) =>
        image.sprite = _sprites[activeImage];

    private void Save()
    {
        SaveManager.Save(_saveKey, GetSaveScore());
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.ImageController>(_saveKey);

        _activeImage = data.CurrentImage;
    }

    private SaveData.ImageController GetSaveScore()
    {
        var data = new SaveData.ImageController()
        {
            CurrentImage = _activeImage
        };

        return data;
    }
}
