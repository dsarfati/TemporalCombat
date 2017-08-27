using UnityEngine;

internal struct TargetMessage
{
    public Transform old;
    public Transform newTrans;
    public TargetMessage(Transform newCharacterTransform, Transform activeCharacterTransform = null)
    {
        old = activeCharacterTransform;
        newTrans = newCharacterTransform;
    }
}