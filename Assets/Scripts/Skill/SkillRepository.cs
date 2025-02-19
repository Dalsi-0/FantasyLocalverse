using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public SkillBase GetSkillBase(string skillName, SkillDataSO dashData, Image image)
    {
        switch (skillName)
        {
            case "Dash":
                return new Dash(dashData, image);

            case "Ride":
                return new Ride(dashData, image);
        }
        return null;
    }
    public SkillDataSO GetSkillData(string skillName)
    {
        if (skillDictionary.TryGetValue(skillName, out SkillDataSO skill))
        {
            return skill;
        }
        return null;
    }

    public GameObject GetSkillUIIcon(string skillName)
    {
        if (skillUIIconDictionary.TryGetValue(skillName, out GameObject skill))
        {
            return skill;
        }
        return null;
    }
}
