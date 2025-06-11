using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Villager")]
public class Villager_SeeZombie : Conditional
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
        if (eye.ViewEnemy != null &&
            Vector3.Distance(transform.position, eye.ViewEnemy.transform.position) <= config.fleeRange)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}