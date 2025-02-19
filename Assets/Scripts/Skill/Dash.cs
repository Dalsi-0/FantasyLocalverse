using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dash : SkillBase
{
    private PlayerController player;
    private float dashTime = 0.15f;  // ��� ���� �ð�

    public Dash(SkillData data, Image image) : base(data, image)
    {
        player = GameManager.Instance.PlayerController;
    }

    protected override void ExecuteSkill()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = dir - player.transform.position;

        player.StartCoroutine(player.Dash(dir, 3f, dashTime)); // ��� �ӵ� 15
    }
}
