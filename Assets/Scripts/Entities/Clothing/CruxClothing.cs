using System.Collections.Generic;

namespace CruxClothing
{
    class NecklaceGold : ClothingAccessory
    {
        public NecklaceGold()
        {
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(0, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Crux[310];
            clothing2.GetSprite = (s) => SpriteDictionary.Crux[311];
            base.Configure(sprite, actor);
        }
    }

    class NecklaceCrux : ClothingAccessory
    {
        public NecklaceCrux()
        {
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(0, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Crux[312];
            clothing2.GetSprite = (s) => SpriteDictionary.Crux[313];
            base.Configure(sprite, actor);
        }
    }

    class TShirt : MainClothing
    {
        public TShirt()
        {
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[389];
            Type = 102;
            OccupiesAllSlots = false;
            coversBreasts = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetColor = (s) => ColorMap.GetClothingColor(actor.Unit.ClothingColor);

            breastSprite = null;
            if (actor.Unit.HasBreasts)
            {
                if (actor.PredatorComponent?.VisibleFullness == 0)
                {
                    if (actor.Unit.BreastSize <= 1) clothing1.GetSprite = (s) => SpriteDictionary.Crux[315];
                    else if (actor.Unit.BreastSize <= 3) clothing1.GetSprite = (s) => SpriteDictionary.Crux[316];
                    else clothing1.GetSprite = (s) => SpriteDictionary.Crux[317];
                }
                else if (actor.GetStomachSize(23) <= 4)
                {
                    if (actor.Unit.BreastSize <= 1) clothing1.GetSprite = (s) => SpriteDictionary.Crux[319];
                    else if (actor.Unit.BreastSize <= 3) clothing1.GetSprite = (s) => SpriteDictionary.Crux[320];
                    else clothing1.GetSprite = (s) => SpriteDictionary.Crux[321];
                }
                else
                {
                    if (actor.Unit.BreastSize <= 1) clothing1.GetSprite = (s) => SpriteDictionary.Crux[323];
                    else if (actor.Unit.BreastSize <= 3) clothing1.GetSprite = (s) => SpriteDictionary.Crux[324];
                    else clothing1.GetSprite = (s) => SpriteDictionary.Crux[325];
                }
            }
            else
            {
                if (actor.PredatorComponent?.VisibleFullness == 0)
                    clothing1.GetSprite = (s) => SpriteDictionary.Crux[314];
                else if (actor.GetStomachSize(23) <= 4)
                    clothing1.GetSprite = (s) => SpriteDictionary.Crux[318];
                else clothing1.GetSprite = (s) => SpriteDictionary.Crux[322];
            }

            base.Configure(sprite, actor);
        }
    }

    class NetShirt : MainClothing
    {
        public NetShirt()
        {
            maleOnly = true;
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[390];
            Type = 103;
            OccupiesAllSlots = false;
            blocksDick = false;

            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetColor = (s) => ColorMap.GetClothingColor(actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.Crux[326];
            base.Configure(sprite, actor);
        }
    }

    class RaggedBra : MainClothing
    {
        public RaggedBra()
        {
            femaleOnly = true;
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[391];
            Type = 104;
            OccupiesAllSlots = false;
            coversBreasts = false;
            blocksDick = false;

            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetColor = (s) => ColorMap.GetClothingColor(actor.Unit.ClothingColor);
            if (actor.Unit.BreastSize <= 1) clothing1.GetSprite = (s) => SpriteDictionary.Crux[327];
            else if (actor.Unit.BreastSize <= 3) clothing1.GetSprite = (s) => SpriteDictionary.Crux[328];
            else clothing1.GetSprite = (s) => SpriteDictionary.Crux[329];
            base.Configure(sprite, actor);
        }
    }

    class LabCoat : MainClothing
    {
        public LabCoat()
        {
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[397];
            Type = 105;
            OccupiesAllSlots = false;
            blocksDick = false;
            coversBreasts = false;

            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(1, null, WhiteColored);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.GetStomachSize(23) <= 5)
            {
                clothing1.layer = 14;
                if (actor.Unit.BreastSize <= 3) clothing1.GetSprite = (s) => SpriteDictionary.Crux[341];
                else clothing1.GetSprite = (s) => SpriteDictionary.Crux[344];
            }
            else if (actor.GetStomachSize(23) <= 10)
            {
                clothing1.layer = 14;
                if (actor.Unit.BreastSize <= 3) clothing1.GetSprite = (s) => SpriteDictionary.Crux[342];
                else clothing1.GetSprite = (s) => SpriteDictionary.Crux[345];
            }
            else
            {
                clothing1.layer = 11;
                if (actor.Unit.BreastSize <= 3) clothing1.GetSprite = (s) => SpriteDictionary.Crux[343];
                else clothing1.GetSprite = (s) => SpriteDictionary.Crux[346];
            }
            clothing2.GetSprite = (s) => SpriteDictionary.Crux[347];

            base.Configure(sprite, actor);
        }
    }

