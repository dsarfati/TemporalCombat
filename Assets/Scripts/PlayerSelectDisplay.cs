using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UniRx;
using UnityEngine;

public class PlayerSelectDisplay : MonoBehaviour
{

    [SerializeField]
    private int _playerNumber;

    [SerializeField]
    private Material _notReadyMaterial;

    [SerializeField]
    private Material _readyMaterial;

    [SerializeField]
    private GameObject _animatedCharacter;

    [SerializeField]
    private GameObject _joinText;

    [SerializeField]
    private GameObject _selectText;

    [SerializeField]
    private GameObject _readyText;

    [SerializeField]
    private GameObject[] _arrows;


    [SerializeField]
    private ColorSet palette;

    private PlayerSelectManager _selectionMgr;

    private GameObject _character;

    private Helmet _selectedHelmet = Helmet.Hair;

    private HelmetManager _helmetManager;

    void Start()
    {
        _character = Instantiate(_animatedCharacter, transform.position, Quaternion.identity, transform);
        _helmetManager = GetComponentInChildren<HelmetManager>();
        ConnectToEvents();
    }

    private void HideAll()
    {
        _joinText.SetActive(false);
        _selectText.SetActive(false);
        _readyText.SetActive(false);

        //_joinText.

        foreach (var arrow in _arrows)
        {
            arrow.SetActive(false);
        }
    }

    private void ConnectToEvents()
    {
        IObservable<float> control = null;
        IObservable<int> isReady = null;

        _selectionMgr = GameObject.FindObjectOfType<PlayerSelectManager>();

        switch (_playerNumber)
        {
            case 0:
                control = InputHandlerSingleton.Instance.Player1Move;
                isReady = _selectionMgr.Player1Status.StartWith(0);
                break;
            case 1:
                control = InputHandlerSingleton.Instance.Player2Move;
                isReady = _selectionMgr.Player2Status.StartWith(0);
                break;
            case 2:
                control = InputHandlerSingleton.Instance.Player3Move;
                isReady = _selectionMgr.Player3Status.StartWith(0);
                break;
            case 3:
                control = InputHandlerSingleton.Instance.Player4Move;
                isReady = _selectionMgr.Player4Status.StartWith(0);
                break;
        }

        // isReady = isReady.DelayFrame(1);
        // control = control.DelayFrame(1);

        isReady.Subscribe(i =>
        {
            print("Ready status " + i);

            HideAll();

            //Not ready
            if (i == 0)
            {
                _character.SetMaterial(_notReadyMaterial);
                _joinText.SetActive(true);
            }
            else
            {
                _character.SetMaterial(_readyMaterial);

                _character.SetColor(palette.HUDColors[_playerNumber]);

                //Selecting character
                if (i == 1)
                {
                    _selectText.SetActive(true);

                    foreach (var arrow in _arrows)
                    {
                        arrow.SetActive(true);
                    }
                }
                else
                {
                    _readyText.SetActive(true);
                }
            }
            // Prompt.SetActive(i == 0);
            // Ready.SetActive(i == 2);
        }).AddTo(this);

        control
            .Select(i => i > 0 ? 1 : i < 0 ? -1 : 0)
            .DistinctUntilChanged()
            //.Throttle(TimeSpan.FromSeconds(0.1f))
            .CombineLatest(isReady, (c, r) =>
            {
                if (r != 1)
                    return 0;
                else
                    return c;
            })
            .Where(i => i != 0)
            .Subscribe(i =>
            {
                print("Change character " + i);

                var newIndex = (int)_selectedHelmet + i;

                var helmetTypes = Enum.GetNames(typeof(Helmet));

                if (newIndex < 0)
                    newIndex = helmetTypes.Length - 1;

                if (newIndex >= helmetTypes.Length)
                    newIndex = 0;

                _selectedHelmet = (Helmet)newIndex;

                print("New Helmet " + _selectedHelmet);

                _helmetManager.SetHelmet(_selectedHelmet);

                print(_playerNumber);
                PlayerSelectManager.Helmets[_playerNumber] = _selectedHelmet;



                // currCharacter += i;
                // if (currCharacter >= Characters.Count)
                // {
                //     currCharacter = 0;
                // }
                // else if (currCharacter < 0)
                // {
                //     currCharacter = Characters.Count - 1;
                // }
                // Display.sprite = Characters[currCharacter];
            }).AddTo(this);
    }
}
