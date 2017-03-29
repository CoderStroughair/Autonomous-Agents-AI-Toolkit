using System.Collections;
using UnityEngine;

public class Undertaker : Agent
{
    private StateMachine<Agent> stateMachine;
    public Agent body;

    public void Awake()
    {
        this.stateMachine = new StateMachine<Agent>();
        this.stateMachine.Init(this, UndertakingState.Instance);
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Bandit.OnBanditDeath += FindBody;
    }

    override public void FixedUpdate()
    {
        bool movement = doMovement();
        if (body)
        {
            body.transform.position = this.transform.position;
        }
        if (!movement)
            return;

        this.stateMachine.Update();
    }

    void FindBody(eLocation location)
    {
        body = GameObject.Find("Keith").GetComponent<Bandit>();
        this.destination = body.location;
        if (this.destination == eLocation.Bank)
            this.location = eLocation.Bank;
    }

    public void BuryBody()
    {
        if (body != null)
        {
            body.dead = false;
            body.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -90.0f);
            body.GetComponent<SpriteRenderer>().color = Color.white;
            body.GetComponent<SpriteRenderer>().flipX = false;
            Debug.Log("The Bandit lives again!");
            this.destination = eLocation.Undertakers;
            body = null;
        }
    }
}
