using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Villager")]
public class Villager_Wander : Action
{
    private IACharacterVehiculo vehiculo;
    private NavMeshAgent agent;
    private IAVillagerBehaviour config;

    public override void OnStart()
    {
        vehiculo = GetComponent<IACharacterVehiculo>();
        agent = GetComponent<NavMeshAgent>();
        config = GetComponent<IAVillagerBehaviour>();

        if (vehiculo != null && config != null)
        {
            vehiculo.RangeWander = config.wanderRadius;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (vehiculo == null || agent == null || !agent.enabled)
            return TaskStatus.Failure;

        vehiculo.MoveToWander();
        return TaskStatus.Running;
    }
}