using MLAgents;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.

    public TankTurretSettings tankTurretData;

    private float m_TurnInputValue;             // The current value of the turn input.
    private float Angle;

    public bool canFire = true;
    public Transform Turret;

    private void FixedUpdate()
    {
        Turn();
    }

    public void Fire(bool agent = false, Agent shoter = null)
    {
        if (canFire)
        {
            canFire = false;

            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            if (agent)
            {
                AgentShellExplosion agentShellExplosion = shellInstance.transform.GetComponent<AgentShellExplosion>();
                agentShellExplosion.shoter = shoter;
            }

            shellInstance.velocity = tankTurretData.m_MaxLaunchForce * m_FireTransform.forward;

            //m_ShootingAudio.clip = m_FireClip;
            //m_ShootingAudio.Play();


            Invoke("ReloadIsReady", tankTurretData.reloadTime);
        }
    }

    private void ReloadIsReady()
    {
        canFire = true;
    }

    public float CanFire()
    {
        if (canFire)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void Turn()
    {
        float turn = m_TurnInputValue * tankTurretData.m_TurnSpeed * Time.deltaTime;
        float angle = GetTurnByAngle();

        float summTurn = turn + angle;

        Turret.Rotate(0, summTurn, 0, Space.Self);

        m_TurnInputValue = 0;
    }

    private float GetTurnByAngle()
    {
        float angle = tankTurretData.m_TurnSpeed * Time.fixedDeltaTime;
        if (Angle > 0)
        {
            float result = Angle > angle ? angle : Angle;
            return result;
        }
        else
        {
            float AngleAbs = Mathf.Abs(Angle);
            float result = AngleAbs > angle ? -angle : Angle;
            return result;
        }
    }

    public void SetAngle(float angle)
    {
        Angle = angle;
    }

    public void AddTurn(float turn)
    {
        m_TurnInputValue += turn;
    }
}
