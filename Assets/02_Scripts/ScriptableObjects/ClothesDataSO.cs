using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


[CreateAssetMenu(fileName = "NewCloth", menuName = "Scriptable Object/Cloth Data", order = int.MaxValue)]
public class ClothesDataSO : ScriptableObject
{
    public int id;
    public Sprite[] spritesUpper;
    public Sprite[] spritesLower;
}