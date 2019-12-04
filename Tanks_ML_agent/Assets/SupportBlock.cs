using UnityEngine;

public class SupportBlock : MonoBehaviour
{
    public int x;
    public int y;

    public string redLocation = "RedLocation";
    public string blueLocation = "BlueLocation";
    public string good = "Good";
    public string bad = "Bad";
    public string both = "Both";

    public SupportManager supportManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RedTank" || other.gameObject.tag == "BlueTank")
        {
            TankMovementAgent tankMovementAgent = other.gameObject.GetComponent<TankMovementAgent>();

            if (tankMovementAgent)
            {
                if (gameObject.tag == bad)
                {
                    tankMovementAgent.EnteredBad();
                }
            }

            supportManager.UpdateBlocks(this, other.gameObject.tag == "RedTank" ? true : false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RedTank" || other.gameObject.tag == "BlueTank")
        {
            TankMovementAgent tankMovementAgent = other.gameObject.GetComponent<TankMovementAgent>();
            if (!tankMovementAgent)
                return;

            if (gameObject.tag == bad)
            {
                tankMovementAgent.ExitBad();
            }
        }
    }

    public void UpdateBlockTag(int minX, int maxX, int minY, int maxY, SupportBlock red, SupportBlock blue)
    {
        if(this == blue && this == red)
        {
            gameObject.tag = both;
        }
        if(this == blue)
        {
            gameObject.tag = blueLocation;
        }
        else if(this == red)
        {
            gameObject.tag = redLocation;
        }
        else
        {
            if (minX <= x && maxX >= x && minY <= y && maxY >= y)
            {
                gameObject.tag = good;
            }
            else
            {
                gameObject.tag = bad;
            }
        }

    }
}
