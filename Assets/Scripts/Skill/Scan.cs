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

        // ���� �ð� ���� ���߰��ϴ� �Լ� �۵���Ű�� �ϱ�

    }
}
