using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public class PlayerClothesData
{
    public int clothID;
    public int colorID;

    public PlayerClothesData(int clothID, int colorID)
    {
        this.clothID = clothID;
        this.colorID = colorID;
    }
}

public class ClothesController : MonoBehaviour
{
    [SerializeField] private ClothesRepository clothesRepository;

    private int clothID;
    private int colorID;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 옷 스프라이트 변경
    /// </summary>
    /// <param name="clothId">변경할 옷 번호</param>
    public void ChangeClothes(int clothId)
    {
        var clothesData = clothesRepository.clothesDatas[clothId];
        
        ApplyClothes(GameManager.Instance.PlayerController.upperWearNomal, clothesData.spritesUpper);
        ApplyClothes(GameManager.Instance.PlayerController.lowerWearNomal, clothesData.spritesLower);
        ApplyClothes(GameManager.Instance.PlayerController.upperWearRide, clothesData.spritesUpper);
        
        this.clothID = clothId;

        GameManager.Instance.SavePlayerClothes(clothID, colorID);
    }

    /// <summary>
    /// 옷 변경 실제 기능
    /// </summary>
    private void ApplyClothes(SpriteRenderer[] target, Sprite[] source)
    {
        for (int i = 0; i < source.Length; i++)
        {
            target[i].sprite = source[i];
        }
    }

    /// <summary>
    /// 옷 색상 변경
    /// </summary>
    /// <param name="colorId">변경할 색상 번호</param>
    public void ChangeColors(int colorId)
    {
        UnityEngine.Color color = UnityEngine.Color.white;
        switch (colorId)
        {
            case 0:
                color = UnityEngine.Color.white;
                break;

            case 1:
                color = UnityEngine.Color.red;
                break;

            case 2:
                color = UnityEngine.Color.green;
                break;
        }

        foreach (var item in GameManager.Instance.PlayerController.upperWearNomal)
        {
            item.color = color;
        }
        foreach (var item in GameManager.Instance.PlayerController.lowerWearNomal)
        {
            item.color = color;
        }
        foreach (var item in GameManager.Instance.PlayerController.upperWearRide)
        {
            item.color = color;
        }

        this.colorID = colorId;

        GameManager.Instance.SavePlayerClothes(clothID, colorID);
    }
}
