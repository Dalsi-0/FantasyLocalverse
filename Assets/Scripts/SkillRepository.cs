using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillRepository : MonoBehaviour
{
    public static SkillRepository Instance { get; private set; }

    [SerializeField]
    private List<SkillData> skillList = new List<SkillData>();

    [SerializeField]
    private List<GameObject> skillUIIcon = new List<GameObject>();

    private Dictionary<string, SkillData> skillDictionary = new Dictionary<string, SkillData>();
    private Dictionary<string, GameObject> skillUIIconDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
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

    public SkillBase GetSkillBase(string skillName, SkillData dashData, Image image)
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
    public SkillData GetSkillData(string skillName)
    {
        if (skillDictionary.TryGetValue(skillName, out SkillData skill))
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
