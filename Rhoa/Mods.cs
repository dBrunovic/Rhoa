using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Rhoa
{
    public static class Mods
    {
        private static int NumOfUniqueModsTaken = 3;

        public static List<string> FilterItemParams(List<string> allParams)
        {
            if (allParams[0].Contains("Unique"))
                return FilterUniqueParams(allParams);
            else
                return FilterRareParams(allParams);

        }

        private static List<string> FilterRareParams(List<string> allParams)
        {
            return allParams.Where(p => RareModExists(p)).ToList();
        }

        private static List<string> FilterUniqueParams(List<string> allParams)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!Mods.RareModExists(allParams[i]))
                    allParams[i] = allParams[i] + " (not adjustable)";
            }

            return allParams.Take(3).ToList();
        }

        public static string GetItemBase(List<string> itemParams)
        {
            if (itemParams[0].Contains("Rare") || itemParams[0].Contains("Unique"))
                return itemParams[2];
            else if (itemParams[0].Contains("Magic"))
            {
                return ItemTypes.GetMagicItemBase(itemParams[1]);
            }
            else if (itemParams[0].Contains("Normal"))
                return itemParams[1];
            else if (itemParams[0].Contains("Gem"))
                return "Gem";
            else
                return null;
        }

        public static int ModCount(List<string> allParams)
        {
            if (allParams[0].Contains("Unique"))
                return UniqueModCount(allParams);
            else
                return RareModCount(allParams);
        }

        public static int UniqueModCount(List<string> allParams)
        {
            return NumOfUniqueModsTaken;
        }

        public static int RareModCount(List<string> allParams)
        {
            return allParams.Where(p => RareModExists(p)).Count();
        }

        public static bool RareModExists(string mod)
        {
            foreach (var rareMod in AllRareMods)
            {
                if (mod.Contains(rareMod))
                {
                    return true;
                }
            }

            foreach (var rareMod in AllJewelMods)
            {
                if (mod.Contains(rareMod))
                {
                    return true;
                }
            }
            return false;
        }

        public static string[] GetSocketsAndLinks(string currParam)
        {
            if (String.IsNullOrEmpty(currParam))
                return null;

            var splitParam = currParam.Split(':')[1].Trim();
            int numOfSockets = 0;
            int numOfLinks = 0;
            int maxNumOfLinks = 0;
            for (int i = 0; i < splitParam.Length; i++)
            {
                if (splitParam[i] != '-' && splitParam[i] != ' ')
                {
                    numOfSockets++;
                }
            }

            var maxGroup = splitParam.Split(' ').OrderByDescending(s => s.Length).FirstOrDefault();
            numOfLinks = maxGroup.Where(p => p == '-').Count() + 1;

            return new string[2] { numOfSockets.ToString(), numOfLinks.ToString() };
        }

        public static string UrlEncodeRareMod(string param)
        {
            if (param.Contains("mods=&"))
                return param;
            int startIndex = param.IndexOf('=') + 1;
            int endIndex = param.IndexOf('&');
            string mod = param.Substring(startIndex, endIndex - startIndex);
            string encodedMod = System.Web.HttpUtility.UrlEncode(mod);
            encodedMod = encodedMod.Replace("(", "%28").Replace(")", "%29");
            return param.Replace(mod, encodedMod);

        }

        public static string ExtractModValue(string param)
        {
            Regex regex = new Regex(@"\b\d+([\.,]\d+)?");
            Match match = regex.Match(param);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return Regex.Match(param, @"\d+").Value;
            }
        }

        #region mod list
        private static List<string> AllRareMods = new List<string>(){
            "to Strength",
            "to Intelligence",
            "to Dexterity",
            "to Lightning Resistance",
            "to Fire Resistance",
            "to Cold Resistance",
            "to  Chaos Resistance",
            "to maximum Life",
            "to Evasion Rating",
            "to Armour Rating",
            "increased Movement Speed",
            "increased Attack Speed",
            "to Accuracy Rating",
            "to maximum Mana",
            "to Armour",
            "increased Projectile Speed",
            "Life Regenerated per second",
            "of Physical Attack Damage Leeched as Life",
            "of Physical Attack Damaged Leeched as Mana",
            "to Strength and Dexterity",
            "to Strength and Intelligence",
            "to Dexterity and Intelligence",
            "Adds x-x Phyisical Damage to Attacks",
            "increased Cold Damage",
            "increased Lightning Damage",
            "increased Fire Damage",
            "increased Global Critical Strike Multiplier",
            "increased Global Critical Strike Chance",
            "increased Armour and Evasion",
            "increased Armour",
            "increased Evasion",
            "increased Energy Shield",
            "increased Armour and Energy Shield",
            "increased Evasion and Energy Shield",
            "increased Rarity of Items found",
            "to all Elemental Resistances",
            "to Fire and Cold Resistances",
            "to maximum Energy Shield",
            "increased Cast Speed",
            "increased Light Radius",
            "increased Mana Regeneration Rate",
            "Mana Gained On Kill",
            "Life Gained On Kill",
            "increased Spell Damage",
            "Lightning Damage to Attacks",
            "Cold Damage to Attacks",
            "Fire Damage to Attacks",
            "increased Elemental Damage with Weapons",
            "Life gained on Kill",
            "Mana gained on Kill",
            "increased Maximum Energy Shield",
            "to all Attributes",
            "increased Stun Duration on Enemies",
            "Physical Damage to Attacks",
            "increased Stun Recovery",
            "increased Accuracy Rating",
            "to Level of Socketed Cold Gems",
            "to Level of Socketed Fire Gems",
            "to Level of Socketed Lightning Gems",
            "to Level of Socketed Melee Gems",
            "to Level of Socketed Bow Gems",
            "increased Critical Strike Chance",
            "increased Critical Strike Chance for Spells",
            "Cold Damage to Spells",
            "Fire Damage to Spells",
            "Lightning Damage to Spells",
            "reduced Attribute Requirements"
        };
        #endregion

        #region jewel mod list
        private static List<string> AllJewelMods = new List<string>(){@"
        increased Critical Strike Multiplier with Cold Skills",
        "increased Mine Laying Speed",
        "increased Armour",
        "increased Critical Strike Chance with Cold Skills",
        "increased Attack Speed with One Handed Melee Weapons",
        "increased Spell Damage while holding a Shield",
        "increased Attack Speed with Maces",
        "increased Attack Speed with Staves",
        "increased Physical Damage with Maces",
        "increased Totem Life",
        "increased Physical Damage with Two Handed Melee Weapons",
        "increased Chaos Damage",
        "increased Attack Speed while holding a Shield",
        "increased Cold Damage",
        "increased Attack Speed with Axes",
        "increased Physical Damage with Wands",
        "increased Cast Speed with Cold Skills",
        "additional Chance to Block with Staves",
        "additional Chance to Block Spells while Dual Wielding",
        "increased Cast Speed with Lightning Skills",
        "increased Mana Regeneration Rate",
        "increased maximum Mana",
        "increased Evasion Rating",
        "increased Attack Speed with Swords",
        "increased Energy Shield Recharge Rate",
        "increased Physical Damage with Bows",
        "increased Fire Damage",
        "increased Melee Physical Damage while holding a Shield",
        "increased Physical Weapon Damage while Dual Wielding",
        "additional Chance to Block Spells with Staves",
        "increased Critical Strike Chance with One Handed Melee Weapons",
        "increased Attack Speed while Dual Wielding",
        "increased Trap Throwing Speed",
        "increased Lightning Damage",
        "of Physical Attack Damage Leeched as Life",
        "increased Critical Strike Chance with Fire Skills",
        "increased Critical Strike Multiplier with Fire Skills",
        "increased Attack Speed with Wands",
        "increased Physical Damage with Staves",
        "increased Damage",
        "increased Physical Damage with Daggers",
        "Minions have (8 to 12)% increased maximum Life",
        "additional Block Chance while Dual Wielding",
        "increased Critical Strike Multiplier with One Handed Melee Weapons",
        "increased Critical Strike Multiplier while Dual Wielding",
        "increased Cast Speed with Fire Skills",
        "increased Cast Speed while Dual Wielding",
        "increased Attack Speed with Claws",
        "increased Critical Strike Multiplier with Two Handed Melee Weapons",
        "increased Mine Damage",
        "increased Physical Damage with Claws",
        "faster start of Energy Shield Recharge",
        "increased Totem Damage",
        "increased Physical Damage",
        "additional Chance to Block with Shields",
        "increased maximum Energy Shield",
        "increased Physical Damage with Axes",
        "increased Attack Speed with Daggers",
        "increased Physical Damage with One Handed Melee Weapons",
        "increased Spell Damage while Dual Wielding",
        "increased Critical Strike Chance with Two Handed Melee Weapons",
        "increased Critical Strike Multiplier with Lightning Skills",
        "increased Weapon Critical Strike Chance while Dual Wielding",
        "of Physical Attack Damage Leeched as Mana",
        "increased Critical Strike Chance with Lightning Skills",
        "additional Chance to Block Spells with Shields",
        "of Absorbtion",
        "to all Attributes",
        "increased Critical Strike Chance for Spells",
        "increased Projectile Damage",
        "to Strength and Dexterity",
        "increased Attack Speed",
        "increased Area Damage",
        "chance to Ignite",
        "increased Ignite Duration on Enemies",
        "increased Melee Damage",
        "to Dexterity and Intelligence",
        "increased Accuracy Rating",
        "increased Global Critical Strike Chance",
        "increased Melee Critical Strike Multiplier",
        "to Dexterity",
        "reduced Mana Cost of Skills",
        "increased Cast Speed",
        "increased Damage over Time",
        "chance to Knock Enemies Back on hit",
        "Energy Shield gained for each Enemy hit by your Attacks",
        "chance to Freeze",
        "increased Freeze Duration on Enemies",
        "to Lightning Resistance",
        "to Fire and Lightning Resistances",
        "to Intelligence",
        "oincreased Global Critical Strike Chance",
        "increased Spell Damage",
        "to Chaos Resistance",
        "increased Global Critical Strike Multiplier",
        "increased Accuracy Rating",
        "increased Rarity of Items found",
        "increased Stun Recovery",
        "Life gained for each Enemy hit by your Attacks",
        "Minions have x to all Elemental Resistances",
        "to all Elemental Resistances",
        "Totems gain x to all Elemental Resistances",
        "to Cold and Lightning Resistances",
        "chance to Shock",
        "increased Shock Duration on Enemies",
        "increased Projectile Speed",
        "to Strength and Intelligence",		
        "to Strength",
        "increased Stun Duration on Enemies",
        "increased Critical Strike Multiplier for Spells",
        "increased Melee Critical Strike Chance",
        "increased Damage",
        "increased Attack and Cast Speed",
        "increased Critical Strike Chance with Elemental Skills",
        "to Cold Resistance",
        "to Fire Resistance",
        "increased Critical Strike Multiplier with Elemental Skills",
        "to Fire and Cold Resistances",
        "Minions have x Chance to Block",	
        "increased Physical Damage with Swords",
        "increased maximum Life",
        "increased Attack Speed with Bows",
        "increased Cast Speed while holding a Shield",
        "increased Attack Speed with Two Handed Melee Weapons",
        "increased Spell Damage while wielding a Staff",
        "increased Cast Speed while wielding a Staff",
        "Mana gained for each Enemy hit by your Attacks",
        "to all Attributes",
        "increased Critical Strike Chance for Spells",
        "increased Projectile Damage",
        "to Strength and Dexterity",
        "increased Attack Speed",
        "increased Area Damage",
        "chance to Ignite",
        "increased Ignite Duration on Enemies",
        "increased Melee Damage",
        "to Dexterity and Intelligence",
        "increased Accuracy Rating",
        "increased Global Critical Strike Chance",
        "increased Melee Critical Strike Multiplier",
        "to Dexterity",
        "reduced Mana Cost of Skills",
        "increased Cast Speed",
        "increased Damage over Time",
        "chance to Knock Enemies Back on hit",
        "Energy Shield gained for each Enemy hit by your Attacks",
        "chance to Freeze",
        "increased Freeze Duration on Enemies",
        "to Lightning Resistance",
        "to Fire and Lightning Resistances",
        "to Intelligence",
        "increased Global Critical Strike Chance",
        "increased Spell Damage",
        "to Chaos Resistance",
        "increased Global Critical Strike Multiplier",
        "increased Accuracy Rating",
        "increased Rarity of Items found",
        "increased Stun Recovery",
        "Life gained for each Enemy hit by your Attacks",
        "Minions have x to all Elemental Resistances",
        "to all Elemental Resistances",
        "Totems gain x to all Elemental Resistances",
        "to Cold and Lightning Resistances",
        "chance to Shock",
        "increased Shock Duration on Enemies",
        "increased Projectile Speed",
        "to Strength and Intelligence",
        "to Strength",
        "increased Stun Duration on Enemies",
        "increased Critical Strike Multiplier for Spells",
        "increased Melee Critical Strike Chance",
        "increased Damage",
        "Mana gained for each Enemy hit by your Attacks",
        "to all Attributes",
        "increased Critical Strike Chance for Spells",
        "increased Projectile Damage",
        "to Strength and Dexterity",
        "increased Attack Speed",
        "increased Area Damage",
        "chance to Ignite",
        "increased Ignite Duration on Enemies",
        "increased Melee Damage",
        "to Dexterity and Intelligence",
        "increased Accuracy Rating",
        "increased Global Critical Strike Chance",	
        "increased Melee Critical Strike Multiplier",
        "to Dexterity",
        "reduced Mana Cost of Skills",
        "increased Cast Speed",
        "increased Damage over Time",
        "chance to Knock Enemies Back on hit",
        "Energy Shield gained for each Enemy hit by your Attacks",
        "chance to Freeze",
        "increased Freeze Duration on Enemies",
        "to Lightning Resistance",
        "to Fire and Lightning Resistances",
        "to Intelligence",
        "increased Global Critical Strike Chance",
        "increased Spell Damage",
        "to Chaos Resistance",
        "increased Global Critical Strike Multiplier",
        "increased Accuracy Rating",
        "increased Rarity of Items found",
        "increased Stun Recovery",
        "Life gained for each Enemy hit by your Attacks",
        "to all Elemental Resistances",
        "to all Elemental Resistances",
        "to all Elemental Resistances",
        "to Cold and Lightning Resistances",
        "chance to Shock",
        "increased Shock Duration on Enemies",
        "increased Projectile Speed",
        "to Strength and Intelligence",
        "to Strength",
        "increased Stun Duration on Enemies",
        "increased Critical Strike Multiplier for Spells",
        "increased Melee Critical Strike Chance",
        "increased Damage",
        "increased Attack and Cast Speed",
        "increased Critical Strike Chance with Elemental Skills",
        "to Cold Resistance",
        "to Fire Resistance",
        "increased Critical Strike Multiplier with Elemental Skills",
        "to Fire and Cold Resistances",
        "Minions have x Chance to Block",
        "increased Attack and Cast Speed",
        "increased Critical Strike Chance with Elemental Skills",
        "to Cold Resistance",
        "to Fire Resistance",
        "increased Critical Strike Multiplier with Elemental Skills",
        "to Fire and Cold Resistances",
        "Minions have x Chance to Block"
        };
        #endregion
    }
}
