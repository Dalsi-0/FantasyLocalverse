using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


[CreateAssetMenu(fileName = "NewSkill", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class SkillDataSO : ScriptableObject
{
    public int id;
    public string skillName;
    public float cooldown;
    public GameObject effectPrefab;
}