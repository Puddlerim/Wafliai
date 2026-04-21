using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

public class FirstPersonAudioEditModeTests
{
    private GameObject host;
    private FirstPersonAudio audioComponent;

    [SetUp]
    public void SetUp()
    {
        host = new GameObject("Audio Host");
        host.SetActive(false);
        audioComponent = host.AddComponent<FirstPersonAudio>();
    }

    [TearDown]
    public void TearDown()
    {
        if (host != null)
        {
            Object.DestroyImmediate(host);
        }
    }

    [Test]
    public void GetOrCreateAudioSource_CreatesNewChildAudioSource_WhenMissing()
    {
        MethodInfo method = typeof(FirstPersonAudio).GetMethod(
            "GetOrCreateAudioSource",
            BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(method);

        AudioSource result = (AudioSource)method.Invoke(audioComponent, new object[] { "Step Audio" });

        Assert.NotNull(result);
        Assert.AreEqual("Step Audio", result.name);
        Assert.AreEqual(host.transform, result.transform.parent);
        Assert.AreEqual(1f, result.spatialBlend);
        Assert.IsFalse(result.playOnAwake);
    }

    [Test]
    public void GetOrCreateAudioSource_ReturnsExistingAudioSource_WhenAlreadyExists()
    {
        GameObject existingObject = new GameObject("Landing Audio");
        existingObject.transform.SetParent(host.transform, false);
        AudioSource existingAudioSource = existingObject.AddComponent<AudioSource>();

        MethodInfo method = typeof(FirstPersonAudio).GetMethod(
            "GetOrCreateAudioSource",
            BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(method);

        AudioSource result = (AudioSource)method.Invoke(audioComponent, new object[] { "Landing Audio" });

        Assert.AreSame(existingAudioSource, result);
    }

    [Test]
    public void PlayRandomClip_DoesNothing_WhenAudioSourceIsNull()
    {
        MethodInfo method = typeof(FirstPersonAudio).GetMethod(
            "PlayRandomClip",
            BindingFlags.Static | BindingFlags.NonPublic);

        Assert.NotNull(method);

        AudioClip clip = AudioClip.Create("clip", 128, 1, 44100, false);

        Assert.DoesNotThrow(() =>
            method.Invoke(null, new object[] { null, new AudioClip[] { clip } }));
    }

    [Test]
    public void PlayRandomClip_DoesNothing_WhenClipArrayIsEmpty()
    {
        AudioSource source = host.AddComponent<AudioSource>();

        MethodInfo method = typeof(FirstPersonAudio).GetMethod(
            "PlayRandomClip",
            BindingFlags.Static | BindingFlags.NonPublic);

        Assert.NotNull(method);

        Assert.DoesNotThrow(() =>
            method.Invoke(null, new object[] { source, new AudioClip[0] }));

        Assert.IsNull(source.clip);
    }

    [Test]
    public void PlayRandomClip_AssignsOneOfProvidedClips()
    {
        AudioSource source = host.AddComponent<AudioSource>();
        AudioClip clipA = AudioClip.Create("clipA", 128, 1, 44100, false);
        AudioClip clipB = AudioClip.Create("clipB", 128, 1, 44100, false);

        MethodInfo method = typeof(FirstPersonAudio).GetMethod(
            "PlayRandomClip",
            BindingFlags.Static | BindingFlags.NonPublic);

        Assert.NotNull(method);

        method.Invoke(null, new object[] { source, new AudioClip[] { clipA, clipB } });

        Assert.IsTrue(source.clip == clipA || source.clip == clipB);
    }
}