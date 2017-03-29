using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine <T> {
	
	private T agent;
	private State<T> state;
    private State<T> prevState;

	public void Awake () {
		this.state = null;
	}

	public void Init (T agent, State<T> startState) {
		this.agent = agent;
		this.state = startState;
	}

	public void Update ()
    {
		if (this.state != null) this.state.Execute(this.agent);
	}
	
	public void ChangeState (State<T> nextState) {
		if (this.state != null) this.state.Exit(this.agent);
		this.state = nextState;
		if (this.state != null) this.state.Enter(this.agent);
	}

    public void EnterGlobalState(GlobalState<T> nextState)
    {
        this.state.Exit(this.agent);
        this.prevState = this.state;
        this.state = nextState;
        this.state.Enter(this.agent);
    }

    public void LeaveGlobalState()
    {
        this.state.Exit(this.agent);
        this.state = this.prevState;
        this.state.Enter(this.agent);
    }
}