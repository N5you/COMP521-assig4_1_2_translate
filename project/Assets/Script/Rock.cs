using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Game_Material //岩石
{
    private Rigidbody rigid = null;
    public override void PickUp(RoleAttributes who)
    {
        isOccupied = true;
        Vector3 pos = new Vector3(who.transform.position.x, who.transform.position.y + transform.position.y, who.transform.position.z);
        transform.position = pos;
    }

    public override bool IsOccupied()
    {
        return isOccupied;
    }

    public override void Use(RoleAttributes who)
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody>();
        }
        Vector3 force = who.transform.localEulerAngles * 25;
        rigid.AddForce(force);
        Debug.Log("扔出去啦，力度：" + force);
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
    }
}
