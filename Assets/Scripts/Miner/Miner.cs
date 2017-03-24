using UnityEngine;

public class Miner : Agent {

	private StateMachine<Miner> stateMachine;

	public static int WAIT_TIME = 20;
	public static int bankedCash = 0;
	public static int totalGold = 0;

	public void Awake ()
    {
		this.stateMachine = new StateMachine<Miner>();
		this.stateMachine.Init(this, SleepState.Instance);
        Bandit.OnBankBalanceChange += RespondToBankBalanceChange;
    }

	public void IncreaseBankedCash (int amount)
    {
        Miner.bankedCash += amount;
	}

	public bool WaitedLongEnough ()
    {
		return Miner.bankedCash >= WAIT_TIME;
	}

    public bool StoredEnough ()
    {
        return Miner.bankedCash >= WAIT_TIME;
    }

	public void CreateTime ()
    {
        Miner.totalGold++;
	}

	public void ChangeState (State<Miner> state)
    {
		this.stateMachine.ChangeState(state);
	}

	override public void FixedUpdate()
    {
        TilingEngine controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<TilingEngine>();
        if (!controller.characterMovement)
            return;

        this.stateMachine.Update();
        Vector2 mapLoc = getPosition();
        this.transform.position = new Vector3(mapLoc.x, mapLoc.y, 0);
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
