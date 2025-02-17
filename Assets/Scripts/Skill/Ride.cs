using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ride : SkillBase
{
    public Ride(SkillData data, Image image) : base(data, image)
    {
    }

    protected override void ExecuteSkill()
    {
        Debug.Log($"{skillData.skillName} ½ÇÇàddd");
    }
}
