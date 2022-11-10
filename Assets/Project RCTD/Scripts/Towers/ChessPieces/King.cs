using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : AttackTower
{
    protected override void UseSkill()
    {
        StartCoroutine(SkillCollTime());
        GameObject obj = GameManager.Instance.ObjectGet(SKILL_TYPE, target.transform);
        Skill skillobj = obj.GetComponent<Skill>();
        skillobj.SKillSetting(CurATK);
        obj.transform.position = target.transform.position;
    }
}
