using System.Collections;
using UnityEngine;

public class Keith : Agent
{
    private StateMachine<Keith> stateMachine;

    public int totalGold = 0;
    public int turnsLurking = 0;
    int MAX_TURNS_LURKING = 5;
    public delegate void BankRobbery(int i);
    public static event BankRobbery OnBankRobbery;

    public void Awake()
    {
        this.stateMachine = new StateMachine<Keith>();
        this.stateMachine.Init(this, CampState.Instance);
    }

    public override void FixedUpdate()
    {
        TilingEngine controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<TilingEngine>();
        if (!controller.characterMovement)
            return;

        this.stateMachine.Update();
        Vector2 mapLoc = getPosition();
        this.transform.position = new Vector3(mapLoc.x, mapLoc.y, 0);
    }


    public void ChangeState(State<Keith> state)
    {
        this.stateMachine.ChangeState(state);
    }

    public void resetMaxTurns()
    {
        turnsLurking = 0;
        MAX_TURNS_LURKING = Random.Range(5, 10);
    }

    public bool finishedLurking()
    {
        return (turnsLurking >= MAX_TURNS_LURKING);
    }

    public void EnterGlobalState(GlobalState<Keith> state)
    {
        this.stateMachine.EnterGlobalState(state);
    }

    public void LeaveGlobalState()
    {
        this.stateMachine.LeaveGlobalState();
    }

    public void RobGold()
    {
        if (Bob.bankedCash > 10)
        {
            Debug.Log("\"STICK EM UP\" says Keith as he robs the bank blind");
            int goldstolen = Random.Range(1, 11);
            totalGold += goldstolen;
            Debug.Log("Total Gold stolen by Keith is " + totalGold);
            OnBankRobbery(goldstolen);
        }
        else
            Debug.Log("There was no money in the bank for Keith to steal. He is sad now.");
    }
}
