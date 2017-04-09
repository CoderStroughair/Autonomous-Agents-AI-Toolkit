using UnityEngine;

public class Miner : Agent {

	private StateMachine<Miner> stateMachine;

	public static int GOLD_GOAL = 20;
	public static int bankedCash = 0;
	public static int totalGold = 0;

	public void Awake ()
    {
		this.stateMachine = new StateMachine<Miner>();
		this.stateMachine.Init(this, SleepState.Instance);
        Bandit.OnBankBalanceChange += RespondToBankBalanceChange;
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void IncreaseGoldBuffer()
    {
        GOLD_GOAL += 20;
    }
	public void IncreaseBankedCash (int amount)
    {
        Miner.bankedCash += amount;
	}

    public bool StoredEnough ()
    {
        return Miner.bankedCash >= GOLD_GOAL;
    }

	public void Mine ()
    {
        Miner.totalGold++;
	}

	public void ChangeState (State<Miner> state)
    {
		this.stateMachine.ChangeState(state);
    }

	override public void FixedUpdate()
    {
        if (!doMovement())
            return;

        this.stateMachine.Update();
	}

    public void Sleep()
    {
        Miner.totalGold = 0;
        Miner.bankedCash = 0;
    }

    public void RespondToBankBalanceChange(int goldLost)
    {
        if (goldLost >= 0)
        {
            Debug.Log("Bob is upset because he lost some savings");
            bankedCash -= goldLost;
        }
        else
        {
            Debug.Log("Bob is thrilled to get his money back");
            bankedCash -= goldLost;
        }
    }
}
