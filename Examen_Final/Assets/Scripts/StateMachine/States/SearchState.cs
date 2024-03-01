using UnityEngine;

public class SearchState : CharacterState
{
    [SerializeField] private float m_timer;


    public override void OnEnter()
    {
        Debug.Log("Entering state: SearchState");

        if (!m_stateMachine.HasKey)
        {
            m_stateMachine.Agent.SetDestination(m_stateMachine.KeyPosition);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting state: SearchState\n");
    }

    public override void OnUpdate()
    {
        if (m_stateMachine.HasKey)
        {
            m_stateMachine.Agent.SetDestination(m_stateMachine.DoorPosition);
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override bool CanEnter(IState currentState)
    {
        return !m_stateMachine.HasFoundEnemy;
    }

    public override bool CanExit()
    {
        return true;
    }
}
