using UnityEngine;
using System.Collections;
using System;

public class SwingBehaviour : MonoBehaviour
{
    [SerializeField]
    private HumanBehaviour humanPrefab;
    [SerializeField]
    private Transform swingChair;
    [SerializeField]
    private Rigidbody leftBar;
    [SerializeField]
    private Rigidbody rightBar;
    [SerializeField]
    private Rigidbody chair;

    [SerializeField]
    private float priod;

    private Coroutine swingRoutine;
    private HumanBehaviour human;

    public float SwingMaxHeight { get; private set; }
    public uint Gene { get; private set; }

    private void Awake()
    {
        Init();
        StartCoroutine(CheckMaxHeight());
    }

    private void Init()
    {
        human = Instantiate(humanPrefab, this.transform);
        human.LeftHand.connectedBody = leftBar;
        human.LeftArm.connectedBody = leftBar;
        human.LeftLeg.connectedBody = leftBar;
        human.LeftFoot.connectedBody = chair;

        human.RightHand.connectedBody = rightBar;
        human.RightArm.connectedBody = rightBar;
        human.RightLeg.connectedBody = rightBar;
        human.RightFoot.connectedBody = chair;

        human.Init(chair.transform);
    }

    private IEnumerator CheckMaxHeight()
    {
        SwingMaxHeight = 0;

        while (true)
        {
            if (swingChair.position.y > SwingMaxHeight)
                SwingMaxHeight = swingChair.position.y;

            yield return null;
        }
    }

    public void StartSwing(uint gene)
    {
        Gene = gene;

        StopSwing();
        swingRoutine = StartCoroutine(SwingRoutine(Gene));
    }

    private IEnumerator SwingRoutine(uint gene)
    {
        
        BitArray bitArray = new BitArray(BitConverter.GetBytes(gene));
        
        bool[] genes = new bool[bitArray.Count];
        bitArray.CopyTo(genes, 0);

        while (true)
        {
            foreach (var bit in genes)
            {
                if (bit)
                    human.Up();
                else
                    human.Down();

                yield return new WaitForSeconds(priod);
            }

            yield return null;
        }
    }

    public void StopSwing()
    {
        if (swingRoutine != null)
            StopCoroutine(swingRoutine);
    }
}