using Godot;
using System;
using System.Collections.Generic;

public partial class WeaponBar : HBoxContainer
{
    Dictionary<Weapon, Panel> weaponPanels = new Dictionary<Weapon, Panel>();
    Weapon activeWeapon;

    public void WeaponChanged(Weapon newActiveWeapon)
    {
        if (activeWeapon == newActiveWeapon)
        {
            return;
        }

        if (activeWeapon != null)
        {
            changePanelColor(weaponPanels[activeWeapon], Colors.DimGray);
        }
        changePanelColor(weaponPanels[newActiveWeapon], Colors.Yellow);

        activeWeapon = newActiveWeapon;
    }

    public void WeaponListChanged(List<Weapon> weapons)
    {
        // Add new weapons to the weaponbar
        foreach (Weapon weapon in weapons)
        {
            if (!weaponPanels.ContainsKey(weapon))
            {
                Panel panel = new Panel();
                TextureRect rect = weapon.GetWeaponIcon();
                if (rect == null) continue;

                weaponPanels.Add(weapon, panel);

                changePanelColor(panel, Colors.DimGray);

                panel.CustomMinimumSize = new Vector2(54, 54);
                panel.AddChild(rect);
                AddChild(panel);
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

    public override void _Ready()
    {
    }

    private void changePanelColor(Panel panel, Color color)
    {
        var style = new StyleBoxFlat();
        style.BgColor = color;
        panel.AddThemeStyleboxOverride("panel", style);
    }
}
