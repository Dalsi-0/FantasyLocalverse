using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ride : SkillBase
{
    public Ride(SkillDataSO data, Image image) : base(data, image)
    {
    }
    
    protected override void ExecuteSkill()
    {
        PlayerController player = GameManager.Instance.PlayerController;
        
        if (player != null)
        {
            player.ChangeSystemState(player.GetPlayerState() == EPlayerState.Normal 
                ? EPlayerState.Ride 
                : EPlayerState.Normal);
        }
    }
}
