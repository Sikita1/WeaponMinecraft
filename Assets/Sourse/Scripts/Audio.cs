using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private AudioSource _source;
    [SerializeField] private Gameplay _gameplay;

    [SerializeField] private AudioSource _winAudio;
    [SerializeField] private AudioClip _win;

    [SerializeField] private Advertisement _advertisement;

    private void Start()
    {
        GetAudio();
    }

    private void OnEnable()
    {
        _gameplay.Win += GetAudio;
    }

    private void OnDisable()
    {
        _gameplay.Win -= GetAudio;
    }

    public void Pause()
    {
        if (_advertisement.IsADS == true)
            _source.Pause();
    }

    public void UnPause()
    {
        if (_advertisement.IsADS == false)
            _source.UnPause();
    }

    private void GetAudio()
    {
        _source.clip = _audioClips[GetRundonIndex()];
        _source.Play();
    }

    private int GetRundonIndex() =>
        Random.Range(0, _audioClips.Count);

    public void Win() =>
        _winAudio.PlayOneShot(_win);
}
