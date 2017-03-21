using UnityEngine;

public sealed class CemeteryState : State<Keith> {
	
	static readonly CemeteryState instance = new CemeteryState();
	
	public static CemeteryState Instance {
		get {
			return instance;
		}
	}
	
	static CemeteryState() {}
	private CemeteryState() {}

    public override void Enter(Keith agent)
    {
        agent.location = eLocation.Cemetery;
        //Debug.Log("Keith has arrived at the Cemetery...");
    }

    public override void Execute(Keith agent)
    {
        agent.turnsLurking++;
        //Debug.Log("Keith is lurking in the Cemetery Camp");
        //Debug.Log("Keith has been lurking for " + agent.turnsLurking + " turns.");
        if (agent.finishedLurking())
        {
            agent.resetMaxTurns();
            agent.ChangeState(CampState.Instance);
        }
        else if (Random.Range(0, 4) == 1)
        {
            agent.EnterGlobalState(RobState.Instance);
        }

    }

    public override void Exit(Keith agent)
    {
       // Debug.Log("Keith is leaving the Cemetery");
    }
}
