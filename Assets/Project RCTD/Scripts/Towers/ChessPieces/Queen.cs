using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : AttackTower
{
    protected override void UseSkill()
    {
        StartCoroutine(SkillCollTime());
        GameObject objOne = GameManager.Instance.ObjectGet(SKILL_TYPE.WHITE_BISHOP_SKILL, target.transform);
        GameObject objTwo = GameManager.Instance.ObjectGet(SKILL_TYPE.BLACK_BISHOP_SKILL, target.transform);
        Skill skillobjOne = objOne.GetComponent<Skill>();
        Skill skillobjTwo = objTwo.GetComponent<Skill>();
        skillobjOne.SKillSetting(CurATK);
        skillobjTwo.SKillSetting(CurATK);
        objOne.transform.position = target.transform.position;
        objTwo.transform.position = target.transform.position;
    }
}
