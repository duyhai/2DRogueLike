using Godot;
using System;
using System.Collections.Generic;

public class WeaponBar : HBoxContainer
{
    public void WeaponChanged(List<Weapon> weapons, Weapon activeWeapon)
    {
        foreach (Node node in GetChildren())
        {
            node.QueueFree();
        }

        int sizeX = 0;

        foreach (var weapon in weapons)
        {
            Panel panel = new Panel();
            TextureRect rect = weapon.GetWeaponIcon();
            if (rect == null) continue;

            var style = new StyleBoxFlat();
            if (weapon == activeWeapon)
            {
                style.BgColor = Colors.Yellow;
            }
            else
            {
                style.BgColor = Colors.DimGray;
            }
            panel.AddStyleboxOverride("panel", style);

            panel.RectMinSize = new Vector2(54, 54);
            panel.AddChild(rect);
            AddChild(panel);

            sizeX += (int)panel.RectMinSize.x;
        }

        RectPosition = new Vector2(512 - (sizeX / 2), 545);
    }

    public void SetWeapons(List<Weapon> weapons)
    {

    }

    public override void _Ready()
    {
    }
}
