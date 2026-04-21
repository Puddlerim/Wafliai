using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerStaminaIntegrationTests
{
    private GameObject playerObject;
    private Rigidbody rb;
    private FirstPersonMovement movement;
    private PlayerStamina playerStamina;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject("Player");
        playerObject.SetActive(false);

        rb = playerObject.AddComponent<Rigidbody>();
        movement = playerObject.AddComponent<FirstPersonMovement>();
        playerStamina = playerObject.AddComponent<PlayerStamina>();

        playerStamina.movement = movement;
        playerStamina.maxStamina = 100f;
        playerStamina.drainPerSecond = 20f;
        playerStamina.regenPerSecond = 10f;
        playerStamina.regenDelay = 1f;
    }

    [TearDown]
    public void TearDown()
    {
        if (playerObject != null)
            Object.DestroyImmediate(playerObject);

        FirstPersonMovement.canRun = true;
    }

    [Test]
    public void Awake_InitializesStamina_AndAllowsRunning()
    {
        playerObject.SetActive(true);

        Assert.IsTrue(FirstPersonMovement.canRun);
    }
}