using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private AudioSource _source;
    [SerializeField] private Gameplay _gameplay;

    private void OnEnable()
    {
        _gameplay.Win += GetAudio;
    }

    private void OnDisable()
    {
        _gameplay.Win -= GetAudio;
    }

    private void GetAudio()
    {
        _source.clip = _audioClips[GetRundonIndex()];
        _source.Play();
    }

    private int GetRundonIndex() =>
        Random.Range(0, _audioClips.Count);
}
