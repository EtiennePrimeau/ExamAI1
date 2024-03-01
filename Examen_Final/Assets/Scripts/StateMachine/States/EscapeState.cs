using System;
using UnityEngine;

public class EscapeState : CharacterState
{
    private Vector3 m_enemyOppositeDirection;

    public override void OnEnter()
    {
        Debug.Log("Entering state: EscapeState");

        Vector3 direction = m_stateMachine.transform.position - m_stateMachine.EnemyPosition;
        m_enemyOppositeDirection = Vector3.Normalize(direction * -1.0f);

        SetAgentEscapeDestination();

    }

    private void SetAgentEscapeDestination()
    {
        Vector3 destination = m_enemyOppositeDirection * m_stateMachine.EscapeDistance;

        m_stateMachine.Agent.SetDestination(destination);
    }

    public override void OnExit()
    {
        Debug.Log("Exiting state: EscapeState\n");
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {

    }

    public override bool CanEnter(IState currentState)
    {
        return m_stateMachine.HasFoundEnemy &&
                !m_stateMachine.IsFighting;
    }

    public override bool CanExit()
    {
        return !m_stateMachine.HasFoundEnemy;
    }
}