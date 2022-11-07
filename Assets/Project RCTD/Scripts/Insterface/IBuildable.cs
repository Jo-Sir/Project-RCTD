using UnityEngine;
public interface IBuildable
{
    public bool BuildCheck();
    public void Build();
    public Tower GetTowerInfo();
    public Transform GetTransForm();
}
