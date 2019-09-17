using UnityEngine;

public class TankTurretController : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.

    private string m_TurnAxisName;              // The name of the input axis for turning.
    private float m_TurnInputValue;             // The current value of the turn input.

    private float Rotation = 0;

    public Transform Turret;

    private void OnEnable()
    {
        //m_Rigidbody.isKinematic = false;

        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        //m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_TurnAxisName = "TurretRotation" + m_PlayerNumber;
    }


    private void Update()
    {
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    private void FixedUpdate()
    {
        Turn();
    }


    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Turret.Rotate(0, turn, 0, Space.Self);
    }
}
