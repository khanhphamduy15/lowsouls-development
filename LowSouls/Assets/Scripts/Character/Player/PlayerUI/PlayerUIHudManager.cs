using LS;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHudManager : MonoBehaviour
{
    [SerializeField] CanvasGroup[] canvasGroup;

    [Header("Stats Bar")]
    [SerializeField] UI_StatBar staminaBar;
    [SerializeField] UI_StatBar healthBar;

    [Header("Quick Slots")]
    [SerializeField] Image rightWeaponQuickSlotIcon;
    [SerializeField] Image leftWeaponQuickSlotIcon;
    [SerializeField] Image quickSlotItemIcon;
    [SerializeField] TextMeshProUGUI quickSlotItemCount;

    [Header("Boss Health Bar")]
    public Transform bossHealthBarParent;
    public GameObject bossHealthBarObject;

    private void Start()
    {
        ToggleHUD(false);
    }
    public void ToggleHUD(bool status)
    {
        //to do transition
        if (status)
        {
            foreach (var canvas in canvasGroup)
            {
                canvas.alpha = 1;
            }
        }
        else
        {
            foreach (var canvas in canvasGroup)
            {
                canvas.alpha = 0;
            }
        }
    }

    public void RefreshHud()
    {
        healthBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(true);
        staminaBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(true);
    }
    public void SetNewStaminaValue(float oldValue, float newValue)
    {
        staminaBar.SetStat(Mathf.RoundToInt(newValue));
    }


    public void SetMaxStaminaValue(int maxStamina)
    {
        staminaBar.SetMaxStat(maxStamina);
    }

    public void SetNewHealthValue(int oldValue, int newValue)
    {
        healthBar.SetStat(newValue);
    }

    public void SetMaxHealthValue(int maxHealth)
    {
        healthBar.SetMaxStat(maxHealth);
    }

    public void SetRightWeaponQuickSlotIcon(int weaponID)
    {
        WeaponItem weapon = WorldItemDatabase.instance.GetWeaponByID(weaponID);
        if (weapon == null)
        {
            Debug.Log("Item is null");
            rightWeaponQuickSlotIcon.enabled = false;
            rightWeaponQuickSlotIcon.sprite = null;
            return;
        }

        if (weapon.itemIcon == null)
        {
            Debug.Log("No Icon");
            rightWeaponQuickSlotIcon.enabled = false;
            rightWeaponQuickSlotIcon.sprite = null;
            return;
        }

        rightWeaponQuickSlotIcon.sprite = weapon.itemIcon;
        rightWeaponQuickSlotIcon.enabled = true;

    }

    public void SetLeftWeaponQuickSlotIcon(int weaponID)
    {
        WeaponItem weapon = WorldItemDatabase.instance.GetWeaponByID(weaponID);
        if (weapon == null)
        {
            Debug.Log("Item is null");
            leftWeaponQuickSlotIcon.enabled = false;
            leftWeaponQuickSlotIcon.sprite = null;
            return;
        }

        if (weapon.itemIcon == null)
        {
            Debug.Log("No Icon");
            leftWeaponQuickSlotIcon.enabled = false;
            leftWeaponQuickSlotIcon.sprite = null;
            return;
        }

        leftWeaponQuickSlotIcon.sprite = weapon.itemIcon;
        leftWeaponQuickSlotIcon.enabled = true;

    }

    public void SetQuickSlotItemIcon(QuickSlotItem quickSlot)
    {
        if (quickSlot == null)
        {
            quickSlotItemIcon.enabled = false;
            quickSlotItemIcon.sprite = null;
            quickSlotItemCount.enabled = false;
            return;
        }
        if (quickSlot.itemIcon == null)
        {
            quickSlotItemIcon.enabled = false;
            quickSlotItemIcon.sprite = null;
            quickSlotItemCount.enabled = false;
            return;
        }
        quickSlotItemIcon.sprite = quickSlot.itemIcon;
        quickSlotItemIcon.enabled = true;

        if (quickSlot.isConsumable)
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            quickSlotItemCount.enabled = true;
            quickSlotItemCount.text = quickSlot.GetCurrentAmount(player).ToString();
        }
        else
        {
            quickSlotItemCount.enabled = false;
        }
    }
}
