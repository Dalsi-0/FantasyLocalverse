using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : SkillBase
{
    protected override void InitSkill()
    {
        skillInfo = new SkillInfo { skillId = 0, cooldownTime = 5f, currentCooldown = 0 };
    }

    public override void UseSkill()
    {
        if (!CanUseSkill()) return;

        Debug.Log("use dash");

        skillInfo.currentCooldown = skillInfo.cooldownTime;
    }

}
