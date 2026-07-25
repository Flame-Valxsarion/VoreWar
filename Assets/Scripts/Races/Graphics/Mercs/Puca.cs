using System;
using System.Collections.Generic;
using UnityEngine;

class Puca : BlankSlate
{
    public Puca()
    {
        AccessoryColors = 5;
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(0, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        Head = new SpriteExtraInfo(1, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        Mouth = new SpriteExtraInfo(2, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        Eyes = new SpriteExtraInfo(2, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored);
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PucaBalls, s.Unit.AccessoryColor));
        EyeTypes = 5;
        MouthTypes = 5;
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            vest,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            loinCloth,
            shorts,
        };
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.Predator &&
           (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ||
           actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
           && actor.GetStomachSize(9) == 9)
        {
            float yOffset = 20 * .625f;
            AddOffset(Body, 0, yOffset);
            AddOffset(BodyAccent, 0, yOffset);
            AddOffset(BodyAccent2, 0, yOffset);
            AddOffset(BodyAccent3, 0, yOffset);
            AddOffset(Head, 0, yOffset);
            AddOffset(Mouth, 0, yOffset);
            AddOffset(Eyes, 0, yOffset);
            AddOffset(Weapon, 0, yOffset);
            AddOffset(Breasts, 0, yOffset);
            AddOffset(Dick, 0, yOffset);
            AddOffset(Balls, 0, yOffset);
            ClothingShift = new Vector3(0, yOffset);
        }
        else
            ClothingShift = new Vector3();
    }

    internal override int DickSizes => 3;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return SpriteDictionary.Puca[1];
        else if (actor.IsAnalVoring)
            return SpriteDictionary.Puca[2];
        else
            return SpriteDictionary.Puca[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.Puca[4];
        return SpriteDictionary.Puca[3];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Puca[8 + actor.Unit.EyeType];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Puca[16 + actor.Unit.MouthType];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < .75f)
            {
                Dick.layer = 18;
                return SpriteDictionary.Puca[33 + actor.Unit.DickSize];
            }
            else
            {
                Dick.layer = 12;
                return SpriteDictionary.Puca[30 + actor.Unit.DickSize];
            }
        }

        Dick.layer = 9;
        return SpriteDictionary.Puca[30 + actor.Unit.DickSize];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            Weapon.layer = 3;
            int pose = actor.IsAttacking ? 1 : 0;
            if (actor.IsAnalVoring)
                pose = 2;
            if (actor.GetWeaponSprite() < 4)
            {
                return SpriteDictionary.Puca[5 + pose];
            }
            else
            {
                Weapon.layer = -1;
                return SpriteDictionary.Puca[13 + pose];
            }

        }
        else
        {
            return null;
        }

    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(9) == 9)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                return SpriteDictionary.Puca[50];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(9) == 9)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                return SpriteDictionary.Puca[49];
            }

            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return SpriteDictionary.Puca[37 + actor.GetStomachSize(9)];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        AddOffset(Balls, 0, -21 * .625f);
        if (actor.Unit.HasDick == false)
            return null;
        int baseSize = actor.Unit.DickSize / 3;
        int ballOffset = actor.GetBallSize(21, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[24];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[23];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 20)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return State.GameManager.SpriteDictionary.Balls[22];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 19)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return State.GameManager.SpriteDictionary.Balls[21];
        }
        int combined = Math.Min(baseSize + ballOffset + 2, 20);
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

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
            return SpriteDictionary.Puca[26];
        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Puca[23];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return SpriteDictionary.Puca[24];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
            return SpriteDictionary.Puca[27];
        return null;
    }

    Vest vest = new Vest();
    LoinCloth loinCloth = new LoinCloth();
    Shorts shorts = new Shorts();

    class Vest : MainClothing
    {
        public Vest()
        {
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int spriteNum;
            breastSprite = null;
            int femaleMod = actor.Unit.HasBreasts ? 19 : 0;
            if (actor.IsAnalVoring)
            {
                spriteNum = 29 + femaleMod;
            }
            else
                spriteNum = 28 + femaleMod;

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.Puca[spriteNum];
            base.Configure(sprite, actor);
        }
    }

    class LoinCloth : MainClothing
    {
        public LoinCloth()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int spriteNum;
            breastSprite = null;
            if (actor.IsAnalVoring)
            {
                spriteNum = 22;
            }
            else
                spriteNum = 21;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.Puca[spriteNum];
            base.Configure(sprite, actor);
        }
    }

    class Shorts : MainClothing
    {
        public Shorts()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int spriteNum;
            breastSprite = null;
            if (actor.IsAnalVoring)
                return;
            else
                spriteNum = 36;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.Puca[spriteNum];
            base.Configure(sprite, actor);
        }
    }

}

