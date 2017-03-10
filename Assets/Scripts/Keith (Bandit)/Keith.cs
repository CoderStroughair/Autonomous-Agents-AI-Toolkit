using System.Collections;
using UnityEngine;

public class Keith : Agent
{
    private StateMachine<Keith> stateMachine;

    public int totalGold = 0;
    public eLocation location;
    public int turnsLurking = 0;
    int MAX_TURNS_LURKING = 5;
    public delegate void BankRobbery();
    public static event BankRobbery OnBankRobbery;

    public void Awake()
    {
        this.stateMachine = new StateMachine<Keith>();
        this.stateMachine.Init(this, CampState.Instance);
    }

    public override void Update()
    {
        this.stateMachine.Update();
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
        if (OnBankRobbery != null)
        {
            Debug.Log("\"STICK EM UP\" says Keith as he robs the bank blind");
            totalGold += Random.Range(1, 11);
            Debug.Log("Total Gold stolen by Keith is " + totalGold);
            OnBankRobbery();
        }
        else
            Debug.Log("There was no money in the bank for Keith to steal. He is sad now.");
    }
}
