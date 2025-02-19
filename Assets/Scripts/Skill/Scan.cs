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
        myEffectObject = SkillManager.Instance.CreateSkillEffectPrefabs(skillData.effectPrefab, GameManager.Instance.player.transform);
    }
    
    protected override void ExecuteSkill()
    {
        Transform target = myEffectObject.transform; 
        float duration = 3f; 
        float scaleMultiplier = 1.5f; 

        SkillManager.Instance.StartCoroutine(ScaleEffect(target, scaleMultiplier, duration));
    }

    private IEnumerator ScaleEffect(Transform target, float scaleMultiplier, float duration)
    {
        Vector3 originalScale = target.localScale;
        target.localScale = originalScale * scaleMultiplier;

        yield return new WaitForSeconds(duration);

        target.localScale = originalScale;
    }
}
