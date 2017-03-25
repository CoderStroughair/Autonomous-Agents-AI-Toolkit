using System.Collections;
using UnityEngine;

public class Bandit : Agent
{
    private StateMachine<Bandit> stateMachine;

    public int totalGold = 0;
    public int turnsLurking = 0;
    int MAX_TURNS_LURKING = 5;
    public delegate void BankRobbery(int i);
    public static event BankRobbery OnBankBalanceChange;

    public delegate void BanditDeath(eLocation location);
    public static event BanditDeath OnBanditDeath;


    public void Awake()
    {
        this.stateMachine = new StateMachine<Bandit>();
        this.stateMachine.Init(this, CampState.Instance);
        this.location = eLocation.OutlawCamp;
        Sheriff.OnArrival += RespondToArrival;
    }

    public override void FixedUpdate()
    {
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (!controller.characterMovement||dead)
            return;

        this.stateMachine.Update();
        Vector2 mapLoc = getPosition();
        this.transform.position = new Vector3(mapLoc.x, mapLoc.y, 0);
    }


    public void ChangeState(State<Bandit> state)
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

    public void EnterGlobalState(GlobalState<Bandit> state)
    {
        this.stateMachine.EnterGlobalState(state);
    }

    public void LeaveGlobalState()
    {
        this.stateMachine.LeaveGlobalState();
    }

    public void RobGold()
    {
        if (Miner.bankedCash > 10)
        {
            Debug.Log("\"STICK EM UP\" says Keith as he robs the bank blind");
            int goldstolen = Random.Range(1, 11);
            totalGold += goldstolen;
            Debug.Log("Total Gold stolen by Keith is " + totalGold);
            if (OnBankBalanceChange != null)
                OnBankBalanceChange(goldstolen);
        }
        else
            Debug.Log("There was no money in the bank for Keith to steal. He is sad now.");
    }

    public void RespondToArrival(eLocation sheriffLocation)
    {
        eLocation adjustedLocation = sheriffLocation;
        //Adjust code so that the Sheriff knows if the Bandit is in town, from any building.
        if (adjustedLocation == eLocation.SheriffsOffice || adjustedLocation == eLocation.Undertakers)
            adjustedLocation = eLocation.Bank;
        if (this.location == adjustedLocation && !dead)
        {
            Debug.Log("The Sheriff has found the Bandit!");
            float survive = Random.Range(0.0f, 1.0f);
            if (survive > 0.6f)
            {
                Debug.Log("The Bandit escaped capture this time!");
            }
            else
            {
                Debug.Log("The Bandit was killed!");
                this.dead = true;
                this.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 90.0f);
                this.GetComponent<SpriteRenderer>().color = Color.gray;
                if (totalGold > 0)
                {
                    Debug.Log("The Sheriff managed to recover some of the stolen gold!");
                    OnBankBalanceChange(-totalGold);
                }
                if(OnBanditDeath != null)
                    OnBanditDeath(location);
            }
        }
    }
}
