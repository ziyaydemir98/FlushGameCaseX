﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System;

[CreateAssetMenu(fileName = "New Gem Type",menuName = "Gem")]

public class GemTypes : ScriptableObject
{/// <summary>
/// Gemlere buradan gerekli nitelikleri tanımlanıyor.
/// </summary>
    #region Variables
    [Header("Gem Properties")]
    public Material MaterialOfGem;
    public Mesh MeshOfGem;
    public string GemName;
    public Vector3 TargetScale;
    public float GrowTime;
    public float BeginPrice;
    #endregion

}
