using UnityEngine;

public class SupportManager : MonoBehaviour
{
    public SupportBlock[] supportBlocks;

    public SupportBlock redLocation = null;
    public SupportBlock blueLocation = null;

    public void ResetMap()
    {
        redLocation = null;
        blueLocation = null;
        UpdateAllGood();
    }

    public void UpdateBlocks(SupportBlock supportBlock, bool isRed)
    {
        if (isRed)
        {
            redLocation = supportBlock;
        }
        else
        {
            blueLocation = supportBlock;
        }

        if(redLocation == null || blueLocation == null)
        {
            UpdateAllGood();
        }
        else
        {
            int minX = redLocation.x > blueLocation.x ? blueLocation.x : redLocation.x;
            int maxX = redLocation.x > blueLocation.x ? redLocation.x : blueLocation.x;

            int minY = redLocation.y > blueLocation.y ? blueLocation.y : redLocation.y;
            int maxY = redLocation.y > blueLocation.y ? redLocation.y : blueLocation.y;

            for (int i = 0; i < supportBlocks.Length; i++)
            {
                supportBlocks[i].UpdateBlockTag(minX, maxX, minY, maxY, redLocation, blueLocation);
            }
        }
    }

    public void UpdateAllGood()
    {
        for (int i = 0; i < supportBlocks.Length; i++)
        {
            supportBlocks[i].UpdateBlockTag(-100, 100, -100, 100, null, null);
        }
    }
}
