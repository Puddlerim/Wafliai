public class StaminaPresenter
{
    private readonly StaminaSystem staminaSystem;
    private readonly IStaminaView staminaView;

    public StaminaPresenter(StaminaSystem staminaSystem, IStaminaView staminaView)
    {
        this.staminaSystem = staminaSystem;
        this.staminaView = staminaView;
    }

    public void Tick(bool wantsToSprint, bool isMoving, float deltaTime)
    {
        staminaSystem.Tick(wantsToSprint, isMoving, deltaTime);

        staminaView?.SetValue(staminaSystem.GetNormalizedStamina());
        staminaView?.ShowExhausted(!staminaSystem.CanSprint());
    }
}