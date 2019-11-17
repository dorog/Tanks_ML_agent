using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public int AgentBrain = 1;
    public TankBattleArenaManager tankBattleArenaManager;

    public void Done()
    {
        AgentBrain--;
        if(AgentBrain == 0)
        {
            tankBattleArenaManager.Done(gameObject);
            Destroy(gameObject);
        }
    }
}
