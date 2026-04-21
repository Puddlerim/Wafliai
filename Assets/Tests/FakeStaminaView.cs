public class FakeStaminaView : IStaminaView
{
    public float LastValue { get; private set; }
    public bool ExhaustedShown { get; private set; }

    public int SetValueCallCount { get; private set; }
    public int ShowExhaustedCallCount { get; private set; }

    public void SetValue(float normalizedValue)
    {
        LastValue = normalizedValue;
        SetValueCallCount++;
    }

    public void ShowExhausted(bool isExhausted)
    {
        ExhaustedShown = isExhausted;
        ShowExhaustedCallCount++;
    }
}