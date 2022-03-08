using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Gun : Item
{
    public abstract override void Use();
    
    public GameObject bulletImpactPrefab;
    public int currentAmmo;
    public int maxAmmo;
    [HideInInspector] public float nextTimeToFire;
    public float fireRate;
    public int reloadTime;
    [HideInInspector] public float timeToReload;
    [HideInInspector] public bool isReloading = false;
    public AudioClip gunShot;
    public TMP_Text ammoText;
}
