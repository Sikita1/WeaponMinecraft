using UnityEngine;
using UnityEngine.UI;

public class ViewSliderImages : MonoBehaviour
{
    [SerializeField] private Gameplay _gameplay;
    [SerializeField] private Score _score;

    [SerializeField] private Image _imageInception;
    [SerializeField] private Image _imageMidleInception;
    [SerializeField] private Image _imageMidle;
    [SerializeField] private Image _imageMidleEnd;
    [SerializeField] private Image _imageEnd;

    public void SetImages()
    {
        _imageInception.sprite = _gameplay.GetAvtiveSprites(_score.GetValue());
        _imageMidleInception.sprite = _gameplay.GetAvtiveSprites(_score.GetValue() + 1);
        _imageMidle.sprite = _gameplay.GetAvtiveSprites(_score.GetValue() + 2);
        _imageMidleEnd.sprite = _gameplay.GetAvtiveSprites(_score.GetValue() + 3);
        _imageEnd.sprite = _gameplay.GetAvtiveSprites(_score.GetValue() + 4);
    }
}
