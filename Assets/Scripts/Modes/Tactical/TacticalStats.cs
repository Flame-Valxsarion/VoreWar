using OdinSerializer;
using System.Collections.Generic;
using System.Text;

public class TacticalStats
{
    class RaceStats
    {
        [OdinSerialize]
        public Dictionary<Weapon, int> DamageDealtBy;
        [OdinSerialize]
        public Dictionary<Weapon, int> KillsWith;
        [OdinSerialize]
        public Dictionary<Spell, int> DamageDealtBySpell;
        [OdinSerialize]
        public Dictionary<Spell, int> KillsWithSpell;
        [OdinSerialize]
        public int Hits;
        [OdinSerialize]
        public int Misses;
        [OdinSerialize]
        public int TotalHPHealed;
        [OdinSerialize]
        public int TargetsVored;
        [OdinSerialize]
        public int TargetsRegurgitated;
        [OdinSerialize]
        public int TargetsEscaped;
        [OdinSerialize]
        public int TargetsFreed;
        [OdinSerialize]
        public int TargetsDigested;
        [OdinSerialize]
        public int AlliesEaten;

        public RaceStats()
        {
            DamageDealtBy = new Dictionary<Weapon, int>();
            KillsWith = new Dictionary<Weapon, int>();
            DamageDealtBySpell = new Dictionary<Spell, int>();
            KillsWithSpell = new Dictionary<Spell, int>();
        }
    }
    [OdinSerialize]
    public int DefenderSide;
    [OdinSerialize]
    public int AttackerSide;
    [OdinSerialize]
    int attackers;
    [OdinSerialize]
    int defenders;
    [OdinSerialize]
    int garrison;
    [OdinSerialize]
    RaceStats AttackerStats;
    [OdinSerialize]
    RaceStats DefenderStats;

    public void SetInitialUnits(int attack, int defend, int garr, int attackerSide, int defenderSide)
    {
        attackers = attack;
        defenders = defend;
        garrison = garr;
        AttackerSide = attackerSide;
        DefenderSide = defenderSide;

        AttackerStats = new RaceStats();
        DefenderStats = new RaceStats();
    }

    public string OverallSummary(double attackerStart, double attackerEnd, double defenderStart, double defenderEnd, bool attackerWon)
    {
        double remainingAttackerPct = attackerEnd / attackerStart;
        double remainingDefenderPct = defenderEnd / defenderStart;

        double attackerValueLost = attackerStart - attackerEnd;
        double defenderValueLost = defenderStart - defenderEnd;

        string lossRatio = "";
        string survivorRatio;

        if (attackerWon)
        {
            if (attackerValueLost > 4 * defenderValueLost)
                lossRatio = "Pyrrhic ";
            else if (attackerValueLost > 2 * defenderValueLost)
                lossRatio = "Costly ";
            if (remainingAttackerPct > .9)
                survivorRatio = "Flawless";
            else if (remainingAttackerPct > .6)
                survivorRatio = "Decisive";
            else if (remainingAttackerPct > .2)
                survivorRatio = "Adequate";
            else
                survivorRatio = "Marginal";
            return $"{lossRatio}{survivorRatio} Attacker Victory";
        }
        else
        {
            if (defenderValueLost > 4 * attackerValueLost)
                lossRatio = "Pyrrhic ";
            else if (defenderValueLost > 2 * attackerValueLost)
                lossRatio = "Costly ";
            if (remainingDefenderPct > .9)
                survivorRatio = "Flawless";
            else if (remainingDefenderPct > .6)
                survivorRatio = "Decisive";
            else if (remainingDefenderPct > .2)
                survivorRatio = "Adequate";
            else
                survivorRatio = "Marginal";
            return $"{lossRatio}{survivorRatio} Defender Victory";
        }
    }

