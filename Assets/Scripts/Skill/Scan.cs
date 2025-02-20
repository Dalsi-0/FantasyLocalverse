using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Scan : SkillBase
{
    public Scan(SkillDataSO data, Image image) : base(data, image)
    {
        myEffectObject = SkillManager.Instance.CreateSkillEffectPrefabs(skillData.effectPrefab, GameManager.Instance.PlayerController.transform);
    }
    
    protected override void ExecuteSkill()
    {
        Transform target = myEffectObject.transform;
        Light2D light2D = target.GetChild(1).GetComponent<Light2D>();
        float duration = 5f; 
        float scaleMultiplier = 2.5f; 

        SkillManager.Instance.StartCoroutine(ScaleEffect(target, light2D, scaleMultiplier, duration));
    }

    private IEnumerator ScaleEffect(Transform target, Light2D light2d, float scaleMultiplier, float duration)
    {
        Vector3 originalScale = target.localScale;
        target.localScale = originalScale * scaleMultiplier;
        light2d.pointLightOuterRadius = 5f;

        yield return new WaitForSeconds(duration);

        target.localScale = originalScale;
        light2d.pointLightOuterRadius = 2.5f;
    }
}
