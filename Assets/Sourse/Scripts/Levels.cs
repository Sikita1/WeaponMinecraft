using UnityEngine;
using UnityEngine.Events;

public class Levels : MonoBehaviour
{
    [SerializeField] private ViewSliderImages _sliderImages;

    private const string _saveKey = "counterSave";

    public event UnityAction Reset; 

    private int _counter = 0;

    private void Start()
    {
        Load();
    }

    public int GetValue() =>
        _counter;

    public void Boost()
    {
        _counter++;
        ZeroOutByConvention();
        Save();
    }

    public void ZeroOut()
    {
        _counter = 0;
        _sliderImages.SetImages();
        Save();
    }

    public void ResetImage()
    {
        _counter = 0;
        _sliderImages.ResetCurrentImage();
        _sliderImages.SetFirstImages();
        Reset?.Invoke();
        Save();
    }

    private void ZeroOutByConvention()
    {
        if (_counter > 4)
            ZeroOut();
    }

    private void Save()
    {
        SaveManager.Save(_saveKey, GetSaveCounter());
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.LevelsController>(_saveKey);

        _counter = data.Counter;
    }

    private SaveData.LevelsController GetSaveCounter()
    {
        var data = new SaveData.LevelsController()
        {
            Counter = _counter
        };

        return data;
    }
}
