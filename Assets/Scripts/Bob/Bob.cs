using UnityEngine;

public class Bob : Agent {

	private StateMachine<Bob> stateMachine;

	public static int WAIT_TIME = 5;
	public int waitedTime = 0;
	public static int totalGold = 0;

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
        return Bob.totalGold >= WAIT_TIME;
    }

	public void CreateTime ()
    {
        Bob.totalGold++;
	}

	public void ChangeState (State<Bob> state)
    {
		this.stateMachine.ChangeState(state);
	}

	override public void Update ()
    {
		this.stateMachine.Update();
        Vector2 mapLoc = getPosition();
        Debug.Log(location + " is in square " + mapLoc);
        this.transform.position = new Vector3(mapLoc.x, mapLoc.y, 0);
	}

    public void Sleep()
    {
        Bob.totalGold = 0;
        this.waitedTime = 0;
    }

    public void RespondToBankRobbery(int goldLost)
    {
        Debug.Log("Bob is upset because he lost some savings");
        totalGold -= goldLost;
    }
}
