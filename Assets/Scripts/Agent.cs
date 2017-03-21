using UnityEngine;
using System.Collections;

abstract public class Agent : MonoBehaviour {

	abstract public void Update ();

    abstract protected Vector2 getPosition();
}