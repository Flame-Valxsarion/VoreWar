using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class WarriorAnt : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.WarriorAnt;

    public WarriorAnt()
    {
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female };
        SpecialAccessoryCount = 9; // antennae
        clothingColors = 0;
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);

        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(3, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // antennae
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // mandibles
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // legs
        Mouth = new SpriteExtraInfo(4, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(4, EyesSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor));

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.AccessoryColor = unit.SkinColor;
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Body, 50 * .625f, 0);
        AddOffset(Belly, 50 * .625f, 0);
    }
    
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return Sprites[16];
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[1];
        }
        else
        {
            return Sprites[0];
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[6 + actor.Unit.SpecialAccessoryType];

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[4];
        }
        else
        {
            return Sprites[3];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[5];

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[2];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[15];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(47);

        if (size >= 47 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[49];
        }

        else if (size >= 47 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[48];
        }

        else if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[47];
        }

        else if (size >= 41 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[46];
        }

        else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[45];
        }

        else if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[44];
        }

        else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[43];
        }

        else if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.WarriorAnt[42];
        }

        if (size > 24) size = 24;

        return State.GameManager.SpriteDictionary.WarriorAnt[17 + size];
    }
    
}
