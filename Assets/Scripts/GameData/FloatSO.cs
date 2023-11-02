using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;


[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    [SerializeField]
    private float moveSpeed;
    private int live;
    private int radius;
    private int bomb;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int Live { get => live; set => live = value; }
    public int Radius { get => radius; set => radius = value; }
    public int Bomb { get => bomb; set => bomb = value; }
}

