using UnityEngine;

public sealed class SleepState : State<Bob>
{
    int timeslept = 0;
    static readonly SleepState instance = new SleepState();

    public static SleepState Instance
    {
        get
        {
            return instance;
        }
    }

    static SleepState() { }
    private SleepState() { }

    public override void Enter(Bob agent)
    {
        agent.location = eLocation.Shack;
        Debug.Log("Bob has headed home to bed.");
    }

    public override void Execute(Bob agent)
    {
        agent.Sleep();
        Debug.Log("...Bob is sleeping. Good night Bob.");
        timeslept++;
        if (timeslept > 4)
            agent.ChangeState(MineState.Instance);
    }

    public override void Exit(Bob agent)
    {
        Debug.Log("Bob has worken up, ready to start a new day.");
    }
}