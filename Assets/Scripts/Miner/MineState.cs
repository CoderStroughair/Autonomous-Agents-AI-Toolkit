using UnityEngine;

public sealed class MineState : State<Miner> {
	
	static readonly MineState instance = new MineState();
	
	public static MineState Instance {
		get {
			return instance;
		}
	}
	
	static MineState () {}
	private MineState () {}
	
	public override void Enter (Miner agent) {
        agent.location = eLocation.Mine;
		Debug.Log("Bob is mining...");
	}
	
	public override void Execute (Miner agent) {
		agent.CreateTime();
		Debug.Log("Mining Gold. Total Mined: " + Miner.totalGold + " unit" + (Miner.totalGold > 1 ? "s" : "") + "...");
        if (Miner.totalGold > 7)
            agent.ChangeState(BankingState.Instance);
	}
	
	public override void Exit (Miner agent) {
		Debug.Log("...Enough mining for one day!");
	}
}
