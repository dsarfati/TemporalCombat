using UnityEngine;

internal struct PositionUpdate
{
    public Vector3 pos;
    public PositionUpdate(Vector3 transformPosition)
    {
        pos = transformPosition;
    }
}