using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _tMPText;
    [SerializeField] private Gameplay _gameplay;

    private const string _saveKey = "valueSave";

    public event UnityAction Changed;

    private int _value;

    private void Awake()
    {
        Load();
    }

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

    public int GetValue() => _value;

    public void ResetValue()
    {
        PlayerPrefs.DeleteAll();
        _value = 1;
        Save();
        Show();
    }

    private void OnWin()
    {
        _value++;

        if (_value == _gameplay.GetSpritesCount() + 1)
            _value = 1;

        Save();
        Show();

        Changed?.Invoke();

        YandexGame.NewLeaderboardScores("BestPlayer", _value);
    }

    private void Show()
    {
        Load();
        _tMPText.text = _value.ToString();
    }

    private void Save()
    {
        SaveManager.Save(_saveKey, GetSaveScore());
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.ScoreController>(_saveKey);

        _value = data.MaxValue;
    }

    private SaveData.ScoreController GetSaveScore()
    {
        var data = new SaveData.ScoreController()
        {
            MaxValue = _value
        };

        return data;
    }
}
