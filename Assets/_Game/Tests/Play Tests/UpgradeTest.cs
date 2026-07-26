using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UpgradeTest
{

    [UnityTest]
    public IEnumerator UpgradeTestWithEnumeratorPasses()
    {
        GameObject playerGO = new GameObject();
        playerGO.AddComponent<PlayerShooting>();

        GameObject upgradeGO = new GameObject();
        Bonus bonus = upgradeGO.AddComponent<Bonus>();
        yield return null;

        int originalWeaponPower = PlayerShooting.instance.weaponPower;

        bonus.PickUp();

        yield return null;
        Assert.True(originalWeaponPower < PlayerShooting.instance.weaponPower);
        Assert.True(upgradeGO == null);
    }
}
