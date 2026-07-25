using System.Collections.Generic;
using UnityEngine;

class FeralLions : BlankSlate
{

    readonly Sprite[] Sprites = SpriteDictionary.FeralLions;
    bool hindView = false;
    RaceFrameList frameListRumpVore = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { .75f, .5f });
    public FeralLions()
    {
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite, Gender.Maleherm };
        HairStyles = 10; // Manes
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralLionsFur);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralLionsEyes);
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralLionsMane);

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(10, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(8, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor)); // Foreground legs / Background legs during hind view
        BodyAccent2 = new SpriteExtraInfo(10, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor)); // facial expression
        BodyAccent3 = new SpriteExtraInfo(9, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsMane, s.Unit.HairColor)); // Mane
        BodyAccent4 = new SpriteExtraInfo(11, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsMane, s.Unit.HairColor)); // Mane over ears
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsMane, s.Unit.HairColor)); // Tail tip
        BodyAccent6 = new SpriteExtraInfo(13, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor)); // The Maw parts that are fur-colored
        Mouth = new SpriteExtraInfo(13, MouthSprite, WhiteColored); // Maw for vore and attack
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsEyes, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(4, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(8, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(7, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor));
        BodyAccent7 = new SpriteExtraInfo(17, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor)); // Pussy
        SecondaryAccessory = new SpriteExtraInfo(18, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, s.Unit.SkinColor)); // AV and UB
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring)
            hindView = true;
        else hindView = false;
        base.RunFirst(actor);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        if ((unit.GetGender() == Gender.Female) || (unit.GetGender() == Gender.Hermaphrodite && Config.HermsOnlyUseFemaleHair))
        {
            unit.HairStyle = 0;
        }
        else unit.HairStyle = State.Rand.Next(HairStyles);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (hindView)
        {
            Body.layer = 15;
            return Sprites[59];
        }
        Body.layer = 3;
        return Sprites[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (hindView)
        {
            return Sprites[48];
        }
        if (actor.IsAttacking || actor.IsOralVoring)
        {
            return Sprites[26];
        }
        return Sprites[27];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Ears
    {
        if (!hindView)
        {
            BodyAccessory.layer = 10;
            return actor.IsAttacking || actor.IsOralVoring || actor.IsAbsorbing || actor.IsBeingSuckled || actor.DamagedColors ? Sprites[42] : Sprites[43];
        }
        BodyAccessory.layer = 1;
        return Sprites[47];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Hind legs / Front legs hind view
    {
        BodyAccent.layer = hindView ? 1 : 8;
        return hindView ? Sprites[49] : Sprites[11];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Face
    {
        if (actor.IsOralVoring || actor.IsAttacking || hindView)
            return null;
        if (actor.Targetable == false && actor.Visible == true && actor.Surrendered)
            return Sprites[41];
        if (actor.IsDigesting || actor.IsAbsorbing || actor.IsBeingSuckled)
            return Sprites[40];
        if (actor.HasJustVored || actor.IsSuckling || actor.IsBeingRubbed)
            return Sprites[39];
        return Sprites[38];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring || actor.IsAttacking || hindView || actor.IsAbsorbing || actor.IsDigesting || actor.HasJustVored || actor.IsSuckling || actor.IsBeingSuckled || actor.IsBeingRubbed)
        {
            return null;
        }
        return Sprites[37];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Manes
    {
        if (actor.Unit.HairStyle == 0)
        {
            return null;
        }
        else return hindView ? Sprites[50 + actor.Unit.HairStyle - 1] : Sprites[28 + actor.Unit.HairStyle - 1];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Mane over Ears
    {
        if (hindView)
            return null;
        return actor.Unit.HairStyle == 0 ? null : actor.IsAttacking || actor.IsOralVoring || actor.IsAbsorbing || actor.DamagedColors ? Sprites[45] : Sprites[44];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => hindView ? Sprites[90] : Sprites[12]; // Tailtip
    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => !actor.IsAttacking && !actor.IsOralVoring ? null : Sprites[46];

    protected override Sprite MouthSprite(Actor_Unit actor) => !actor.IsAttacking && !actor.IsOralVoring ? null : Sprites[91];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        Belly.layer = hindView ? 14 : 4;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            return hindView ? Sprites[75] : Sprites[10];
        return hindView ? Sprites[60 + actor.GetStomachSize(13)] : Sprites[1 + actor.GetStomachSize(8)];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        Balls.layer = hindView ? 18 : 7;
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                return hindView ? Sprites[86] : Sprites[25];

            return hindView ? Sprites[78 + actor.GetBallSize(7)] : Sprites[15 + actor.GetBallSize(9)];
        }
        return hindView ? Sprites[77] : Sprites[14];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        //if ((hindView ? actor.GetBallSize(8) : actor.GetBallSize(9)) > 2)
        Dick.layer = hindView ? 15 : 6;
        //else 
        //Dick.layer = hindView ? 19 : 6;
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect())
        {
            return hindView ? Sprites[76] : Sprites[13];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Pussy
    {
        if (!actor.Unit.HasVagina)
            return null;
        return hindView ? Sprites[88] : null;
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // AV and UB
    {
        if (!actor.IsAnalVoring && !actor.IsUnbirthing)
            return null;
        return actor.IsAnalVoring ? Sprites[87] : Sprites[89];
    }













}
