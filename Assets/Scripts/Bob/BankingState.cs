using UnityEngine;

public sealed class BankingState : State<Bob> {

	static readonly BankingState instance = new BankingState();

	public static BankingState Instance {
		get {
			return instance;
		}
	}

	static BankingState () {}
	private BankingState () {}

	public override void Enter (Bob agent) {
        agent.location = eLocation.Bank;
		Debug.Log("Bob is entering the Bank...");
	}

	public override void Execute (Bob agent) {
		agent.IncreaseBankedCash(Bob.totalGold);
		Debug.Log("Banking the money - Stored: " + Bob.totalGold + " - Total Gold: " + Bob.bankedCash);
        Bob.totalGold = 0;

        if (agent.StoredEnough())
            agent.ChangeState(SleepState.Instance);
        else
            agent.ChangeState(MineState.Instance);
	}

	public override void Exit (Bob agent) {
		Debug.Log("That's enough work for one day!");
	}
}
