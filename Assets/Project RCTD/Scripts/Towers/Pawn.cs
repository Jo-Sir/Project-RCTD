using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Tower
{
    public override void Attack()
    {
        StartCoroutine(AttackCool(baseAS));
        LooksTarget(target, 10f);
        // ����ü Ǯ������ ��������
        GameManager.Instance.ObjectGet(COLOR_TYPE, this.transform);
        Projectiles projectiles = GetComponentInChildren<Projectiles>();
        // Ǯ���ؿ� ������Ʈ�� ��������
        projectiles.ProjectilesSet(COLOR_TYPE, curATK, projectilesRange, target, targetLayerMask);
        // �θ�����
        projectiles.transform.SetParent(null); 
    }
}