    public string AttackerSummary(int remAttackers)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Attackers Remaining: {remAttackers} / {attackers}");
        GetRaceStats(sb, AttackerStats);
        return sb.ToString();
    }

    public string DefenderSummary(int remDefenders, int remGarrison)
    {
        if (remDefenders < 0)
            remDefenders = 0;
        if (remGarrison < 0)
            remGarrison = 0;
        StringBuilder sb = new StringBuilder();
        if (defenders > 0)
            sb.AppendLine($"Defenders Remaining: {remDefenders} / {defenders}");
        if (garrison > 0)
            sb.AppendLine($"Garrison Remaining: {remGarrison} / {garrison}");
        GetRaceStats(sb, DefenderStats);
        return sb.ToString();
    }

    private void GetRaceStats(StringBuilder sb, RaceStats Stats)
    {
        foreach (var item in Stats.DamageDealtBy)
        {
            sb.AppendLine($"{item.Key.Name} - Damage Dealt: {Stats.DamageDealtBy[item.Key]} - Kills: {(Stats.KillsWith.ContainsKey(item.Key) ? Stats.KillsWith[item.Key] : 0)}");
        }
        if (Stats.DamageDealtBySpell != null && Stats.KillsWithSpell != null)
        {
            foreach (var item in Stats.DamageDealtBySpell)
            {
                sb.AppendLine($"{item.Key.Name} - Damage Dealt: {Stats.DamageDealtBySpell[item.Key]} - Kills: {(Stats.KillsWithSpell.ContainsKey(item.Key) ? Stats.KillsWithSpell[item.Key] : 0)}");
            }
        }
        sb.AppendLine($"Hits: {Stats.Hits}");
        sb.AppendLine($"Misses: {Stats.Misses}");
        sb.AppendLine($"Total HP Healed: {Stats.TotalHPHealed}");
        sb.AppendLine($"Targets Vored: {Stats.TargetsVored}");
        sb.AppendLine($"Prey Freed: {Stats.TargetsFreed}");
        sb.AppendLine($"Prey Escaped: {Stats.TargetsEscaped}");
        sb.AppendLine($"Prey Regurgitated: {Stats.TargetsRegurgitated}");
        sb.AppendLine($"Prey Digested: {Stats.TargetsDigested}");
        if (Stats.AlliesEaten > 0)
            sb.AppendLine($"Allies Eaten: {Stats.AlliesEaten}");
    }

    public void RegisterKill(Weapon weapon, int attackerSide)
    {
        if (attackerSide == AttackerSide)
        {
            if (AttackerStats.KillsWith.ContainsKey(weapon))
                AttackerStats.KillsWith[weapon] += 1;
            else
                AttackerStats.KillsWith[weapon] = 1;
        }
        else
        {
            if (DefenderStats.KillsWith.ContainsKey(weapon))
                DefenderStats.KillsWith[weapon] += 1;
            else
                DefenderStats.KillsWith[weapon] = 1;
        }
    }

    internal void RegisterKill(Spell spell, int attackerSide)
    {
        if (attackerSide == AttackerSide)
        {
            if (AttackerStats.KillsWithSpell == null)
                AttackerStats.KillsWithSpell = new Dictionary<Spell, int>();
            if (AttackerStats.KillsWithSpell.ContainsKey(spell))
                AttackerStats.KillsWithSpell[spell] += 1;
            else
                AttackerStats.KillsWithSpell[spell] = 1;
        }
        else
        {
            if (DefenderStats.KillsWithSpell == null)
                DefenderStats.KillsWithSpell = new Dictionary<Spell, int>();
            if (DefenderStats.KillsWithSpell.ContainsKey(spell))
                DefenderStats.KillsWithSpell[spell] += 1;
            else
                DefenderStats.KillsWithSpell[spell] = 1;
        }
    }

    public void RegisterHit(Weapon weapon, int damage, int attackerSide)
    {
        if (attackerSide == AttackerSide)
        {
            if (AttackerStats.DamageDealtBy.ContainsKey(weapon))
                AttackerStats.DamageDealtBy[weapon] += damage;
            else
                AttackerStats.DamageDealtBy[weapon] = damage;
            AttackerStats.Hits++;
        }
        else
        {
            if (DefenderStats.DamageDealtBy.ContainsKey(weapon))
                DefenderStats.DamageDealtBy[weapon] += damage;
            else
                DefenderStats.DamageDealtBy[weapon] = damage;
            DefenderStats.Hits++;
        }
    }

    public void RegisterHit(Spell spell, int damage, int attackerSide)
    {
        if (attackerSide == AttackerSide)
        {
            if (AttackerStats.DamageDealtBySpell == null)
                AttackerStats.DamageDealtBySpell = new Dictionary<Spell, int>();
            if (AttackerStats.DamageDealtBySpell.ContainsKey(spell))
                AttackerStats.DamageDealtBySpell[spell] += damage;
            else
                AttackerStats.DamageDealtBySpell[spell] = damage;
            AttackerStats.Hits++;
        }
        else
        {
            if (DefenderStats.DamageDealtBySpell == null)
                DefenderStats.DamageDealtBySpell = new Dictionary<Spell, int>();
            if (DefenderStats.DamageDealtBySpell.ContainsKey(spell))
                DefenderStats.DamageDealtBySpell[spell] += damage;
            else
                DefenderStats.DamageDealtBySpell[spell] = damage;
            DefenderStats.Hits++;
        }
    }

    public void RegisterMiss(int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.Misses++;
        else
            DefenderStats.Misses++;
    }

    public void RegisterHealing(int amount, int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.TotalHPHealed += amount;
        else
            DefenderStats.TotalHPHealed += amount;
    }

    public void RegisterVore(int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.TargetsVored++;
        else
            DefenderStats.TargetsVored++;
    }

    public void RegisterAllyVore(int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.AlliesEaten++;
        else
            DefenderStats.AlliesEaten++;
    }

    public void RegisterEscape(int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.TargetsEscaped++;
        else
            DefenderStats.TargetsEscaped++;
    }

    public void RegisterFreed(int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.TargetsFreed++;
        else
            DefenderStats.TargetsFreed++;
    }

    public void RegisterRegurgitation(int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.TargetsRegurgitated++;
        else
            DefenderStats.TargetsRegurgitated++;
    }

    public void RegisterDigestion(int attackerSide)
    {
        if (attackerSide == AttackerSide)
            AttackerStats.TargetsDigested++;
        else
            DefenderStats.TargetsDigested++;
    }
}
