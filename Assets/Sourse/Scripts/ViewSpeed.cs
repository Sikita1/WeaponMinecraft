using TMPro;
using UnityEngine;
using DG.Tweening;

public class ViewSpeed : MonoBehaviour
{
    [SerializeField] private TMP_Text _tMP_Text;
    [SerializeField] private Score _score;
    [SerializeField] private Gameplay _gameplay;

    private Tween _tween;

    private int _speed = 1;

    private void OnEnable()
    {
        _gameplay.Win += OnWin;
    }

    private void OnDisable()
    {
        _gameplay.Win -= OnWin;
    }

    private void Start()
    {
        _tween = transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f)
                          .SetLoops(-1, LoopType.Yoyo)
                          //.SetRelative()
                          .SetEase(Ease.Linear);
    }

    private void OnWin() =>
        Show(SetSpeed());

    private void Show(int speed) =>
        _tMP_Text.text = $"{speed}X";

    private int SetSpeed()
    {
        if (_score.GetValue() < 5)
            _speed = 1;

        if (_score.GetValue() >= 5 && _score.GetValue() < 15)
            _speed = 3;

        if (_score.GetValue() >= 15 && _score.GetValue() < 20)
            _speed = 5;

        if (_score.GetValue() >= 20 && _score.GetValue() <= 160)
            _speed = 10;

        return _speed;
    }
}
