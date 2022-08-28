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
            Sprite weaponSprite = weapon.GetNodeOrNull<Sprite>("Sprite");

            if (weaponSprite == null) continue;

            Panel panel = new Panel();
            TextureRect rect = new TextureRect();

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

            rect.Texture = weaponSprite.Texture;
            rect.RectScale = weaponSprite.Scale;
            panel.RectMinSize = weaponSprite.Scale * rect.Texture.GetSize();
            panel.AddChild(rect);
            AddChild(panel);

            sizeX += (int)panel.RectMinSize.x;
        }

        System.Console.WriteLine(sizeX);
        RectPosition = new Vector2(512 - (3 * sizeX / 2), 545);
    }

    public void SetWeapons(List<Weapon> weapons)
    {

    }

    public override void _Ready()
    {
    }
}
