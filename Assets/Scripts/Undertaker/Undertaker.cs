using System.Collections;
using UnityEngine;

public class Undertaker : Agent
{
    private StateMachine<Undertaker> stateMachine;
    public Agent body;

    public void Awake()
    {
        this.stateMachine = new StateMachine<Undertaker>();
        this.stateMachine.Init(this, UndertakingState.Instance);
        Bandit.OnBanditDeath += FindBody;
    }

    override public void FixedUpdate()
    {
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (!controller.characterMovement)
            return;

        Vector2 mapLoc = getPosition();
        this.transform.position = new Vector3(mapLoc.x, mapLoc.y, 0);

        this.stateMachine.Update();


    }

    void FindBody(eLocation location)
    {
        body = GameObject.Find("Keith").GetComponent<Bandit>();
        this.location = body.location;
    }

    public void BuryBody()
    {
        if (body != null)
        {
            body.dead = false;
            body.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -90.0f);
            body.GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("The Bandit lives again!");
            this.location = eLocation.Undertakers;
            body = null;
        }
    }
}
