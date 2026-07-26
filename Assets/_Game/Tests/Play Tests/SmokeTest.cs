using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SmokeTest
{
    [UnityTest]
    public IEnumerator SmokeTests()
    {
        yield return SceneManager.LoadSceneAsync(0);
        yield return null;

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(playerGO);

        Assert.IsTrue(playerGO.TryGetComponent<Player>(out _));
        Assert.IsTrue(playerGO.TryGetComponent<PlayerShooting>(out _));
        Assert.IsTrue(playerGO.TryGetComponent<PlayerMoving>(out _));

        LevelController levelController = GameObject.FindFirstObjectByType<LevelController>();
        Assert.IsTrue(levelController != null);
        Assert.IsTrue(levelController.levels != null);
    }
}
