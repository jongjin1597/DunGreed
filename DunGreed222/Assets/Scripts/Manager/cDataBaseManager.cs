using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDataBaseManager : cSingleton<cDataBaseManager>
{

    //아이템 리스트
    public  List<Item> _ItemList = new List<Item>();
    //음식 리스트
    public  List<cFood> _FoodList = new List<cFood>();
    protected override void Awake()
    {
        base.Awake();
        //검
        _ItemList.Add(new cShortSward(Resources.Load<Sprite>("Itemp/BasicShortSword_New")));
        _ItemList.Add(new Gladius(Resources.Load<Sprite>("Itemp/Gladius")));
        _ItemList.Add(new LightSaver(Resources.Load<Sprite>("Itemp/LightSaber")));
        //창
        _ItemList.Add(new Halberd(Resources.Load<Sprite>("Itemp/Halberd")));
        _ItemList.Add(new Berdysh(Resources.Load<Sprite>("Itemp/Berdysh")));
        _ItemList.Add(new Gwendolyn(Resources.Load<Sprite>("Itemp/Gwendolyn")));

        //음식 
        _FoodList.Add(new cFood("계란후라이", "위력", 10.0f, "최대 체력", 8, 60, 450, 6, Resources.Load<Sprite>("UI/food/02_FriedEgg")));
        _FoodList.Add(new cFood("디럭스 버거", "위력", 5.0f, "방어력", 4, 55, 340, 8, Resources.Load<Sprite>("UI/food/09_DeluxeBurger")));
        _FoodList.Add(new cFood("매운 소스 미트볼", "위력", 20.0f, " ", 0, 40, 500, 10, Resources.Load<Sprite>("UI/food/98_HotMeatball")));
        _FoodList.Add(new cFood("초콜릿 쿠키", "최대 체력", 8.0f, "방어력", 5, 43, 400, 4, Resources.Load<Sprite>("UI/food/10_ChocolateCookie")));
        _FoodList.Add(new cFood("야채 살사 수프", "방어력", 20.0f, " ", 0, 60, 550, 6, Resources.Load<Sprite>("UI/food/07_VegetableSalsaSoup")));
        _FoodList.Add(new cFood("양파 수프", "최대 체력", 20.0f, " ", 0, 70, 520, 10, Resources.Load<Sprite>("UI/food/05_OnionSoup")));
        //_FoodList.Add(new cFood("계란후라이", "위력", 10.0f, "최대 체력", 8, 60, 450, 6, Resources.Load<Sprite>("UI/food/02_FriedEgg")));
        //_FoodList.Add(new cFood("계란후라이", "위력", 10.0f, "최대 체력", 8, 60, 450, 6, Resources.Load<Sprite>("UI/food/02_FriedEgg")));
        //_FoodList.Add(new cFood("계란후라이", "위력", 10.0f, "최대 체력", 8, 60, 450, 6, Resources.Load<Sprite>("UI/food/02_FriedEgg")));

    }

}
