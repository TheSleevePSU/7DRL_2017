using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 targetPosition;
	private Vector3 startPosition;
	private float startTime;
	private float journeyLength;

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
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = transform.position + new Vector3(0, 1, 0);
        }
        */


		if (Input.GetKeyDown(KeyCode.UpArrow)
		||  Input.GetKeyDown(KeyCode.DownArrow)
		||  Input.GetKeyDown(KeyCode.LeftArrow)
		||  Input.GetKeyDown(KeyCode.RightArrow)
		   )
		{
			startPosition = transform.position;
			startTime = Time.time;
			
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				targetPosition = transform.position + new Vector3(0, 1, 0);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				targetPosition = transform.position + new Vector3(0, -1, 0);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				targetPosition = transform.position + new Vector3(-1, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				targetPosition = transform.position + new Vector3(1, 0, 0);
			}

            Collider2D[] c2d = Physics2D.OverlapPointAll(targetPosition);
            foreach (Collider2D c in c2d)
            {
                Tile t = c.gameObject.GetComponent<Tile>();
                if (t != null)
                {
                    if (!t.isWalkable)
                    {
                        targetPosition = startPosition;
                    }
                }
            }

            targetPosition.x = Mathf.RoundToInt(targetPosition.x);
            targetPosition.y = Mathf.RoundToInt(targetPosition.y);

            journeyLength = Vector2.Distance(targetPosition, startPosition);
            
		}

    }


    /// <summary>
    /// Moves the object from its current position towards the targetPosition with a certain speed.
    /// TODO: Currently this only works for the player but I want to break it out into a separate class so it can work with any generic moving object.
    /// </summary>
    void UpdateMovement()
	{
        if (Vector2.Distance(startPosition, targetPosition) <= endDistance)
		{
			transform.position = targetPosition;
			startPosition = targetPosition;
		}
		else
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector2.Lerp(startPosition, targetPosition, fracJourney);
		}
	}
}
