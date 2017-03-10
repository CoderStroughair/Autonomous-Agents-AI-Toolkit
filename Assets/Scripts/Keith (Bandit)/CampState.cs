using UnityEngine;

public sealed class CampState : State<Keith> {
	
	static readonly CampState instance = new CampState();
	
	public static CampState Instance {
		get {
			return instance;
		}
	}
	
	static CampState() {}
	private CampState() {}
	
	public override void Enter (Keith agent) {
        agent.location = eLocation.OutlawCamp;
        Debug.Log("Keith arrived at the Outlaw Camp...");
	}
	
	public override void Execute (Keith agent)
    {
        agent.turnsLurking++;
        Debug.Log("Keith is lurking in the Outlaw Camp");
        Debug.Log("Keith has been lurking for " + agent.turnsLurking + " turns.");
        if (agent.totalGold >= 20)
        {
            Debug.Log("Keith has stored his gold in his hiding place");
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
	
	public override void Exit (Keith agent)
    {
        Debug.Log("Keith is leaving the Outlaw Camp");
	}
}
