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
		Debug.Log("Starting to wait...");
	}

	public override void Execute (Bob agent) {
		agent.IncreaseWaitedTime(1);
		Debug.Log("Banking the money - Stored: " + agent.totalGold + " - Total Gold: " + agent.totalGold*agent.waitedTime);
        agent.totalGold = 0;
        if (agent.WaitedLongEnough())
        {
            if (agent.CreatedEnough())
                agent.ChangeState(SleepState.Instance);
            else
                agent.ChangeState(MineState.Instance);
        }
	}

	public override void Exit (Bob agent) {
		Debug.Log("That's enough work for one day!");
	}
}
