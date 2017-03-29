using UnityEngine;

public sealed class CampState : State<Bandit> {
	
	static readonly CampState instance = new CampState();
	
	public static CampState Instance {
		get {
			return instance;
		}
	}
	
	static CampState() {}
	private CampState() {}
	
	public override void Enter (Bandit agent)
    {
        if (agent.destination == eLocation.UNSET)
            agent.location = eLocation.OutlawCamp;
        agent.destination = eLocation.OutlawCamp;
        Debug.Log("Keith arrived at the Outlaw Camp...");
	}
	
	public override void Execute (Bandit agent)
    {
        agent.turnsLurking++;
       // Debug.Log("Keith is lurking in the Outlaw Camp");
       // Debug.Log("Keith has been lurking for " + agent.turnsLurking + " turns.");
        if (agent.totalGold >= 20)
        {
       //     Debug.Log("Keith has stored his gold in his hiding place");
            agent.totalGold = 0;
        }
        if (agent.finishedLurking())
        {
            agent.resetMaxTurns();
            agent.ChangeState(CemeteryState.Instance);
        }
        else if (Random.Range(0, 4) == 1)
        {
            agent.EnterGlobalState(RobState.Instance);
        }

	}
	
	public override void Exit (Bandit agent)
    {
       // Debug.Log("Keith is leaving the Outlaw Camp");
	}
}
