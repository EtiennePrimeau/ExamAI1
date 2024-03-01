using UnityEngine;

public class FightState : CharacterState
{
    private float m_timer = 0.0f;
    private const float MAX_TIMER = 3.0f;

    public override void OnEnter()
    {
        Debug.Log("Entering state: FightState");
        m_timer = MAX_TIMER;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting state: FightState\n");
        m_stateMachine.DestroyEnemy();
    }

    public override void OnUpdate()
    {
        m_timer -= Time.deltaTime;
    }

    public override void OnFixedUpdate()
    {

    }

    public override bool CanEnter(IState currentState)
    {
        return m_stateMachine.IsFighting;
    }

    public override bool CanExit()
    {
        return m_timer < 0;
    }
}
