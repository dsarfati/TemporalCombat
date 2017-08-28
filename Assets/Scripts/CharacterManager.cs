using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using Anima2D;

public class CharacterManager : MonoBehaviour
{
    public ColorSet palette;

    public GameObject characterPrefab;

    public GameObject personExplode;

    private Character _activeCharacter;

    public Transform ActiveCharacterTransform;

    private List<Character> _characters = new List<Character>();

    private int _characterIndex = 0;

    private const int NEXT_CODE = 1;
    private const int PREVIOUS_CODE = 0;


    private string[] titles = { "RB", "LB", "RT", "LT" };

    void Awake()
    {
    }

    public void Initialize(GameSettings settings)
    {
        var playerId = settings.PlayerId;
        playerId--;
        GameObject baseSpawn = GameObject.FindGameObjectWithTag("Spawn");

        Debug.Log(baseSpawn.name + " Access " + playerId + " out of " + baseSpawn.transform.childCount);
        Transform spawnSet = baseSpawn.transform.GetChild(playerId);

        for (int i = 0; i < 4; i++)
        {
            var characterColor = palette.TextColors[playerId];
            GameObject characterObj = Instantiate(characterPrefab, spawnSet.GetChild(i).position, Quaternion.identity, transform);
            characterObj.SetActive(true);
            TextMesh buttonTitle = characterObj.GetComponentInChildren<TextMesh>();
            if (buttonTitle != null)
            {
                buttonTitle.text = titles[i];
                buttonTitle.color = characterColor;
            }

            characterObj.transform.FindDeepChild("Body").GetComponent<SpriteRenderer>().color = characterColor;
            characterObj.transform.FindDeepChild("Leg L").GetComponent<SpriteMeshInstance>().color = characterColor;
            characterObj.transform.FindDeepChild("Leg R").GetComponent<SpriteMeshInstance>().color = characterColor;
        }

        var characters = GetComponentsInChildren<Character>();

        var characterNum = 0;

        foreach (var character in characters)
        {
            var helmetMgr = character.GetComponentInChildren<HelmetManager>();
            helmetMgr.SetHelmet(settings.Helmet);

            _characters.Add(character);
            character.Deactivate();

            character.Receive<DeathEvent>().Subscribe(CharacterDied).AddTo(this);
        }

        //Activate the first character
        ActivateCharacter(characters[_characterIndex]);
    }

    public void ActivateCharacter(int characterDirection)
    {
        if (_characters.Count == 0)
            return;
        //Previous
        if (characterDirection == PREVIOUS_CODE)
        {
            _characterIndex--;

            if (_characterIndex < 0)
                _characterIndex = _characters.Count - 1;
        }
        //Next
        else if (characterDirection == NEXT_CODE)
        {
            _characterIndex++;

            if (_characterIndex >= _characters.Count)
                _characterIndex = 0;
        }

        Character newCharacter = _characters[_characterIndex];

        //if (!_characters.TryGetValue(_characterIndex, out newCharacter))
        //{
        //    Debug.LogErrorFormat("Invalid character index {0}", _characterIndex);
        //    return;
        //}

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
        ActiveCharacterTransform = _activeCharacter.transform;
        newCharacter.Activate();
    }

    private void CharacterDied(DeathEvent death)
    {
        var pos = death.Character.transform.position;
        var go = Instantiate<GameObject>(personExplode);
        go.transform.position = pos;
        _characters.Remove(death.Character);
        // ActivateCharacter(bestCharacter);
        ActivateCharacter(NEXT_CODE);
    }

    public void MoveToSpawn()
    {

    }
}