using System.Globalization;
using UnityEngine;

namespace LS
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Stamina Regeneration")]
        [SerializeField] float staminaRegenerationAmount = 0.5f;
        [SerializeField] float staminaRegenerationDelay = 2f;
        private float staminaRegenerationTimer = 0;
        private float staminaTickTimer = 0;

        [Header("Blocking Absorptions")]
        public float blockingPhysicalAbsorption;
        public float blockingMagicAbsorption;
        public float blockingLightningAbsorption;
        public float blockingFireAbsorption;
        public float blockingHolyAbsorption;
        public float blockingStability;

        [Header("Poise")]
        public float totalPoiseDamage;
        public float offensivePoiseBonus;
        public float basePoiseDefense;
        public float defaultPoiseResetTimer = 8;
        public float poiseResetTimer = 0;


        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }

        public int CalculateHealthBasedOnVitalityLevel(int vitality)
        {
            float health = 0;

            //stamina calc logic
            health = vitality * 10;

            return Mathf.RoundToInt(health);

        }

        public int CalculateStaminaBasedOnEnduranceLevel(int endurance)
        {
            float stamina = 0;

            //stamina calc logic
            stamina = endurance * 15;

            return Mathf.RoundToInt(stamina);

        }

        public virtual void RegenerateStamina()
        {
            //Only owners can edit their network variables
            if (!character.IsOwner)
            {
                return;
            }
            if (character.characterNetworkManager.isSprinting.Value)
                return;

            if (character.isPerformingAction)
                return;

            staminaRegenerationTimer += Time.deltaTime;

            if (staminaRegenerationTimer >= staminaRegenerationDelay)
            {
                if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
                {
                    staminaTickTimer += Time.deltaTime;
                    if (staminaTickTimer >= 0.1)
                    {
                        staminaTickTimer = 0;
                        character.characterNetworkManager.currentStamina.Value += staminaRegenerationAmount;
                    }
                }
            }
        }

        public virtual void ResetStaminaRegenTimer(float prevStaminaAmount, float curStaminaAmount)
        {
            //regen if action used stamina
            //no delay if is already regenerating
            if (curStaminaAmount < prevStaminaAmount)
            {
                staminaRegenerationTimer = 0;
            }
        }

        protected virtual void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                totalPoiseDamage = 0;
            }
        }
    }
}
