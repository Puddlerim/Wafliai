using NUnit.Framework;
using System.Linq;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

public class PickUpItemsEditModeTests
{
    private GameObject host;
    private PickUpItems pickUpItems;

    private MethodInfo onTriggerEnterMethod;
    private MethodInfo onTriggerExitMethod;

    [SetUp]
    public void SetUp()
    {
        host = new GameObject("PickUp Host");
        host.SetActive(false);

        pickUpItems = host.AddComponent<PickUpItems>();

        onTriggerEnterMethod = typeof(PickUpItems).GetMethod(
            "OnTriggerEnter",
            BindingFlags.Instance | BindingFlags.NonPublic);

        onTriggerExitMethod = typeof(PickUpItems).GetMethod(
            "OnTriggerExit",
            BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(onTriggerEnterMethod);
        Assert.NotNull(onTriggerExitMethod);
    }

    [TearDown]
    public void TearDown()
    {
        if (host != null)
            Object.DestroyImmediate(host);
    }

    private Collider CreateColliderWithTag(string tagName)
    {
        if (!InternalEditorUtility.tags.Contains(tagName))
        {
            Assert.Fail($"Tag '{tagName}' does not exist in project. Add it in Tag Manager.");
        }

        GameObject other = new GameObject("Other");
        other.tag = tagName;
        return other.AddComponent<BoxCollider>();
    }

    [Test]
    public void OnTriggerEnter_SetsInReachTrue_AndShowsPickupText_WhenTagIsReach()
    {
        GameObject textObject = new GameObject("PickupText");
        textObject.SetActive(false);
        pickUpItems.pickupText = textObject;

        Collider collider = CreateColliderWithTag("Reach");

        onTriggerEnterMethod.Invoke(pickUpItems, new object[] { collider });

        Assert.IsTrue(pickUpItems.inReach);
        Assert.IsTrue(textObject.activeSelf);

        Object.DestroyImmediate(collider.gameObject);
        Object.DestroyImmediate(textObject);
    }

    [Test]
    public void OnTriggerExit_SetsInReachFalse_AndHidesPickupText_WhenTagIsReach()
    {
        GameObject textObject = new GameObject("PickupText");
        textObject.SetActive(true);
        pickUpItems.pickupText = textObject;
        pickUpItems.inReach = true;

        Collider collider = CreateColliderWithTag("Reach");

        onTriggerExitMethod.Invoke(pickUpItems, new object[] { collider });

        Assert.IsFalse(pickUpItems.inReach);
        Assert.IsFalse(textObject.activeSelf);

        Object.DestroyImmediate(collider.gameObject);
        Object.DestroyImmediate(textObject);
    }

    [Test]
    public void OnTriggerEnter_DoesNothing_WhenTagIsNotReach()
    {
        GameObject textObject = new GameObject("PickupText");
        textObject.SetActive(false);
        pickUpItems.pickupText = textObject;

        Collider collider = CreateColliderWithTag("Untagged");

        onTriggerEnterMethod.Invoke(pickUpItems, new object[] { collider });

        Assert.IsFalse(pickUpItems.inReach);
        Assert.IsFalse(textObject.activeSelf);

        Object.DestroyImmediate(collider.gameObject);
        Object.DestroyImmediate(textObject);
    }

    [Test]
    public void OnTriggerExit_DoesNothing_WhenTagIsNotReach()
    {
        GameObject textObject = new GameObject("PickupText");
        textObject.SetActive(true);
        pickUpItems.pickupText = textObject;
        pickUpItems.inReach = true;

        Collider collider = CreateColliderWithTag("Untagged");

        onTriggerExitMethod.Invoke(pickUpItems, new object[] { collider });

        Assert.IsTrue(pickUpItems.inReach);
        Assert.IsTrue(textObject.activeSelf);

        Object.DestroyImmediate(collider.gameObject);
        Object.DestroyImmediate(textObject);
    }

    [Test]
    public void OnTriggerEnter_DoesNotThrow_WhenPickupTextIsNull()
    {
        pickUpItems.pickupText = null;
        Collider collider = CreateColliderWithTag("Reach");

        Assert.DoesNotThrow(() =>
            onTriggerEnterMethod.Invoke(pickUpItems, new object[] { collider }));

        Assert.IsTrue(pickUpItems.inReach);

        Object.DestroyImmediate(collider.gameObject);
    }

    [Test]
    public void OnTriggerExit_DoesNotThrow_WhenPickupTextIsNull()
    {
        pickUpItems.pickupText = null;
        pickUpItems.inReach = true;
        Collider collider = CreateColliderWithTag("Reach");

        Assert.DoesNotThrow(() =>
            onTriggerExitMethod.Invoke(pickUpItems, new object[] { collider }));

        Assert.IsFalse(pickUpItems.inReach);

        Object.DestroyImmediate(collider.gameObject);
    }
}