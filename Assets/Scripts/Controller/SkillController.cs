using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillController : MonoBehaviour
{
    private Dictionary<Key, SkillBase> skillBindings = new Dictionary<Key, SkillBase>();

    private void Update()
    {
        UpdateSkillsCooldown();
    }

    private void UpdateSkillsCooldown()
    {
        float deltaTime = Time.deltaTime;
        foreach (SkillBase skill in skillBindings.Values)
        {
            skill.UpdateCooldown(deltaTime);
        }
    }

    public void AssignSkill(Key key, SkillBase skill)
    {
        skillBindings[key] = skill;
    }

    public void UseSkill(Key key)
    {
        if (skillBindings.ContainsKey(key) && skillBindings[key] != null)
        {
            skillBindings[key].UseSkill();
        }
        else
        {
            Debug.Log($"{key}에 할당된 스킬이 없습니다.");
        }
    }
}
