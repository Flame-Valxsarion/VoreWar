using System;
using System.Collections.Generic;
using UnityEngine;

class Kobolds : BlankSlate
{
    bool facingFront = true;
    readonly Sprite[] Sprites = SpriteDictionary.Kobolds;

    public Kobolds()
    {
        GentleAnimation = true;
        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(-2, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));
        Weapon = new SpriteExtraInfo(14, WeaponSprite, WhiteColored);
        Breasts = new SpriteExtraInfo(8, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, s.Unit.AccessoryColor));

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new BeltTop(),
            new Tabard(),
            new Rags(),
        };
        AvoidedMainClothingTypes = 1;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new BikiniBottom(),
            new LoinCloth(),
        };


        TailTypes = 2;
        HeadTypes = 3;
        SpecialAccessoryCount = 5;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Kobold);
    }
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.TailType = (0);
    }
    internal override int BreastSizes => 3;
    internal override int DickSizes => 3;

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.IsOralVoring || actor.IsAttacking)
            facingFront = true;
        else if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring || actor.Unit.TailType == 1)
            facingFront = false;
        else
            facingFront = true;
        base.RunFirst(actor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.Predator)
        {
            float ballsYOffset = 0;
            int ballSize = actor.Unit.DickSize + actor.GetBallSize(16 - actor.Unit.DickSize);
            if (ballSize == 13) ballsYOffset = 14;
            if (ballSize == 14) ballsYOffset = 16;
            if (ballSize == 15) ballsYOffset = 24;
            if (ballSize == 16) ballsYOffset = 30;
            if (ballSize == 16 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls)) ballsYOffset = 30;
            if (ballSize == 16 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls)) ballsYOffset = 30;
            bool OnBalls = ballsYOffset > 0;
            float stomachYOffset = 0;
            int stomachSize = actor.GetStomachSize(12);
            if (stomachSize == 10) stomachYOffset = 6;
            if (stomachSize == 11) stomachYOffset = 10;
            if (stomachSize == 12) stomachYOffset = 20;
            if (stomachSize == 12 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb)) stomachYOffset = 20;
            if (stomachSize == 12 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb)) stomachYOffset = 20;
            ballsYOffset *= .625f; //Change from pixels to units
            stomachYOffset *= .625f; //Change from pixels to units
            AddOffset(Balls, 0, -ballsYOffset);
            AddOffset(Belly, 0, -stomachYOffset);

            WholeBodyOffset = new Vector2(0, Math.Max(ballsYOffset, stomachYOffset));
            //AddOffset(Body, 0, ballsYOffset);
            //AddOffset(Head, 0, ballsYOffset);
            //AddOffset(BodyAccent, 0, ballsYOffset);
            //AddOffset(BodyAccent2, 0, ballsYOffset);
            //AddOffset(BodyAccent3, 0, ballsYOffset);
            //AddOffset(BodyAccent4, 0, ballsYOffset);
            //AddOffset(Weapon, 0, ballsYOffset);
            //AddOffset(Breasts, 0, ballsYOffset);
            //AddOffset(Dick, 0, ballsYOffset);
            //if (OnBalls == false)
            //    AddOffset(Balls, 0, ballsYOffset);
            //ClothingShift = new Vector3(0, ballsYOffset);
        }
        else
            WholeBodyOffset = new Vector2(0, 0);

    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (actor.IsAttacking) return Sprites[1];
            return Sprites[0];
        }
        return Sprites[2];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => facingFront ? null : Sprites[3];

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (facingFront)
        {
            BodyAccent2.layer = 4;
            if (actor.IsAttacking) return Sprites[5];
            return Sprites[4];
        }
        BodyAccent2.layer = 1;
        return Sprites[6];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int spr = 7 + (3 * actor.Unit.HeadType);
        if (facingFront == false)
        {
            Head.layer = 1;
            return Sprites[spr + 2];
        }
        Head.layer = 4;
        if (actor.IsOralVoring) return Sprites[spr + 1];
        return Sprites[spr];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (facingFront == false)
            return null;
        if (actor.BestRanged != null)
        {
            return actor.IsAttacking ? null : Sprites[19];
        }
        if (actor.Unit.HasWeapon)
        {
            return actor.IsAttacking ? Sprites[18] : Sprites[17];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (facingFront == false)
            return null;
        if (actor.BestRanged == null && actor.Unit.HasWeapon && actor.IsAttacking == false)
            return Sprites[16];
        return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        int spr = 21 + (2 * actor.Unit.SpecialAccessoryType);
        if (facingFront)
        {
            return actor.IsOralVoring ? Sprites[spr + 1] : Sprites[spr];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0)
        {
            return Sprites[facingFront ? 31 : 32];
        }
        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        Dick.layer = facingFront ? 9 : 17;
        if (actor.GetBallSize(10) > 2)
            Dick.layer -= 2;
        if (actor.Unit.DickSize >= 0)
        {
            int spr = 33 + (3 * actor.Unit.DickSize);
            if (facingFront == false)
                return Sprites[spr + 2];
            return Sprites[actor.IsErect() ? (spr + 1) : spr];
        }
        else
        {
            //if (facingFront == false)
            //    Dick.layer = 4;
            return Sprites[facingFront ? 31 : 32];
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (Config.LizardsHaveNoBreasts)
            return null;
        if (actor.Unit.HasBreasts && facingFront)
            return Sprites[42 + actor.Unit.BreastSize];
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        int baseSize = actor.Unit.DickSize;
        Balls.layer = facingFront ? 8 : 18;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && actor.GetBallSize(16) == 16)
                return Sprites[62];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
            {
                if (actor.GetBallSize(16, 0.7f) == 16)
                    return Sprites[113];
                else if (actor.GetBallSize(16, 0.8f) == 16)
                    return Sprites[112];
                else if (actor.GetBallSize(16, 0.9f) == 16)
                    return Sprites[111];
            }
            return Sprites[45 + baseSize + actor.GetBallSize(16 - baseSize)];
        }
        return Sprites[45 + baseSize];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.Unit.Predator == false || actor.HasBelly == false)
            return null;
        if (facingFront)
        {
            Belly.layer = 15;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(12) == 12)
                return Sprites[84];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(12, 0.7f) == 12)
                    return Sprites[110];
                else if (actor.GetStomachSize(12, 0.8f) == 12)
                    return Sprites[109];
                else if (actor.GetStomachSize(12, 0.9f) == 12)
                    return Sprites[108];
            }
            return Sprites[71 + actor.GetStomachSize(12)];
        }
        else
        {
            Belly.layer = 2;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(12) == 12)
                return Sprites[102];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(12, 0.7f) == 12)
                    return Sprites[116];
                else if (actor.GetStomachSize(12, 0.8f) == 12)
                    return Sprites[115];
                else if (actor.GetStomachSize(12, 0.9f) == 12)
                    return Sprites[114];
            }
            return Sprites[89 + actor.GetStomachSize(12)];
        }

    }

    class BikiniBottom : MainClothing
    {
        public BikiniBottom()
        {
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(10, null, null);
            clothing2 = new SpriteExtraInfo(11, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            bool facingFront;
            if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring)
                facingFront = false;
            else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking)
                facingFront = true;
            else
                facingFront = false;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            if (facingFront)
            {
                blocksDick = true;
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[105];
                if (actor.Unit.HasDick)
                    clothing2.GetSprite = (s) => SpriteDictionary.Kobolds[106];
                else
                    clothing2.GetSprite = null;
            }
            else
            {
                blocksDick = false;
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[107];
                clothing2.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }

    class Tabard : MainClothing
    {
        public Tabard()
        {
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            coversBreasts = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            bool facingFront;
            if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring)
                facingFront = false;
            else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking)
                facingFront = true;
            else
                facingFront = false;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            if (facingFront)
            {
                clothing1.layer = 20;
                if (actor.Unit.BreastSize > 1)
                    clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[88];
                else if (actor.Unit.BreastSize == 0)
                    clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[87];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[85];
            }
            else
            {
                clothing1.layer = -1;
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[86];
            }
            base.Configure(sprite, actor);
        }
    }

    class LoinCloth : MainClothing
    {
        public LoinCloth()
        {

            clothing1 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            bool facingFront;
            if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring)
                facingFront = false;
            else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking)
                facingFront = true;
            else
                facingFront = false;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            if (facingFront)
            {
                clothing1.layer = 10;
                blocksDick = true;
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[68];
            }
            else
            {
                clothing1.layer = 1;
                blocksDick = false;
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[70];
            }
            base.Configure(sprite, actor);
        }
    }

    class Rags : MainClothing
    {
        public Rags()
        {
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, null);
            clothing2 = new SpriteExtraInfo(11, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            bool facingFront;
            if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring)
                facingFront = false;
            else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking)
                facingFront = true;
            else
                facingFront = false;
            if (facingFront)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[63];
                clothing2.GetSprite = (s) => SpriteDictionary.Kobolds[66];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[65];
                clothing2.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }

    class BeltTop : MainClothing
    {
        public BeltTop()
        {
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, null);
            femaleOnly = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            bool facingFront;
            if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring)
                facingFront = false;
            else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking)
                facingFront = true;
            else
                facingFront = false;
            if (facingFront)
            {
                clothing1.layer = 10;
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[103];
            }
            else
            {
                clothing1.layer = 1;
                clothing1.GetSprite = (s) => SpriteDictionary.Kobolds[104];
            }
            base.Configure(sprite, actor);
        }
    }
}
