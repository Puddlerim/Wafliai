using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("References")]
    public FirstPersonMovement movement;
    public Slider staminaSlider;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float drainPerSecond = 20f;
    public float regenPerSecond = 15f;
    public float regenDelay = 1.25f;

    [Header("Movement Check")]
    public float movementThreshold = 0.1f;

    private StaminaSystem staminaSystem;
    private Rigidbody rb;

    public float CurrentStamina => staminaSystem != null ? staminaSystem.CurrentStamina : 0f;
    public float NormalizedStamina => staminaSystem != null ? staminaSystem.GetNormalizedStamina() : 0f;

    void Awake()
    {
        if (movement == null)
            movement = GetComponent<FirstPersonMovement>();

        rb = GetComponent<Rigidbody>();
        staminaSystem = new StaminaSystem(maxStamina, drainPerSecond, regenPerSecond, regenDelay);
        UpdateUI();
    }

    void Update()
    {
        bool isMoving = false;
        if (rb != null)
        {
            Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            isMoving = horizontalVelocity.magnitude > movementThreshold;
        }

        bool wantsToSprint = movement != null &&
                             Input.GetKey(movement.runningKey) &&
                             isMoving;

        staminaSystem.Tick(wantsToSprint, isMoving, Time.deltaTime);

        FirstPersonMovement.canRun = staminaSystem.CanSprint();

        UpdateUI();
    }

    void UpdateUI()
    {
        if (staminaSlider != null)
        {
            staminaSlider.value = staminaSystem.GetNormalizedStamina();
        }
    }
}