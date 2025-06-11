using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Villager")]
public class Villager_Socialize : Action
{
    private IACharacterVehiculo vehiculo;
    private NavMeshAgent agent;

    public override void OnStart()
    {
        vehiculo = GetComponent<IACharacterVehiculo>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        if (vehiculo == null || agent == null || !agent.enabled)
            return TaskStatus.Failure;

        vehiculo.MoveToAllied();
        return TaskStatus.Running;
    }
}