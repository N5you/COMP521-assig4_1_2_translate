using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Game_Material //板条箱
{
    private Rigidbody rigid = null;
    public override void PickUp(RoleAttributes who)
    {
        isOccupied = true;
        Vector3 pos = new Vector3(who.transform.position.x, who.transform.position.y + transform.position.y, who.transform.position.z);
        transform.position = pos;
    }

    public override void Use(RoleAttributes who) //投射
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody>();
        }
        Vector3 force = who.transform.localEulerAngles * 25;
        rigid.AddForce(force);
        Debug.Log("扔出去啦，力度：" + force);
    }

    public override bool IsOccupied()
    {
        return isOccupied;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isOccupied.Equals(false)) return;
        if (collision.gameObject.tag.Equals("GameController")) return;
        Debug.Log(collision.gameObject.tag.Equals("GameController"));
        isOccupied = false;
        RoleAttributes roleAttributes = null;
        roleAttributes = collision.gameObject.GetComponent<RoleAttributes>();
        if (roleAttributes != null)
        {
            roleAttributes.Injured(100);
        }
        hp -= 50;
        if (hp <= 0) GameObject.Destroy(this.gameObject);
    }
}
