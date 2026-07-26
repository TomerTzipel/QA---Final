using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DamageUnitTests
{
    
    [Test]
    public void EnemyDamageTest()
    {
        GameObject enemyGO = new GameObject();
        Enemy enemy = enemyGO.AddComponent<Enemy>();
        enemy.health = 100;
        int enemyStartHealth = enemy.health;
        enemy.GetDamage(1);
        Assert.IsTrue(enemyStartHealth > enemy.health);
    }
    [Test]
    public void BossDamageTest()
    {
        GameObject bossGo = new GameObject();
        Boss boss = bossGo.AddComponent<Boss>();
        boss.health = 100;
        int bossStartHealth = boss.health;
        boss.GetDamage(1);
        Assert.IsTrue(bossStartHealth > boss.health);
    }
}
