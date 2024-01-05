using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _tMPText;
    [SerializeField] private Gameplay _gameplay;

    private int _value = 0;

    private void Start()
    {
        Show();
    }

    private void OnEnable()
    {
        _gameplay.Win += OnWin;
    }

    private void OnDisable()
    {
        _gameplay.Win -= OnWin;
    }

    private void OnWin()
    {
        _value++;
        Show();
    }

    private void Show() =>
        _tMPText.text = _value.ToString();
}
