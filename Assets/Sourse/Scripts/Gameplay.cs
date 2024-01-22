using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;

    [SerializeField] private Image _active;
    [SerializeField] private Image _target;
    [SerializeField] private Score _score;
    [SerializeField] private Dancer _dancer;
    [SerializeField] private float _rotationSpeed;

    private const string _saveKey = "imageSave";

    public event UnityAction Win;
    private Coroutine _coroutine;

    private bool _isSpinning;
    private int _activeImage = 0;

    private Tween _tween;
    private float _activeRotation;

    private float _upperLimit = 10f;
    private float _lowerLimit = 355f;
    private float _delay = 0f;
    private float _fullTurnover = 360f;

    private void Start()
    {
        _activeRotation = _active.transform.rotation.z;

        Load();
        ShowPictures(_activeImage);
        //_dancer.Activ();

        Win?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Toggle();
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
        if (_isSpinning == true)
        {
            _tween.Kill();
            Time.timeScale = 0f;
            CheckWin();
            _isSpinning = false;
        }
        else
        {
            Time.timeScale = 1f;
            _isSpinning = true;
            Rotate();
        }
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
        if(_active.transform.localEulerAngles.z <= _upperLimit || _active.transform.localEulerAngles.z >= _lowerLimit)
        {
            if (_coroutine != null)
                StopCoroutine(Winning());

            _coroutine = StartCoroutine(Winning());

            _dancer.Activ();

            Time.timeScale = 1f;
            _isSpinning = true;

            Win?.Invoke();
        }
    }

    private void ResetRotationImage()
    {
        _activeRotation = 0f;
        _active.transform.rotation = Quaternion.identity;
        //_active.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void SetColor(Image image)
    {
        float alfaColor = 0.3f;

        Color color = image.color;
        color.a = alfaColor;
        image.color = color;
    }

    private float SetSpeed()
    {
        if (_score.GetValue() < 5)
            _rotationSpeed = 0.6f;

        if (_score.GetValue() >= 5 && _score.GetValue() < 15)
            _rotationSpeed = 0.5f;

        if (_score.GetValue() >= 15 && _score.GetValue() < 20)
            _rotationSpeed = 0.4f;

        if (_score.GetValue() >= 20 && _score.GetValue() <= 160)
            _rotationSpeed = 0.1f;

        return _rotationSpeed;
    }

    private void ShowPictures(int activeImage)
    {
        AssignPicture(_active, activeImage);
        AssignPicture(_target, activeImage);
        SetColor(_target);
    }

    private IEnumerator Winning()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        yield return wait;

        ResetRotationImage();
        SetPicture();
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
