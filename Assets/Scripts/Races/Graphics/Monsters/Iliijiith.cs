using System.Collections.Generic;
using UnityEngine;

class Iliijiith : BlankSlate
{
    RaceFrameList frameListLegs = new RaceFrameList(new int[4] { 0, 1, 2, 3 }, new float[4] { .15f, .10f, .15f, .10f });

    //bool aggro = false;

    internal Iliijiith()

    {
        CanBeGender = new List<Gender>() { Gender.None };
        GentleAnimation = true;
        WeightGainDisabled = true;
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.IliijiithIdleColor);

        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => (s.IsAttacking || s.IsOralVoring) ? ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.IliijiithAttackColor, s.Unit.SkinColor) : ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.IliijiithIdleColor, s.Unit.SkinColor)); // Leggies
        Head = null;
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => (s.IsAttacking || s.IsOralVoring) ? ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.IliijiithAttackColor, s.Unit.SkinColor) : ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.IliijiithIdleColor, s.Unit.SkinColor)); // Core. Hardcore. Crystalcore!
        BodyAccent2 = null;
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Eyes = null;
        SecondaryEyes = null;
        Belly = null;
        SecondaryBelly = null;
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
    }
    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 4), 0, true)};
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null)
            SetUpAnimations(actor);
        actor.Unit.Pronouns = new List<string> { "it", "it", "its", "its", "itself", "singular" };//Special Pronouns!
    }

protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists[0].currentTime >= frameListLegs.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListLegs.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }
        //if (actor.IsOralVoring || actor.IsAttacking)
        //    aggro = true;
        //else
        //    aggro = false;
        return State.GameManager.SpriteDictionary.Iliijiith[0 + frameListLegs.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == true)
        {
            return State.GameManager.SpriteDictionary.Iliijiith[5 + actor.GetStomachSize(18)];
        }
        else
            return State.GameManager.SpriteDictionary.Iliijiith[4];
    }


}

