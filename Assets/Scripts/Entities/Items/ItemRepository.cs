using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType
{
    Mace,
    Axe,
    Bow,
    CompoundBow,
    Helmet,
    BodyArmor,
    Gauntlet,
    Gloves,
    Shoes,

    EnduringAmulet, //Tier 1 Equip Start
    ShieldRing, //Tier 1 Equip End
    BarrierRing, //Tier 2 Equip Start
    ConvergenceGem, //Tier 2 Equip End
    RangerEmblem, //Tier 3 Equip Start
    BrambleBand,//Tier 3 Equip End
    WarpStone, //Tier 4 Equip start
    GoddessPendant, //Tier 4 Equip End

    HealthPotion, //Tier 1 Equip Start
    ManaPotion, 
    HastePotion, //Tier 1 Equip End
    RejuvinationElixer, //Tier 2 Equip Start
    FrostLilyExtract,
    SparkBarkExtract,
    IchorOfDilution, //Tier 2 Equip End
    EnlargementPotion, //Tier 3 Equip Start
    PotionOfPotential,
    IchorOfErrosion, //Tier 3 Equip End
    PotionOfPower, //Tier 4 Equip Start
    OmniPotion, //Tier 4 Equip End

    //FireBomb,

    Meditate,
    CaptureNet,
    FireBall,
    PowerBolt,
    Icicle,
    LightningBolt,
    JoltCrash,
    FlameWave,
    ForcePulse,
    Bolas,
    Shield,
    Mending,
    Speed,
    Valor,
    Predation,
    IceBlast,
    Pyre,
    CrossShock,
    //Warp
    //Magic Wall
    Bloodrite,
    Trance,
    Poison,
    //Quicksand
    PreysCurse,
    Maw,
    Enlarge,
    Charm,
    SummonDoppelganger,
    Summon,
    //Raze,
    Diminishment,
    GateMaw,
    Resurrection,

}


public enum SpecialItems
{
    SeliciaWeapon,
    SuccubusWeapon,
    VisionWeapon,
    KiWeapon,
    ScorchWeapon,
    DRACOWeapon,
    ZoeyWeapon,
    ZeraWeapon,
    CierihakaWeapon,
    AurilikaWeapon,
    SalixWeapon,
    ErinWeapon,
    ErinWings,
    AbaWeapon,
    AbaArmor,
    BellaWeapon,
    SingularityWeapon,
    SingularityArmor,
    FeitWeapon,
    FeitArmor,
    OmniBuster,
    OmniLauncher,
    TaraWeapon,
    XelhildeWeapon,
    SkapaWeapon,
    TatltuaeWeapon,
    FireflyMelee,
    FireflyRange,
    NectarWeapon,
    RyanWeapon,
}


public class ItemRepository
{
    [OdinSerialize]
    List<Item> items;
    [OdinSerialize]
    List<Item> specialItems;
    [OdinSerialize]
    List<Item> monsterItems;
    [OdinSerialize]
    public Weapon Claws;
    [OdinSerialize]
    public Weapon Bite;

    [OdinSerialize]
    List<Item> AllItems;

