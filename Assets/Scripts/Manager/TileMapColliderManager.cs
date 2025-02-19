using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapColliderManager : MonoBehaviour
{
    [SerializeField] private GameObject[] floorColliders; 
    public int currentFloor;

    public void SetCurrentFloor(int floor)
    {
        currentFloor = floor;
        UpdateFloorColliders();
    }
    public int GetCurrentFloor()
    {
        return currentFloor;
    }

    void UpdateFloorColliders()
    {
        for (int i = 0; i < floorColliders.Length; i++)
        {
            floorColliders[i].SetActive(i == currentFloor - 1);
        }
    }
}
