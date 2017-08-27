using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class CharacterManager : MonoBehaviour
{
    public ColorSet palette;

    public GameObject characterPrefab;

    private Character _activeCharacter;

    private Dictionary<int, Character> _characters = new Dictionary<int, Character>();

    private string[] titles = { "RB", "RT", "LB", "LT" };

    void Awake()
    {

    }

    public void Initialize(int x)
    {
        GameObject baseSpawn = GameObject.FindGameObjectWithTag("Spawn");


        Debug.Log(baseSpawn.name + " Access " + x + " out of " + baseSpawn.transform.childCount);
        Transform spawnSet = baseSpawn.transform.GetChild(x-1);

        for (int i = 0; i < 4; i++)
        {
            GameObject characterObj = Instantiate(characterPrefab, spawnSet.GetChild(i).position, Quaternion.identity, transform);
            characterObj.SetActive(true);
            TextMesh buttonTitle = characterObj.GetComponentInChildren<TextMesh>();
            if (buttonTitle != null)
            {
                buttonTitle.text = titles[i];
                buttonTitle.color = palette.TextColors[GetComponent<Player>().playerId];
            }
        }

        var characters = GetComponentsInChildren<Character>();

        var characterNum = 0;

        foreach (var character in characters)
        {
            _characters.Add(characterNum++, character);
            character.Deactivate();

            character.Receive<DeathEvent>().Subscribe(CharacterDied).AddTo(this);
        }

        ActivateCharacter(0);
    }

    public void ActivateCharacter(int characterNum)
    {
        Character newCharacter;

        if (!_characters.TryGetValue(characterNum, out newCharacter))
        {
            Debug.LogErrorFormat("Invalid character index {0}", characterNum);
            return;
        }

        ActivateCharacter(newCharacter);
    }

    private void ActivateCharacter(Character newCharacter)
    {

        if (newCharacter == _activeCharacter || newCharacter == null)
            return;

        if (_activeCharacter != null)
        {
            _activeCharacter.Deactivate();
        }

        _activeCharacter = newCharacter;
        newCharacter.Activate();
    }

    private void CharacterDied(DeathEvent death)
    {
        //Get the position of the death to find the next closest player
        var pos = death.Character.transform.position;

        var bestDist = float.MaxValue;
        Character bestCharacter = null;

        foreach (var character in _characters.Values)
        {
            if (character == null || character == death.Character)
                continue;

            var dist = Vector2.Distance(character.transform.position, pos);

            if (dist > bestDist)
                continue;

            bestDist = dist;
            bestCharacter = character;
        }

        ActivateCharacter(bestCharacter);
    }

    public void MoveToSpawn()
    {

    }
}
