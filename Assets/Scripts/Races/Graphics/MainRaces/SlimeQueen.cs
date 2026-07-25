using System;
using System.Collections.Generic;
using UnityEngine;

class SlimeQueen : Slimes
{


    public SlimeQueen()
    {
        EyeTypes = 1;
        HairStyles = 2;
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Hermaphrodite };
        AllowedMainClothingTypes = new List<MainClothing>(){
                new SlimeQueenClothes.SlimeWithCrown(),
                };
        AllowedWaistTypes = new List<MainClothing>();
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        ExtraColors1 = 2;
        ExtraColors2 = 2;
        BodySizes = 0;
        Mouth = new SpriteExtraInfo(4, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Mouth, s.Unit.SkinColor));
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));
        BodyAccent2 = null;
        Weapon = new SpriteExtraInfo(12, WeaponSprite, WhiteColored);
        BodySize = null;


    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Mouth, 0, 8.125f);
        AddOffset(Belly, 0, 2 * .625f);
        AddOffset(Balls, 0, 6 * .625f);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.HairStyle = State.Rand.Next(HairStyles);
        unit.ExtraColor1 = 0;
        unit.ExtraColor2 = 0;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        AddOffset(Balls, 0, 0);
        int baseSize = 6;
        int ballOffset = actor.GetBallSize(21, 1);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[24];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[23];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 20)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return State.GameManager.SpriteDictionary.Balls[22];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 19)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return State.GameManager.SpriteDictionary.Balls[21];
        }
        int combined = Math.Min(baseSize + ballOffset + 3, 20);
        if (combined == 21)
            AddOffset(Balls, 0, -14 * .625f);
        else if (combined == 20)
            AddOffset(Balls, 0, -12 * .625f);
        else if (combined >= 17 && combined <= 19)
            AddOffset(Balls, 0, -8 * .625f);
        if (ballOffset > 0)
        {
            return State.GameManager.SpriteDictionary.Balls[combined];
        }

        return State.GameManager.SpriteDictionary.Balls[baseSize];

    }

    internal override int BreastSizes => 4;
    internal override int DickSizes => 1;

    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.SlimeQueen[actor.GetSimpleBodySprite()];
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.SlimeQueen[3 + (actor.IsAttacking ? 1 : 0)];

    protected override Sprite BreastsSprite(Actor_Unit actor) => SpriteDictionary.SlimeQueen[5 + actor.Unit.BreastSize];
    protected override Sprite DickSprite(Actor_Unit actor) => actor.Unit.HasDick ? SpriteDictionary.SlimeQueen[9] : null;

    protected override Sprite HairSprite(Actor_Unit actor) => SpriteDictionary.SlimeQueen[10 + actor.Unit.HairStyle];
    protected override Sprite HairSprite2(Actor_Unit actor) => SpriteDictionary.SlimeQueen[12];

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.SlimeQueen[27];

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        return SpriteDictionary.SlimeQueen[23 + (actor.IsAttacking ? 1 : 0)];

    }

}

namespace SlimeQueenClothes
{
    class SlimeWithCrown : MainClothing
    {
        public SlimeWithCrown()
        {
            OccupiesAllSlots = true;
            blocksDick = false;
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(8, null, null);
            clothing3 = new SpriteExtraInfo(8, null, null);
            clothing4 = new SpriteExtraInfo(12, null, null);
            clothing5 = new SpriteExtraInfo(10, null, null);
            clothing6 = new SpriteExtraInfo(8, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => s.Unit.ExtraColor1 == 0 ? null : SpriteDictionary.SlimeQueen[17 + s.Unit.BreastSize];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, s.Unit.ClothingColor2);
            clothing2.GetSprite = (s) => SpriteDictionary.SlimeQueen[13];
            clothing2.GetColor = (s) => Color.white;
            clothing3.GetSprite = (s) => SpriteDictionary.SlimeQueen[14];
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, s.Unit.ClothingColor);
            clothing4.GetSprite = (s) => s.Unit.ExtraColor2 == 0 ? null : SpriteDictionary.SlimeQueen[15];
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, s.Unit.ClothingColor);
            clothing5.GetSprite = (s) => SpriteDictionary.SlimeQueen[21 + (actor.IsAttacking ? 1 : 0)];
            clothing6.GetSprite = (s) => SpriteDictionary.SlimeQueen[25 + (s.IsAttacking ? 1 : 0)];
            clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, s.Unit.ClothingColor3);


            if (Config.CockVoreHidesClothes && actor.PredatorComponent?.BallsFullness > 0)
                clothing4.GetSprite = null;

            base.Configure(sprite, actor);
        }
    }
}
