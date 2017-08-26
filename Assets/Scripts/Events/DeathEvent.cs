
using UnityEngine;

public struct DeathEvent
{
    public Character Character { get; set; }

    public DeathEvent(Character character)
    {
        Character = character;
    }
}