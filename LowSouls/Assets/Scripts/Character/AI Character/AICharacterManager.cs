using UnityEngine;
using UnityEngine.AI;

namespace LS {
    public class AICharacterManager : CharacterManager
    {
        [Header("Character Name")]
        public string characterName = "";

        [HideInInspector] public AICharacterNetworkManager aiCharacterNetworkManager;
        [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager;
        [HideInInspector] public AICharacterLocomotionManager aICharacterLocomotionManager;

        [Header("Current State")]
        [SerializeField] protected AIState currentState;

        [Header("Navmesh Agent")]
        public NavMeshAgent navMeshAgent;

        [Header("State")]
        public IdleState idle;
        public PursueTargetState pursueTarget;
        public CombatStanceState combatStance;
        public AttackState attack;

        protected override void Update()
        {
            base.Update();
            aiCharacterCombatManager.HandleActionRecovery(this);
        }

        protected override void Awake()
        {
            base.Awake();
            aiCharacterNetworkManager = GetComponent<AICharacterNetworkManager>();
            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aICharacterLocomotionManager = GetComponent <AICharacterLocomotionManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                idle = Instantiate(idle);
                pursueTarget = Instantiate(pursueTarget);
                combatStance = Instantiate(combatStance);
                attack = Instantiate(attack);
                currentState = idle;
            }
            aiCharacterNetworkManager.currentHealth.OnValueChanged += aiCharacterNetworkManager.CheckHP;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            aiCharacterNetworkManager.currentHealth.OnValueChanged -= aiCharacterNetworkManager.CheckHP;

        }

        private void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);
            if (nextState != null)
            {
                currentState = nextState;
            }

            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;

            if (aiCharacterCombatManager.currentTarget != null)
            {
                aiCharacterCombatManager.targetDirection = aiCharacterCombatManager.currentTarget.transform.position - transform.position;
                aiCharacterCombatManager.viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, aiCharacterCombatManager.targetDirection);
                aiCharacterCombatManager.distanceFromTarget = Vector3.Distance(transform.position, aiCharacterCombatManager.currentTarget.transform.position);

                if (navMeshAgent.enabled)
                {
                    Vector3 agentDestination = navMeshAgent.destination;
                    float remainingDistance = Vector3.Distance(agentDestination, transform.position);

                    if (remainingDistance > navMeshAgent.stoppingDistance)
                    {
                        aiCharacterNetworkManager.isMoving.Value = true;
                    }
                    else
                    {
                        aiCharacterNetworkManager.isMoving.Value = false;
                    }
                }
                else
                {
                    aiCharacterNetworkManager.isMoving.Value = false;
                }
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (IsOwner)
                ProcessStateMachine(); 
        }
    }
}
