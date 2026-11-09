using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Skeleton : MonoBehaviour
{
    public GameObject target;

    public IList<Vector3> kinematicChain;
    public int angleConstraint = 40;
    public int boneCounts = 25;
    public float boneLength = 8f;
    public int iterations = 10;

    void Start()
    {
        kinematicChain = new List<Vector3>();

        for (int i = 0; i < boneCounts + 1; i++)
        {
            Vector3 joint = new Vector3();
            joint = transform.position + Vector3.up * i * boneLength;
            kinematicChain.Add(joint);
        }
    }

    void Update()
    {
        for (int i = 0; i < iterations; i++)
        {
            kinematicChain[boneCounts] = target.transform.position;

            //backward
            for (int j = boneCounts - 1; j > 0; j--)
            {
                kinematicChain[j] = kinematicChain[j + 1] + (kinematicChain[j] - kinematicChain[j + 1]).normalized * boneLength;
            }

            // anchor/root
            kinematicChain[0] = transform.position;

            // forward
            for (int k = 1; k <= boneCounts; k++)
            {
                kinematicChain[k] = kinematicChain[k - 1] + (kinematicChain[k] - kinematicChain[k - 1]).normalized * boneLength;
            }

            //for (int f = 1;  f < boneCounts; f++)
            //{
            //    float angle = Vector3.Angle(kinematicChain[f] + kinematicChain[f - 1], kinematicChain[f] + kinematicChain[f + 1]);

            //    if (angle > angleConstraint)
            //    {
                    //kinematicChain[f + 1] = kinematicChain[f + 1] * ;
            //    }
            //}
        }
    }

    void OnDrawGizmos()
    {
        if (kinematicChain == null) return;

        for (int i = 0; i < kinematicChain.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(kinematicChain[i], 0.8f);
            if (i > 0)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(kinematicChain[i - 1], kinematicChain[i]);
            }
        }
    }
}
