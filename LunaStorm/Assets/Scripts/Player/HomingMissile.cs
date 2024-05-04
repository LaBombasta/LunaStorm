using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public Vector3 missileSpawnOffset = new Vector3(0f, 0f, 2f);
    public float missileSpeed = 20f;

    public void FireMissile()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (turrets.Length > 0)
        {
            GameObject visibleTurret = FindVisibleTarget(turrets);
            if (visibleTurret != null)
            {
                LaunchMissile(visibleTurret.transform);
                return;
            }
        }

        if (enemies.Length > 0)
        {
            GameObject visibleEnemy = FindVisibleTarget(enemies);
            if (visibleEnemy != null)
            {
                LaunchMissile(visibleEnemy.transform);
                return;
            }
        }

        // No visible targets, shoot straight
        GameObject missile = Instantiate(missilePrefab, transform.position + missileSpawnOffset, Quaternion.identity);
        Vector3 direction = transform.forward; // Get the direction the missile should travel
        StartCoroutine(MoveMissileStraight(missile, direction)); // Start moving the missile straight
        Destroy(missile, 5f); // Destroy after 5 seconds if it doesn't hit anything

    }

    GameObject FindVisibleTarget(GameObject[] targets)
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            if (IsObjectVisible(target))
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }

        return closestTarget;
    }


    bool IsObjectVisible(GameObject obj)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1 && screenPoint.z > 0;
    }

    void LaunchMissile(Transform target)
    {
        GameObject missile = Instantiate(missilePrefab, transform.position + missileSpawnOffset, Quaternion.identity);
        missile.transform.LookAt(target);
        StartCoroutine(Homing(missile, target));
    }

    public IEnumerator Homing(GameObject missile, Transform target)
    {
        while (missile != null)
        {
            if (target != null)
            {
                if (Vector3.Distance(target.position, missile.transform.position) > 0.3f)
                {
                    Vector3 direction = (target.position - missile.transform.position).normalized;
                    missile.transform.position += direction * missileSpeed * Time.deltaTime;
                    missile.transform.LookAt(target);
                }
                else
                {
                    if (target.CompareTag("Turret") || target.CompareTag("Enemy"))
                    {
                        Destroy(target.gameObject);
                    }
                    target = null;
                }
            }
            else
            {
                missile.transform.position += missile.transform.forward * missileSpeed * Time.deltaTime;
            }

            yield return null;
        }

        Destroy(missile);
    }

    IEnumerator MoveMissileStraight(GameObject missile, Vector3 direction)
    {
        while (missile != null)
        {
            missile.transform.position += direction * missileSpeed * Time.deltaTime;
            yield return null;
        }
    }


}
