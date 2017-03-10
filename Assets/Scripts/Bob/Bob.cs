using UnityEngine;

public class Bob : Agent {

	private StateMachine<Bob> stateMachine;

	public static int WAIT_TIME = 5;
	public int waitedTime = 0;
	public int totalGold = 0;
    public eLocation location;

	public void Awake ()
    {
		this.stateMachine = new StateMachine<Bob>();
		this.stateMachine.Init(this, SleepState.Instance);
        Keith.OnBankRobbery += RespondToBankRobbery;
    }

	public void IncreaseWaitedTime (int amount)
    {
		this.waitedTime += amount;
	}

	public bool WaitedLongEnough ()
    {
		return this.waitedTime >= WAIT_TIME;
	}

    public bool CreatedEnough ()
    {
        return this.totalGold >= WAIT_TIME;
    }

	public void CreateTime ()
    {
		this.totalGold++;
		this.waitedTime = 0;
	}

	public void ChangeState (State<Bob> state)
    {
		this.stateMachine.ChangeState(state);
	}

	override public void Update ()
    {
		this.stateMachine.Update();
	}

    public void Sleep()
    {
        this.totalGold = 0;
        this.waitedTime = 0;
    }

    public void RespondToBankRobbery()
    {
        Debug.Log("Bob is upset because he lost some savings");
    }
}
