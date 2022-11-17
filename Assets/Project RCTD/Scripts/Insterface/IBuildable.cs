using UnityEngine;
public interface IBuildable
{
    public bool BuildCheck(out Tower tower);
    public Transform GetTransForm();
    public void ParticleOnOff(bool on);
}