    public ItemRepository()
    {
        Claws = new Weapon("Claw", "innate claws", 0, 0, 2, 1);
        Bite = new Weapon("Bite", "bite attack", 0, 0, 4, 1);
        items = new List<Item>
        {
            new Weapon(name:"Mace",description:"Moderate melee weapon", cost:4, graphic:0, damage:4, range:1, accuracyModifier: 1.25f ),
            new Weapon(name:"Axe", description:"Strong melee weapon", cost:12, graphic:2, damage:8, range:1, accuracyModifier: 1),
            new Weapon(name:"Simple Bow", description:"Ranged weapon", cost:6, graphic:4, damage:4, range:5, accuracyModifier: 1.25f),
            new Weapon(name:"Compound Bow", description:"Advanced Ranged weapon", cost:12, graphic:6, damage:6, range:7, accuracyModifier: 1),
            new Accessory(name:"Helmet", description:"+8 agility", cost:6, changedStat:(int)Stat.Agility, statBonus:8 ),
            new Accessory(name:"Body Armor", description:"+6 endurance", cost:6, changedStat:(int)Stat.Endurance, statBonus:6 ),
            new Accessory(name:"Gauntlet", description:"+6 strength", cost:8, changedStat:(int)Stat.Strength, statBonus:6 ),
            new Accessory(name:"Gloves", description:"+6 dexterity", cost:10, changedStat:(int)Stat.Dexterity, statBonus:6 ),
            new Accessory(name:"Shoes", description:"+2 agility, +1 movement tile", cost:6, changedStat:(int)Stat.Agility, statBonus:2),

            new Equipment(name:"Enduring Amulet", description:"If below 50% HP at start of turn, heal 1% max HP. Five uses per battle.", cost:7, tier:1, type: EquipmentType.RechargeStrategy, uses: 5, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.OnTacticalTurnStart] = (x,y,z) => {if(((Actor_Unit)x).Unit.HealthPct <= 0.5f){((Actor_Unit)x).Unit.HealPercentage(0.01f); }; return true; },
            }),
            new Equipment(name:"Shield Ring", description:"Before taking damage, grants 5 barrier, three uses, three turn cooldown", cost:6, tier:1, type: EquipmentType.RechargeTactical, uses: 3, itemCD:3, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.WhenRangedHit] = (x,y,z) => {((Actor_Unit)x).Unit.RestoreBarrier(5); return true; },
                [EquipmentActivator.WhenMeleeHit] = (x,y,z) => {((Actor_Unit)x).Unit.RestoreBarrier(5); return true; }
            }),
            new Equipment(name:"Barrier Ring", description:"Provides +10 barrier at start of battle, grants +2 barrier at start of turn if below 10", cost:15, tier:2, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.OnTacticalBattleStart] = (x,y,z) => {((Actor_Unit) x).Unit.RestoreBarrier(10); return true;},
                [EquipmentActivator.OnTacticalTurnStart] = (x,y,z) => EquipmentFunctions.UseEquipmentBarrierRing(((Actor_Unit)x).Unit),
            }),
            new Equipment(name:"Convergence Gem", description:"When casting a spell, heals 5 HP. When taking damage, restores 5 mana.", cost:15, tier:2, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.OnDamage] = (x,y,z) => {((Actor_Unit)x).Unit.RestoreMana(5); return true; },
                [EquipmentActivator.OnSpellCast] = (x,y,z) => {((Actor_Unit)x).Unit.Heal(5); return true; },
            }),
            new Equipment(name:"Ranger Emblem", description:"Once per turn, grants +1 MP when missing a ranged attack.", cost:20, tier:3, type: EquipmentType.RechargeTactical, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.OnRangedMiss] = (x,y,z) => {((Actor_Unit)x).Movement++; return true; }
            }),
            new Equipment(name:"Bramble Band", description:"When this unit is hit by a melee attack, the attacker suffer 10% of this unit's edurance as damage.", cost:20, tier:3, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.WhenMeleeHit] = (x,y,z) => EquipmentFunctions.UseEquipmentBrambleBand(((Actor_Unit)x).Unit, ((Actor_Unit)y))
            }),
            new Equipment(name:"Warp Stone", description:"If below 50% HP at start of turn, causes unit to flee after 2 turns, regardless of location, one strategic turn cooldown.", cost:50, tier:4, type: EquipmentType.RechargeStrategy, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.OnTacticalTurnStart] = (x,y,z) => EquipmentFunctions.UseEquipmentWarpStone(((Actor_Unit)x).Unit)
            }),
            new Equipment(name:"Goddess Pendant", description:"At start of battle, grant all allies in army 10 stacks of Bolstered. Every five turns, grants one respawn to unit.", cost:75, tier:4, itemCD:5, triggersCD: new bool[] {false,true}, type: EquipmentType.RechargeTactical, func: new Dictionary<EquipmentActivator, Func <object, object, object, bool>>
            {
                [EquipmentActivator.OnTacticalBattleStart] = (x,y,z) => EquipmentFunctions.UseEquipmentGoddessPendantStart((Army)y),
                [EquipmentActivator.OnTacticalTurnStart] = (x,y,z) => {((Actor_Unit)x).Unit.AddRespawns(1); return true; }
            }),

            new Potion(name:"Health Potion", description:"Heals 10 HP when used", cost:5, tier:1, negative:false ,func: (x,y) => ((Actor_Unit)x).Unit.Heal(10)),
            new Potion(name:"Mana Potion", description:"Restores 10 mana when used", cost:5, tier:1, negative:false ,func: (x,y) => ((Actor_Unit)x).Unit.RestoreMana(10)),
            new Potion(name:"Haste Potion", description:"Grants Fast for 2 turns, then Valor for 2 turns when used", cost:7, tier:1, negative:false ,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Fast, 1, 2, null, new StatusEffect(StatusEffectType.Valor, 1, 2))),
            new Potion(name:"Rejuvination Elixir", description:"Restores 50 mana when used. Overcapped mana restores HP instead. Reduce stats by 20% for 3 turns.", cost:10, tier:2, negative:false ,func: (x,y) => {int resto = 50; int diff = resto - ((Actor_Unit)x).Unit.MaxMana - ((Actor_Unit)x).Unit.Mana; ((Actor_Unit)x).Unit.RestoreMana(40); if(diff > 0) ((Actor_Unit)x).Unit.Heal(diff); ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Shaken,0.2f,3); }),
            new Potion(name:"Frost Lily Extract", description:"Spreads ice on tiles surrounding target. Freezes target if it hits for 3 turns.", cost:12, tier:2, negative:true ,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Frozen,1f,3), tileFunc: (a,b) => TacticalUtilities.CreateEffect((Vec2i)a, TileEffectType.IcePatch, 1, 2, 5)),
            new Potion(name:"Spark Bark Extract", description:"Spreads Fire on tiles surrounding target. Snares target if it hits for 3 turns.", cost:12, tier:2, negative:true ,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Snared,1f,3), tileFunc: (a,b) => TacticalUtilities.CreateEffect((Vec2i)a, TileEffectType.Fire, 1, 2, 5)),
            new Potion(name:"Ichor Of Dilution", description:"Reduces Digestion damage of target for 4 turns.", cost:10, tier:2, negative:true ,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Diluted,0.2f,4)),
            new Potion(name:"Enlargement Elixir", description:"Grants Enlarged for 3 turns, causes Lethargy for 3 turns after the effect ends.", cost:10, tier:3, negative:false ,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Enlarged, 1, 3, null, new StatusEffect(StatusEffectType.Lethargy, 4, 3))),
            new Potion(name:"Potion Of Potential", description:"Race changes to 'Morph Race' for 3 turns.", cost:15, tier:3, negative:false ,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Morphed, 1, 3)),
            new Potion(name:"Ichor Of Errosion", description:"Inflicts Errosion for 3 turns, increasing damage taken.", cost:15, tier:3, negative:true,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Errosion, 2, 3)),
            new Potion(name:"Potion Of Power", description:"Target becomes Empowered for 5 turns, causes Sleep for 1 turn after the effect ends", cost:25, tier:4, negative:false ,func: (x,y) => ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Empowered, 0.5f, 5, null, new StatusEffect(StatusEffectType.Sleeping, 1, 1))),
            new Potion(name:"OmniPotion", description:"Grants Shield, Mending, Fast, Valor, and Predation for 5 turns.", cost:25, tier:4, negative:false ,func: (x,y) =>  
            { ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Shielded, .25f, 5); ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Mending, 24, 5); ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Fast, 0.3f, 5); ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Valor, .25f, 5); ((Actor_Unit)x).Unit.ApplyStatusEffect(StatusEffectType.Predation, 25, 5);}), 


            //new SpellBook("Fire Bomb", "A belt of incendiary grenades", 60, 1, SpellTypes.FireBomb),

            new SpellBook("Meditate Book", "Allows the focusing of Mana mid-battle", 30, 1, SpellTypes.Meditate),
            new SpellBook("Capture Nets", "A set of large weighted nets for capturing enemies", 15, 1, SpellTypes.CaptureNet),
            new SpellBook("Fireball Book", "Allows the casting of Fireball", 30, 1, SpellTypes.Fireball),
            new SpellBook("Power Bolt Book", "Allows the casting of Power Bolt", 30, 1, SpellTypes.PowerBolt),
            new SpellBook("Icicle Book", "Allows the casting of Icicle", 30, 1, SpellTypes.Icicle),
            new SpellBook("Lightning Bolt Book", "Allows the casting of Lightning Bolt", 30, 1, SpellTypes.LightningBolt),
            new SpellBook("JoltCrash Book", "Allows the casting of Jolt Crash", 30, 1, SpellTypes.JoltCrash),
            new SpellBook("Flame Wave Book", "Allows the casting of Flame Wave", 30, 1, SpellTypes.FlameWave),
            new SpellBook("Force Pulse Book", "Allows the casting of Force Pulse", 30, 1, SpellTypes.ForcePulse),
            new SpellBook("Bolas", "A set of hunting bolas for stopping prey", 30, 1, SpellTypes.Bolas),
            new SpellBook("Shield Book", "Allows the casting of Shield", 30, 1, SpellTypes.Shield),
            new SpellBook("Mending Book", "Allows the casting of Mending", 30, 1, SpellTypes.Mending),
            new SpellBook("Speed Book", "Allows the casting of Speed", 30, 1, SpellTypes.Speed),
            new SpellBook("Valor Book", "Allows the casting of Valor", 30, 1, SpellTypes.Valor),
            new SpellBook("Predation Book", "Allows the casting of Predation", 30, 1, SpellTypes.Predation),
            new SpellBook("Prey's Hex Book", "Allows the casting of Prey's Hex", 30, 1, SpellTypes.PreysHex),
            new SpellBook("Ice Blast Book", "Allows the casting of Ice Blast", 60, 2, SpellTypes.IceBlast),
            new SpellBook("Pyre Book", "Allows the casting of Pyre", 60, 2, SpellTypes.Pyre),
            new SpellBook("CrossShock Book", "Allows the casting of Cross Shock", 60, 2, SpellTypes.CrossShock),
            //new SpellBook("Warp Book", "Allows the casting of Warp", 60, 2, SpellTypes.Warp),
            //new SpellBook("Magic Wall Book", "Allows the casting of Magic Wall", 60, 2, SpellTypes.MagicWall),           
            new SpellBook("Bloodrite Book", "Allows the casting of Bloodrite", 60, 2, SpellTypes.Bloodrite),
            new SpellBook("Trance Book", "Allows the casting of Trance", 60, 2, SpellTypes.Trance),
            new SpellBook("Poison Book", "Allows the casting of Poison", 60, 2, SpellTypes.Poison),            
            //new SpellBook("Quicksand Book", "Allows the casting of Quicksand", 90, 3, SpellTypes.Quicksand),
            new SpellBook("Prey's Curse Book", "Allows the casting of Prey's Curse", 90, 3, SpellTypes.PreysCurse),
            new SpellBook("Maw Book", "Allows the casting of Maw", 90, 3, SpellTypes.Maw),
            new SpellBook("Enlarge Book", "Allows the casting of Enlarge", 90, 3, SpellTypes.Enlarge),
            new SpellBook("Charm Book", "Allows the casting of Charm", 90, 3, SpellTypes.Charm),
            new SpellBook("Summon Doppelganger Book", "Allows the casting of Summon Doppelganger", 90, 3, SpellTypes.SummonDoppelganger),
            new SpellBook("Summon Book", "Allows the casting of Summon", 90, 3, SpellTypes.Summon),
            //new SpellBook("Raze Book", "Allows the casting of Raze", 150, 4, SpellTypes.Raze),
            new SpellBook("Diminishment Book", "Allows the casting of Diminishment", 150, 4, SpellTypes.Diminishment),
            new SpellBook("Gatemaw Book", "Allows the casting of Gatemaw", 150, 4, SpellTypes.GateMaw),
            new SpellBook("Resurrection Book", "Allows the casting of Resurrection", 150, 4, SpellTypes.Resurrection),
            new SpellBook("Reanimate Book", "Allows the casting of Reanimate", 150, 4, SpellTypes.Reanimate),
        };
        monsterItems = new List<Item>()
        {
            new Weapon(name:"Vagrant Stinger", description:"Jellyfish stinger", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Serpent Fangs", description:"Fangs", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Wyvern Claws", description:"Claws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Wyvern Matron Claws", description:"Claws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Puny Claws", description:"Puny Claws", cost:4, graphic:0, damage:2, range:1),
            new Weapon(name:"Shark Jaws", description:"Shark Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Wolf Claws", description:"Fearsome claws upon massive paws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Dark Swallower Jaws", description:"Dark Swallower Jaws", cost:4, graphic:0, damage:2, range:1),
            new Weapon(name:"Pointy Teeth", description:"Cake Jaws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Harvester Scythes", description:"Scythes", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Collector Maw", description:"Maw", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Voilin Jaws", description:"Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Bat Jaws", description:"Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Frog Tongue", description:"Tongue", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Dragon Claws", description:"Claws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Dragonfly Mandibles", description:"Mandibles", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Plant Bite", description:"Bite", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Fairy Spark", description:"Magical Attack", cost:4, graphic:0, damage:5, range:5, omniWeapon: true, magicWeapon: true),
            new Weapon(name:"Ant Mandibles", description:"Mandibles", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Gryphon Claws", description:"Gryphon Claws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Slug Slime", description:"Slug Slime", cost:4, graphic:0, damage:4, range:5, omniWeapon: true),
            new Weapon(name:"Slug Headbash", description:"Headbash", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Slug Body Slam", description:"Body Slam", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Slug Stinger", description:"Venomous Stinger", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Salamander Jaws", description:"Salamander Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Mantis Scythes", description:"Mantis Scythes", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Eastern Dragon Jaws", description:"Eastern Jaws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Catfish Jaws", description:"Catfish Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Raptor Jaws", description:"Raptor Jaws", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Warrior Ant Mandibles", description:"Warrior Ant Mandibles", cost:4, graphic:0, damage:3, range:1),
            new Weapon(name:"Gazelle Headbash", description:"Gazelle Headbash", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Earthworm Maw", description:"Earthworm Maw", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Lizard Jaws", description:"Lizard Jaws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Monitor Lizard Claws", description:"Monitor Lizard Claws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Schiwardez Jaws", description:"Schiwardez Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Terrorbird Beak", description:"Terrorbird Beak", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Dratopyr Jaws", description:"Dratopyr Jaws", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Lion Fangs", description:"Serrated and pointy (Feline) Canines", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Goodra Slug Antenna", description:"Goodra's Power Whip", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Whisp fire", description:"Whisp's FoxFire", cost:4, graphic:0, damage:5, range:5, omniWeapon: true, magicWeapon: true),
            new Weapon(name:"Horse Hooves", description:"A terribly painful kick to the gut!", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Fox Claws", description:"While smaller than say a wolf,\n these claws are equally as painful.", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Bladed Appendage", description:"The Terminids' armored exoskeleton is both sturdy and sharp enough to pierce through most contemporary armor.  And when four or five of them strike you at once.... ^v><^ ", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Orca Jaws", description:"The fearsome jaws of a killer whale", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Exploding Paw", description:"Normaly a bunny's punch would be a laughable attack, but this bunny's paws seem to explode on contact. So...", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Otachi Claws", description:"The all-powerful claws of the Otachi.", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Slime Tackle", description:"Slime attack.", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Viral Stinger", description:"A viral stinger laced with Virae Ultimae cells to infect prey.", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Viisel Claw", description:"Claw Attack", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Eevee Paw", description:"Paw Attack", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Umbreon Paw", description:"Paw Attack", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Equaleon Paw", description:"Paw Attack", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Raiju Jaws", description:"The crushing jaws of the Raiju.", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Smudger Claws", description:"Smudger Claws", cost:4, graphic:0, damage:5, range:1),
            new Weapon(name:"Space Roach Mandibles", description:"Space Roach Mandibles", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Dryad Punch", description:"Both Bark and Bite", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Earth Spike", description:"A heavy spike made of packed dirt", cost:4, graphic:0, damage:4, range:3, omniWeapon: true),
            new Weapon(name:"Torrent", description:"Raw element of water right in your face", cost:4, graphic:0, damage:4, range:5, omniWeapon: true, magicWeapon: true),
            new Weapon(name:"Fungal Claw", description:"Sharp claws of a fungal dryad", cost:4, graphic:0, damage:4, range:1),
            new Weapon(name:"Rex Jaws", description:"Chomp!", cost:4, graphic:0, damage:15, range:1),
            new Weapon(name:"Utahraptor Claws", description:"Utahraptor Claws", cost:4, graphic:0, damage:6, range:1),
            new Weapon(name:"Asterial Strike", description:"A channeled strike from a dimension that exists and doesn't", cost:4, graphic:0, damage:4, range:1),
        };

        specialItems = new List<Item>()
        {
            new Weapon(name:"Selicia's Bite", description:"Bite attack", cost:4, graphic:0, damage:10, range:1),
            new Weapon(name:"Summoned Sword", description:"Imp that drops a sword on target", cost:4, graphic:0, damage:4, range:3, omniWeapon:true, lockedItem:true),
            new Weapon(name:"Vision's Bite", description:"Bite Attack", cost:4, graphic:0, damage:12, range:1),
            new Weapon(name:"Ki's Bite", description:"Bite Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"Scorch's Bite", description:"Bite Attack", cost:4, graphic:0, damage:10, range:1),
            new Weapon(name:"DRACO's Bite", description:"Bite Attack", cost:4, graphic:0, damage:12, range:1),
            new Weapon(name:"Zoey's Limbs", description:"Fist / Tail Attack", cost:4, graphic:0, damage:8, range:1),
            new Weapon(name:"Zera's Claws", description:"Claw Attack", cost:4, graphic:0, damage:12, range:1),
            new Weapon(name:"Cierihaka's Bite", description:"Bite Attack", cost:4, graphic:0, damage:15, range:1, accuracyModifier: 1.2f),
            new Weapon(name:"Aurilika's Gohei", description:"A sacred talisman", cost:4, graphic:0, damage:4, range:1, accuracyModifier: 5f, lockedItem:true),
            new Weapon(name:"Salix's Staff", description:"A weighty magic staff", cost:4, graphic:0, damage:6, range:1, lockedItem:true),
            new Weapon(name:"Nyangel Claws", description:"Near-useless cat scratch!", cost:4, graphic:0, damage:8, range:1),
            new Accessory(name:"Nyangel Wings", description:"The softest, most delicious looking pair of wings you ever did see!\n+5 Willpower", cost:6, changedStat:(int)Stat.Will, statBonus:5 ),
            new Weapon(name:"Grand Talons", description:"All the better to grasp you with, my dear.", cost:4, graphic:0, damage:16, range:1, lockedItem:true),
            new Accessory(name:"Grand Plating", description:"Abakhanskya is covered from head to toe in highly durable composite armor plating\n+10 Endurance", cost:6, changedStat:(int)Stat.Endurance, statBonus:10 ),
            new Weapon(name:"Bella's Staff", description:"A Shepherd's Staff", cost:4, graphic:0, damage:10, range:1, lockedItem:true, accuracyModifier:.75f),
            new Weapon(name:"Voidshift Antlers", description:"With power over dark energy, Singularity can shape the void matter of her antlers to any shape in order to attack.", cost:4, graphic:0, damage:8, range:2, lockedItem:true),
            new Accessory(name:"Comfy Sweater", description:"Singularity's favorite sweater.", cost:0, changedStat:(int)Stat.Endurance, statBonus:10 ),
            new Weapon(name:"Feit's Claws", description:"The deadly claws upon Feit's forelimbs synergize well with the wing-like feathers, allowing her to effectively glide and pin down her enemies.", cost:4, graphic:0, damage:8, range:1),
            new Accessory(name:"Feit's Talons", description:"A Draco-Raptor's talons are quite powerful, enabling these predators to quickly pounce upon distant prey.", cost:6, changedStat:(int)Stat.Endurance, statBonus:10 ),
            new Weapon(name:"Omni Buster", description:"A melee weapon made from a semi-morphic material that takes on a form best suited to it's wielder. (Dissapates upon separation from it's wielder.)", cost:4, graphic:2, damage:9, range:1, accuracyModifier:1.35f),
            new Weapon(name:"Omni Launcher", description:"A ranged weapon made from a semi-morphic material that takes on a form best suited to it's wielder. (Dissapates upon separation from it's wielder.)", cost:4, graphic:6, damage:8, range:6, accuracyModifier:.75f),
            new Weapon(name:"Tara's Claws", description:"A fearsome set of claws attatched to an equally fearsome dragon.", cost:4, graphic:0, damage:8, range:1, lockedItem:true),
            new Weapon(name:"Xelhilde's Zweihänder", description:"A cobalt zweihänder forged in Mondfeld. It's blade seems to shimer like the moon.", cost:4, graphic:0, damage:9, range:1, accuracyModifier: 1.4f, lockedItem:true),
            new Weapon(name:"Skapa's Wingtalons", description:"Skapa go pounce.", cost:4, graphic:0, damage:8, range:1, lockedItem:true),
            new Weapon(name:"Entropic Chaos", description:"Cantrip Tatltuae learned some time ago that creates pockets of intense chaotic entropy, ripping the very matter of his foes apart.", cost:4, graphic:0, damage:7, range:8, accuracyModifier: 1f, omniWeapon:true, magicWeapon: true, lockedItem:true),
            new Weapon(name:"Pilot Knife", description:"Standard issue holdout weapon for Wanzer pilots; Firefly is rather skilled with this one.", cost:4, graphic:0, damage:8, range:1, accuracyModifier: 1.4f, lockedItem:true),
            new Weapon(name:"HND15", description:"Firefly's faithful pistol.", cost:4, graphic:0, damage:5, range:7, accuracyModifier: 0.7f, lockedItem:true),
            new Weapon(name:"Nectar's Claws", description:"Large razor sharp sphinx claws, more than capable of leaving deep gashes in most foes.", cost:4, graphic:0, damage:10, range:1, accuracyModifier: 1.1f, lockedItem:true),
            new Weapon(name:"Captain's Polearm", description:"A trusty polearm that has seen many battles, and slain many more.", cost:4, graphic:0, damage:10, range:1, accuracyModifier: 1.1f, lockedItem:true),
        };


        AllItems = new List<Item>();
        AllItems.AddRange(items);
        AllItems.AddRange(monsterItems);
        AllItems.AddRange(specialItems);


    }

    public int NumItems => items.Count;

    public Item GetItem(int i) => items[i];

    public Item GetMonsterItem(int i) => monsterItems[i];

    public Item GetSpecialItem(SpecialItems i) => specialItems[(int)i];

    public Item GetItem(ItemType i) => items[(int)i];

    public ItemType GetItemType(Item item)
    {
        return (ItemType)items.IndexOf(items.Where(s => s.Name == item.Name).FirstOrDefault());
    }

    public Item GetRandomBook(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        return GetItem(GetRandomBookType(minTier, maxTier, ignoreLimit));
    }
    public Item GetRandomEquipment(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        return GetItem(GetRandomEquipmentType(minTier, maxTier, ignoreLimit));
    }
    
    public Item GetRandomPotion(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        return GetItem(GetRandomPotionType(minTier, maxTier, ignoreLimit));
    }

    public int GetRandomBookType(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        if (ignoreLimit == false)
            maxTier = UnityEngine.Mathf.Clamp(maxTier, 1, Config.MaxSpellLevelDrop);
        minTier = UnityEngine.Mathf.Clamp(minTier, 1, maxTier);
        int min = (int)ItemType.Meditate;
        int max = (int)ItemType.Resurrection;
        if (minTier == 1) min = (int)ItemType.Meditate;
        if (minTier == 2) min = (int)ItemType.IceBlast;
        if (minTier == 3) min = (int)ItemType.PreysCurse;
        if (minTier == 4) min = (int)ItemType.Diminishment;
        if (maxTier == 1) max = (int)ItemType.Predation;
        if (maxTier == 2) max = (int)ItemType.Poison;
        if (maxTier == 3) max = (int)ItemType.Summon;
        if (maxTier >= 4) max = (int)ItemType.Resurrection;

        return State.Rand.Next(min, max + 1);
    }
    public int GetRandomEquipmentType(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        if (ignoreLimit == false)
            maxTier = UnityEngine.Mathf.Clamp(maxTier, 1, Config.MaxEquipmentLevelDrop);
        minTier = UnityEngine.Mathf.Clamp(minTier, 1, maxTier);
        int min = (int)ItemType.EnduringAmulet;
        int max = (int)ItemType.GoddessPendant;
        if (minTier == 1) min = (int)ItemType.EnduringAmulet;
        if (minTier == 2) min = (int)ItemType.BarrierRing;
        if (minTier == 3) min = (int)ItemType.RangerEmblem;
        if (minTier == 4) min = (int)ItemType.WarpStone;
        if (maxTier == 1) max = (int)ItemType.ShieldRing;
        if (maxTier == 2) max = (int)ItemType.ConvergenceGem;
        if (maxTier == 3) max = (int)ItemType.BrambleBand;
        if (maxTier >= 4) max = (int)ItemType.GoddessPendant;

        return State.Rand.Next(min, max + 1);
    }
    public int GetRandomPotionType(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        if (ignoreLimit == false)
            maxTier = UnityEngine.Mathf.Clamp(maxTier, 1, Config.MaxEquipmentLevelDrop);
        minTier = UnityEngine.Mathf.Clamp(minTier, 1, maxTier);
        int min = (int)ItemType.EnduringAmulet;
        int max = (int)ItemType.GoddessPendant;
        if (minTier == 1) min = (int)ItemType.HealthPotion;
        if (minTier == 2) min = (int)ItemType.HastePotion;
        if (minTier == 3) min = (int)ItemType.RejuvinationElixer;
        if (minTier == 4) min = (int)ItemType.IchorOfDilution;
        if (maxTier == 1) max = (int)ItemType.EnlargementPotion;
        if (maxTier == 2) max = (int)ItemType.IchorOfErrosion;
        if (maxTier == 3) max = (int)ItemType.PotionOfPower;
        if (maxTier >= 4) max = (int)ItemType.OmniPotion;

        return State.Rand.Next(min, max + 1);
    }

    internal bool ItemIsUnique(Item item)
    {
        return monsterItems.Contains(item) || specialItems.Contains(item);
    }

    public bool ItemIsRangedWeapon(int i)
    {
        if (items[i] is Weapon weapon)
        {
            if (weapon.Range > 1)
                return true;
        }
        return false;
    }

    public bool ItemIsRangedWeapon(Item item)
    {
        if (item is Weapon weapon)
        {
            if (weapon.Range > 1)
                return true;
        }
        return false;
    }

    public Item GetUpgrade(Item item)
    {
        if (item == GetItem(ItemType.Mace))
            return GetItem(ItemType.Axe);
        if (item == GetItem(ItemType.Bow))
            return GetItem(ItemType.CompoundBow);
        return null;
    }

    public Item GetNewItemType(Item item)
    {
        var ret = items.Where(s => s.Name == item.Name).FirstOrDefault();
        if (ret == null)
            ret = monsterItems.Where(s => s.Name == item.Name).FirstOrDefault();
        if (ret == null)
            ret = specialItems.Where(s => s.Name == item.Name).FirstOrDefault();
        if (item.Name.Contains("Frog ???"))
            ret = monsterItems.Where(s => s.Name.Contains("Frog Tongue")).FirstOrDefault();
        if (ret == null)
        {
            if (item is Weapon weap)
            {
                if (weap.AccuracyModifier == 0)
                    weap.ResetAccuracy();
            }
            return item;
        }

        return ret;
    }

    //public Weapon[] GetWeapons()
    //{
    //    return items.Append(Claws).Where(s => s is Weapon).Select(s => s as Weapon).ToArray();
    //}

    //public Weapon[] GetMonsterWeapons()
    //{
    //    return monsterItems.Where(s => s is Weapon).Select(s => s as Weapon).ToArray();
    //}

    public List<Item> GetAllItems()
    {
        return AllItems;
    }

}
