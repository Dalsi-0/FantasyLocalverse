using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapColliderManager : MonoBehaviour
{
    [SerializeField] private GameObject[] floorColliders; 
    public int currentFloor;


    /// <summary>
    /// 현재 층을 설정하고 콜라이더를 업데이트하는 함수
    /// </summary>
    public void SetCurrentFloor(int floor)
    {
        currentFloor = floor;
        UpdateFloorColliders();
    }

    public int GetCurrentFloor()
    {
        return currentFloor;
    }

    /// <summary>
    /// 층별 콜라이더 활성화를 업데이트하는 함수
    /// </summary>
    private void UpdateFloorColliders()
    {
        for (int i = 0; i < floorColliders.Length; i++)
        {
            floorColliders[i].SetActive(i == currentFloor - 1);
        }
    }
}
