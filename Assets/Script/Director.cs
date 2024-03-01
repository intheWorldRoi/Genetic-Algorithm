using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField]
    private SwingBehaviour swingPrefab;
    [SerializeField]
    private int swingRowCount;
    [SerializeField]
    private float distanceBetweenSwing;
    [SerializeField]
    private float generationChangeTime;

    private int SwingCount { get { return swingRowCount * swingRowCount; } }

    private List<SwingBehaviour> swings = new List<SwingBehaviour>();
    private List<uint> genes = new List<uint>();

    private void Awake()
    {
        InitSwings();
        StartCoroutine(MainRoutine());
    }

    private void Update()
    {
        Debug.Log("genes Count : " + genes.Count);
    }
    private void InitSwings()
    {
        for (int i = 0; i < SwingCount; i++)
        {
            var posX = distanceBetweenSwing * (i / swingRowCount);
            var posZ = distanceBetweenSwing * (i % swingRowCount);

            var swingObject = Instantiate(swingPrefab, this.transform);
            swingObject.gameObject.SetActive(false);
            swingObject.transform.position = new Vector3(posX, 0, posZ);

            swings.Add(swingObject);
            genes.Add((uint)Random.Range(uint.MinValue, uint.MaxValue));
        }
    }

    private IEnumerator MainRoutine()
    {
        while (true)
        {
            foreach (var swing in swings)
                swing.gameObject.SetActive(true);

            for (int i = 0; i < swings.Count; i++)
            {
                swings[i].StartSwing(genes[i]);
                Debug.Log(genes[i]);
            }

            

            yield return new WaitForSeconds(generationChangeTime);

            UpdateGenes();

            foreach (var swing in swings)
                swing.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateGenes()
    {
        genes.Clear();


        genes.AddRange(Selection(swings.Count / 5));
        genes.AddRange(Crossover(genes));
        Mutation(genes);
    }

    private List<uint> Selection(int selectCount)
    {
        List<uint> selectedGenes = new List<uint>();

        swings.Sort(delegate (SwingBehaviour a, SwingBehaviour b)
        {
            return a.SwingMaxHeight.CompareTo(b.SwingMaxHeight);
        });

        for (int i = 0; i < selectCount; i++)
            selectedGenes.Add(swings[i].Gene);

        return selectedGenes;
    }

    private List<uint> Crossover(List<uint> genes)
    {
        List<uint> crossoveredGenes = new List<uint>();

        for (int i = 0; i < swings.Count - genes.Count; i++) //swings.Count - genes.Count
        {
            uint gene1 = genes[Random.Range(0, genes.Count - 1)];
            uint gene2 = genes[Random.Range(0, genes.Count - 1)];
            int crossPoint = Random.Range(0, 10);

            uint mask = ~(4294967295 ^ ((uint)Mathf.Pow(2, crossPoint) - 1));
            gene1 &= mask;
            gene2 &= ~((uint)Mathf.Pow(2, crossPoint) - 1);

            var newGene = gene1 | gene2;
            crossoveredGenes.Add(newGene);
        }

        return crossoveredGenes;
    }

    private void Mutation(List<uint> genes)
    {
        for (int i = 0; i < genes.Count / 10; i++)
        {
            if (Random.Range(0, 1) > 0.5f)
            {
                genes[genes.Count - 1 - i] = (uint)Random.Range(uint.MinValue, uint.MaxValue);
                Debug.Log(genes[genes.Count - 1 - i]);
            }
               
        }
    }
}