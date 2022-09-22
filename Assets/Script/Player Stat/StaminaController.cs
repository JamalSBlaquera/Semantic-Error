using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    [Header("Player Stamina")]
    [SerializeField] protected float stamina = 100.0f;
    [SerializeField] protected float maxValue = 100.0f;
    [SerializeField] protected float jumpCost = 20f;
    [HideInInspector] public bool isRegenerated = true;
    [HideInInspector] public bool isSprinting = false;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)][SerializeField] protected float staminaDrain = 0.5f;
    [Range(0, 50)] [SerializeField] protected float staminaRegen = 0.5f;

    [Header("Stamina Speed Parameters")]
    [SerializeField] protected int slowRunSpeed = 4;
    [SerializeField] protected int normalRunSpeed = 0;



}
