using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    三、在单独的文档中描述您使用的世界状态向量，指示每个值的含义及其可能的类型。绘制 HTN 树并指示任何预/后条件。

    当前正在遵循的计划应该被直观地显示在所有times 一the 有序的计划步骤序列应该是明确的，以及怪物目前正在执行的步骤。

    请注意，这些要求需要一些创造性的设计。
    把它当作一个建立战斗场景的问题，在这个场景中，怪物通常试图阻止玩家获取宝藏，提供有意义的战斗，因为玩家通常可以赢得，如果他们试图这样做，但这不是微不足道的。
    玩家应该有可能观察所有的行为（也许在不止一个游戏中）。

    4. 老鼠必须完全由转向力控制。他们通常应该在地形上闲逛，偶尔也会4短暂的停顿。
    老鼠不得碰撞岩石或板条箱，或进入洞穴或玩家入口。小鼠应避免与之发生碰撞4彼此，玩家和洞穴怪物。被弹丸击中的老鼠或被洞穴怪物踩到的老鼠应该从游戏中移除。
    
    注意：对于鼠标移动，如果您希望您可以将您的实现基于（而不是逐字复制)现有的转向行为实现(仅）。如果是这样的话，源必须是公开可用的，并且您必须记录/信任源-如果不这样做，将导致这个问题的0。
*/

public class HTN_AI : RoleAttributes //设计一个HTN 树，使所有怪物行为。每个行为应包括至少一个具有多个方法的复合任务
{
    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        BehavioralDecision();
    }

    private IEnumerator Behavioral()
    {
        yield return new WaitForSeconds(1f);
        isFree = true;
    }

    private int layerMask;
    private float vigilanceRange = 3f; //警觉范围

    private RoleAttributes target = null;
    private void BehavioralDecision() //行为决定
    {
        if (!isFree) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, vigilanceRange, layerMask); //获取周围（范围内）敌人
        if (colliders.Length > 0) //
        {
            RoleAttributes roleAttributes = null;
            for (int index = 0; index < colliders.Length; index++)
            {
                roleAttributes = colliders[index].GetComponent<RoleAttributes>();
                if (roleAttributes != null)
                {
                    target = roleAttributes;
                    AggressiveBehavior();
                    return;
                }
            }
        }
        LdleBehavior(); //没有敌人，进入休闲状态
    }

    //从玩家那里守卫区域是一个巨大的洞穴怪物，当玩家进入该区域时，从洞穴中出现。
    //怪物由基于 HTN 的人工智能控制，具有以下可能的行为：
    private bool isFree = true; //（每一个行为都在4 之后进行1s 冷却，然后再进行另一个动作。）
    private void EndBehavior() //行为结束
    {
        isFree = false;
        StartCoroutine(Behavioral());
    }

    //1.至少一个空闲行为，包括在位置之间移动。
    private void LdleBehavior() 
    {
        //不移动
        //巡逻
        Debug.Log("空闲");
        EndBehavior();
    }

    private Game_Material arms = null;
    //2.至少两种形式的距离攻击，其中一种必须涉及使用岩石作为射弹，其中一种必须涉及使用板条箱。所有的弹丸如果击中玩家就会消失；没有击中玩家的石头是可重复使用的，但板条箱会被任何使用破坏。
    private void AggressiveBehavior()
    {
        if (target == null) return;//是否有攻击目标，目标是否在范围内
        float dis = Vector3.Distance(transform.position, target.transform.position);
        if (dis > vigilanceRange)
        {
            target = null;
            return;
        }

        Debug.Log(gameObject.name + "攻击目标：" + target);
        transform.LookAt(target.transform.position - transform.position);
        if (arms == null) //是否有物品
        {
            Game_Material game_Material = null;
            Collider[] colliders = Physics.OverlapSphere(transform.position, vigilanceRange);//获取周围可用的物品
            for (int index = 0; index < colliders.Length; index++)
            {
                game_Material = colliders[index].gameObject.GetComponent<Game_Material>();
                if (game_Material != null && (!game_Material.IsOccupied()))
                {
                    arms = game_Material;
                    Debug.Log(gameObject.name + "——找到了武器：" + game_Material);
                    arms.PickUp(this); //捡起
                    EndBehavior();
                    return;
                }
            }
            Debug.Log("没有攻击物品：" + colliders.Length);
            EndBehavior();
            return;
        }
        Debug.Log("攻击物品成功扔出");
        arms.Use(this); //捡岩石攻击 or //拿起板条箱攻击
        arms = null;
        EndBehavior();
    }

    //3.您的怪物AI 应该基于一个前向HTN 计划，如课堂上所描述的。确保所有必要的原始 5 操作都是可能的。
}
