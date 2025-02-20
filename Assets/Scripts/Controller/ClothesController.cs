using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class ClothesController : MonoBehaviour
{
    [SerializeField] private ClothesRepository clothesRepository;
    private PlayerController controller;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        controller = GameManager.Instance.PlayerController;
    }

    /// <summary>
    /// �� ��������Ʈ ����
    /// </summary>
    /// <param name="clothID">������ �� ��ȣ</param>
    public void ChangeClothes(int clothID)
    {
        var clothesData = clothesRepository.clothesDatas[clothID];

        ApplyClothes(controller.upperWearNomal, clothesData.spritesUpper);
        ApplyClothes(controller.lowerWearNomal, clothesData.spritesLower);
        ApplyClothes(controller.upperWearRide, clothesData.spritesUpper);
    }

    /// <summary>
    /// �� ���� ���� ���
    /// </summary>
    private void ApplyClothes(SpriteRenderer[] target, Sprite[] source)
    {
        for (int i = 0; i < source.Length; i++)
        {
            target[i].sprite = source[i];
        }
    }

    /// <summary>
    /// �� ���� ����
    /// </summary>
    /// <param name="colorId">������ ���� ��ȣ</param>
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

        foreach (var item in controller.upperWearNomal)
        {
            item.color = color;
        }
        foreach (var item in controller.lowerWearNomal)
        {
            item.color = color;
        }
        foreach (var item in controller.upperWearRide)
        {
            item.color = color;
        }
    }
}
