using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Player's guns.
/// Controls all guns, including player's support units.
/// 
/// Currently script is modified for player specificaly.
/// </summary>
public class Guns : MonoBehaviour {

    /// <summary>
    /// Parent for bullets
    /// </summary>
    static Transform allBulletsHolder;

    bool nextFire = false;

    float fireTime = 0;
    public float fireRate = 0.1f;

    // An empty object, where to shoot from
    public Transform activeGun;

    // What to fire
    public Transform activeBulletPrefab;

    // Different bullet types, regular, explosive, and weaker for support units
    public Transform bulletPrefab_small;
    public Transform bulletPrefab_big;
    public Transform bulletPrefab_supportUnits;

    // Guns on supporting units
    public Guns[] supportGuns;
    public bool gunEnabled = true;

    void Awake() {
        if (allBulletsHolder == null) {
            allBulletsHolder = new GameObject("AllBulletsHolder").transform;
        }
    }

    void Update() {
        if (!gunEnabled) return;

        if (nextFire && Time.time >= fireTime) {
            fireTime = Time.time + fireRate;
            nextFire = false;
            FireGun();
        }
    }

    private void FireGun() {
        Transform bullet = Instantiate(activeBulletPrefab);
        bullet.position = activeGun.position;
        bullet.rotation = activeGun.rotation;

        // assign bullet to empty so we don't have a mess in inspector
        bullet.parent = allBulletsHolder;
    }

    public void Fire () {
        nextFire = true;
	}

    internal void SmallerBullets() {
        activeBulletPrefab = bulletPrefab_small;
    }

    internal void BiggerBullets() {
        activeBulletPrefab = bulletPrefab_big;
    }

    internal void SupportFire(bool supportEnabled) {
        for (int i = 0; i < supportGuns.Length; i++) {
            supportGuns[i].gunEnabled = supportEnabled;
        }
    }
}
