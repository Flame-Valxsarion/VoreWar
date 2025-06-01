using System.Collections.Generic;
using UnityEngine;

class Viisels : BlankSlate
{
    internal Viisels()
    {
        SpecialAccessoryCount = 4;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;
        GentleAnimation = true;

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViiselSkin);// Body color
        HairColors = 1;
        HairStyles = 1;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViiselSkin); // Faceplate color
        EyeTypes = 4;
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        BodySizes = 0;
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;
        TailTypes = 4;

        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.AccessoryColor)); // Face plate
        BodyAccessory = new SpriteExtraInfo(6, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.SkinColor)); // Pattern
        BodyAccent = new SpriteExtraInfo(9, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.SkinColor)); // Paw
        BodyAccent2 = new SpriteExtraInfo(6, BodyAccentSprite2, WhiteColored); // Tongue
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.SkinColor)); // Tail
        BodyAccent4 = new SpriteExtraInfo(7, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.SkinColor)); // Sheathe
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.AccessoryColor)); // Eyes
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor)); // Eye color
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(8, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.SkinColor));
        SecondaryBelly = null;
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        BreastShadow = null;
        Dick = new SpriteExtraInfo(7, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViiselSkin, s.Unit.SkinColor));
    }
    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.TailType = State.Rand.Next(TailTypes);
        unit.SpecialAccessoryType = State.Rand.Next(0, 4);
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        actor.UnitSprite.GraphicsFolder.transform.localScale = new Vector3(0.4f, 0.4f, 1); // Smolness activated!
    }

    protected override Sprite BodySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Viisels[0];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        int size = actor.GetStomachSize(6);
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 6)
            return State.GameManager.SpriteDictionary.Viisels[35];
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 6)
            return State.GameManager.SpriteDictionary.Viisels[34];
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 5)
            return State.GameManager.SpriteDictionary.Viisels[33];
        return State.GameManager.SpriteDictionary.Viisels[26 + actor.GetStomachSize(6)];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
            return null;
        return State.GameManager.SpriteDictionary.Viisels[10 + (actor.Unit.SpecialAccessoryType)]; // Pattern
    }
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Viisels[1 + (actor.IsAttacking ? 1 : 0)]; // Paw
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Tongue
    {
        if (actor.IsOralVoring)
            return State.GameManager.SpriteDictionary.Viisels[17];
        return null;
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => State.GameManager.SpriteDictionary.Viisels[7 + actor.Unit.TailType]; // Tail

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect())
        {
            return State.GameManager.SpriteDictionary.Viisels[5];
        }
        else
        {
            return State.GameManager.SpriteDictionary.Viisels[3];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect())
        {
            return State.GameManager.SpriteDictionary.Viisels[6];
        }
        else
        {
            return State.GameManager.SpriteDictionary.Viisels[4];
        }
    }
    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        int offset = actor.GetBallSize(6);
        if (actor.Unit.HasDick == false || actor.GetBallSize(6) == 0)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && offset == 6)
            return State.GameManager.SpriteDictionary.Viisels[45];
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && offset == 6)
            return State.GameManager.SpriteDictionary.Viisels[44];
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && offset == 5)
            return State.GameManager.SpriteDictionary.Viisels[43];
        return State.GameManager.SpriteDictionary.Viisels[36 + actor.GetBallSize(6)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Viisels[15 + (actor.IsOralVoring ? 1 : 0)];

    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Viisels[19 + (actor.Unit.EyeType * 2)]; // Eye color
    protected override Sprite EyesSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Viisels[18 + (actor.Unit.EyeType * 2)]; // Eyes

    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

