using UnityEngine;

public class PlayerTurretController : MonoBehaviour
{
    public Transform Turret;
    public TankShooting tankShooting;

    private readonly float ignoreAngle = 5f;

    private void OnEnable()
    {
        //m_Rigidbody.isKinematic = false;

        //m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        //m_Rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tankShooting.Fire();
        }
    }

    private void FixedUpdate()
    {
        float angle = GetTurn();
        if(Mathf.Abs(angle) < ignoreAngle)
        {
            angle = 0;
        }
        tankShooting.SetAngle(angle);
    }

    private float GetTurn()
    {
        var v3 = Input.mousePosition;

        v3 = Camera.main.ScreenToWorldPoint(v3);
        v3.y = 0;

        Vector3 cursor = (v3 - Turret.transform.position).normalized;
        Vector3 forw = Turret.transform.forward;
        forw.y = 0;
        Vector3 forward = forw.normalized;

        return Vector3.SignedAngle(forward, cursor, new Vector3(0, 1, 0));
    }
}
