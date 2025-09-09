using UnityEngine;

namespace LS {
    public class AIBigfootCharacterManager : AIBossCharacterManager
    {
        [HideInInspector] public AIBigfootSoundFXManager bigfootSoundFXManager;
        [HideInInspector] public AIBigfootCombatManager bigfootCombatManager;


        protected override void Awake()
        {
            base.Awake();
            bigfootSoundFXManager = GetComponent<AIBigfootSoundFXManager>();
            bigfootCombatManager = GetComponent<AIBigfootCombatManager>();
        }
    }
}
