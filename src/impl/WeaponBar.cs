using Godot;
using System;
using System.Collections.Generic;

public partial class WeaponBar : Container
{
    Dictionary<Weapon, WeaponSlot> weaponPanels = new Dictionary<Weapon, WeaponSlot>();
    Weapon activeWeapon;
    HBoxContainer hBoxContainer;

    public override void _Ready()
    {
        this.hBoxContainer = GetNode<HBoxContainer>("MarginContainer/HBoxContainer");
    }

    public void WeaponChanged(Weapon newActiveWeapon)
    {
        if (activeWeapon == newActiveWeapon)
        {
            return;
        }

        if (activeWeapon != null)
        {
            weaponPanels[activeWeapon].Active = false;
        }
        weaponPanels[newActiveWeapon].Active = true;

        activeWeapon = newActiveWeapon;
    }

    public void WeaponListChanged(List<Weapon> weapons)
    {
        // Add new weapons to the weaponbar
        foreach (Weapon weapon in weapons)
        {
            if (!weaponPanels.ContainsKey(weapon))
            {
                WeaponSlot weaponSlot = WeaponSlot.Scene.Instantiate<WeaponSlot>();
                weaponSlot.Name = "asd";
                TextureRect rect = weapon.GetWeaponIcon();
                if (rect == null) continue;

                weaponPanels.Add(weapon, weaponSlot);

                weaponSlot.AddChild(rect);
                this.hBoxContainer.AddChild(weaponSlot);
            }
        }

        // Remove unused TextureRects
        foreach (Weapon weaponKey in weaponPanels.Keys)
        {
            if (!weapons.Contains(weaponKey))
            {
                weaponPanels[weaponKey].QueueFree();
                weaponPanels.Remove(weaponKey);
            }
        }
    }
}
