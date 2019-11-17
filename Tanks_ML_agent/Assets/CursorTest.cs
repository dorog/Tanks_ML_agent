using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTest : MonoBehaviour
{
    public float RotationSpeed = 12f;
    public Transform turret;

    void Update()
    {
        float turn = GetTurn();

        Debug.Log(turn);


        turret.Rotate(0, turn * RotationSpeed * Time.deltaTime, 0, Space.Self);
    }

    private float GetTurn()
    {
        var v3 = Input.mousePosition;
        v3.z = 10f;
        v3 = Camera.main.ScreenToWorldPoint(v3);


        Vector3 cursor = v3.normalized;
        Vector3 forward = turret.transform.forward.normalized;

        return Vector3.SignedAngle(forward, cursor, new Vector3(0, 1, 0));
    }
}
