using UnityEngine;

public sealed class BankingState : State<Miner> {

	static readonly BankingState instance = new BankingState();

	public static BankingState Instance {
		get {
			return instance;
		}
	}

	static BankingState () {}
	private BankingState () {}

	public override void Enter (Miner agent) {
        agent.location = eLocation.Bank;
		Debug.Log("Bob is entering the Bank...");
	}

	public override void Execute (Miner agent) {
		agent.IncreaseBankedCash(Miner.totalGold);
		Debug.Log("Banking the money - Stored: " + Miner.totalGold + " - Total Gold: " + Miner.bankedCash);
        Miner.totalGold = 0;

        if (agent.StoredEnough())
            agent.ChangeState(SleepState.Instance);
        else
            agent.ChangeState(MineState.Instance);
	}

	public override void Exit (Miner agent) {
		Debug.Log("That's enough work for one day!");
	}
}
