using System.Collections.Generic;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    [SerializeField] private List<Character> _characters;

    private Character _currentCharacter;

    private Character GetCharacter()
    {
        int index = Random.Range(0, _characters.Count);

        return _characters[index];
    }

    public void Activ()
    {
        foreach (Character character in _characters)
            if (character.gameObject.activeSelf == true)
                character.gameObject.SetActive(false);

        _currentCharacter = GetCharacter();

        _currentCharacter.gameObject.SetActive(true);
    }
}


