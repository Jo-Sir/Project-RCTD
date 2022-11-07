using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Tower
{
    public override void Attack()
    {
        StartCoroutine(AttackCool(baseAS));
        LooksTarget(target, 10f);
        // 투사체 풀링에서 가져오기
        GameManager.Instance.ObjectGet(COLOR_TYPE, this.transform);
        Projectiles projectiles = GetComponentInChildren<Projectiles>();
        // 풀링해온 오브젝트에 정보전달
        projectiles.ProjectilesSet(COLOR_TYPE, curATK, projectilesRange, target, targetLayerMask);
        // 부모해제
        projectiles.transform.SetParent(null); 
    }
}
