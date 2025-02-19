using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MiniGameData", menuName = "MiniGame/Create MiniGame Data")]
public class MiniGameDataSO : ScriptableObject
{
    public ESceneType sceneType;  // 미니게임 씬 타입
    public string gameName;       // 게임 이름
    [TextArea] public string description;  // 설명
}