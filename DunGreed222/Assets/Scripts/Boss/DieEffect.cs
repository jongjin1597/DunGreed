using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : MonoBehaviour
{
    public List<Animator> _Effect = new List<Animator>();

    private void Awake()
    {
        for(int i =0; i < transform.childCount; ++i)
        {
            _Effect.Add(transform.GetChild(i).GetComponent<Animator>());
        }
    }

    public void Die()
    {
        StartCoroutine(Effect());
    }
    IEnumerator Effect()
    {
        for (int i = 0; i < _Effect.Count; ++i)
        {
            yield return new WaitForSeconds(0.2f);
            _Effect[i].SetTrigger("Die");
        }
    }

}
