using UnityEngine;

public class TargetReserController : MonoBehaviour
{
    private Target[] target;

    void Awake()
    {
        target = Object.FindObjectsByType<Target>();
    }

    public void ResetAllTargets()
    {
        foreach (Target t in target)
        {
            t.ResetTarget();
        }
    }
}
