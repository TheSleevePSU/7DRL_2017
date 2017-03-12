using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

public class Enemy : Entity {

    public Sword sword;

    public LayerMask sightBlockMask;

    private List<Node> pathToClosestPlayer;
    private List<Player> playersInSight;

    // Use this for initialization
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        if (!IsMyTurn())
        {
            return;
        }

        ScanForPlayers(this.position, this.stats.GetSightRange());

        //Debug. TODO: Only call when needed.
        pathToClosestPlayer = PathfindToPlayer(FindClosestPlayer());
    }

    public override void HandleInstruction(Instruction instruction)
    {
        base.HandleInstruction(instruction);
    }

    public override void HandleHit(Weapon weapon)
    {
        base.HandleHit(weapon);
    }

    private List<Player> ScanForPlayers(Vector2 origin, float range)
    {
        // This first attempt at implementation is lazy and poor performance.
        // Find all players in scene (slow) and check if a raycast to them is blocked by a wall.
        List<Player> visiblePlayers = new List<Player>();
        Player[] playersInScene = FindObjectsOfType<Player>(); //TODO: Only execute this when the number of players in the scene changes (Have GameManager send a message)
        foreach (Player p in playersInScene)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, p.transform.position, sightBlockMask);
            if (hit.collider == null)
            {
                visiblePlayers.Add(p);
            }
        }
        //Debug
        if (visiblePlayers.Count > 0)
        {
            foreach (Player p in visiblePlayers)
            {
                Debug.Log("Enemy " + position.ToString() + " has line of sight to Player " + p.position.ToString());
            }
        }
        return visiblePlayers; // TODO: Figure out how to handle null case, where no players exist in the scene
    }

    private Player FindClosestPlayer()
    {
        // This first attempt at implementation is lazy and poor performance.
        // Find all players in scene (slow) and calculate distance to each (slow), then select the closest as the target.
        Player closestPlayer = null;
        float distanceToClosestPlayer = float.MaxValue;
        Player[] playersInScene = FindObjectsOfType<Player>(); //TODO: Only execute this when the number of players in the scene changes (Have GameManager send a message)
        foreach (Player p in playersInScene)
        {
            float distance = Vector2.Distance(transform.position, p.transform.position);
            if (distance < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distance;
                closestPlayer = p;
            }
        }
        return closestPlayer; // TODO: Figure out how to handle null case, where no players exist in the scene
    }

    List<Node> PathfindToPlayer(Player player)
    {
        Grid.instance.pathfinding.FindPath(transform.position, player.transform.position);
        return Grid.instance.path;
    }

    public override bool IsMyTurn()
    {
        return GameManager.instance.gameState == GameManager.GameState.AiExecute || GameManager.instance.gameState == GameManager.GameState.AiPlanning;
    }
}
