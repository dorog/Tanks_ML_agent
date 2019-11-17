using UnityEngine;

public class DieInTime : MonoBehaviour
{
    public float liveTime = 1.5f;

    void Start()
    {
        Destroy(gameObject, liveTime);
    }
}
