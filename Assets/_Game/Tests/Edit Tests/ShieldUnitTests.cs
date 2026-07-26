using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShieldUnitTests
{

    [Test]
    public void ShieldActivateTest()
    {
        GameObject enemyGO = new GameObject();
        Enemy enemy = enemyGO.AddComponent<Enemy>();
        enemy.ActivateShield();
        Assert.IsTrue(enemy.IsShielded);
    }

    [Test]
    public void ShieldBlockTest()
    {
        GameObject enemyGO = new GameObject();
        Enemy enemy = enemyGO.AddComponent<Enemy>();
        enemy.ActivateShield();
        enemy.health = 100;
        int enemyStartHealth = enemy.health;
        enemy.GetDamage(1);
        Assert.IsTrue(enemyStartHealth == enemy.health);
        Assert.IsTrue(!enemy.IsShielded);
    }
}
