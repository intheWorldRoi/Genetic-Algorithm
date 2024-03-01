using UnityEngine;
using System.Collections;

public class HumanBehaviour : MonoBehaviour
{
    [SerializeField]
    private FixedJoint leftArm;
    public FixedJoint LeftArm { get { return leftArm; } }

    [SerializeField]
    private FixedJoint rightArm;
    public FixedJoint RightArm { get { return rightArm; } }

    [SerializeField]
    private FixedJoint leftLeg;
    public FixedJoint LeftLeg { get { return leftLeg; } }

    [SerializeField]
    private FixedJoint rightLeg;
    public FixedJoint RightLeg { get { return rightLeg; } }

    [SerializeField]
    private FixedJoint leftHand;
    public FixedJoint LeftHand { get { return leftHand; } }

    [SerializeField]
    private FixedJoint rightHand;
    public FixedJoint RightHand { get { return rightHand; } }

    [SerializeField]
    private FixedJoint leftFoot;
    public FixedJoint LeftFoot { get { return leftFoot; } }

    [SerializeField]
    private FixedJoint rightFoot;
    public FixedJoint RightFoot { get { return rightFoot; } }

    [SerializeField]
    private Rigidbody chest;

    [SerializeField]
    private float downForce;
    [SerializeField]
    private float upForce;

    private Transform chair;

    public void Init(Transform chair)
    {
        this.chair = chair;
    }

    public void Up()
    {
        var dir = chest.transform.position - chair.position;
        Act(Vector3.up, upForce);
        Debug.Log("Up");
    }

    public void Down()
    {
        var dir = chest.transform.position - chair.position;
        Act(dir, downForce);
        Debug.Log("Down");
    }

    private void Act(Vector3 dir, float force)
    {
        chest.velocity = Vector3.zero;
        
        chest.AddForce(dir * force);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            Up();
        else if (Input.GetKey(KeyCode.D))
            Down();
    }
}