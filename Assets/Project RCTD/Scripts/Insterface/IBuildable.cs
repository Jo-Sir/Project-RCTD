using UnityEngine;
public interface IBuildable
{
    /// <summary>
    /// 타일에 타워가 있는지 확인하고 타워가없으면 건설 가능
    /// </summary>
    /// <param name="tower"></param>
    /// <returns></returns>
    public bool BuildCheck(out Tower tower);
    public Transform GetTransForm();
    public void ParticleOnOff(bool on);
}
