using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    private Entity entity;
    private Vector2 moveDestination;

    public enum AiState
    {
        patrol,
        hunt,
        aim,
        attack,
        cooldown
    };
    public AiState aiState;

    // Use this for initialization
    void Start () {
        entity = this.gameObject.GetComponent<Entity>() as Entity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Plan()
    {
        switch (aiState)
        {
            case AiState.patrol:
                moveDestination = transform.position;
                List<Vector2> possibleMoves = new List<Vector2>();
                possibleMoves.Add(Vector2.up);
                possibleMoves.Add(Vector2.down);
                possibleMoves.Add(Vector2.left);
                possibleMoves.Add(Vector2.right);
                possibleMoves.Shuffle();
                foreach (Vector2 pm in possibleMoves)
                {
                    if (Utilities.IsWalkable(pm)) moveDestination += pm;
                }
                //TODO Use moveDestination in the Entity's Update() function (or Enemy's Execute() function) to move it
                //TODO Remove Enemy from gameController's enemiesExecutingTurn list when move is complete
                break;
            case AiState.hunt:
                break;
            case AiState.aim:
                break;
            case AiState.attack:
                break;
            case AiState.cooldown:
                break;
            default:
                break;
        }
    }
}
