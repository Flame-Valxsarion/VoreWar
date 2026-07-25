using UnityEngine;


class Alligators : BlankSlate
{
    public Alligators()
    {
        // These set the layers and colour options used by the various alligator parts.

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Alligator); // All parts use these colours or are precoloured.

        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, s.Unit.SkinColor)); // Main body.
        Dick = new SpriteExtraInfo(12, DickSprite, WhiteColored); // Come pre coloured.
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, s.Unit.SkinColor)); // The cloacal ring for the dick.
        Mouth = new SpriteExtraInfo(2, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, s.Unit.SkinColor)); // The mouth corners, not the actual mouth.
        Belly = new SpriteExtraInfo(4, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, s.Unit.SkinColor)); // Tummy.
        Eyes = new SpriteExtraInfo(0, EyesSprite, WhiteColored); // The eyes come precoloured for the 'gators, and go under the body to boot.
        Weapon = new SpriteExtraInfo(11, WeaponSprite, WhiteColored); // Either the mace or spear, 'gators haven't got ranged weapons. They'd just smack things with those.
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, s.Unit.SkinColor)); // Arms and hands.
        BodyAccent2 = new SpriteExtraInfo(6, BodyAccentSprite2, WhiteColored); // Toenails.
        Hair = new SpriteExtraInfo(7, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, s.Unit.SkinColor)); // Open mouth edges.
        Hair2 = new SpriteExtraInfo(8, HairSprite2, WhiteColored); // Open mouth inside.
        BodyAccessory = new SpriteExtraInfo(9, AccessorySprite, WhiteColored); // The level band.
        SecondaryAccessory = new SpriteExtraInfo(10, SecondaryAccessorySprite, WhiteColored); // Gear.
        BodyAccent4 = new SpriteExtraInfo(10, BodyAccentSprite4, WhiteColored); // Gear.
        BodyAccent5 = new SpriteExtraInfo(10, BodyAccentSprite5, WhiteColored); // Gear.

        // Alligators have 4 mouth options (three different mouth corner variants and an empty option showing the base expression) and 4 different eyes to choose from.
        MouthTypes = 4;
        EyeTypes = 4;


        // Alligators use the gentler belly struggle animation since the stronger one would make a mess of the scale pattern.
        GentleAnimation = true;
    }

    // Alligators have three different dick sizes and no breasts.
    internal override int DickSizes => 3;
    internal override int BreastSizes => 1;

    protected override Sprite BodySprite(Actor_Unit actor) => actor.IsAttacking ? SpriteDictionary.Alligators[1] : SpriteDictionary.Alligators[0]; // Gets either normal body or the attack one.
    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Alligators[8 + actor.Unit.EyeType]; // Slips eyes under the body.
    protected override Sprite MouthSprite(Actor_Unit actor) => actor.Unit.MouthType == 0 ? null : SpriteDictionary.Alligators[4 + actor.Unit.MouthType];
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.Alligators[2];
    protected override Sprite HairSprite(Actor_Unit actor) => actor.IsOralVoring ? SpriteDictionary.Alligators[3] : null;
    protected override Sprite HairSprite2(Actor_Unit actor) => actor.IsOralVoring ? SpriteDictionary.Alligators[4] : null;

    protected override Sprite WeaponSprite(Actor_Unit actor) // Gets the correct weapon sprite, or a lack of one.
    {
        if (actor.Unit.HasWeapon == false)
            return null;
        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpriteDictionary.Alligators[12];
            case 1:
                return SpriteDictionary.Alligators[14];
            case 2:
                return SpriteDictionary.Alligators[13];
            case 3:
                return SpriteDictionary.Alligators[15];
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // All arms aboard!
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return SpriteDictionary.Alligators[48];
            return SpriteDictionary.Alligators[45];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpriteDictionary.Alligators[46];
            case 1:
                return SpriteDictionary.Alligators[48];
            case 2:
                return SpriteDictionary.Alligators[47];
            case 3:
                return SpriteDictionary.Alligators[48];
            default:
                return SpriteDictionary.Alligators[45];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Either slaps the "Cloaca Be Gone!" patch on the 'gator's groin or slaps on the stretched open cloaca if the unit gets erect.
    {
        if (actor.IsUnbirthing) return SpriteDictionary.Alligators[23];
        if (actor.IsAnalVoring) return SpriteDictionary.Alligators[23];

        if (Config.HideCocks) return SpriteDictionary.Alligators[16];

        if (actor.IsErect())
        {
            return SpriteDictionary.Alligators[17];
        }
        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Checks if dicks are on and pops up the correct size if the unit gets erect.
    {
        if (actor.IsAnalVoring) return null;
        if (actor.IsCockVoring)
        {
            if (actor.Unit.DickSize < 2)
                return SpriteDictionary.Alligators[21];
            else
                return SpriteDictionary.Alligators[22];
        }
        if (actor.IsUnbirthing)
        {
            return SpriteDictionary.Alligators[24];
        }
        if (actor.IsErect() && !Config.HideCocks)
        {
            switch (actor.Unit.DickSize)
            {
                case 0:
                    return SpriteDictionary.Alligators[18];
                case 1:
                    return SpriteDictionary.Alligators[19];
                case 2:
                    return SpriteDictionary.Alligators[20];
            }
        }
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Checks if the unit has eaten something, then uses dark magic to determine the belly size.
    {
        int bellySize = actor.GetUniversalSize(8); //One extra for the empty check
        bellySize -= 1;
        if (bellySize == -1)
            return null;
        return SpriteDictionary.Alligators[25 + bellySize];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Level bands. Checks from highest down, giving the first band that the unit has sufficient level for. If too low for everything, returns null.
    {
        if (actor.Unit.Level > 19) return SpriteDictionary.Alligators[44];
        if (actor.Unit.Level > 17) return SpriteDictionary.Alligators[43];
        if (actor.Unit.Level > 14) return SpriteDictionary.Alligators[42];
        if (actor.Unit.Level > 11) return SpriteDictionary.Alligators[41];
        if (actor.Unit.Level > 8) return SpriteDictionary.Alligators[40];
        if (actor.Unit.Level > 4) return SpriteDictionary.Alligators[39];
        return null;
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 1)
            return null;
        if (actor.Unit.Items[0] is Accessory)
            acc = (Accessory)actor.Unit.Items[0];
        return GetItemAccessorySprite(actor, acc);
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 2)
            return null;
        if (actor.Unit.Items[1] is Accessory)
            acc = (Accessory)actor.Unit.Items[1];
        return GetItemAccessorySprite(actor, acc);
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 3)
            return null;
        if (actor.Unit.Items[2] is Accessory)
            acc = (Accessory)actor.Unit.Items[2];
        return GetItemAccessorySprite(actor, acc);
    }

    private static Sprite GetItemAccessorySprite(Actor_Unit actor, Accessory acc)
    {
        if (acc == null)
            return null;
        if (acc == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
            return SpriteDictionary.Alligators[37];
        if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
            return SpriteDictionary.Alligators[36];
        if (acc == State.World.ItemRepository.GetItem(ItemType.Shoes))
            return SpriteDictionary.Alligators[38];
        if (acc == State.World.ItemRepository.GetItem(ItemType.Gauntlet))
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return SpriteDictionary.Alligators[35];
                return SpriteDictionary.Alligators[33];
            }
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Alligators[34];
                case 1:
                    return SpriteDictionary.Alligators[35];
                case 2:
                    return SpriteDictionary.Alligators[34];
                case 3:
                    return SpriteDictionary.Alligators[35];
                default:
                    return SpriteDictionary.Alligators[33];
            }
        }
        return null;
    }
}
