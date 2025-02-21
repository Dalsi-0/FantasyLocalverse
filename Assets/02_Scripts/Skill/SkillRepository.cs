using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRepository : MonoBehaviour
{
    [SerializeField]
    private List<SkillDataSO> skillList = new List<SkillDataSO>();

    [SerializeField]
    private List<GameObject> skillUIIcon = new List<GameObject>();

    private Dictionary<string, SkillDataSO> skillDictionary = new Dictionary<string, SkillDataSO>();
    private Dictionary<string, GameObject> skillUIIconDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            var skillData = skillList[i];
            skillDictionary[skillData.skillName] = skillData;
            skillUIIconDictionary[skillData.skillName] = skillUIIcon[i];
        }
    }

    /// <summary>
    /// 스킬 이름을 기반으로 SkillBase 객체를 생성하여 반환
    /// </summary>
    public SkillBase GetSkillBase(string skillName, SkillDataSO skillData, Image image)
    {
        switch (skillName)
        {
            case "Dash":
                return new Dash(skillData, image);

            case "Ride":
                return new Ride(skillData, image);

            case "Scan":
                return new Scan(skillData, image);
        }
        return null;
    }

    /// <summary>
    /// 스킬 이름을 기반으로 SkillDataSO를 반환
    /// </summary>
    public SkillDataSO GetSkillData(string skillName)
    {
        if (skillDictionary.TryGetValue(skillName, out SkillDataSO skill))
        {
            return skill;
        }
        return null;
    }

    /// <summary>
    /// 스킬 이름을 기반으로 UI 아이콘을 반환
    /// </summary>
    public GameObject GetSkillUIIcon(string skillName)
    {
        if (skillUIIconDictionary.TryGetValue(skillName, out GameObject skill))
        {
            return skill;
        }
        return null;
    }
}
