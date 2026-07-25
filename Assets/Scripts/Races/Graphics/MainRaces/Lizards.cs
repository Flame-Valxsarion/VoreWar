using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Lizards : DefaultRaceData
{
    bool facingFront = true;
    public Lizards()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        EyeTypes = 5;
        HairStyles = 6;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        MouthTypes = 1;
        BodySizes = 0;

        
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Head = new SpriteExtraInfo(2, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.HairColor)); //Horns / Skin
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor)); //Ventral Colour
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, WhiteColored); //Claws
        BodyAccent3 = new SpriteExtraInfo(14, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor)); //Left Hand
        BodyAccent4 = new SpriteExtraInfo(14, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor)); //Right Arm
        BodyAccent5 = new SpriteExtraInfo(24, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor)); //Right Hand
        BodyAccent6 = new SpriteExtraInfo(19, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor)); //Right Hand Shadow
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(16, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        Weapon = new SpriteExtraInfo(1, WeaponSprite, WhiteColored);
        BackWeapon = new SpriteExtraInfo(0, BackWeaponSprite, WhiteColored);
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        Pussy = new SpriteExtraInfo(21, PussySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        PussyIn = new SpriteExtraInfo(22, PussyInSprite, WhiteColored);
        Anus = new SpriteExtraInfo(21, AnusSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.AccessoryColor));
        AnusIn = new SpriteExtraInfo(22, AnusInSprite, WhiteColored);


        AllowedClothingHatTypes = new List<ClothingAccessory>() //Crown
        {
            RaceSpecificClothing.LizardLeaderCrown,
            RaceSpecificClothing.LizardBoneCrown,
            RaceSpecificClothing.LizardLeatherCrown,
            RaceSpecificClothing.LizardClothCrown,
            RaceSpecificClothing.LizardNoCrown,
        };

        AvoidedMainClothingTypes = 3;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardLight);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BeltTop,
            ClothingTypes.Leotard,
            RaceSpecificClothing.LizardBlackTop,
            RaceSpecificClothing.LizardBikiniTop,
            RaceSpecificClothing.LizardStrapTop,            
            RaceSpecificClothing.LizardBoneTop,
            RaceSpecificClothing.LizardLeatherTop,
            RaceSpecificClothing.LizardClothTop,
            RaceSpecificClothing.LizardPeasant,
            ClothingTypes.Rags,
            RaceSpecificClothing.LizardLeaderTop,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniBottom,
            ClothingTypes.Loincloth,
            ClothingTypes.Shorts,
            RaceSpecificClothing.LizardLeaderSkirt,
            RaceSpecificClothing.LizardBoneLoins,
            RaceSpecificClothing.LizardLeatherLoins,
            RaceSpecificClothing.LizardClothLoins,
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
            RaceSpecificClothing.LizardLeaderLegguards,
            RaceSpecificClothing.LizardBoneLegguards,
            RaceSpecificClothing.LizardLeatherLegguards,
            RaceSpecificClothing.LizardClothShorts,
        };
        ExtraMainClothing2Types = new List<MainClothing>()
        {
            RaceSpecificClothing.LizardLeaderArmbands,
            RaceSpecificClothing.LizardBoneArmbands,
            //RaceSpecificClothing.LizardBoneArmbands2,
            //RaceSpecificClothing.LizardBoneArmbands3,
            RaceSpecificClothing.LizardLeatherArmbands,
            //RaceSpecificClothing.LizardLeatherArmbands2,
            //RaceSpecificClothing.LizardLeatherArmbands3,
            RaceSpecificClothing.LizardClothArmbands,
            //RaceSpecificClothing.LizardClothArmbands2,
            //RaceSpecificClothing.LizardClothArmbands3,
        };

        AvoidedMouthTypes = 0;
        AvoidedEyeTypes = 0;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (unit.Type == UnitType.Leader)
        {
            unit.ClothingHatType = 1 + AllowedClothingHatTypes.IndexOf(RaceSpecificClothing.LizardLeaderCrown);
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.LizardLeaderTop);
            unit.ClothingType2 = 1 + AllowedWaistTypes.IndexOf(RaceSpecificClothing.LizardLeaderSkirt);
            unit.ClothingExtraType1 = 1 + ExtraMainClothing1Types.IndexOf(RaceSpecificClothing.LizardLeaderLegguards);
            unit.ClothingExtraType2 = 1 + ExtraMainClothing2Types.IndexOf(RaceSpecificClothing.LizardLeaderArmbands);
        }

        if (unit.ClothingType == 10) //If in prison garb
        {
            unit.ClothingHatType = 0;
            unit.ClothingExtraType1 = 0;
            unit.ClothingExtraType2 = 0;
        }
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.IsAnalVoring || actor.IsUnbirthing)
            facingFront = false;
        else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking || actor.IsCockVoring || actor.IsBreastVoring)
            facingFront = true;
        else
            facingFront = true;
        base.RunFirst(actor);
    }

    internal override int BreastSizes => Config.AllowHugeBreasts ? 8 : 5;

    //protected override Color BodyColor(Actor_Unit actor) => ColorMap.GetLizardColor(actor.Unit.AccessoryColor);
    //protected override Color BodyAccessoryColor(Actor_Unit actor) => ColorMap.GetLizardColor(actor.Unit.AccessoryColor);

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (facingFront)
            {
            Body.layer = 2;
            return SpriteDictionary.Lizards[actor.GetSimpleBodySprite()];
        }
        Body.layer = 16; return SpriteDictionary.LizardsBooty[1];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        else return SpriteDictionary.LizardsBooty[0];
    }
    //protected override Sprite HairSprite(Actor_Unit actor) => State.GameManager.OldSpriteDictionary.LizardHorns[actor.Unit.HairStyle];
    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (facingFront)
        { return SpriteDictionary.Lizards[7 + actor.Unit.HairStyle];}
        return SpriteDictionary.LizardsBooty[68 + actor.Unit.HairStyle];
    } 
    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (facingFront)
        { BodyAccent.layer = 7; return SpriteDictionary.Lizards[5 + (actor.IsOralVoring ? 1 : 0)];}
        BodyAccent.layer = 17;
        return SpriteDictionary.LizardsBooty[76];
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (facingFront) 
        { 
            BodyAccent2.layer = 7; 
            return SpriteDictionary.Lizards[3 + (actor.IsAttacking ? 1 : 0)];
		}
        else 
        {
            BodyAccent2.layer = 25;

			try
            {
				if (actor.Unit.HasDick && actor.PredatorComponent.BallsFullness >= 2.5)
				{
					return SpriteDictionary.LizardsBooty[75];
				}
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine($"Missing the predator component: {e.Message}");
			}
			if (actor.GetStomachSize(16, 1.0f) >= 16)
			{ 
				return SpriteDictionary.LizardsBooty[74];
			}
			else return SpriteDictionary.LizardsBooty[6];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        else return SpriteDictionary.LizardsBooty[2];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        else return SpriteDictionary.LizardsBooty[3];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        else return SpriteDictionary.LizardsBooty[4];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        else return SpriteDictionary.LizardsBooty[5];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (facingFront)
        { return SpriteDictionary.Lizards[13 + actor.Unit.EyeType];}
        else return null;
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (facingFront == true)
        {
            Breasts.layer = 16;
            if (Config.LizardsHaveNoBreasts)
                return null;
            if (actor.Unit.HasBreasts == false)
                {return null;}
            if (actor.SquishedBreasts && actor.Unit.BreastSize >= 3 && actor.Unit.BreastSize <= 6)
                {return State.GameManager.SpriteDictionary.SquishedBreasts[actor.Unit.BreastSize - 3];}
            return SpriteDictionary.Lizards[18 + actor.Unit.BreastSize];
        }
        else
        {
            Breasts.layer = 15;
            if (Config.LizardsHaveNoBreasts)
                return null;
            if (actor.Unit.HasBreasts == false)
                {return null;}
            if (actor.Unit.BreastSize <= 2)
                {return null;}
            if (actor.Unit.BreastSize >= 3)
                {return SpriteDictionary.LizardsBooty[46 + actor.Unit.BreastSize - 3];}
            return null; //Does this work?  I don't know anymore
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (facingFront == true)
        {
            if (actor.HasBelly)
            {
                belly.SetActive(true);
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -30 * .625f);
                    return State.GameManager.SpriteDictionary.Bellies[17];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -30 * .625f);
                    return State.GameManager.SpriteDictionary.Bellies[16];
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
                return State.GameManager.SpriteDictionary.Bellies[actor.GetStomachSize()];
            }
            else {return null;}
        }
        else {if (actor.HasBelly)
            {
                belly.SetActive(true);
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -30 * .625f);
                    return State.GameManager.SpriteDictionary.Bellies[17];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -30 * .625f);
                    return State.GameManager.SpriteDictionary.Bellies[16];
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
                return SpriteDictionary.LizardsBooty[52 + actor.GetStomachSize()];
            }
            else {return null;}
        }
    }
    
    

    protected Sprite PussySprite(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        if (actor.IsUnbirthing)
            { 
            if (actor.Unit.HasDick == false && actor.Unit.HasBreasts == true) // Visible for Females
                { return SpriteDictionary.LizardsBooty[12];}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == false) // Hide for Males
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == false) // Hide for Herms (Didn't work)
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == true) // Visible for Herms
                { return SpriteDictionary.LizardsBooty[12];}
            return null; // ????
            }
        if (actor.IsAnalVoring)
            { 
            if (actor.Unit.HasDick == false && actor.Unit.HasBreasts == true) // Visible for Females
                { return SpriteDictionary.LizardsBooty[10];}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == false) // Hide for Males
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == false) // Hide for Herms (Didn't work)
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == true) // Visible for Herms
                { return SpriteDictionary.LizardsBooty[10];}
            return null; // uhhh
            }
        else return null;
    }
    
    protected Sprite PussyInSprite(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        if (actor.IsUnbirthing)
            { 
            if (actor.Unit.HasDick == false && actor.Unit.HasBreasts == true) // Visible for Females
                { return SpriteDictionary.LizardsBooty[13];}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == false) // Hide for Males
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == false) // Hide for Herms (Didn't work)
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == true) // Visible for Herms
                { return SpriteDictionary.LizardsBooty[13];}
            return null; // i dunno what's going on
            }
        if (actor.IsAnalVoring)
            { 
            if (actor.Unit.HasDick == false && actor.Unit.HasBreasts == true) // Visible for Females
                { return SpriteDictionary.LizardsBooty[11];}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == false) // Hide for Males
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == false) // Hide for Herms (Didn't work)
                { return null;}
            if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == true && Config.HermsCanUB == true) // Visible for Herms
                { return SpriteDictionary.LizardsBooty[11];}
            return null; // pls help
            }
        else return null;
    }

    protected Sprite AnusSprite(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        if (actor.IsUnbirthing)
        { return SpriteDictionary.LizardsBooty[7];}
        if (actor.IsAnalVoring)
        { return SpriteDictionary.LizardsBooty[8];}
        else return null;
    
    }
    
    protected Sprite AnusInSprite(Actor_Unit actor)
    {
        if (facingFront)
        { return null;}
        if (actor.IsUnbirthing)
        { return null;}
        if (actor.IsAnalVoring)
        { return SpriteDictionary.LizardsBooty[9];}
        else return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (facingFront == false) {return null;}
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.GetWeaponSprite() == 7)
                return null;
            return SpriteDictionary.Lizards[46 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }


protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (actor.Unit.HasDick == false)
                return null;
            if (actor.IsErect())
            {
                if (actor.PredatorComponent?.VisibleFullness < .75f)
                { Dick.layer = 18;
                    return State.GameManager.SpriteDictionary.ErectDicks[actor.Unit.DickSize];}
                else
                { Dick.layer = 12;
                    return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];}
            }
            Dick.layer = 9;
            return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
        }
        else
        {   
            if (actor.Unit.HasDick == false)
                return null;
            Dick.layer = 19;
            return SpriteDictionary.LizardsBooty[14 + actor.Unit.DickSize];
        }
    }


    protected override Sprite BallsSprite(Actor_Unit actor)
    {
    if (facingFront == true) {
        Balls.layer = 8;
    
        if (actor.Unit.HasDick == false)
            return null;
        AddOffset(Balls, 0, 0);
        
        int baseSize = actor.Unit.DickSize / 3;
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
    else {
        Balls.layer = 20;
    
        if (actor.Unit.HasDick == false)
            return null;
        AddOffset(Balls, 0, 0);
        int baseSize = actor.Unit.DickSize / 3;
        int ballOffset = actor.GetBallSize(21, 1);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return SpriteDictionary.LizardsBooty[42];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return SpriteDictionary.LizardsBooty[41];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 20)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return SpriteDictionary.LizardsBooty[40];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 19)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return SpriteDictionary.LizardsBooty[39];
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
            return SpriteDictionary.LizardsBooty[23 + combined];
        }
        return SpriteDictionary.LizardsBooty[23 + baseSize];
        }
    }

}
