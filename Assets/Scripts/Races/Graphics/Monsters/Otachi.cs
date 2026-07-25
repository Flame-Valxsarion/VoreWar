using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Otachi : BlankSlate
{

    readonly Sprite[] OtachiSprites = SpriteDictionary.Otachi;
    bool standing = false;

    public Otachi()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OtachiSkin);
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite };

        Head = new SpriteExtraInfo(9, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor));
        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor)); // Background Body / Tail
        BodyAccent = new SpriteExtraInfo(8, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor)); // Neck / Back
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor)); // Main Arm
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor)); // Off Arm
        BodyAccent4 = new SpriteExtraInfo(10, BodyAccentSprite4, WhiteColored); // Teeth/Eyes/Nose
        BodyAccent5 = new SpriteExtraInfo(2, BodyAccentSprite5, WhiteColored); // Static Claws
        BodyAccent6 = new SpriteExtraInfo(10, BodyAccentSprite6, WhiteColored); // Claws Mainhand
        BodyAccent7 = new SpriteExtraInfo(10, BodyAccentSprite7, WhiteColored); // Claws Standing Offhand
        BodyAccent8 = new SpriteExtraInfo(4, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor)); // Sheath
        Belly = new SpriteExtraInfo(6, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(5, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(3, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OtachiSkin, s.Unit.SkinColor));
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(37, 0.7f);
        int ballsSize = actor.GetBallSize(38, 0.7f);
        if (size >= 13 || ballsSize >= 14) standing = true;
        else standing = false;
        if (actor.Unit.GetScale() == 1)
        actor.UnitSprite.GraphicsFolder.transform.localScale = new Vector3(1.2f, 1.2f, 1); // Embiggify!
        base.RunFirst(actor);
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (standing)
        {
            if (actor.IsOralVoring) return OtachiSprites[23];
            if (actor.IsAttacking || actor.IsCockVoring || actor.IsUnbirthing) return OtachiSprites[22];
            return OtachiSprites[21];
        }
        if (actor.IsOralVoring) return OtachiSprites[20];
        if (actor.IsAttacking || actor.IsCockVoring || actor.IsUnbirthing) return OtachiSprites[19];
        return OtachiSprites[18];
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Background Body/Tail
    {
        if (standing) 
        {
            if (actor.IsAttacking) return OtachiSprites[3];
            return OtachiSprites[2];
        }
        return OtachiSprites[1];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Neck/Back
    {
        if (standing) return null;
        return OtachiSprites[0];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Main Arm
    {
        if (standing)
        {
            AddOffset(BodyAccent2, -107, 0);
            if (actor.IsAttacking) return OtachiSprites[7];
            return OtachiSprites[6];
        }
        if (actor.IsAttacking) return OtachiSprites[5];
        return OtachiSprites[4];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Off Arm
    {
        if (standing)
        {
            AddOffset(BodyAccent3, 107, 0);
            if (actor.IsAttacking) return OtachiSprites[9];
            return OtachiSprites[8];
        }
        else return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Teeth/Eyes/Nose
    {
        if (standing)
        {
            if (actor.IsOralVoring) return OtachiSprites[29];
            if (actor.IsAttacking || actor.IsCockVoring || actor.IsUnbirthing) return OtachiSprites[28];
            return OtachiSprites[27];
        }
        if (actor.IsOralVoring) return OtachiSprites[26];
        if (actor.IsAttacking || actor.IsCockVoring || actor.IsUnbirthing) return OtachiSprites[25];
        return OtachiSprites[24];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Static Claws
    {
        if (standing) return OtachiSprites[11];
        return OtachiSprites[10];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Claws Mainhand
    {
        if (standing)
        {
            AddOffset(BodyAccent6, -107, 0);
            if (actor.IsAttacking) return OtachiSprites[15];
            return OtachiSprites[14];
        }
        else
        {
            if (actor.IsAttacking) return OtachiSprites[13];
            return OtachiSprites[12];
        }
    }
    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Claws Standing Offhand
    {
        if (standing)
        {
            AddOffset(BodyAccent7, 107, 0);
            if (actor.IsAttacking) return OtachiSprites[17];
            return OtachiSprites[16];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Sheath
    {
        if (actor.Unit.HasDick == false) return null;
        if (standing) return null;
        else return OtachiSprites[30];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // That gurgling dopamine deliverance device
    {
        int size = actor.GetStomachSize(37, 0.7f);
        // Sel at 31
        if (actor.HasBelly)
        {
            if (size >= 20) AddOffset(Belly, 0, -30);
            if (standing)
            {
                Belly.layer = 11;
                if (size >= 30 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach)) return OtachiSprites[148];
                if (size >= 30 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach)) return OtachiSprites[111 + size];
                if (size > 30) size = 29;
                return OtachiSprites[111 + size];
            }
            else 
            {
                Belly.layer = 6;
                return OtachiSprites[96 + size];
            }
        }
        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor) // A similar dopamine deliverance device, emphasis on the DELIVERY!
    {
        int size = actor.GetStomachSize(37, 0.7f);

        // Vagina
        if (actor.Unit.HasDick == false)
        {
            if (standing)
            {
                if (actor.IsUnbirthing) return OtachiSprites[36];
                return OtachiSprites[35];
            }
            else 
            {
                if (actor.IsUnbirthing) return OtachiSprites[32];
                return OtachiSprites[31];
            }
        }

        // Dick
        if (standing)
        {
            if (size >= 4)
            {
                if (actor.IsCockVoring) return OtachiSprites[40];
                if (actor.IsErect()) return OtachiSprites[39];
            }
            if (actor.IsCockVoring) return OtachiSprites[38];
            if (actor.IsErect()) return OtachiSprites[37];
            return null;
        }
        if (actor.IsCockVoring) return OtachiSprites[34];
        if (actor.IsErect()) return OtachiSprites[33];
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // She'll never admit it, but Selicia loves to be inside these~
    {
        int ballsSize = actor.GetBallSize(38, 0.7f);
        if (actor.Unit.HasDick == false) return null;
        if (ballsSize >= 20) AddOffset(Balls, 0, -63);
        if (standing)
        {
            if (ballsSize >= 30 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls)) return OtachiSprites[95];
            if (ballsSize >= 30 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls)) return OtachiSprites[58 + ballsSize];
            if (ballsSize >= 30) ballsSize = 29;
            return OtachiSprites[58 + ballsSize];
        }
        else 
        {
            return OtachiSprites[41 + ballsSize];
        }
    }
}