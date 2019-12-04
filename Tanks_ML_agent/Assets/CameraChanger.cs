using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    private Vector3 originalPos;
    private int position = 1;
    public int maxPosition = 8;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(position < maxPosition)
            {
                position++;
                transform.position += new Vector3(0, -30f, 0); 
            }
            else
            {
                position = 1;
                transform.position = originalPos;
            }
        }
    }
}
