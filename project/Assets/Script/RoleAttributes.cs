using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
玩家将需要一个防御值（最初10）表示盾牌resource 一if 防御值为>0， 3
按空格键切换屏蔽的激活，然后以1/s 的速率降低防御值，如果切换或一旦防御值达到0，则关闭屏蔽。应该有一些可见
的表示屏蔽是活动的和防御值。
一旦玩家越过宝藏并返回入口通道，或成功击中两次（见下面1)，模拟应该停止(游戏结束）。
 */
public class RoleAttributes : MonoBehaviour //角色属性
{
    protected int hp;
    protected virtual void Mobile() //移动
    {
    }

    public virtual void Injured(int hurt) //受伤
    {
        hp -= hurt;
    }
}
