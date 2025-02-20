using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ride : SkillBase
{
    ParticleSystem myParticleSystem;

    public Ride(SkillDataSO data, Image image) : base(data, image)
    {
        if(skillData.effectPrefab != null)
        {
            myEffectObject = SkillManager.Instance.CreateSkillEffectPrefabs(skillData.effectPrefab, GameManager.Instance.player.transform);
            myParticleSystem = myEffectObject.transform.GetComponent<ParticleSystem>();
        }
        else
        {
            Debug.Log($"{data.name}¿« ¿Ã∆Â∆Æ «¡∏Æ∆’¿Ã null");
        }
    }
    
    protected override void ExecuteSkill()
    {
        PlayerController player = GameManager.Instance.PlayerController;

        if (myParticleSystem != null) { myParticleSystem.Play(); }

        if (player != null)
        {
            player.ChangeSystemState(player.GetPlayerState() == EPlayerState.Normal 
                ? EPlayerState.Ride 
                : EPlayerState.Normal);
        }
    }
}
