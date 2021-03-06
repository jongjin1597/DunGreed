﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cSkill : MonoBehaviour
{
    private Image _Slot;
    private Image _SkillImage;
    private Image _Qbutton;
    private Image _CoolTime;
    private void Start()
    {
        _Slot = GetComponent<Image>();
        _SkillImage = transform.GetChild(0).GetComponent<Image>();
        _Qbutton = transform.GetChild(1).GetComponent<Image>();
        _SkillImage.sprite = cInventory.GetInstance.GetWeaponSlot(0)._item._SkillIcon;
        _CoolTime = transform.GetChild(2).GetComponent<Image>();
        SetImage(cInventory.GetInstance.GetWeaponSlot(0)._item);
    }
    public void SetImage(Item item)
    {
        if(item._SkillIcon == null)
        {
          _Slot.color = new Color(1, 1, 1, 0);
            _SkillImage.color = new Color(1, 1, 1, 0);
            _Qbutton.color = new Color(1, 1, 1, 0);
            _CoolTime.color = new Color(1, 1, 1, 0);
        }
        else if (item._SkillIcon != null)
        {
            _Slot.color = new Color(1, 1, 1, 1);
            _SkillImage.color = new Color(1, 1, 1, 1);
            _Qbutton.color = new Color(1, 1, 1, 1);
     
            _SkillImage.sprite = item._SkillIcon;
        }
    }

}
