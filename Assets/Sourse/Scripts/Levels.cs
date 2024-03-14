using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private ViewSliderImages _sliderImages;

    private int _counter = 0;

    private void Start()
    {
        ZeroOutByConvention();
    }

    public int GetValue() =>
        _counter;

    public void Boost()
    {
        _counter++;
        ZeroOutByConvention();
    }

    public void ZeroOut()
    {
        _counter = 0;
        _sliderImages.SetImages();
    }

    private void ZeroOutByConvention()
    {
        if (_counter > 4)
            ZeroOut();
    }
}
