using System;
using UnityEngine.UI;
using UnityEngine;

public class UnitCustomizer
{
    protected Actor_Unit actor;

    internal DefaultRaceData RaceData;

    protected CustomizerButton[] buttons;

    protected CustomizerPanel CustomizerUI;

    enum ButtonTypes
    {
        Skintone,
        HairColor,
        HairStyle,
        BeardStyle,
        BodyAccessoryColor,
        BodyAccessoryType,
        HeadType,
        EyeColor,
        EyeType,
        MouthType,
        BreastSize,
        CockSize,
        BodyWeight,
        ClothingType,
        Clothing2Type,
        ClothingExtraType1,
        ClothingExtraType2,
        ClothingExtraType3,
        ClothingExtraType4,
        ClothingExtraType5,
        HatType,
        ClothingAccessoryType,
        ClothingColor,
        ClothingColor2,
        ClothingColor3,
        ExtraColor1,
        ExtraColor2,
        ExtraColor3,
        ExtraColor4,
        Furry,
        TailTypes,
        FurTypes,
        EarTypes,
        BodyAccentTypes1,
        BodyAccentTypes2,
        BodyAccentTypes3,
        BodyAccentTypes4,
        BodyAccentTypes5,
        BallsSizes,
        VulvaTypes,
        AltWeaponTypes,
        LastIndex
    }

    public string HairColorLookup(int colorNumber)
    {
        switch (colorNumber)
        {
            case 0:
                return "Black";
            case 1:
                return "Cream";
            case 2:
                return "Orange";
            case 3:
                return "Blonde";
            case 4:
                return "Pink";
            case 5:
                return "Brown";
            case 6:
                return "Dark Gray";
            case 7:
                return "Yellow";
            case 8:
                return "Red";
            case 9:
                return "Maroon";
            case 10:
                return "Light Gray";
            case 11:
                return "Purple";
            case 12:
                return "Teal";
            case 13:
                return "Grape";
            case 14:
                return "Blue";
            case 15:
                return "Lime";
            case 16:
                return "Light Blue";
            case 17:
                return "Silver";
            case 18:
                return "Fire";
            case 19:
                return "Bubblegum";
            case 20:
                return "Bright Red";
            case 21:
                return "Tangerine";
            default:
                return colorNumber.ToString();
        }
    }

    public UnitCustomizer(Unit unit, CustomizerPanel UI)
    {
        CustomizerUI = UI;
        CreateButtons();
        SetUnit(unit);
    }

    public UnitCustomizer(Actor_Unit actor, CustomizerPanel UI)
    {
        CustomizerUI = UI;
        CreateButtons();
        SetActor(actor);
    }

