using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class FirePowerup : Powerup
{

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
    }

    /// <summary>
    /// Launches rockets for enemies (number of rockets are equal to number of enemies).
    /// </summary>
    private void LaunchRockets()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            GameObject tmpRocket = Instantiate(player.bulletPrefab, player.transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<Bullet>().Fire(enemy.transform);
        }
    }
}
