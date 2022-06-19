using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int jumpCountLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        jumpCountLeft = playerData.jumpCount;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity);

        isAbilityDone = true;

        jumpCountLeft--;

        
    }

    public bool CanJump()
    {
        if(jumpCountLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetJumpCountLeft() => jumpCountLeft = playerData.jumpCount;

    public void DecreaseJumpCountLeft() => jumpCountLeft--;
}
