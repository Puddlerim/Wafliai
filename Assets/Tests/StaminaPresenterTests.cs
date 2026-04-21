using NUnit.Framework;

public class StaminaPresenterTests
{
    private StaminaSystem staminaSystem;
    private FakeStaminaView fakeView;
    private StaminaPresenter presenter;

    [SetUp]
    public void SetUp()
    {
        staminaSystem = new StaminaSystem(100f, 20f, 10f, 1f);
        fakeView = new FakeStaminaView();
        presenter = new StaminaPresenter(staminaSystem, fakeView);
    }

    [TearDown]
    public void TearDown()
    {
        staminaSystem = null;
        fakeView = null;
        presenter = null;
    }

    [Test]
    public void Tick_UpdatesViewValue()
    {
        presenter.Tick(true, true, 1f);

        Assert.AreEqual(0.8f, fakeView.LastValue, 0.001f);
        Assert.AreEqual(1, fakeView.SetValueCallCount);
    }

    [Test]
    public void Tick_DoesNotShowExhausted_WhenStaminaStillAvailable()
    {
        presenter.Tick(true, true, 1f);

        Assert.IsFalse(fakeView.ExhaustedShown);
        Assert.AreEqual(1, fakeView.ShowExhaustedCallCount);
    }

    [Test]
    public void Tick_ShowsExhausted_WhenStaminaBecomesZero()
    {
        presenter.Tick(true, true, 5f);

        Assert.IsTrue(fakeView.ExhaustedShown);
        Assert.AreEqual(1, fakeView.ShowExhaustedCallCount);
    }

    [Test]
    public void Tick_UpdatesViewAfterRegeneration()
    {
        presenter.Tick(true, true, 5f);
        presenter.Tick(false, false, 2f);

        Assert.Greater(fakeView.LastValue, 0f);
    }
}