using UnityEngine;

public class IKFKDemo : MonoBehaviour
{
    public Transform target;
    public Transform hand;
    public Transform elbow;

    public bool useIK = true;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (useIK)
        {
            // Activate IK
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, target.position);
            animator.SetIKPosition(AvatarIKGoal.RightHand, target.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, target.rotation);
            animator.SetIKRotation(AvatarIKGoal.RightHand, target.rotation);
        }
        else
        {
            // Deactivate IK
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);

            // Update FK
            hand.position = target.position;
            hand.rotation = target.rotation;

            Vector3 direction = target.position - elbow.position;
            elbow.rotation = Quaternion.LookRotation(direction, transform.up);
        }
    }
}