    void SetUpNameChangeButton(Unit unit, CustomizerPanel UI)
    {
        var button = UI.DisplayedSprite.Name.GetComponent<Button>();
        if (button == null)
        {
            button = UI.DisplayedSprite.Name.gameObject.AddComponent<Button>();
            UI.DisplayedSprite.Name.gameObject.GetComponent<Text>().raycastTarget = true;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            var input = State.GameManager.CreateInputBox();
            input.SetData((s) =>
            {
                unit.Name = s;
                RefreshView();
            }, "Change", "Cancel", $"Modify name?", 100);
        });
    }

    public Unit Unit { get; private set; }

    public void SetUnit(Unit unit)
    {
        Vec2i noLoc = new Vec2i(0, 0);
        actor = new Actor_Unit(noLoc, unit);
        RaceData = Races.GetRace(unit);
        Unit = unit;
        Normal(unit);
        SetUpNameChangeButton(Unit, CustomizerUI);
        RefreshView();
    }

    public void SetActor(Actor_Unit actor)
    {
        this.actor = actor;
        RaceData = Races.GetRace(actor.Unit);
        Unit = actor.Unit;
        Normal(actor.Unit);
        SetUpNameChangeButton(Unit, CustomizerUI);
        RefreshView();
    }


    protected void Normal(Unit unit)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].Label.text = buttons[i].defaultText;
        }

        buttons[(int)ButtonTypes.BodyAccessoryType].gameObject.SetActive(RaceData.SpecialAccessoryCount > 1);

        buttons[(int)ButtonTypes.BodyAccessoryColor].gameObject.SetActive(RaceData.AccessoryColors > 1);

        buttons[(int)ButtonTypes.ExtraColor1].gameObject.SetActive(false);
        buttons[(int)ButtonTypes.EyeType].gameObject.SetActive(RaceData.EyeTypes > 1);
        buttons[(int)ButtonTypes.Skintone].gameObject.SetActive(RaceData.SkinColors > 1);
        buttons[(int)ButtonTypes.EyeColor].gameObject.SetActive(RaceData.EyeColors > 1);
        buttons[(int)ButtonTypes.HairColor].gameObject.SetActive(RaceData.HairColors > 1);
        buttons[(int)ButtonTypes.HairStyle].gameObject.SetActive(RaceData.HairStyles > 1);
        buttons[(int)ButtonTypes.BeardStyle].gameObject.SetActive(RaceData.BeardStyles > 1);
        buttons[(int)ButtonTypes.BreastSize].gameObject.SetActive(unit.HasBreasts && RaceData.BreastSizes > 1);
        buttons[(int)ButtonTypes.CockSize].gameObject.SetActive(unit.HasDick && RaceData.DickSizes > 1);
        CustomizerUI.Gender.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Nominative.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Accusative.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.PronominalPossessive.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.PredicativePossessive.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Reflexive.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Quantification.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        RefreshGenderDropdown(unit);
        RefreshPronouns(unit);
        buttons[(int)ButtonTypes.BodyWeight].gameObject.SetActive(RaceData.BodySizes > 0);
        buttons[(int)ButtonTypes.ClothingColor].gameObject.SetActive(RaceData.clothingColors > 1 && (RaceData.MainClothingTypesCount > 1 || RaceData.WaistClothingTypesCount > 1 || RaceData.ClothingHatTypesCount > 1));
        buttons[(int)ButtonTypes.ClothingType].gameObject.SetActive(RaceData.MainClothingTypesCount > 1);
        buttons[(int)ButtonTypes.Clothing2Type].gameObject.SetActive(RaceData.WaistClothingTypesCount > 1);
        buttons[(int)ButtonTypes.ClothingExtraType1].gameObject.SetActive(RaceData.ExtraMainClothing1Count > 1);
        buttons[(int)ButtonTypes.ClothingExtraType2].gameObject.SetActive(RaceData.ExtraMainClothing2Count > 1);
        buttons[(int)ButtonTypes.ClothingExtraType3].gameObject.SetActive(RaceData.ExtraMainClothing3Count > 1);
        buttons[(int)ButtonTypes.ClothingExtraType4].gameObject.SetActive(RaceData.ExtraMainClothing4Count > 1);
        buttons[(int)ButtonTypes.ClothingExtraType5].gameObject.SetActive(RaceData.ExtraMainClothing5Count > 1);
        buttons[(int)ButtonTypes.HatType].gameObject.SetActive(RaceData.ClothingHatTypesCount > 1);
        buttons[(int)ButtonTypes.ClothingAccessoryType].gameObject.SetActive(RaceData.ClothingAccessoryTypesCount > 1 || (unit.EarnedMask && Unit.Race <= Race.Goblins && Unit.Race != Race.Lizards));
        buttons[(int)ButtonTypes.ClothingColor2].gameObject.SetActive(RaceData == Races.SlimeQueen || RaceData == Races.Panthers || RaceData == Races.Imps || RaceData == Races.Goblins); //The additional clothing colors are slime queen only for the moment
        buttons[(int)ButtonTypes.ClothingColor3].gameObject.SetActive(RaceData == Races.SlimeQueen || RaceData == Races.Panthers);
        buttons[(int)ButtonTypes.MouthType].gameObject.SetActive(RaceData.MouthTypes > 1);

        buttons[(int)ButtonTypes.ExtraColor1].gameObject.SetActive(RaceData.ExtraColors1 > 0);
        buttons[(int)ButtonTypes.ExtraColor2].gameObject.SetActive(RaceData.ExtraColors2 > 0);
        buttons[(int)ButtonTypes.ExtraColor3].gameObject.SetActive(RaceData.ExtraColors3 > 0);
        buttons[(int)ButtonTypes.ExtraColor4].gameObject.SetActive(RaceData.ExtraColors4 > 0);


        buttons[(int)ButtonTypes.Furry].gameObject.SetActive(RaceData.FurCapable);
        buttons[(int)ButtonTypes.HeadType].gameObject.SetActive(RaceData.HeadTypes > 1);
        buttons[(int)ButtonTypes.TailTypes].gameObject.SetActive(RaceData.TailTypes > 1);
        buttons[(int)ButtonTypes.FurTypes].gameObject.SetActive(RaceData.FurTypes > 1);
        buttons[(int)ButtonTypes.EarTypes].gameObject.SetActive(RaceData.EarTypes > 1);
        buttons[(int)ButtonTypes.BodyAccentTypes1].gameObject.SetActive(RaceData.BodyAccentTypes1 > 1);
        buttons[(int)ButtonTypes.BodyAccentTypes2].gameObject.SetActive(RaceData.BodyAccentTypes2 > 1);
        buttons[(int)ButtonTypes.BodyAccentTypes3].gameObject.SetActive(RaceData.BodyAccentTypes3 > 1);
        buttons[(int)ButtonTypes.BodyAccentTypes4].gameObject.SetActive(RaceData.BodyAccentTypes4 > 1);
        buttons[(int)ButtonTypes.BodyAccentTypes5].gameObject.SetActive(RaceData.BodyAccentTypes5 > 1);
        buttons[(int)ButtonTypes.BallsSizes].gameObject.SetActive(RaceData.BallsSizes > 1);
        buttons[(int)ButtonTypes.VulvaTypes].gameObject.SetActive(RaceData.VulvaTypes > 1);
        buttons[(int)ButtonTypes.AltWeaponTypes].gameObject.SetActive(RaceData.BasicMeleeWeaponTypes > 1 || RaceData.BasicRangedWeaponTypes > 1 || RaceData.AdvancedMeleeWeaponTypes > 1 || RaceData.AdvancedRangedWeaponTypes > 1);

        switch (unit.Race)
        {
            case Race.Cats:
            case Race.Dogs:
            case Race.Foxes:
            case Race.Wolves:
            case Race.Bunnies:
            case Race.Tigers:
                buttons[(int)ButtonTypes.HairColor].Label.text = "Hair Color: " + HairColorLookup(Unit.HairColor);
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color: " + HairColorLookup(Unit.AccessoryColor);
                break;
            case Race.Imps:
                Imp();
                break;
            case Race.Goblins:
                Goblin();
                break;
            case Race.Lizards:
                Lizard();
                break;
            case Race.Slimes:
                Slime();
                break;
            case Race.Crypters:
                buttons[(int)ButtonTypes.ClothingColor].gameObject.SetActive(false);
                break;
            case Race.Harpies:
                Harpy();
                break;
            case Race.Lamia:
                Lamia();
                break;
            case Race.Kangaroos:
                buttons[(int)ButtonTypes.HairStyle].Label.text = "Ear Type";
                break;
            case Race.Taurus:
                buttons[(int)ButtonTypes.EyeType].Label.text = "Face Expression";
                break;
            case Race.Crux:
                Crux();
                break;
            case Race.Wyvern:
                buttons[(int)ButtonTypes.BodyWeight].Label.text = "Horn Type";
                break;
            case Race.Succubi:
                buttons[(int)ButtonTypes.ClothingColor2].gameObject.SetActive(true);
                break;
            case Race.Collectors:
                buttons[(int)ButtonTypes.ExtraColor2].Label.text = "Mouth / Dick Color";
                break;
            case Race.Asura:
                buttons[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Mask";
                break;
            case Race.Kobolds:
                buttons[(int)ButtonTypes.TailTypes].Label.text = "Preferred Facing";
                break;
            case Race.Fairies:
                buttons[(int)ButtonTypes.HatType].Label.text = "Leg Accessory";
                buttons[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Arm Accessory";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Fairy Season";
                break;
            case Race.Equines:
                buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Overtop";
                buttons[(int)ButtonTypes.ClothingExtraType2].Label.text = "Overbottom";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Skin Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Head Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Torso Color";
                break;
            case Race.Zera:
                buttons[(int)ButtonTypes.TailTypes].Label.text = "Default Facing";
                break;
            case Race.Bees:
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Exoskeleton Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                break;
            case Race.Driders:
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Spider Half Color";
                buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Spider Accent Color";
                break;
            case Race.Alraune:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Hair Accessory";
                buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Plant Colors";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Inner Petals";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Outer Petals";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Plant Base";
                break;
            case Race.Gryphons:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Style";
                break;
            case Race.Bats:
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Collar Fur Type";
                break;
            case Race.Panthers:
                Panther();
                break;
            case Race.Salamanders:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Spine Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Spine Type";
                break;
            case Race.Vipers:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Hood Type";
                buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Accent Color";
                buttons[(int)ButtonTypes.TailTypes].Label.text = "Tail Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Accent Pattern";
                break;
            case Race.Merfolk:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Head Fin";
                buttons[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Necklace / Hair Ornament";
                buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Scale Color";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Tail Fin";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Arm Fin";
                buttons[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Eyebrow";

                break;
            case Race.Avians:
                buttons[(int)ButtonTypes.HairStyle].Label.text = "Head Type";
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Beak Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Head Pattern";
                buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Core Color";
                buttons[(int)ButtonTypes.ExtraColor2].Label.text = "Feather Color";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Underwing Palettes";
                break;
            case Race.Hippos:
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Accent Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                buttons[(int)ButtonTypes.HatType].Label.text = "Headwear Type";
                buttons[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Necklace Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Left Arm Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Right Arm Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Head Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Leg Pattern";

                break;
            case Race.Mantis:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Wing Type";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Back Spines";

                break;
            case Race.Auri:
                buttons[(int)ButtonTypes.ClothingType].Label.text = "Breast Wrap";
                buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Kimono";
                buttons[(int)ButtonTypes.ClothingExtraType2].Label.text = "Socks";
                buttons[(int)ButtonTypes.ClothingExtraType3].Label.text = "Hair Ornament";
                buttons[(int)ButtonTypes.TailTypes].Label.text = "Tail Quantity";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Beast Mode";

                break;
            case Race.Catfish:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Barbel (Whisker) Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Dorsal Fin Type";

                break;
            case Race.Ants:
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Exoskeleton Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                break;
            case Race.WarriorAnts:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                break;

            case Race.Frogs:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Primary Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Secondary Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Extra Colors for Females";
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Secondary Pattern Colors";
                break;

            case Race.Gazelle:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Fur Color";
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Fur Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Horn Type (for males)";
                break;

            case Race.Sharks:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Body Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Secondary Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Nose Type";
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Secondary Pattern Colors";
                buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Hats";
                break;

            case Race.Komodos:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Head Shape";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Secondary Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Head Pattern on/off";
                break;

            case Race.FeralLizards:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Visible Teeth (during attacks)";
                break;

            case Race.Cockatrice:
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Feather Color";
                break;

            case Race.Monitors:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Pattern Colors";
                break;

            case Race.Deer:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Antlers Type";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Body Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Leg Type";
                break;

            case Race.Schiwardez:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                break;

            case Race.Terrorbird:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Head Plumage Type";
                break;

            case Race.Erin:
                buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Panties";
                buttons[(int)ButtonTypes.ClothingExtraType2].Label.text = "Stockings";
                buttons[(int)ButtonTypes.ClothingExtraType3].Label.text = "Shoes";
                break;

            case Race.Vargul:
                buttons[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Ear Type";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Head Pattern Type";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Mask On/Off (for armors)";
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Pattern Colors";
                buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Armor Details Color";
                break;

            case Race.FeralLions:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Fur Color";
                buttons[(int)ButtonTypes.HairStyle].Label.text = "Mane Style";
                buttons[(int)ButtonTypes.HairColor].Label.text = "Mane Color";
                break;
            case Race.FeralHorses:
                buttons[(int)ButtonTypes.Skintone].Label.text = "Fur Color";
                buttons[(int)ButtonTypes.HairStyle].Label.text = "Mane Style";
                buttons[(int)ButtonTypes.HairColor].Label.text = "Mane Color";
                break;
            case Race.Aabayx:
                buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Head Color";
                buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Face Paint";
                buttons[(int)ButtonTypes.EyeColor].Label.text = "Face Paint Color";
                break;
            case Race.Mice:
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Face Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Chest Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Hands/Feet Pattern";
                buttons[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Left Ear Damage";
                buttons[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Right Ear Damage";
                break;
            case Race.FeralOrcas:
                buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Dorsal Fin";
                break;
            case Race.RwuMercenaries:
                buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Race Detail";
                buttons[(int)ButtonTypes.ClothingType].Label.text = "Bonus Accessory";
                buttons[(int)ButtonTypes.EyeType].Label.text = "Race";
                buttons[(int)ButtonTypes.MouthType].Label.text = "Helmet";
                buttons[(int)ButtonTypes.HairStyle].Label.text = "Custom livery (for Sharks)";
                break;
            case Race.Taraluxia:
                Taraluxia();
                break;
        }
    }

    private void RefreshGenderDropdown(Unit unit)
    {
        if (unit.HasBreasts)
        {
            if (unit.HasDick)
            {
                if (unit.HasVagina || Config.HermsCanUB == false)
                    CustomizerUI.Gender.value = 2;
                else
                    CustomizerUI.Gender.value = 3;
            }
            else
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 1;
                else
                    CustomizerUI.Gender.value = 6;
            }
        }
        else
        {
            if (unit.HasDick)
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 4;
                else
                    CustomizerUI.Gender.value = 0;
            }
            else
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 5;
                else
                    // What in the hell--
                    CustomizerUI.Gender.value = 0;
            }
        }
        CustomizerUI.Gender.options[0].text = RaceData.CanBeGender.Contains(Gender.Male) ? "Male" : "--";
        CustomizerUI.Gender.options[1].text = RaceData.CanBeGender.Contains(Gender.Female) ? "Female" : "--";
        CustomizerUI.Gender.options[2].text = RaceData.CanBeGender.Contains(Gender.Hermaphrodite) ? "Hermaphrodite" : "--";
        CustomizerUI.Gender.options[3].text = RaceData.CanBeGender.Contains(Gender.Gynomorph) ? "Gynomorph" : "--";
        CustomizerUI.Gender.options[4].text = RaceData.CanBeGender.Contains(Gender.Maleherm) ? "Maleherm" : "--";
        CustomizerUI.Gender.options[5].text = RaceData.CanBeGender.Contains(Gender.Andromorph) ? "Andromorph" : "--";
        CustomizerUI.Gender.options[6].text = RaceData.CanBeGender.Contains(Gender.Agenic) ? "Agenic" : "--";

    }

    private void RefreshPronouns(Unit unit)
    {
        CustomizerUI.Nominative.text = unit.GetPronoun(0);
        CustomizerUI.Accusative.text = unit.Pronouns[1];
        CustomizerUI.PronominalPossessive.text = unit.Pronouns[2];
        CustomizerUI.PredicativePossessive.text = unit.Pronouns[3];
        CustomizerUI.Reflexive.text = unit.Pronouns[4];
        if (Unit.Pronouns[5] == "singular")
            CustomizerUI.Quantification.value = 0;
        else
            CustomizerUI.Quantification.value = 1;
    }

    void Panther()
    {
        buttons[(int)ButtonTypes.EyeType].Label.text = "Face Type";
        buttons[(int)ButtonTypes.ClothingColor].Label.text = "Innerwear Color";
        buttons[(int)ButtonTypes.ClothingColor2].Label.text = "Outerwear Color";
        buttons[(int)ButtonTypes.ClothingColor3].Label.text = "Clothing Accent Color";
        buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Arm Bodypaint";
        buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Shoulder Bodypaint";
        buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Feet Bodypaint";
        buttons[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Thigh Bodypaint";
        buttons[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Face Bodypaint";
        buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Bodypaint Color";
        buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Over Tops";
        buttons[(int)ButtonTypes.ClothingExtraType2].Label.text = "Over Bottoms";
        buttons[(int)ButtonTypes.ClothingExtraType3].Label.text = "Hats";
        buttons[(int)ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
        buttons[(int)ButtonTypes.ClothingExtraType5].Label.text = "Legs";
    }

    void Imp()
    {
        buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Accent Color";
        buttons[(int)ButtonTypes.ClothingType].Label.text = "Under Tops";
        buttons[(int)ButtonTypes.Clothing2Type].Label.text = "Under Bottoms";
        buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Over Bottoms";
        buttons[(int)ButtonTypes.ClothingExtraType2].Label.text = "Over Tops";
        buttons[(int)ButtonTypes.ClothingExtraType3].Label.text = "Legs";
        buttons[(int)ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
        buttons[(int)ButtonTypes.ClothingExtraType5].Label.text = "Hats";
        buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Center Pattern";
        buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Outer Pattern";
        buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Horn Type";
        buttons[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Special Type";
    }

    void Goblin()
    {
        buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Accent Color";
        buttons[(int)ButtonTypes.ClothingType].Label.text = "Under Tops";
        buttons[(int)ButtonTypes.Clothing2Type].Label.text = "Under Bottoms";
        buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Over Bottoms";
        buttons[(int)ButtonTypes.ClothingExtraType2].Label.text = "Over Tops";
        buttons[(int)ButtonTypes.ClothingExtraType3].Label.text = "Legs";
        buttons[(int)ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
        buttons[(int)ButtonTypes.ClothingExtraType5].Label.text = "Hats";
        buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Ear Type";
        buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Eyebrow Type";
    }

    void Lizard()
    {
        buttons[(int)ButtonTypes.Skintone].gameObject.SetActive(false);
        buttons[(int)ButtonTypes.HairColor].gameObject.SetActive(true);
        buttons[(int)ButtonTypes.HairColor].Label.text = "Horn Color";
        buttons[(int)ButtonTypes.HairStyle].Label.text = "Horn Style";
        buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Color";
        buttons[(int)ButtonTypes.ClothingExtraType1].Label.text = "Leg Guards";
        buttons[(int)ButtonTypes.ClothingExtraType2].Label.text = "Armlets";
        buttons[(int)ButtonTypes.HatType].Label.text = "Crown";
    }

    void Slime()
    {
        buttons[(int)ButtonTypes.HairColor].gameObject.SetActive(true);
        buttons[(int)ButtonTypes.Skintone].gameObject.SetActive(false);
        buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Color";
        buttons[(int)ButtonTypes.HairColor].Label.text = "Secondary Color";
        if (Unit.Type == UnitType.Leader)
        {
            buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Breast Covering";
            buttons[(int)ButtonTypes.ExtraColor2].Label.text = "Cock Covering";
        }

    }

    void Harpy()
    {
        buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Upper Feathers";
        buttons[(int)ButtonTypes.ExtraColor2].Label.text = "Middle Feathers";
        buttons[(int)ButtonTypes.ExtraColor3].Label.text = "Lower Feathers";
        buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Lower Feather brightness";
    }

    void Crux()
    {
        buttons[(int)ButtonTypes.ClothingColor2].Label.text = "Pack / Boxer Color";
        buttons[(int)ButtonTypes.ClothingColor2].gameObject.SetActive(true);
        buttons[(int)ButtonTypes.BodyWeight].Label.text = "Body Type";
        buttons[(int)ButtonTypes.EyeType].Label.text = "Face Expression";
        buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Primary Color";
        buttons[(int)ButtonTypes.ExtraColor2].Label.text = "Secondary Color";
        buttons[(int)ButtonTypes.ExtraColor3].Label.text = "Flesh Color";
        buttons[(int)ButtonTypes.ExtraColor4].gameObject.SetActive(Config.HideCocks == false && actor.Unit.GetBestMelee().Damage == 4);
        buttons[(int)ButtonTypes.ExtraColor4].Label.text = "Dildo Color";
        buttons[(int)ButtonTypes.FurTypes].Label.text = "Head Fluff";
        buttons[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Visible Areola";
        buttons[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Leg Stripes";
        buttons[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Arm Stripes";
    }

    void Lamia()
    {
        buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Scale Color";
        buttons[(int)ButtonTypes.ExtraColor1].Label.text = "Accent Color";
        buttons[(int)ButtonTypes.ExtraColor2].Label.text = "Tail Pattern Color";
    }

    void Taraluxia()
    {
        buttons[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Glowing Maw";
        buttons[(int)ButtonTypes.TailTypes].Label.text = "Facing Front";
    }


    internal void RefreshGenderSelector()
    {
        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.HasDick)
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 2;
                else
                    CustomizerUI.Gender.value = 3;
            }
            else
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 1;
                else
                    CustomizerUI.Gender.value = 6;
            }
        }
        else
        {
            if (actor.Unit.HasDick)
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 4;
                else
                    CustomizerUI.Gender.value = 0;
            }
            else
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 5;
                else
                    // What in the hell--
                    CustomizerUI.Gender.value = 0;
            }
        }
        if (RaceData.CanBeGender.Count <= 2)
        {

        }
    }

    internal void RefreshView()
    {
        actor.UpdateBestWeapons();
        CustomizerUI.DisplayedSprite.UpdateSprites(actor);
        CustomizerUI.DisplayedSprite.Name.text = Unit.Name;
    }

    void CreateButtons()
    {
        buttons = new CustomizerButton[(int)ButtonTypes.LastIndex];
        buttons[(int)ButtonTypes.Skintone] = CreateNewButton("Skintone", ChangeSkinTone);
        buttons[(int)ButtonTypes.HairColor] = CreateNewButton("Hair Color", ChangeHairColor);
        buttons[(int)ButtonTypes.HairStyle] = CreateNewButton("Hair Style", ChangeHairStyle);
        buttons[(int)ButtonTypes.BeardStyle] = CreateNewButton("Beard Style", ChangeBeardStyle);
        buttons[(int)ButtonTypes.BodyAccessoryColor] = CreateNewButton("Body Accessory Color", ChangeBodyAccessoryColor);
        buttons[(int)ButtonTypes.BodyAccessoryType] = CreateNewButton("Body Accessory Type", ChangeBodyAccessoryType);
        buttons[(int)ButtonTypes.HeadType] = CreateNewButton("Head Type", ChangeHeadType);
        buttons[(int)ButtonTypes.EyeColor] = CreateNewButton("Eye Color", ChangeEyeColor);
        buttons[(int)ButtonTypes.EyeType] = CreateNewButton("Eye Type", ChangeEyeType);
        buttons[(int)ButtonTypes.MouthType] = CreateNewButton("Mouth Type", ChangeMouthType);
        buttons[(int)ButtonTypes.BreastSize] = CreateNewButton("Breast Size", ChangeBreastSize);
        buttons[(int)ButtonTypes.CockSize] = CreateNewButton("Cock Size", ChangeDickSize);
        buttons[(int)ButtonTypes.BodyWeight] = CreateNewButton("Body Weight", ChangeBodyWeight);
        buttons[(int)ButtonTypes.ClothingType] = CreateNewButton("Main Clothing Type", ChangeClothingType);
        buttons[(int)ButtonTypes.Clothing2Type] = CreateNewButton("Waist Clothing Type", ChangeClothing2Type);
        buttons[(int)ButtonTypes.ClothingExtraType1] = CreateNewButton("Extra Clothing Type 1", ChangeExtraClothing1Type);
        buttons[(int)ButtonTypes.ClothingExtraType2] = CreateNewButton("Extra Clothing Type 2", ChangeExtraClothing2Type);
        buttons[(int)ButtonTypes.ClothingExtraType3] = CreateNewButton("Extra Clothing Type 3", ChangeExtraClothing3Type);
        buttons[(int)ButtonTypes.ClothingExtraType4] = CreateNewButton("Extra Clothing Type 4", ChangeExtraClothing4Type);
        buttons[(int)ButtonTypes.ClothingExtraType5] = CreateNewButton("Extra Clothing Type 5", ChangeExtraClothing5Type);
        buttons[(int)ButtonTypes.HatType] = CreateNewButton("Hat Type", ChangeClothingHatType);
        buttons[(int)ButtonTypes.ClothingAccessoryType] = CreateNewButton("Clothing Accessory Type", ChangeClothingAccesoryType);
        buttons[(int)ButtonTypes.ClothingColor] = CreateNewButton("Clothing Color", ChangeClothingColor);
        buttons[(int)ButtonTypes.ClothingColor2] = CreateNewButton("Clothing Color 2", ChangeClothingColor2);
        buttons[(int)ButtonTypes.ClothingColor3] = CreateNewButton("Clothing Color 3", ChangeClothingColor3);
        buttons[(int)ButtonTypes.ExtraColor1] = CreateNewButton("Extra Color 1", ChangeExtraColor1);
        buttons[(int)ButtonTypes.ExtraColor2] = CreateNewButton("Extra Color 2", ChangeExtraColor2);
        buttons[(int)ButtonTypes.ExtraColor3] = CreateNewButton("Extra Color 3", ChangeExtraColor3);
        buttons[(int)ButtonTypes.ExtraColor4] = CreateNewButton("Extra Color 4", ChangeExtraColor4);
        buttons[(int)ButtonTypes.Furry] = CreateNewButton("Furry", ChangeFurriness);
        buttons[(int)ButtonTypes.TailTypes] = CreateNewButton("Tail Types", ChangeTailType);
        buttons[(int)ButtonTypes.FurTypes] = CreateNewButton("Fur Types", ChangeFurType);
        buttons[(int)ButtonTypes.EarTypes] = CreateNewButton("Ear Types", ChangeEarType);
        buttons[(int)ButtonTypes.BodyAccentTypes1] = CreateNewButton("BATypes1", ChangeBodyAccentTypes1Type);
        buttons[(int)ButtonTypes.BodyAccentTypes2] = CreateNewButton("BATypes2", ChangeBodyAccentTypes2Type);
        buttons[(int)ButtonTypes.BodyAccentTypes3] = CreateNewButton("BATypes3", ChangeBodyAccentTypes3Type);
        buttons[(int)ButtonTypes.BodyAccentTypes4] = CreateNewButton("BATypes4", ChangeBodyAccentTypes4Type);
        buttons[(int)ButtonTypes.BodyAccentTypes5] = CreateNewButton("BATypes5", ChangeBodyAccentTypes5Type);
        buttons[(int)ButtonTypes.BallsSizes] = CreateNewButton("Ball Size", ChangeBallsSize);
        buttons[(int)ButtonTypes.VulvaTypes] = CreateNewButton("Vulva Type", ChangeVulvaType);
        buttons[(int)ButtonTypes.AltWeaponTypes] = CreateNewButton("Alt Weapon Sprite", ChangeWeaponSprite);
    }

    CustomizerButton CreateNewButton(string text, Action<int> action)
    {
        CustomizerButton button = UnityEngine.Object.Instantiate(CustomizerUI.ButtonPrefab, CustomizerUI.ButtonPanel.transform).GetComponent<CustomizerButton>();
        button.SetData(text, action);
        return button;
    }


    public void RandomizeUnit()
    {
        RaceData.RandomCustom(Unit);
        RefreshView();
    }


    void ChangeSkinTone(int change)
    {
        Unit.SkinColor = (RaceData.SkinColors + Unit.SkinColor + change) % RaceData.SkinColors;
        RefreshView();
    }

    void ChangeHairColor(int change)
    {
        Unit.HairColor = (RaceData.HairColors + Unit.HairColor + change) % RaceData.HairColors;
        if (Unit.Race == Race.Cats | Unit.Race == Race.Dogs | Unit.Race == Race.Bunnies | Unit.Race == Race.Wolves | Unit.Race == Race.Foxes | Unit.Race == Race.Tigers)
        {
            buttons[(int)ButtonTypes.HairColor].Label.text = "Hair Color: " + HairColorLookup(Unit.HairColor);
        }
        RefreshView();
    }

    void ChangeEyeColor(int change)
    {
        Unit.EyeColor = (RaceData.EyeColors + Unit.EyeColor + change) % RaceData.EyeColors;
        RefreshView();
    }

    void ChangeExtraColor1(int change)
    {
        Unit.ExtraColor1 = (RaceData.ExtraColors1 + Unit.ExtraColor1 + change) % RaceData.ExtraColors1;
        RefreshView();
    }
    void ChangeExtraColor2(int change)
    {
        Unit.ExtraColor2 = (RaceData.ExtraColors2 + Unit.ExtraColor2 + change) % RaceData.ExtraColors2;
        RefreshView();
    }
    void ChangeExtraColor3(int change)
    {
        Unit.ExtraColor3 = (RaceData.ExtraColors3 + Unit.ExtraColor3 + change) % RaceData.ExtraColors3;
        RefreshView();
    }
    void ChangeExtraColor4(int change)
    {
        Unit.ExtraColor4 = (RaceData.ExtraColors4 + Unit.ExtraColor4 + change) % RaceData.ExtraColors4;
        RefreshView();
    }

    void ChangeEyeType(int change)
    {
        Unit.EyeType = (RaceData.EyeTypes + Unit.EyeType + change) % RaceData.EyeTypes;
        RefreshView();
    }

    void ChangeHairStyle(int change)
    {
        Unit.HairStyle = (RaceData.HairStyles + Unit.HairStyle + change) % RaceData.HairStyles;

        RefreshView();
    }

    void ChangeBeardStyle(int change)
    {
        Unit.BeardStyle = (RaceData.BeardStyles + Unit.BeardStyle + change) % RaceData.BeardStyles;

        RefreshView();
    }


    void ChangeBodyAccessoryColor(int change)
    {
        Unit.AccessoryColor = (RaceData.AccessoryColors + Unit.AccessoryColor + change) % RaceData.AccessoryColors;
        if (Unit.Race == Race.Cats | Unit.Race == Race.Dogs | Unit.Race == Race.Bunnies | Unit.Race == Race.Wolves | Unit.Race == Race.Foxes | Unit.Race == Race.Tigers)
            buttons[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color: " + HairColorLookup(Unit.AccessoryColor);
        RefreshView();
    }

    void ChangeBodyAccessoryType(int change)
    {
        Unit.SpecialAccessoryType = (RaceData.SpecialAccessoryCount + Unit.SpecialAccessoryType + change) % RaceData.SpecialAccessoryCount;
        RefreshView();
    }

    internal void ChangeGender()
    {
        bool changedGender = false;
        if (CustomizerUI.Gender.value == 0 && Unit.GetGender() != Gender.Male)
        {
            if (RaceData.CanBeGender.Contains(Gender.Male) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(-1);
        }
        else if (CustomizerUI.Gender.value == 1 && Unit.GetGender() != Gender.Female)
        {
            if (RaceData.CanBeGender.Contains(Gender.Female) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = true;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }
        else if (CustomizerUI.Gender.value == 2 && Unit.GetGender() != Gender.Hermaphrodite)
        {
            if (RaceData.CanBeGender.Contains(Gender.Hermaphrodite) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
            Unit.HasVagina = Config.HermsCanUB;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }
        else if (CustomizerUI.Gender.value == 3 && Unit.GetGender() != Gender.Gynomorph)
        {
            if (RaceData.CanBeGender.Contains(Gender.Gynomorph) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }
        else if (CustomizerUI.Gender.value == 4 && Unit.GetGender() != Gender.Maleherm)
        {
            if (RaceData.CanBeGender.Contains(Gender.Maleherm) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
        }
        else if (CustomizerUI.Gender.value == 5 && Unit.GetGender() != Gender.Andromorph)
        {
            if (RaceData.CanBeGender.Contains(Gender.Andromorph) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = true;
            Unit.SetDefaultBreastSize(-1);
        }
        else if (CustomizerUI.Gender.value == 6 && Unit.GetGender() != Gender.Agenic)
        {
            if (RaceData.CanBeGender.Contains(Gender.Agenic) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }

        buttons[(int)ButtonTypes.BreastSize].gameObject.SetActive(Unit.HasBreasts && RaceData.BreastSizes > 1);
        buttons[(int)ButtonTypes.CockSize].gameObject.SetActive(Unit.HasDick && RaceData.DickSizes > 1);
        if (changedGender)
        {
            if (CustomizerUI.Gender.value == 0 || CustomizerUI.Gender.value == 5)
            {
                CustomizerUI.Nominative.text = "he";
                CustomizerUI.Accusative.text = "him";
                CustomizerUI.PronominalPossessive.text = "his";
                CustomizerUI.PredicativePossessive.text = "his";
                CustomizerUI.Reflexive.text = "himself";
                CustomizerUI.Quantification.value = 0;
            }
            else if (CustomizerUI.Gender.value == 1)
            {
                CustomizerUI.Nominative.text = "she";
                CustomizerUI.Accusative.text = "her";
                CustomizerUI.PronominalPossessive.text = "her";
                CustomizerUI.PredicativePossessive.text = "hers";
                CustomizerUI.Reflexive.text = "herself";
                CustomizerUI.Quantification.value = 0;
            }
            else
            {
                CustomizerUI.Nominative.text = "they";
                CustomizerUI.Accusative.text = "them";
                CustomizerUI.PronominalPossessive.text = "their";
                CustomizerUI.PredicativePossessive.text = "theirs";
                CustomizerUI.Reflexive.text = "themself";
                CustomizerUI.Quantification.value = 1;
            }
            CheckClothes(Unit);
            RefreshPronouns(Unit);
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            RefreshView();
        }

    }
    internal void CheckClothes(Unit unit)
    {
        if (RaceData.AllowedMainClothingTypes.Count > 0)
        {
            MainClothing current_cloth = RaceData.AllowedMainClothingTypes[unit.ClothingType > 0 ? unit.ClothingType - 1 : 0];
            if (!current_cloth.CanWear(Unit) && current_cloth.ExposeSwapValue() >= 0)
            {
                unit.ClothingType = current_cloth.ExposeSwapValue();
            }
        }

        if (RaceData.AllowedWaistTypes.Count > 0)
        {
            MainClothing current_cloth = RaceData.AllowedWaistTypes[unit.ClothingType2 > 0 ? unit.ClothingType2 - 1 : 0];
            if (!current_cloth.CanWear(Unit) && current_cloth.ExposeSwapValue() >= 0)
            {
                unit.ClothingType2 = current_cloth.ExposeSwapValue();
            }
        }
    }

    internal void ChangePronouns()
    {
        if (Unit.Pronouns == null)
            Unit.GeneratePronouns();
        Unit.Pronouns[0] = CustomizerUI.Nominative.text;
        Unit.Pronouns[1] = CustomizerUI.Accusative.text;
        Unit.Pronouns[2] = CustomizerUI.PronominalPossessive.text;
        Unit.Pronouns[3] = CustomizerUI.PredicativePossessive.text;
        Unit.Pronouns[4] = CustomizerUI.Reflexive.text;
        if (CustomizerUI.Quantification.value == 0)
            Unit.Pronouns[5] = "singular";
        else
            Unit.Pronouns[5] = "plural";
    }

    void ChangeBreastSize(int change)
    {
        Unit.SetDefaultBreastSize((RaceData.BreastSizes + Unit.BreastSize + change) % RaceData.BreastSizes);
        RefreshView();
    }


    void ChangeDickSize(int change)
    {
        Unit.DickSize = (RaceData.DickSizes + Unit.DickSize + change) % RaceData.DickSizes;
        RefreshView();
    }

    void ChangeMouthType(int change)
    {
        Unit.MouthType = (RaceData.MouthTypes + Unit.MouthType + change) % RaceData.MouthTypes;
        RefreshView();
    }

    void ChangeBodyWeight(int change)
    {
        if (Unit.BodySizeManuallyChanged == false)
            Unit.BodySize = Config.DefaultStartingWeight;
        Unit.BodySizeManuallyChanged = true;
        Unit.BodySize = (RaceData.BodySizes + Unit.BodySize + change) % RaceData.BodySizes;
        RefreshView();
    }

    void ChangeClothingType(int change)
    {
        int totalClothingTypes = RaceData.MainClothingTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingType = 0;
            return;
        }

        if (Unit.ClothingType > RaceData.MainClothingTypesCount)
            Unit.ClothingType = 0;

        Unit.ClothingType = (totalClothingTypes + Unit.ClothingType + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingType == 0)
                break;
            if (RaceData.AllowedMainClothingTypes[Unit.ClothingType - 1].CanWear(Unit))
                break;
            Unit.ClothingType = (totalClothingTypes + Unit.ClothingType + change) % totalClothingTypes;
        }
        RefreshView();
    }
    void ChangeClothing2Type(int change)
    {
        int totalClothingTypes = RaceData.WaistClothingTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingType2 = 0;
            return;
        }

        if (Unit.ClothingType2 > RaceData.WaistClothingTypesCount)
            Unit.ClothingType2 = 0;

        Unit.ClothingType2 = (totalClothingTypes + Unit.ClothingType2 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingType2 == 0)
                break;
            if (RaceData.AllowedWaistTypes[Unit.ClothingType2 - 1].CanWear(Unit))
                break;
            Unit.ClothingType2 = (totalClothingTypes + Unit.ClothingType2 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing1Type(int change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing1Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType1 = 0;
            return;
        }

        if (Unit.ClothingExtraType1 > RaceData.ExtraMainClothing1Count)
            Unit.ClothingExtraType1 = 0;

        Unit.ClothingExtraType1 = (totalClothingTypes + Unit.ClothingExtraType1 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType1 == 0)
                break;
            if (RaceData.ExtraMainClothing1Types[Unit.ClothingExtraType1 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType1 = (totalClothingTypes + Unit.ClothingExtraType1 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing2Type(int change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing2Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType2 = 0;
            return;
        }

        if (Unit.ClothingExtraType2 > RaceData.ExtraMainClothing2Count)
            Unit.ClothingExtraType2 = 0;

        Unit.ClothingExtraType2 = (totalClothingTypes + Unit.ClothingExtraType2 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType2 == 0)
                break;
            if (RaceData.ExtraMainClothing2Types[Unit.ClothingExtraType2 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType2 = (totalClothingTypes + Unit.ClothingExtraType2 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing3Type(int change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing3Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType3 = 0;
            return;
        }

        if (Unit.ClothingExtraType3 > RaceData.ExtraMainClothing3Count)
            Unit.ClothingExtraType3 = 0;

        Unit.ClothingExtraType3 = (totalClothingTypes + Unit.ClothingExtraType3 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType3 == 0)
                break;
            if (RaceData.ExtraMainClothing3Types[Unit.ClothingExtraType3 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType3 = (totalClothingTypes + Unit.ClothingExtraType3 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing4Type(int change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing4Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType4 = 0;
            return;
        }

        if (Unit.ClothingExtraType4 > RaceData.ExtraMainClothing4Count)
            Unit.ClothingExtraType4 = 0;

        Unit.ClothingExtraType4 = (totalClothingTypes + Unit.ClothingExtraType4 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType4 == 0)
                break;
            if (RaceData.ExtraMainClothing4Types[Unit.ClothingExtraType4 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType4 = (totalClothingTypes + Unit.ClothingExtraType4 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing5Type(int change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing5Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType5 = 0;
            return;
        }

        if (Unit.ClothingExtraType5 > RaceData.ExtraMainClothing5Count)
            Unit.ClothingExtraType5 = 0;

        Unit.ClothingExtraType5 = (totalClothingTypes + Unit.ClothingExtraType5 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType5 == 0)
                break;
            if (RaceData.ExtraMainClothing5Types[Unit.ClothingExtraType5 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType5 = (totalClothingTypes + Unit.ClothingExtraType5 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeClothingAccesoryType(int change)
    {
        int totalClothingTypes = RaceData.ClothingAccessoryTypesCount;
        if (Unit.EarnedMask && Unit.Race <= Race.Goblins && Unit.Race != Race.Lizards)
            totalClothingTypes += 1;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingAccessoryType = 0;
            return;
        }

        if (Unit.ClothingAccessoryType > totalClothingTypes)
            Unit.ClothingAccessoryType = 0;

        Unit.ClothingAccessoryType = (totalClothingTypes + Unit.ClothingAccessoryType + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingAccessoryType == 0)
                break;
            if (Unit.ClothingAccessoryType == RaceData.ClothingAccessoryTypesCount && Unit.EarnedMask)
                break;
            if (RaceData.AllowedClothingAccessoryTypes[Unit.ClothingAccessoryType - 1].CanWear(Unit))
                break;
            Unit.ClothingAccessoryType = (totalClothingTypes + Unit.ClothingAccessoryType + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeClothingHatType(int change)
    {
        int totalClothingTypes = RaceData.ClothingHatTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingHatType = 0;
            return;
        }

        if (Unit.ClothingHatType > RaceData.ClothingHatTypesCount)
            Unit.ClothingHatType = 0;

        Unit.ClothingHatType = (totalClothingTypes + Unit.ClothingHatType + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingHatType == 0)
                break;
            if (RaceData.AllowedClothingHatTypes[Unit.ClothingHatType - 1].CanWear(Unit))
                break;
            Unit.ClothingHatType = (totalClothingTypes + Unit.ClothingHatType + change) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeClothingColor(int change)
    {
        Unit.ClothingColor = (RaceData.clothingColors + Unit.ClothingColor + change) % RaceData.clothingColors;
        RefreshView();
    }
    void ChangeClothingColor2(int change)
    {
        Unit.ClothingColor2 = (RaceData.clothingColors + Unit.ClothingColor2 + change) % RaceData.clothingColors;
        RefreshView();
    }
    void ChangeClothingColor3(int change)
    {
        Unit.ClothingColor3 = (RaceData.clothingColors + Unit.ClothingColor3 + change) % RaceData.clothingColors;
        RefreshView();
    }

    void ChangeFurriness(int change)
    {
        Unit.Furry = !Unit.Furry;
        RefreshView();
    }

    //    FurTypes = 11;
    //    EarTypes = 17;
    //    BodyAccentTypes1 = 2; // Used for checking if breasts have a visible areola or not.
    //    BodyAccentTypes2 = 7; // Leg Stripe patterns.
    //    BodyAccentTypes3 = 5; // Arm Stripe patterns.
    //    BallsSizes = 3;
    //    VulvaTypes = 3;
    //    BasicMeleeWeaponTypes = 2;
    //    AdvancedMeleeWeaponTypes = 2;
    //    BasicRangedWeaponTypes = 1;
    //    AdvancedRangedWeaponTypes = 1;

    void ChangeHeadType(int change)
    {
        Unit.HeadType = (RaceData.HeadTypes + Unit.HeadType + change) % RaceData.HeadTypes;
        RefreshView();
    }

    void ChangeTailType(int change)
    {
        Unit.TailType = (RaceData.TailTypes + Unit.TailType + change) % RaceData.TailTypes;
        RefreshView();
    }

    void ChangeFurType(int change)
    {
        Unit.FurType = (RaceData.FurTypes + Unit.FurType + change) % RaceData.FurTypes;
        RefreshView();
    }

    void ChangeEarType(int change)
    {
        Unit.EarType = (RaceData.EarTypes + Unit.EarType + change) % RaceData.EarTypes;
        RefreshView();
    }

    void ChangeBodyAccentTypes1Type(int change)
    {
        Unit.BodyAccentType1 = (RaceData.BodyAccentTypes1 + Unit.BodyAccentType1 + change) % RaceData.BodyAccentTypes1;
        RefreshView();
    }

    void ChangeBodyAccentTypes2Type(int change)
    {
        Unit.BodyAccentType2 = (RaceData.BodyAccentTypes2 + Unit.BodyAccentType2 + change) % RaceData.BodyAccentTypes2;
        RefreshView();
    }

    void ChangeBodyAccentTypes3Type(int change)
    {
        Unit.BodyAccentType3 = (RaceData.BodyAccentTypes3 + Unit.BodyAccentType3 + change) % RaceData.BodyAccentTypes3;
        RefreshView();
    }

    void ChangeBodyAccentTypes4Type(int change)
    {
        Unit.BodyAccentType4 = (RaceData.BodyAccentTypes4 + Unit.BodyAccentType4 + change) % RaceData.BodyAccentTypes4;
        RefreshView();
    }

    void ChangeBodyAccentTypes5Type(int change)
    {
        Unit.BodyAccentType5 = (RaceData.BodyAccentTypes5 + Unit.BodyAccentType5 + change) % RaceData.BodyAccentTypes5;
        RefreshView();
    }

    void ChangeBallsSize(int change)
    {
        Unit.BallsSize = (RaceData.BallsSizes + Unit.BallsSize + change) % RaceData.BallsSizes;
        RefreshView();
    }

    void ChangeVulvaType(int change)
    {
        Unit.VulvaType = (RaceData.VulvaTypes + Unit.VulvaType + change) % RaceData.VulvaTypes;
        RefreshView();
    }

    void ChangeWeaponSprite(int change)
    {
        int basicMeleeTypes = RaceData.BasicMeleeWeaponTypes;
        if (Config.HideCocks == false)
            basicMeleeTypes++;
        Unit.BasicMeleeWeaponType = (basicMeleeTypes + Unit.BasicMeleeWeaponType + change) % basicMeleeTypes;

        Unit.AdvancedMeleeWeaponType = (RaceData.AdvancedMeleeWeaponTypes + Unit.AdvancedMeleeWeaponType + change) % RaceData.AdvancedMeleeWeaponTypes;
        Unit.BasicRangedWeaponType = (RaceData.BasicRangedWeaponTypes + Unit.BasicRangedWeaponType + change) % RaceData.BasicRangedWeaponTypes;
        Unit.AdvancedRangedWeaponType = (RaceData.AdvancedRangedWeaponTypes + Unit.AdvancedRangedWeaponType + change) % RaceData.AdvancedRangedWeaponTypes;
        RefreshView();
    }




}

