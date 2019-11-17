using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PractiseTankController : MonoBehaviour
{
    public float amount = 0;
    public TankMovement tankMovement;

    private float direction = 1;

    private Vector3 position;

    public Rigidbody rigid;

    private void Start()
    {
        position = transform.position;
    }

    /*void Update()
    {
        DistanceCheck();
        tankMovement.AddMovement(1 * direction);
    }*/

    private void DistanceCheck()
    {
        float distance = (transform.position - position).magnitude;
        if (distance > amount)
        { 
            Vector3 result = position + direction * amount * transform.forward;
            direction *= -1;
            rigid.MovePosition(result);
            //tankMovement.AddMovement(1 * direction);
        }
    }

    private void FixedUpdate()
    {
        Check();
        tankMovement.AddMovement(1 * direction);
        //DistanceCheck();
    }

    private void Check()
    {
        float distance = (transform.position - position).magnitude;
        if (distance > amount)
        {
            Vector3 vector = (position - transform.position).normalized;
            if(vector != transform.forward * direction)
            {
                direction *= -1;
            }
        }
    }
}
