using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scan : SkillBase
{
    public Scan(SkillDataSO data, Image image) : base(data, image)
    {
        SkillManager.Instance.CreateSkillEffectPrefabs(skillData.effectPrefab, GameManager.Instance.player.transform);
    }
    
    protected override void ExecuteSkill()
    {

        // 일정 시간 이후 멈추게하는 함수 작동시키게 하기

    }
}
