using TMPro;
using UnityEngine;
using DG.Tweening;

public class ViewSpeed : MonoBehaviour
{
    [SerializeField] private TMP_Text _tMP_Text;
    [SerializeField] private Score _score;
    [SerializeField] private Gameplay _gameplay;
    [SerializeField] private Levels _levels;

    private Tween _tween;

    private int _speed = 1;
    private float _delay = 0.3f;

    private void OnEnable()
    {
        _gameplay.Win += OnWin;
        _levels.Reset += OnWin;
    }

    private void OnDisable()
    {
        _gameplay.Win -= OnWin;
        _levels.Reset -= OnWin;
    }

    private void Start()
    {
        float maxScale = 0.5f;

        OnWin();

        _tween = transform.DOScale(new Vector3(maxScale, maxScale, maxScale), _delay)
                          .SetLoops(-1, LoopType.Yoyo)
                          .SetEase(Ease.Linear);
    }

    private void OnWin() =>
        Show(SetSpeed());

    private void Show(int speed) =>
        _tMP_Text.text = $"{speed}X";

    private int SetSpeed()
    {
        if (_levels.GetValue() == 0)
            _speed = 1;

        if (_levels.GetValue() == 1)
            _speed = 3;

        if (_levels.GetValue() == 2)
            _speed = 5;

        if (_levels.GetValue() == 3)
            _speed = 7;

        if (_levels.GetValue() == 4)
            _speed = 10;

        return _speed;
    }
}
