using UnityEngine;

[CreateAssetMenu(fileName = "New Tank Movement Settings", menuName = "TankMovementSettings")]
public class TankMovementSettings : ScriptableObject
{
    public float Speed = 12f;
    public float TurnSpeed = 180f;

    public float PitchRange = 0.2f;
    public AudioClip EngineIdling;
    public AudioClip EngineDriving;
}
