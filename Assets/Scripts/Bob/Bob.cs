using UnityEngine;

public class Bob : Agent {

	private StateMachine<Bob> stateMachine;

	public static int WAIT_TIME = 20;
	public static int bankedCash = 0;
	public static int totalGold = 0;

	public void Awake ()
    {
		this.stateMachine = new StateMachine<Bob>();
		this.stateMachine.Init(this, SleepState.Instance);
        Keith.OnBankRobbery += RespondToBankRobbery;
    }

	public void IncreaseBankedCash (int amount)
    {
        Bob.bankedCash += amount;
	}

	public bool WaitedLongEnough ()
    {
		return Bob.bankedCash >= WAIT_TIME;
	}

    public bool StoredEnough ()
    {
        return Bob.bankedCash >= WAIT_TIME;
    }

	public void CreateTime ()
    {
        Bob.totalGold++;
	}

	public void ChangeState (State<Bob> state)
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
        Bob.totalGold = 0;
        Bob.bankedCash = 0;
    }

    public void RespondToBankRobbery(int goldLost)
    {
        Debug.Log("Bob is upset because he lost some savings");
        bankedCash -= goldLost;
    }
}