    class Boxers1 : MainClothing
    {
        public Boxers1()
        {
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[392];
            Type = 106;
            OccupiesAllSlots = false;
            blocksDick = true;
            coversBreasts = false;
            DiscardUsesColor2 = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetColor = (s) => ColorMap.GetClothingColor(actor.Unit.ClothingColor2);
            if (actor.Unit.DickSize == -1) clothing1.GetSprite = (s) => SpriteDictionary.Crux[330];
            else if (actor.Unit.DickSize <= 2) clothing1.GetSprite = (s) => SpriteDictionary.Crux[331];
            else if (actor.Unit.DickSize <= 5) clothing1.GetSprite = (s) => SpriteDictionary.Crux[332];
            else clothing1.GetSprite = (s) => SpriteDictionary.Crux[333];
            base.Configure(sprite, actor);
        }
    }

    class Boxers2 : MainClothing
    {
        public Boxers2()
        {
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[393];
            Type = 107;
            OccupiesAllSlots = false;
            blocksDick = true;
            coversBreasts = false;
            DiscardUsesColor2 = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetColor = (s) => ColorMap.GetClothingColor(actor.Unit.ClothingColor2);
            if (actor.Unit.DickSize == -1) clothing1.GetSprite = (s) => SpriteDictionary.Crux[334];
            else if (actor.Unit.DickSize <= 2) clothing1.GetSprite = (s) => SpriteDictionary.Crux[335];
            else if (actor.Unit.DickSize <= 5) clothing1.GetSprite = (s) => SpriteDictionary.Crux[336];
            else clothing1.GetSprite = (s) => SpriteDictionary.Crux[337];
            base.Configure(sprite, actor);
        }
    }

    class CruxJeans : MainClothing
    {
        public CruxJeans()
        {
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[396];
            Type = 108;
            OccupiesAllSlots = false;
            blocksDick = true;
            coversBreasts = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Crux[340];
            base.Configure(sprite, actor);
        }
    }

    class FannyBag : MainClothing
    {
        public FannyBag()
        {
            DiscardUsesPalettes = false;
            DiscardUsesColor2 = true;
            DiscardSprite = SpriteDictionary.Crux[395];
            Type = 109;
            OccupiesAllSlots = false;
            blocksDick = false;
            coversBreasts = false;

            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetColor = (s) => ColorMap.GetClothingColor(actor.Unit.ClothingColor2);
            clothing1.GetSprite = (s) => SpriteDictionary.Crux[339];
            base.Configure(sprite, actor);
        }
    }

    class BeltBags : MainClothing
    {
        public BeltBags()
        {
            DiscardUsesPalettes = false;
            DiscardUsesColor2 = true;
            DiscardSprite = SpriteDictionary.Crux[394];
            Type = 110;
            OccupiesAllSlots = false;
            blocksDick = false;
            coversBreasts = false;

            clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetColor = (s) => ColorMap.GetClothingColor(actor.Unit.ClothingColor2);
            clothing1.GetSprite = (s) => SpriteDictionary.Crux[338];
            base.Configure(sprite, actor);
        }
    }

    class Rags : MainClothing
    {
        public Rags()
        {
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[399];
            Type = 111;
            OccupiesAllSlots = true;
            blocksDick = true;
            coversBreasts = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(14, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.layer = 14;
                DiscardSprite = SpriteDictionary.Crux[398];
                clothing1.GetSprite = (s) => SpriteDictionary.Crux[348];
                clothing2.GetSprite = null;
            }
            else
            {
                clothing1.layer = 9;
                clothing2.layer = 14;
                DiscardSprite = SpriteDictionary.Crux[399];
                clothing1.GetSprite = (s) => SpriteDictionary.Crux[349];
                clothing2.GetSprite = (s) => SpriteDictionary.Crux[350];
            }
            base.Configure(sprite, actor);
        }
    }

    class SlaveCollar : MainClothing
    {
        public SlaveCollar()
        {
            DiscardUsesPalettes = false;
            DiscardSprite = SpriteDictionary.Crux[400];
            Type = 112;
            OccupiesAllSlots = true;
            blocksDick = false;
            coversBreasts = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Crux[350];
            base.Configure(sprite, actor);
        }
    }

    static class CruxClothingTypes
    {
        internal static NecklaceGold NecklaceGold = new NecklaceGold();
        internal static NecklaceCrux NecklaceCrux = new NecklaceCrux();
        internal static TShirt TShirt = new TShirt();
        internal static NetShirt NetShirt = new NetShirt();
        internal static RaggedBra RaggedBra = new RaggedBra();
        internal static LabCoat LabCoat = new LabCoat();
        internal static Boxers1 Boxers1 = new Boxers1();
        internal static Boxers2 Boxers2 = new Boxers2();
        internal static CruxJeans CruxJeans = new CruxJeans();
        internal static FannyBag FannyBag = new FannyBag();
        internal static BeltBags BeltBags = new BeltBags();
        internal static Rags Rags = new Rags();
        internal static SlaveCollar SlaveCollar = new SlaveCollar();



        internal static List<MainClothing> All = new List<MainClothing>()
        {
            TShirt, NetShirt, RaggedBra, LabCoat, Boxers1, Boxers2, CruxJeans, FannyBag, BeltBags, Rags, SlaveCollar
        };

        internal static List<ClothingAccessory> Accessories = new List<ClothingAccessory>()
        {
            NecklaceGold, NecklaceCrux
        };
    }
}
