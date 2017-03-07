using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 targetPosition;
	private Vector3 startPosition;
	private float startTime;
	private float journeyLength;
    private bool isMoving = false;

	public float speed = 1.0f;
	public float endDistance = 0.05f;
	
	/// <summary>
    /// Initialize player
    /// </summary>
    void Start () {
		targetPosition = this.transform.position;
		startPosition = this.transform.position;
	}
	
	/// <summary>
    /// 
    /// </summary>
    void Update () {
		UpdateControl();
		UpdateMovement();
	}

	/// <summary>
    /// Gets keyboard input and uses it to set the target position of the player.
    /// TODO: Make it so that keys are not hard-coded?
    /// TODO: Make this generic so that it can take input from the keyboard for moving the player or from AI for moving the enemies?
    /// TODO: Move this into the GameManager? Or somewhere else?
    /// </summary>
    void UpdateControl()
	{
		if (GameManager.GetCurrentInstruction() != Assets.Scripts.Entities.Instruction.Nothing)
		{
			startPosition = transform.position;
			startTime = Time.time;
			
            journeyLength = Vector2.Distance(targetPosition, startPosition);
		}
    }


    /// <summary>
    /// Moves the object from its current position towards the targetPosition with a certain speed.
    /// TODO: Currently this only works for the player but I want to break it out into a separate class so it can work with any generic moving object.
    /// </summary>
    void UpdateMovement()
	{
        if (isMoving)
        {
            if (Vector2.Distance(transform.position, targetPosition) <= endDistance)
            {
                transform.position = targetPosition;
                startPosition = targetPosition;
                GameManager.instance.SendMessage("OnPlayerTurnCompleted", this.gameObject.GetComponent<Player>());
                isMoving = false;
            }
            else
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector2.Lerp(startPosition, targetPosition, fracJourney);
            }
        }
	}
}
