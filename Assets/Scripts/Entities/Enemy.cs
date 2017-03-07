using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

public class Enemy : Entity {

    // Distance -- how far can I see?
    public int sightRange;

    public Sword sword;

    private List<Node> pathToClosestPlayer;

    // Use this for initialization
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        ScanForPlayers(this.position, this.sightRange);

        //Debug. TODO: Only call when needed.
        pathToClosestPlayer = PathfindToPlayer(FindClosestPlayer());
    }

    public override void HandleInstruction(Instruction instruction)
    {

    }

    public override void HandleHit(Weapon weapon)
    {
        base.HandleHit(weapon);
    }

    private void ScanForPlayers(Vector2 origin, float range)
    {
        // TODO: Scan (sightRange) for any instances of Players.
    }

    private Player FindClosestPlayer()
    {
        // This first attempt at implementation is lazy and poor performance.
        // Find all players in scene (slow) and calculate distance to each (slow), then select the closest as the target.
        // TODO: Figure out how to handle null case, where no players exist in the scene
        Player closestPlayer = null;
        float distanceToClosestPlayer = float.MaxValue;
        Player[] playersInScene = FindObjectsOfType<Player>();
        foreach (Player p in playersInScene)
        {
            float distance = Vector2.Distance(transform.position, p.transform.position);
            if (distance < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distance;
                closestPlayer = p;
            }
        }
        return closestPlayer;
    }

    List<Node> PathfindToPlayer(Player player)
    {
        Grid.instance.pathfinding.FindPath(transform.position, player.transform.position);
        return Grid.instance.path;
    }
}
