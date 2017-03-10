using UnityEngine;

public sealed class MineState : State<Bob> {
	
	static readonly MineState instance = new MineState();
	
	public static MineState Instance {
		get {
			return instance;
		}
	}
	
	static MineState () {}
	private MineState () {}
	
	public override void Enter (Bob agent) {
        agent.location = eLocation.Mine;
		Debug.Log("Bob is mining...");
	}
	
	public override void Execute (Bob agent) {
		agent.CreateTime();
		Debug.Log("Mining Gold. Total Mined: " + agent.totalGold + " unit" + (agent.totalGold > 1 ? "s" : "") + "...");
        if (agent.totalGold > 7)
            agent.ChangeState(BankingState.Instance);
	}
	
	public override void Exit (Bob agent) {
		Debug.Log("...Enough mining for one day!");
	}
}
