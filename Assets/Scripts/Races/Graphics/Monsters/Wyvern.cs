using UnityEngine;

class Wyvern : BlankSlate
{
    RaceFrameList frameListTail = new RaceFrameList(new int[6] { 2, 1, 0, 5, 4, 3 }, new float[6] { 0.55f, 0.55f, 0.55f, 0.55f, 0.55f, 0.55f });
    RaceFrameList frameListTongue = new RaceFrameList(new int[6] { 0, 1, 2, 3, 4, 5 }, new float[6] { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f });

    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    public Wyvern()
    {
        SkinColors = ColorMap.WyvernColorCount; // Main body, legs, wingarms, head, tail upper
        AccessoryColors = ColorMap.WyvernBellyColorCount; // Belly, tail under
        ExtraColors1 = ColorMap.WyvernBellyColorCount; // Wings
        EyeColors = ColorMap.WyvernColorCount; // Eyes
        GentleAnimation = true;

        WeightGainDisabled = true;

        Body = new SpriteExtraInfo(2, BodySprite, (s) => ColorMap.GetWyvernColor(s.Unit.SkinColor)); // Body
        Eyes = new SpriteExtraInfo(0, EyesSprite, (s) => ColorMap.GetWyvernBellyColor(s.Unit.EyeColor)); // Eyes
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, (s) => ColorMap.GetWyvernColor(s.Unit.SkinColor)); // Tail
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, (s) => ColorMap.GetWyvernBellyColor(s.Unit.AccessoryColor)); // Tail Under
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, (s) => ColorMap.GetWyvernBellyColor(s.Unit.ExtraColor1)); // Wings
        Mouth = new SpriteExtraInfo(4, MouthSprite, WhiteColored); // Inner Mouth
        BodySize = new SpriteExtraInfo(1, BodySizeSprite, (s) => ColorMap.GetWyvernBellyColor(s.Unit.AccessoryColor)); // Horns
        Dick = new SpriteExtraInfo(4, DickSprite, WhiteColored); // Dick, CV, UB
        Belly = new SpriteExtraInfo(5, null, (s) => ColorMap.GetWyvernBellyColor(s.Unit.AccessoryColor)); // Belly
        Hair = new SpriteExtraInfo(6, HairSprite, (s) => ColorMap.GetWyvernColor(s.Unit.SkinColor)); // Footpads
        Hair2 = new SpriteExtraInfo(6, HairSprite2, (s) => ColorMap.GetWyvernColor(s.Unit.SkinColor)); // Body Overlay
        BodyAccessory = new SpriteExtraInfo(4, AccessorySprite, WhiteColored); // Talons & Claws
        SecondaryAccessory = new SpriteExtraInfo(5, SecondaryAccessorySprite, WhiteColored); // Tongue
        Head = new SpriteExtraInfo(3, HeadSprite, (s) => ColorMap.GetWyvernBellyColor(s.Unit.AccessoryColor)); // Lower belly piece

        EyeTypes = 4; // Eye types
        BodySizes = 4; // Horn types
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Tail controller. Index 0.
            new AnimationController.FrameList(0, 0, false)}; // Tongue controller. Index 1.
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.IsOralVoring)
            return SpriteDictionary.Wyvern[2];
        if (actor.IsAttacking)
            return SpriteDictionary.Wyvern[1];
        if (actor.PredatorComponent?.VisibleFullness > 0)
            return SpriteDictionary.Wyvern[3];
        return SpriteDictionary.Wyvern[0];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.Wyvern[9];
        return null;
    }

    protected override Sprite HairSprite2(Actor_Unit actor) // The body overlay. Needed so the belly can't get where it isn't supposed to.
    {
        if (actor.IsAttacking) return SpriteDictionary.Wyvern[5];
        return SpriteDictionary.Wyvern[4];
    }

    protected override Sprite HairSprite(Actor_Unit actor) // The footpads during attack.
    {
        if (actor.IsAttacking) return SpriteDictionary.Wyvern[8];
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Wing membranes.
    {
        if (actor.IsAttacking) return SpriteDictionary.Wyvern[19];
        return SpriteDictionary.Wyvern[18];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Talons & claws.
    {
        if (actor.IsAttacking) return SpriteDictionary.Wyvern[21];
        return SpriteDictionary.Wyvern[20];
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // Tongue
    {
        if (!actor.Targetable) return null;

        if (actor.IsAttacking || actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[1].currentlyActive = false;
            actor.AnimationController.frameLists[1].currentFrame = 0;
            actor.AnimationController.frameLists[1].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[1].currentlyActive)
        {
            if (actor.AnimationController.frameLists[1].currentTime >= frameListTongue.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;

                if (actor.AnimationController.frameLists[1].currentFrame >= frameListTongue.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }

            return SpriteDictionary.Wyvern[34 + frameListTongue.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }

        if (actor.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick + CV and UB.
    {
        if (actor.IsCockVoring) return SpriteDictionary.Wyvern[53];
        if (actor.IsUnbirthing) return SpriteDictionary.Wyvern[51];
        if (actor.IsAnalVoring) return SpriteDictionary.Wyvern[51];
        if (actor.IsErect()) return SpriteDictionary.Wyvern[52];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Wyvern[10 + actor.Unit.EyeType]; // One of four eye options.
    protected override Sprite BodySizeSprite(Actor_Unit actor) => SpriteDictionary.Wyvern[14 + actor.Unit.BodySize]; // One of four horn options.
    protected override Sprite HeadSprite(Actor_Unit actor) => SpriteDictionary.Wyvern[54]; // A piece to cover the lower belly.

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Tail sprites.
    {
        if (!actor.Targetable) return SpriteDictionary.Wyvern[24];

        if (actor.IsAttacking)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListTail.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListTail.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return SpriteDictionary.Wyvern[22 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(400) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return SpriteDictionary.Wyvern[24];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Tail under side sprites. Frame list functions handled by the BodyAccentSprite method.
    {
        if (!actor.Targetable) return SpriteDictionary.Wyvern[30];

        if (actor.IsAttacking)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            return SpriteDictionary.Wyvern[6];
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            return SpriteDictionary.Wyvern[28 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        return SpriteDictionary.Wyvern[30];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly, both empty and filled.
    {
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
        {
            if (actor.PredatorComponent.VisibleFullness > 3)
                return SpriteDictionary.Wyvern[50];
        }

        if (actor.GetUniversalSize(1) == 0)
            return SpriteDictionary.Wyvern[7];
        return SpriteDictionary.Wyvern[40 + actor.GetUniversalSize(9, .8f)];
    }

    // Wyvern colours: Flame, Crimson, Blue, Sky, Black, Deep Green, Purple, Yellow(Main wyvern palette only), Rose Red, Pale Green,

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodySize = State.Rand.Next(0, 4);
        unit.EyeType = State.Rand.Next(0, 3);
        while (unit.SkinColor == unit.EyeColor)
        {
            unit.EyeColor = State.Rand.Next(0, ColorMap.WyvernColorCount);
        }
        string name = GetRaceSpecialName(unit);
        if (name != null) unit.Name = name;
    }

    private string GetRaceSpecialName(Unit unit)
    {
        int rand = State.Rand.Next(0, 20);

        if (rand <= 2)
        {
            switch (unit.SkinColor)
            {
                case 0: return "Flamescale";
                case 1: return "Bloodscale";
                case 2: return "Cobaltscale";
                case 3: return "Skyscale";
                case 4: return "Ebonscale";
                case 5: return "Jadescale";
                case 6: return "Duskscale";
                case 7: return "Sunscale";
                case 8: return "Rosescale";
                case 9: return "Budscale";
                case 10: return "Dustscale";
                default: break;
            }
        }
        if (rand <= 3 && unit.EyeType == 2) return "Blackeye";
        if (rand <= 5 && unit.EyeType == 2) return "Darkeye";

        if (rand <= 5)
        {
            switch (unit.ExtraColor1)
            {
                case 0: return "Firewing";
                case 1: return "Gorewing";
                case 2: return "Thunderwing";
                case 3: return "Skywing";
                case 4: return "Nightwing";
                case 5: return "Jadewing";
                case 6: return "Duskwing";
                case 7: return "Sunsetwing";
                case 8: return "Leafwing";
                case 9: return "Dustscale";
                default: break;
            }
        }

        if (rand <= 8)
        {
            switch (unit.SkinColor)
            {
                case 0: return "Firescale";
                case 1: return "Crimsonscale";
                case 2: return "Thunderscale";
                case 3: return "Aquascale";
                case 4: return "Darkscale";
                case 5: return "Viridscale";
                case 6: return "Poisonscale";
                case 7: return "Brightscale";
                case 8: return "Warmscale";
                case 9: return "Mintscale";
                case 10: return "Sandscale";
                default: break;
            }
        }

        if (rand <= 11)
        {
            switch (unit.ExtraColor1)
            {
                case 0: return "Flamewing";
                case 1: return "Crimsonwing";
                case 2: return "Deepwing";
                case 3: return "Aquawing";
                case 4: return "Shadowwing";
                case 5: return "Viridwing";
                case 6: return "Poisonwing";
                default: break;
            }
        }
        return null;
    }
}

