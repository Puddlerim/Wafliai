using NUnit.Framework;

public class StaminaSystemTests
{
    private StaminaSystem staminaSystem;

    [SetUp]
    public void SetUp()
    {
        staminaSystem = new StaminaSystem(100f, 20f, 10f, 1f);
    }

    [TearDown]
    public void TearDown()
    {
        staminaSystem = null;
    }

    [Test]
    public void Constructor_SetsCurrentStaminaToMax()
    {
        Assert.AreEqual(100f, staminaSystem.CurrentStamina);
    }

    [Test]
    public void CanSprint_ReturnsTrue_WhenStaminaAboveZero()
    {
        Assert.IsTrue(staminaSystem.CanSprint());
    }

    [Test]
    public void Tick_DecreasesStamina_WhenSprintingAndMoving()
    {
        staminaSystem.Tick(true, true, 1f);

        Assert.AreEqual(80f, staminaSystem.CurrentStamina, 0.001f);
    }

    [Test]
    public void Tick_DoesNotDecreaseStamina_WhenNotMoving()
    {
        staminaSystem.Tick(true, false, 1f);

        Assert.AreEqual(100f, staminaSystem.CurrentStamina, 0.001f);
    }

    [Test]
    public void Tick_DoesNotDecreaseStamina_WhenNotSprinting()
    {
        staminaSystem.Tick(false, true, 1f);

        Assert.AreEqual(100f, staminaSystem.CurrentStamina, 0.001f);
    }

    [Test]
    public void Tick_DoesNotGoBelowZero()
    {
        staminaSystem.Tick(true, true, 10f);

        Assert.AreEqual(0f, staminaSystem.CurrentStamina, 0.001f);
        Assert.IsFalse(staminaSystem.CanSprint());
    }

    [Test]
    public void Tick_RegeneratesAfterDelay_WhenNotSprinting()
    {
        staminaSystem.Tick(true, true, 2f);
        staminaSystem.Tick(false, false, 0.5f);
        staminaSystem.Tick(false, false, 1f);

        Assert.Greater(staminaSystem.CurrentStamina, 60f);
    }

    [Test]
    public void Tick_DoesNotRegenBeforeDelay()
    {
        staminaSystem.Tick(true, true, 1f);
        staminaSystem.Tick(false, false, 0.5f);

        Assert.AreEqual(80f, staminaSystem.CurrentStamina, 0.001f);
    }

    [Test]
    public void Tick_DoesNotExceedMaxStamina()
    {
        staminaSystem.SetCurrentStamina(95f);
        staminaSystem.Tick(false, false, 5f);

        Assert.AreEqual(100f, staminaSystem.CurrentStamina, 0.001f);
    }

    [TestCase(100f, 1f)]
    [TestCase(50f, 0.5f)]
    [TestCase(0f, 0f)]
    public void GetNormalizedStamina_ReturnsExpectedValue(float current, float expectedNormalized)
    {
        staminaSystem.SetCurrentStamina(current);

        Assert.AreEqual(expectedNormalized, staminaSystem.GetNormalizedStamina(), 0.001f);
    }

    [TestCase(-10f, 0f)]
    [TestCase(20f, 20f)]
    [TestCase(120f, 100f)]
    public void SetCurrentStamina_ClampsToValidRange(float input, float expected)
    {
        staminaSystem.SetCurrentStamina(input);

        Assert.AreEqual(expected, staminaSystem.CurrentStamina, 0.001f);
    }
}