using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scan : SkillBase
{
    public Scan(SkillDataSO data, Image image) : base(data, image)
    {
    }
    
    protected override void ExecuteSkill()
    {
        Debug.Log($"{skillData.skillName} 실행ss");

        // 지속시간 동안 이동 멈춤.

    }
}
