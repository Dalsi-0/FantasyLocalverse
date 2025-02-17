using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class SkillBase
{
    protected SkillData skillData;
    protected float currentCooldown;

    private Image coolDownImage;

    public SkillBase(SkillData data, Image image)
    {
        skillData = data;
        currentCooldown = 0;

        coolDownImage = image;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= deltaTime;
        }

        coolDownImage.fillAmount = currentCooldown / skillData.cooldown;
    }

    private bool CanUseSkill()
    {
        return currentCooldown <= 0;
    }

    public void UseSkill()
    {
        if (!CanUseSkill())
        {
            Debug.Log($"{skillData.skillName} 스킬이 아직 쿨다운 중");
            return;
        }

        ExecuteSkill();
        currentCooldown = skillData.cooldown;
    }

    protected abstract void ExecuteSkill(); // 스킬 실행 로직은 개별 스킬에서 구현

}
