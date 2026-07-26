using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ProjectileIntegrationTest
{

    [UnityTest]
    public IEnumerator ProjectilesIntegrationTest()
    {
        yield return SceneManager.LoadSceneAsync(1);
        yield return null;

        Boss boss = GameObject.FindFirstObjectByType<Boss>();
        Enemy enemy = GameObject.FindFirstObjectByType<Enemy>();
        Player player = GameObject.FindFirstObjectByType<Player>();
        int bossStartHP = boss.health;
        int enemyStartHP = enemy.health;
        yield return new WaitForSeconds(3);

        LevelController levelController = GameObject.FindFirstObjectByType<LevelController>();
        Assert.IsTrue(player == null);
        Assert.IsTrue(enemyStartHP > enemy.health);
        Assert.IsTrue(bossStartHP > boss.health);
    }
}
