using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

public class Player : Entity {

    public Weapon currentWeapon;

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

        Instruction currentInstruction = GameManager.GetCurrentInstruction();
        HandleInstruction(currentInstruction);
    }

    public override void HandleInstruction(Instruction instruction)
    {
        isTurnFinished = instruction != Instruction.Nothing;
        base.HandleInstruction(instruction);
    }

    public override bool IsMyTurn()
    {
        return GameManager.instance.gameState == GameManager.GameState.PlayerExecute;
    }

}
