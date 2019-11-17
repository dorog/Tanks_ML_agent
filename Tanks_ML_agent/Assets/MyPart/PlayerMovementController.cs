using Complete;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public TankMovement tankMovement;

    private string m_MovementAxisName;
    private string m_TurnAxisName;

    void Start()
    {
        m_MovementAxisName = "VerticalUI";
        m_TurnAxisName = "HorizontalUI";
    }

    void Update()
    {
        float movementInput = Input.GetAxis(m_MovementAxisName);
        float turnInput = Input.GetAxis(m_TurnAxisName);

        tankMovement.AddMovement(movementInput);
        tankMovement.AddRotation(turnInput);
    }
}
