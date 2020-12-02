using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPmovement : MonoBehaviour
{
    private Transform characterTransform;
    private Rigidbody characterRigidbody;
    public float JumpHeight = 1f;
    public float Speed = 1f;
    public float gravity;
    private bool isGrounded = true;
    //[FormerlySerializedAs("grative")]public float gravity;
    void Start()
    {
        characterTransform = transform;
        characterRigidbody = GetComponent<Rigidbody>();
        offs = characterTransform.transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        Spin();
        if (isGrounded)
        {
            var tmp_Horizontal = Input.GetAxis("Horizontal");
            var tmp_Vertical = Input.GetAxis("Vertical");
            var tmp_CurrentDirection = new Vector3(tmp_Horizontal, 0, tmp_Vertical);
            //自身坐标转世界坐标
            tmp_CurrentDirection = characterTransform.TransformDirection(tmp_CurrentDirection);
            tmp_CurrentDirection *= Speed;

            var tmp_CurrentVelocity = characterRigidbody.velocity;
            var tmp_VelocityChange = tmp_CurrentDirection - tmp_CurrentVelocity;

            tmp_VelocityChange.y = 0;
            characterRigidbody.AddForce(tmp_VelocityChange, ForceMode.VelocityChange);

            //if (Input.GetButtonDown("Jump"))
            //{
            //    characterRigidbody.velocity = new Vector3(tmp_CurrentVelocity.x, CalculateJumpHeightSpeed(), tmp_CurrentVelocity.z);
            //}
        }
        //竖直y方向只受到了重力 -gravity*characterRigidbody.mass
        characterRigidbody.AddForce(new Vector3(0, -gravity * characterRigidbody.mass, 0));
    }

    private Vector2 offs;
    private void Spin()
    {
        offs += new Vector2(0, Input.GetAxis("Mouse X")) * Speed;   //摄像机旋转的控制 Input.GetAxis("Mouse Y")
        characterTransform.transform.eulerAngles = offs;
    }

    private float CalculateJumpHeightSpeed()
    {
        return Mathf.Sqrt(2 * gravity * JumpHeight);    //物理动能定理
    }

}
