using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterManager : MonoBehaviour
{
    private Character _activeCharacter;

    private Character[] _characters;

    void Awake()
    {
        _characters = GetComponentsInChildren<Character>();

        foreach (var character in _characters)
        {
            character.Deactivate();
        }

        ActivateCharacter(0);
    }

    public void ActivateCharacter(int characterNum)
    {
        if (characterNum < 0 || characterNum >= _characters.Length)
        {
            Debug.LogErrorFormat("Invalid character index {0}", characterNum);
            return;
        }

        if (_activeCharacter != null)
        {
            _activeCharacter.Deactivate();
        }

        var character = _characters[characterNum];
        _activeCharacter = character;
        character.Activate();
    }
}
