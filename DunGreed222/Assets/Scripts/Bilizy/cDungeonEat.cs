using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDungeonEat : MonoBehaviour
{
    SpriteRenderer _Renderer;

    private void Awake()
    {
        _Renderer = GetComponent<SpriteRenderer>();
    }
    void EatPlayer()
    {
        _Renderer.sortingOrder = 10;
        Player.GetInstance.gameObject.SetActive(false);
    }
    void NextScene()
    {
        cLoading.LoadScene("Play");
    }
}
