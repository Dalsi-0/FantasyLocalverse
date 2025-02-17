using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkillInfo
{
    public int skillId;
    public float cooldownTime;
    public float currentCooldown;
}

public abstract class SkillBase : MonoBehaviour
{
    protected SkillInfo skillInfo; // 스킬 정보 저장

    protected virtual void Start()
    {
        InitSkill();
    }

    private void Update()
    {
        UpdateCooldown();
    }

    protected abstract void InitSkill();

    protected virtual void UpdateCooldown()
    {
        if(skillInfo.currentCooldown > 0)
        {
            skillInfo.currentCooldown -= Time.deltaTime;
        }
    }

    public abstract void UseSkill();

    protected bool CanUseSkill()
    {
        return skillInfo.currentCooldown <= 0;
    }
}
