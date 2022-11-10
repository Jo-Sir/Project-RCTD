using UnityEngine;
public interface IBuildable
{
    public bool BuildCheck(out Tower tower);
    public void Build();
    public Transform GetTransForm();
    public void ParticleOnOff(bool on);
}
