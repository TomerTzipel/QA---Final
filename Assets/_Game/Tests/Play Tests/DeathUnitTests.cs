using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DeathUnitTests
{
    [UnityTest]
    public IEnumerator PlayerDeathTest()
    {
        GameObject playerGO = new GameObject();
        Player player = playerGO.AddComponent<Player>();
        player.GetDamage(1);
        yield return null;
        Assert.True(playerGO == null);
    }

    [UnityTest]
    public IEnumerator BossDeathTest()
    {
        GameObject bossGO = new GameObject();
        Boss boss = bossGO.AddComponent<Boss>();
        boss.GetDamage(boss.health);
        yield return null;
        Assert.True(bossGO == null);
    }

    [UnityTest]
    public IEnumerator EnemyDeathTest()
    {
        GameObject enemyGO = new GameObject();
        Enemy enemy = enemyGO.AddComponent<Enemy>();
        enemy.GetDamage(enemy.health);
        yield return null;
        Assert.True(enemyGO == null);
    }
}
