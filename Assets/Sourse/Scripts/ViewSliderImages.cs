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

    private const string _saveKey = "imageSliderSave";

    private int _numberCurrentImage = 0;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        SetFirstImages();
    }

    public void ResetCurrentImage()
    {
        _numberCurrentImage = 0;
        Save(0);
    }

    public void SetImages()
    {
        Save(_score.GetValue());
        Load();

        AssignPictures();
    }

    public void SetFirstImages()
    {
        AssignPictures();

        Save(0);
    }

    private void AssignPictures()
    {
        _imageInception.sprite = _gameplay.GetAvtiveSprites(_numberCurrentImage);
        _imageMidleInception.sprite = _gameplay.GetAvtiveSprites(_numberCurrentImage + 1);
        _imageMidle.sprite = _gameplay.GetAvtiveSprites(_numberCurrentImage + 2);
        _imageMidleEnd.sprite = _gameplay.GetAvtiveSprites(_numberCurrentImage + 3);
        _imageEnd.sprite = _gameplay.GetAvtiveSprites(_numberCurrentImage + 4);
    }

    private void Save(int value) =>
        SaveManager.Save(_saveKey, GetSaveNumberImage(value));

    private void Load()
    {
        var data = SaveManager.Load<SaveData.ImagesSliderController>(_saveKey);

        _numberCurrentImage = data.NumberImageSlider;
    }

    private SaveData.ImagesSliderController GetSaveNumberImage(int value)
    {
        var data = new SaveData.ImagesSliderController()
        {
            NumberImageSlider = value
        };

        return data;
    }
}
