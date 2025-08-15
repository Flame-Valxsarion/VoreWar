using System.Collections.Generic;

namespace TaurusClothes
{
    class Overall : MainClothing
    {
        public Overall()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.CowClothing[12];
            Type = 81;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Config.AllowTopless && actor.PredatorComponent?.VisibleFullness > 0f)
            {
                coversBreasts = false;
                breastSprite = null;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[10];
                base.Configure(sprite, actor);
                return;
            }
            else
            {
                coversBreasts = true;
            }
            int spriteNum;
            breastSprite = null;
            if (actor.Unit.HasBreasts)
            {
                spriteNum = actor.Unit.BreastSize;
                breastSprite = State.GameManager.SpriteDictionary.CowClothing[5 + spriteNum];
            }
            else
                spriteNum = 11;

            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[spriteNum];
            base.Configure(sprite, actor);
        }
    }

    class OverallBottom : MainClothing
    {
        public OverallBottom()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.CowClothing[12];
            coversBreasts = false;
            Type = 80;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[10];
            base.Configure(sprite, actor);
        }
    }

    class Bikini : MainClothing
    {
        public Bikini()
        {
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            Type = 82;
            clothing1 = new SpriteExtraInfo(17, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts == false)
                return;


            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[16 + actor.Unit.BreastSize];
            actor.SquishedBreasts = true;
            base.Configure(sprite, actor);
        }
    }

    class LeaderOutfit : MainClothing
    {
        public LeaderOutfit()
        {
            Type = 87;
            coversBreasts = false;
            OccupiesAllSlots = true;
            leaderOnly = true;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(11, null, null);
            clothing3 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[36];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[37];
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[38 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[31 + (actor.IsAttacking ? 1 : 0)];
                clothing2.GetColor = WhiteColored;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[35];
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[33 + (actor.IsAttacking ? 1 : 0)];
            }

            base.Configure(sprite, actor);
        }
    }

    class Shirt : MainClothing
    {
        public Shirt()
        {
            coversBreasts = false;
            blocksDick = false;
            Type = 84;
            clothing1 = new SpriteExtraInfo(17, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int spriteNum;
            if (actor.Unit.HasBreasts)
            {
                spriteNum = actor.Unit.BreastSize;
            }
                spriteNum = 5;

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);

            if (actor.Unit.HasBreasts == false && actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[45];
            }
            else
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[23 + spriteNum];
            base.Configure(sprite, actor);
        }
    }

    class BikiniBottom : MainClothing
    {
        public BikiniBottom()
        {
            Type = 85;
            clothing1 = new SpriteExtraInfo(11, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(10, null, WhiteColored);
            coversBreasts = false;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasDick)
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[44];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[15];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);

            }
            else
            {
                clothing1.GetSprite = null;
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[13 + (actor.Unit.HasBreasts ? 0 : 1)];
            base.Configure(sprite, actor);
        }

    }

    class Loincloth : MainClothing
    {
        public Loincloth()
        {
            Type = 86;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            coversBreasts = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[21 + (actor.Unit.HasBreasts ? 0 : 1)];
            base.Configure(sprite, actor);
        }
    }



    class CowBell : ClothingAccessory
    {
        public CowBell()
        {
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[29 + (actor.Unit.HasBreasts ? 0 : 1)];
            base.Configure(sprite, actor);
        }
    }

    class Hat : ClothingAccessory
    {
        public Hat()
        {
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[43];
            base.Configure(sprite, actor);
        }
    }

    class HolidayHat : ClothingAccessory
    {
        public HolidayHat()
        {
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            ReqWinterHoliday = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowHoliday[0];
            base.Configure(sprite, actor);
        }
    }

    class HolidayOutfit : MainClothing
    {
        public HolidayOutfit()
        {
            Type = 0;
            coversBreasts = false;
            OccupiesAllSlots = true;
            ReqWinterHoliday = true;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, WhiteColored);
            //clothing3 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowHoliday[7];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.CowHoliday[1 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.CowHoliday[8 + (actor.IsAttacking ? 1 : 0)];
                //clothing2.GetColor = WhiteColored;
                //clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.CowClothing[6];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.CowHoliday[10 + (actor.IsAttacking ? 1 : 0)];
            }

            base.Configure(sprite, actor);
        }
    }

    static class TaurusClothingTypes
    {
        internal static Overall Overall = new Overall();
        internal static OverallBottom OverallBottom = new OverallBottom();
        internal static Shirt Shirt = new Shirt();
        internal static Bikini Bikini = new Bikini();
        internal static Hat Hat = new Hat();
        internal static HolidayHat HolidayHat = new HolidayHat();
        internal static CowBell CowBell = new CowBell();
        internal static BikiniBottom BikiniBottom = new BikiniBottom();
        internal static Loincloth Loincloth = new Loincloth();
        internal static HolidayOutfit HolidayOutfit = new HolidayOutfit();
        internal static LeaderOutfit LeaderOutfit = new LeaderOutfit();


        internal static List<MainClothing> All = new List<MainClothing>()
        {
            Overall, OverallBottom, Shirt, Bikini, BikiniBottom, Loincloth, HolidayOutfit, LeaderOutfit
        };

        //internal static List<ClothingAccessory> Accessories = new List<ClothingAccessory>()
        //{
        //    Hat, CowBell, HolidayHat
        //};

    }

}

