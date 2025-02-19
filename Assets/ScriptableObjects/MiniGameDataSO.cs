using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MiniGameData", menuName = "MiniGame/Create MiniGame Data")]
public class MiniGameDataSO : ScriptableObject
{
    public ESceneType sceneType;  // �̴ϰ��� �� Ÿ��
    public string gameName;       // ���� �̸�
    [TextArea] public string description;  // ����
}