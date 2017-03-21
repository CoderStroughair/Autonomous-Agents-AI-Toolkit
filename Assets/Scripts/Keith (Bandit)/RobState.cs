using UnityEngine;

public sealed class RobState : GlobalState<Keith> {
	
	static readonly RobState instance = new RobState();
	
	public static RobState Instance {
		get {
			return instance;
		}
	}
	
	static RobState() {}
	private RobState() {}

    public override void Enter(Keith agent)
    {
        agent.location = eLocation.Cemetery;
        //Debug.Log("Keith has arrived at the Bank...");
    }

    public override void Execute(Keith agent)
    {
        agent.RobGold();
        agent.LeaveGlobalState();
    }

    public override void Exit(Keith agent)
    {
        //Debug.Log("Keith is leaving the Cemetery");
    }
}
