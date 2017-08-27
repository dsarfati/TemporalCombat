using UnityEngine;

internal struct PositionUpdate
{
    public Vector3 pos;
    public Vector3 vel;
    public PositionUpdate(Vector3 transformPosition, Vector3 velocity)
    {
        pos = transformPosition;
        vel = velocity;
    }
}