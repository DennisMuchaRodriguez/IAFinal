using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class IACharacterVehiculo : IACharacterControl
{
    protected CalculateDiffuse _CalculateDiffuse;
    protected float speedRotation = 0;

    public float RangeWander;
    protected Vector3 positionWander;
    float FrameRate = 0;
    float Rate = 4;
    public override void LoadComponent()
    {
        base.LoadComponent();
        positionWander = RandoWander(transform.position, RangeWander);
        _CalculateDiffuse = GetComponent<CalculateDiffuse>();
    }
    public virtual void LookEnemy()
    {
        if (AIEye.ViewEnemy == null) return;
        Vector3 dir = (AIEye.ViewEnemy.transform.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        rot.x = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 50);
    }
    public virtual void LookPosition(Vector3 position)
    {

        Vector3 dir = (position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        rot.x = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speedRotation);
    }
    public virtual void LookRotationCollider()
    {

        if (_CalculateDiffuse.Collider)
        {
            speedRotation = _CalculateDiffuse.speedRotation;

            Vector3 posNormal = _CalculateDiffuse.hit.point + _CalculateDiffuse.hit.normal * 2;

            LookPosition(posNormal);
        }
    }


    public virtual void MoveToPosition(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
    public virtual void MoveToEnemy()
    {
        if (AIEye.ViewEnemy == null) return;
        MoveToPosition(AIEye.ViewEnemy.transform.position);
    }
    public virtual void MoveToAllied()
    {
        if (AIEye.ViewAllie == null) return;
        MoveToPosition(AIEye.ViewAllie.transform.position);
    }
    public virtual void MoveToEvadEnemy()
    {
        if (AIEye.ViewEnemy == null) return;
        
        
        
        
        Vector3 dir = (transform.position - AIEye.ViewEnemy.transform.position).normalized;
        Vector3 newPosition = transform.position + dir * 5f;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newPosition, out hit, RangeWander, NavMesh.AllAreas))
        {
            newPosition = hit.position;
        }
        else
        {
            newPosition = RandoWander(transform.position, RangeWander);
        }
        MoveToPosition(newPosition);

    }

    Vector3 RandoWander(Vector3 position, float range)
    {
        // Generar un punto aleatorio en una esfera
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection.y = 0; // Mantener el mismo nivel en el eje Y

        Vector3 randomPosition = position + randomDirection;

        // Buscar la posición más cercana en el NavMesh
        NavMeshHit hit;
        for (int i = 0; i < 30; i++)
        {
            if (NavMesh.SamplePosition(randomPosition, out hit, range, NavMesh.AllAreas))
            {
                return hit.position;
            }

        }
        

        // Si no se encuentra una posición válida, devolver la posición original
        return position;
    }
    public virtual void MoveToWander()
    {
        if (AIEye.ViewEnemy != null) return;

        float distance = (transform.position - positionWander).magnitude;

        if(distance<2)
        {
            positionWander = RandoWander(transform.position, RangeWander);
        }

        if(FrameRate>Rate)
        {
            FrameRate = 0;
            positionWander = RandoWander(transform.position, RangeWander);
        }
        FrameRate += Time.deltaTime;


        MoveToPosition(positionWander);
    }

    public virtual void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up, positionWander);
        Gizmos.DrawSphere(positionWander, 0.5f);

        Gizmos.DrawWireSphere(transform.position + Vector3.up, RangeWander);
    }
}
