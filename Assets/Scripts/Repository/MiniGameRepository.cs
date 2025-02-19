using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueDataSO;

public class MiniGameRepository : MonoBehaviour
{
    [SerializeField] private List<MiniGameDataSO> miniGameDataList;

    private Dictionary<ESceneType, MiniGameDataSO> miniGameDictionary = new Dictionary<ESceneType, MiniGameDataSO>();

    private void Awake()
    {
        foreach (var miniGameData in miniGameDataList)
        {
            if (!miniGameDictionary.ContainsKey(miniGameData.sceneType))
            {
                miniGameDictionary.Add(miniGameData.sceneType, miniGameData);
            }
        }
    }

    public MiniGameDataSO GetMiniGameInfo(ESceneType sceneType)
    {
        if (miniGameDictionary.TryGetValue(sceneType, out MiniGameDataSO miniGameData))
        {
            return miniGameData;
        }
        return null;
    }
}
