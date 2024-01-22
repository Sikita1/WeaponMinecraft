using UnityEngine;
using UnityEngine.UI;

public class VolumeMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Slider _slider;

    private float _volume;

    public void SetValue()
    {
        _volume = _slider.value;
        _audio.volume = _volume;
    }
}
