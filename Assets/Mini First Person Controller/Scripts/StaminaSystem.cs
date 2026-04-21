using System;

public class StaminaSystem
{
    public float MaxStamina { get; }
    public float CurrentStamina { get; private set; }
    public float DrainPerSecond { get; }
    public float RegenPerSecond { get; }
    public float RegenDelay { get; }

    private float timeSinceDrain;

    public StaminaSystem(float maxStamina, float drainPerSecond, float regenPerSecond, float regenDelay)
    {
        MaxStamina = Math.Max(0f, maxStamina);
        DrainPerSecond = Math.Max(0f, drainPerSecond);
        RegenPerSecond = Math.Max(0f, regenPerSecond);
        RegenDelay = Math.Max(0f, regenDelay);

        CurrentStamina = MaxStamina;
        timeSinceDrain = 0f;
    }

    public void SetCurrentStamina(float value)
    {
        CurrentStamina = Clamp(value, 0f, MaxStamina);
    }

    public bool CanSprint()
    {
        return CurrentStamina > 0f;
    }

    public float GetNormalizedStamina()
    {
        if (MaxStamina <= 0f)
            return 0f;

        return CurrentStamina / MaxStamina;
    }

    public void Tick(bool wantsToSprint, bool isMoving, float deltaTime)
    {
        if (deltaTime < 0f)
            deltaTime = 0f;

        bool shouldDrain = wantsToSprint && isMoving && CanSprint();

        if (shouldDrain)
        {
            CurrentStamina -= DrainPerSecond * deltaTime;
            CurrentStamina = Clamp(CurrentStamina, 0f, MaxStamina);
            timeSinceDrain = 0f;
            return;
        }

        timeSinceDrain += deltaTime;

        if (timeSinceDrain >= RegenDelay)
        {
            CurrentStamina += RegenPerSecond * deltaTime;
            CurrentStamina = Clamp(CurrentStamina, 0f, MaxStamina);
        }
    }

    private static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}