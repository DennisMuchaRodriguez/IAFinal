using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Villager")]
public class Villager_SeeFriend : Conditional
{
    private IAEyeBase eye;
    private IAVillagerBehaviour config;

    public override void OnStart()
    {
        eye = GetComponent<IAEyeBase>();
        config = GetComponent<IAVillagerBehaviour>();
    }

    public override TaskStatus OnUpdate()
    {
        if (eye == null || config == null) return TaskStatus.Failure;
        if (eye.ViewAllie != null &&
            Vector3.Distance(transform.position, eye.ViewAllie.transform.position) <= config.socialRange)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}