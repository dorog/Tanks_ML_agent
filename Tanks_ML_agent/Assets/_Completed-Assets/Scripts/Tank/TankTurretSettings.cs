using UnityEngine;

[CreateAssetMenu(fileName = "New Tank Turret Settings", menuName = "TankTurretSettings")]
public class TankTurretSettings : ScriptableObject
{
    public float reloadTime = 2f;
    public float m_TurnSpeed = 180f;
    public float m_MaxLaunchForce = 30f;
}
