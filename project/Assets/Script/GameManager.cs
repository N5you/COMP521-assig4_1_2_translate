using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
从玩家那里守卫区域是一个巨大的洞穴怪物，当玩家进入该区域时，从洞穴中出现。
怪物由基于HTN 的人工智能控制，具有以下可能的行为，每一个行为都在4 之后进行1s 冷却，然后再进行另一个动作。
• 至少一个空闲行为，包括在位置之间移动。
• 至少两种形式的距离攻击，其中一种必须涉及使用岩石作为射弹，其中一种必须涉及使用板条箱。所有的弹丸如果
2
击中玩家就会消失；没有击中玩家的石头是可重复使用的，但板条箱会被任何使用破坏。
 */
public class GameManager : MonoBehaviour //游戏管理器
{

}


public class Game_Material:MonoBehaviour  //游戏物品
{
    protected int hp = 200;
    [SerializeField] protected bool isOccupied = false;

    public virtual bool IsOccupied()
    {
        return isOccupied;
    }
    public virtual void PickUp(RoleAttributes who) { } //捡起
    public virtual void Use(RoleAttributes who) { } //使用

}

