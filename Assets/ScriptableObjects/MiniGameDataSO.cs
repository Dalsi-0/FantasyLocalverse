using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMiniGame", menuName = "Scriptable Object/MiniGame Data")]
public class MiniGameDataSO : ScriptableObject
{
    public ESceneType sceneType;  // �̴ϰ��� �� Ÿ��
    public string gameName;       // ���� �̸�
    [TextArea] public string description;  // ����
}