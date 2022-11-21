using UnityEngine;
public interface IBuildable
{
    /// <summary>
    /// Ÿ�Ͽ� Ÿ���� �ִ��� Ȯ���ϰ� Ÿ���������� �Ǽ� ����
    /// </summary>
    /// <param name="tower"></param>
    /// <returns></returns>
    public bool BuildCheck(out Tower tower);
    public Transform GetTransForm();
    public void ParticleOnOff(bool on);
}
