using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChevalierSM : StateMachine<CharacterState>
{
    [field: SerializeField] public GameObject Enemy { get; private set; } = null;
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public Vector3 EnemyPosition { get; private set; } = Vector3.zero;
    [field: SerializeField] public Vector3 KeyPosition { get; private set; } = Vector3.zero;
    [field: SerializeField] public Vector3 DoorPosition { get; private set; } = Vector3.zero;
    [field: SerializeField] public float Strength { get; private set; } = 50.0f;
    [field: SerializeField] public float EnemyDetectionRadius { get; private set; } = 10.0f;
    [field: SerializeField] public float DetectionTimer { get; private set; } = 2.0f;
    [field: SerializeField] public float EscapeDistance { get; private set; } = 10.0f;
    [field: SerializeField] public bool HasFoundEnemy { get; private set; } = false;
    [field: SerializeField] public bool IsFighting { get; private set; } = false;
    [field: SerializeField] public bool HasKey { get; private set; } = false;


    private float m_timer = 0.0f;

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new SearchState());
        m_possibleStates.Add(new EscapeState());
        m_possibleStates.Add(new FightState());
    }

    protected override void Awake()
    {
        base.Awake();

        Agent = GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        m_timer = DetectionTimer;
        SetKeyPosition();
        SetDoorPosition();

        foreach (CharacterState state in m_possibleStates)
        {

            state.OnStart(this);
        }

        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

    }

    private void SetDoorPosition()
    {
        var door = GameObject.Find("Door");
        if (door == null)
        {
            Debug.Log("There is no door");
        }
        DoorPosition = door.transform.position;
    }

    private void SetKeyPosition()
    {
        var key = GameObject.Find("Key");
        if (key == null)
        {
            Debug.Log("There is no key");
        }
        KeyPosition = key.transform.position;
    }

    protected override void Update()
    {
        base.Update();


        if (m_timer < 0.0f)
        {
            SearchForEnemies();
            m_timer = DetectionTimer;
        }
        m_timer -= Time.deltaTime;

        if (!HasKey)
        {
            CheckForKey();
        }

        //if (HasKey && Vector3.Distance(transform.position, DoorPosition) < 1)
        //{
        //    Debug.Log("YOU WIN !!!!");
        //}
    }

    private void CheckForKey()
    {
        //if (Vector3.Distance(transform.position, KeyPosition) < 1)
        //{
        //    HasKey = true;
        //}
    }

    private void SearchForEnemies()
    {
        EnemyPosition = Vector3.zero;
        
        var collidedObjs = Physics.OverlapSphere(transform.position, EnemyDetectionRadius);

        foreach (var obj in collidedObjs)
        {
            if (!obj.name.StartsWith("Enemy"))
            {
                continue;
            }

            if (EnemyPosition != Vector3.zero && 
                Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, EnemyPosition))
            {
                EnemyPosition = obj.transform.position;
                Debug.Log(obj.name + " is new EnemyPosition");
                HasFoundEnemy = true;
                Enemy = obj.gameObject;
                EvaluateEnemy(obj.gameObject);
                return;
            }
        }

        if (EnemyPosition == Vector3.zero)
        {
            HasFoundEnemy = false;
        }
    }

    private void EvaluateEnemy(GameObject enemy)
    {
        var enemyStrength = enemy.GetComponent<EnemyRandomStrength>().GetStrength();

        if (enemyStrength < Strength)
        {
            IsFighting = true;
        }
        else
        {
            IsFighting = false;
        }
    }

    public void DestroyEnemy()
    {
        Destroy(Enemy);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Key")
        {
            HasKey = true;
        }
        if (collision.gameObject.name == "Door")
        {
            Debug.Log("YOU WIN !!!");
        }
    }
}