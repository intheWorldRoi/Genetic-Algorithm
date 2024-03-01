using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public Dictionary<bool, float> genes;
    int dnaLength;

    public DNA()
    {
        genes = new Dictionary<bool, float>();
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        genes.Add(false, Random.Range(-90, 91)); //블록이 앞에 없을 때 회전할 각도를 key값을 false로 해놓고 float값은 랜덤하게 설정(우리가 원하는 값이 아닌)
        genes.Add(true, Random.Range(-90, 91)); //블록이 앞에 있을 때 회전할 각도를 key값을 true로 해놓고 float값은 랜덤하게 설정(우리가 원하는 값이 아닌)

        dnaLength = genes.Count;
    }

    public void Combine(DNA d1, DNA d2)
    {
        int i = 0;
        Dictionary<bool, float> newGenes = new Dictionary<bool, float>();
        
        foreach(KeyValuePair<bool, float> g in genes)
        {
            if(i < dnaLength / 2)
            {
                newGenes.Add(g.Key, d1.genes[g.Key]);
            }
            else
            {
                newGenes.Add(g.Key, d2.genes[g.Key]);
            }
            i++;
        }
        genes = newGenes;
    }

    public float GetGene(bool front)
    {
        return genes[front];
    }

}
