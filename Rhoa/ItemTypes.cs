﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhoa
{
    public static class ItemTypes
    {
        public static bool IsWeapon(string itemBase)
        {
            if (Bows.Contains(itemBase))
                return true;
            if (Claws.Contains(itemBase))
                return true;
            if (OneHandedAxes.Contains(itemBase))
                return true;
            if (OneHandedMaces.Contains(itemBase))
                return true;
            if (OneHandedSwords.Contains(itemBase))
                return true;
            if (TwoHandedAxes.Contains(itemBase))
                return true;
            if (TwoHandedMaces.Contains(itemBase))
                return true;
            if (TwoHandedSwords.Contains(itemBase))
                return true;
            if (Sceptres.Contains(itemBase))
                return true;
            if (Daggers.Contains(itemBase))
                return true;
            if (Staves.Contains(itemBase))
                return true;
            if (Wands.Contains(itemBase))
                return true;
            return false;
        }

        public static bool IsArmor(string itemBase)
        {
            if (Boots.Contains(itemBase))
                return true;
            if (Gloves.Contains(itemBase))
                return true;
            if (BodyArmors.Contains(itemBase))
                return true;
            if (Helmets.Contains(itemBase))
                return true;
            if (Shields.Contains(itemBase))
                return true;
            return false;
        }

        public static bool IsJewelery(string itemBase)
        {
            if (Belts.Contains(itemBase))
                return true;
            if (Amulets.Contains(itemBase))
                return true;
            if (Rings.Contains(itemBase))
                return true;
            return false;
        }

        public static bool IsBelt(string itemBase)
        {
            if(Belts.Contains(itemBase))
                return true;
            return false;
        }

        public static bool IsQuiver(string itemBase)
        {
            if (Quivers.Contains(itemBase))
                return true;
            return false;
        }

        public static bool IsJewel(string itemBase)
        {
            if (Jewels.Contains(itemBase))
                return true;
            return false;
        }

        public static string GetItemType(string itemBase)
        {
            if (Bows.Contains(itemBase))
                return "Bow";
            if (Claws.Contains(itemBase))
                return "Claw";
            if (OneHandedAxes.Contains(itemBase))
                return "One Hand Axe";
            if (OneHandedMaces.Contains(itemBase))
                return "One Hand Mace";
            if (OneHandedSwords.Contains(itemBase))
                return "One Hand Sword";
            if (TwoHandedAxes.Contains(itemBase))
                return "Two Hand Axe";
            if (TwoHandedMaces.Contains(itemBase))
                return "Two Hand Mace";
            if (Sceptres.Contains(itemBase))
                return "Sceptre";
            if (Daggers.Contains(itemBase))
                return "Dagger";
            if (Staves.Contains(itemBase))
                return "Staff";
            if (Belts.Contains(itemBase))
                return "Belt";
            if (Amulets.Contains(itemBase))
                return "Amulet";
            if (Rings.Contains(itemBase))
                return "Ring";
            if (Boots.Contains(itemBase))
                return "Boots";
            if (Gloves.Contains(itemBase))
                return "Gloves";
            if (BodyArmors.Contains(itemBase))
                return "Body Armour";
            if (Wands.Contains(itemBase))
                return "Wand";
            if (Helmets.Contains(itemBase))
                return "Helmet";
            if (Shields.Contains(itemBase))
                return "Shield";
            if (Jewels.Contains(itemBase))
                return "Jewel";
            if (itemBase == "Gem")
                return itemBase;
            if (Quivers.Contains(itemBase))
                return "Quiver";

            return "No type found";
        }

        public static bool IsSixLinkable(string itemType)
        {
            if (Bows.Contains(itemType))
                return true;
            if (TwoHandedAxes.Contains(itemType))
                return true;
            if (TwoHandedMaces.Contains(itemType))
                return true;
            if (TwoHandedSwords.Contains(itemType))
                return true;
            if (BodyArmors.Contains(itemType))
                return true;
            return false;
        }

        public static string GetMagicItemBase(string magicItemBase)
        {
            var baseWords = magicItemBase.Split(' ');

            foreach (var word in baseWords)
            {
                foreach (var bow in Bows) 
                {
                    if (bow.Contains(word))
                    {
                        return bow;
                    }
                };
                foreach (var quiver in Quivers) 
                {
                    if (quiver.Contains(word))
                    {
                        return quiver;
                    }
                };
                foreach (var claw in Claws) 
                {
                    if (claw.Contains(word))
                    {
                        return claw;
                    }
                };
                foreach (var dagger in Daggers) 
                {
                    if (dagger.Contains(word))
                    {
                        return dagger;
                    }
                };
                foreach (var axe in OneHandedAxes) 
                {
                    if (axe.Contains(word))
                    {
                        return axe;
                    }
                };
                foreach (var mace in OneHandedMaces)
                {
                    if (mace.Contains(word))
                    {
                        return mace;
                    }
                };
                foreach (var sword in OneHandedSwords) 
                {
                    if (sword.Contains(word))
                    {
                        return sword;
                    }
                };
                foreach (var axe in TwoHandedAxes)
                {
                    if (axe.Contains(word))
                    {
                        return axe;
                    }
                };
                foreach (var mace in TwoHandedMaces) 
                {
                    if (mace.Contains(word))
                    {
                        return mace;
                    }
                };
                foreach (var sword in TwoHandedSwords) 
                {
                    if (sword.Contains(word))
                    {
                        return sword;
                    }
                };
                foreach (var sceptre in Sceptres) 
                {
                    if (sceptre.Contains(word))
                    {
                        return sceptre;
                    }
                };
                foreach (var staff in Staves) 
                {
                    if (staff.Contains(word))
                    {
                        return staff;
                    }
                };
                foreach (var wand in Wands) 
                {
                    if (wand.Contains(word))
                    {
                        return wand;
                    }
                };
                foreach (var body in BodyArmors) 
                {
                    if (body.Contains(word))
                    {
                        return body;
                    }
                };
                foreach (var boots in Boots) 
                {
                    if (boots.Contains(word))
                    {
                        return boots;
                    }
                };
                foreach (var gloves in Gloves)
                {
                    if (gloves.Contains(word))
                    {
                        return gloves;
                    }
                };
                foreach (var helm in Helmets) 
                {
                    if (helm.Contains(word))
                    {
                        return helm;
                    }
                };
                foreach (var amulet in Amulets)
                {
                    if (amulet.Contains(word))
                    {
                        return amulet;
                    }
                };
                foreach (var ring in Rings) 
                {
                    if (ring.Contains(word))
                    {
                        return ring;
                    }
                };
                foreach (var belt in Belts) 
                {
                    if (belt.Contains(word))
                    {
                        return belt;
                    }
                };
                foreach (var shield in Shields)
                {
                    if (shield.Contains(word))
                    {
                        return shield;
                    }
                };
                foreach (var jewel in Jewels)
                {
                    if (jewel.Contains(word))
                    {
                        return jewel;
                    }
                };
            }
            return "Not found";
        }

        #region All item bases categorized
        public static List<string> Bows = new List<string>(){
            "Crude Bow",
            "Short Bow",
            "Long Bow",
            "Composite Bow",
            "Recurve Bow",
            "Bone Bow",
            "Royal Bow",
            "Death Bow",
            "Grove Bow",
            "Reflex Bow",
            "Decurve Bow",
            "Compound Bow",
            "Sniper Bow",
            "Ivory Bow",
            "Highborn Bow",
            "Decimation Bow",
            "Thicket Bow",
            "Steelwood Bow",
            "Citadel Bow",
            "Ranger Bow",
            "Assassin Bow",
            "Spine Bow",
            "Imperial Bow",
            "Harbinger Bow",
            "Maraketh Bow"
        };

        public static List<string> Quivers = new List<string>()
        {
            "Serrated Arrow Quiver",
            "Two-Point Arrow Quiver",
            "Sharktooth Arrow Quiver",
            "Blunt Arrow Quiver",
            "Fire Arrow Quiver",
            "Broadhead Arrow Quiver",
            "Penetrating Arrow Quiver",
            "Spike-Point Arrow Quiver"
        };

        public static List<string> Claws = new List<string>(){
            "Nailed Fist",
            "Sharktooth Claw",
            "Awl",
            "Cat's Paw",
            "Blinder",
            "Timeworn Claw",
            "Sparkling Claw",
            "Fright Claw",
            "Double Claw",
            "Thresher Claw",
            "Gouger",
            "Tiger's Paw",
            "Gut Ripper",
            "Prehistoric Claw",
            "Noble Claw",
            "Eagle Claw",
            "Twin Claw",
            "Great White Claw",
            "Throat Stabber",
            "Hellion's Paw",
            "Eye Gouger",
            "Vaal Claw",
            "Imperial Claw",            
            "Terror Claw",
            "Gemini Claw"
        };

        public static List<string> Daggers = new List<string>(){
            "Glass Shank",
            "Skinning Knife",
            "Carving Knife",
            "Stiletto",
            "Boot Knife",
            "Copper Kris",
            "Skean",
            "Imp Dagger",
            "Flaying Knife",
            "Prong Dagger",
            "Butcher Knife",
            "Poignard",
            "Boot Blade",
            "Golden Kris",
            "Royal Skean",
            "Fiend Dagger",
            "Trisula",
            "Gutting Knife",
            "Slaughter Knife",
            "Ambusher",            
            "Ezomyte Dagger",
            "Platinum Kris",
            "Imperial Skean",
            "Demon Dagger",
            "Sai"
        };
        public static List<string> OneHandedAxes = new List<string>(){
            "Rusted Hatchet",
            "Jade Hatchet",
            "Boarding Axe",
            "Cleaver",
            "Broad Axe",
            "Arming Axe",
            "Decorative Axe",
            "Spectral Axe",
            "Etched Hatchet",
            "Jasper Axe",
            "Tomahawk",
            "Wrist Chopper",
            "War Axe",
            "Chest Splitter",
            "Ceremonial Axe",
            "Wraith Axe",
            "Engraved Hatchet",
            "Karui Axe",
            "Siege Axe",
            "Reaver Axe",
            "Butcher Axe",
            "Vaal Hatchet",
            "Royal Axe",
            "Infernal Axe",
            "Runic Hatchet"
        };
        public static List<string> OneHandedMaces = new List<string>(){
            "Driftwood Club",
            "Tribal Club",
            "Spiked Club",
            "Stone Hammer",
            "War Hammer",
            "Bladed Mace",
            "Ceremonial Mace",
            "Dream Mace",
            "Wyrm Mace",            
            "Petrified Club",
            "Barbed Club",
            "Rock Breaker",
            "Battle Hammer",
            "Flanged Mace",
            "Ornate Mace",
            "Phantom Mace",
            "Dragon Mace",
            "Ancestral Club",
            "Tenderizer",
            "Gavel",
            "Legion Hammer",
            "Pernarch",
            "Auric Mace",
            "Nightmare Mace",
            "Behemoth Mace"
        };
        public static List<string> OneHandedSwords = new List<string>(){
            "Rusted Sword",
            "Copper Sword",            
            "Sabre",
            "Broad Sword",
            "War Sword",
            "Ancient Sword",
            "Elegant Sword",
            "Dusk Blade",
            "Hook Sword",
            "Variscite Blade",
            "Cutlass",
            "Baselard",
            "Battle Sword",
            "Elder Sword",
            "Graceful Sword",
            "Twilight Blade",
            "Grappler",
            "Gemstone Sword",
            "Corsair Sword",
            "Gladius",
            "Legion Sword",
            "Vaal Blade",
            "Eternal Sword",
            "Midnight Blade",
            "Tiger Hook",
            "Rusted Spike",
            "Whalebone Rapier",
            "Battered Foil",
            "Basket Rapier",
            "Jagged Foil",
            "Antique Rapier",
            "Elegant Foil",
            "Thorn Rapier",
            "Smallsword",
            "Wyrmbone Rapier",
            "Burnished Foil",
            "Estoc",
            "Serrated Foil",
            "Primeval Rapier",
            "Fancy Foil",
            "Apex Rapier",
            "Courtesan Sword",
            "Dragonbone Rapier",
            "Tempered Foil",
            "Pecoraro",
            "Spiraled Foil",
            "Vaal Rapier",
            "Jewelled Foil",
            "Harpy Rapier",
            "Dragoon Sword",
        };
        public static List<string> Sceptres = new List<string>(){
            "Driftwood Sceptre",
            "Darkwood Sceptre",
            "Bronze Sceptre",
            "Quartz Sceptre",
            "Iron Sceptre",
            "Ochre Sceptre",
            "Ritual Sceptre",
            "Shadow Sceptre",
            "Grinning Fetish",
            "Horned Sceptre",
            "Sekhem",
            "Crystal Sceptre",
            "Lead Sceptre",
            "Blood Sceptre",
            "Royal Sceptre",
            "Abyssal Sceptre",
            "Stag Sceptre",
            "Karui Sceptre",
            "Tyrant's Sekhem",
            "Opal Sceptre",
            "Platinum Sceptre",
            "Vaal Sceptre",
            "Carnal Sceptre",
            "Void Sceptre",
            "Sambar Sceptre"
        };
        public static List<string> Staves = new List<string>(){
            "Gnarled Branch",
            "Primitive Staff",
            "Long Staff",
            "Iron Staff",
            "Coiled Staff",
            "Royal Staff",
            "Vile Staff",
            "Crescent Staff",
            "Woodful Staff",
            "Quarterstaff",
            "Military Staff",
            "Serpentine Staff",
            "Highborn Staff",
            "Foul Staff",
            "Moon Staff",
            "Primordial Staff",
            "Lathi",
            "Ezomyte Staff",
            "Maelström Staff",
            "Imperial Staff",
            "Judgement Staff",
            "Eclipse Staff"
        };

        public static List<string> TwoHandedAxes = new List<string>(){
            "Stone Axe",
            "Jade Chopper",
            "Woodsplitter",
            "Poleaxe",
            "Double Axe",
            "Gilded Axe",
            "Shadow Axe",
            "Dagger Axe",
            "Jasper Chopper",
            "Timber Axe",
            "Headsman Axe",
            "Labrys",
            "Noble Axe",
            "Abyssal Axe",
            "Karui Chopper",
            "Talon Axe",
            "Sundering Axe",
            "Ezomyte Axe",
            "Vaal Axe",
            "Despot Axe",
            "Void Axe",
            "Fleshripper"
        };
        public static List<string> TwoHandedMaces = new List<string>()
        {
            "Driftwood Maul",
            "Tribal Maul",
            "Mallet",
            "Sledgehammer",
            "Jagged Maul",
            "Brass Maul",
            "Fright Maul",
            "Morning Star",
            "Totemic Maul",
            "Great Mallet",
            "Steelhead",
            "Spiny Maul",
            "Plated Maul",
            "Dread Maul",
            "Solar Maul",
            "Karui Maul",
            "Colossus Mallet",
            "Piledriver",
            "Meatgrinder",
            "Imperial Maul",
            "Terror Maul",
            "Coronal Maul",
            "Corroded Blade"
        };
        public static List<string> TwoHandedSwords = new List<string>()
        {
            "Longsword",
            "Bastard Sword",
            "Two-Handed Sword",
            "Etched Greatsword",
            "Ornate Sword",
            "Spectral Sword",
            "Curved Blade",
            "Butcher Sword",
            "Footman Sword",
            "Highland Blade",
            "Engraved Greatsword",
            "Tiger Sword",
            "Wraith Sword",
            "Lithe Blade",
            "Headman's Sword",
            "Reaver Sword",
            "Ezomyte Blade",
            "Vaal Greatsword",
            "Lion Sword",
            "Infernal Sword",
            "Exquisite Blade"
        };
        public static List<string> Wands = new List<string>()
        {
            "Driftwood Wand",
            "Goat's Horn",
            "Carved Wand",
            "Quartz Wand",
            "Spiraled Wand",
            "Sage Wand",
            "Pagan Wand",
            "Faun's Horn",
            "Engraved Wand",
            "Crystal Wand",
            "Serpent Wand",
            "Omen Wand",
            "Heathen Wand",
            "Demon's Horn",
            "Imbued Wand",
            "Opal Wand",
            "Tornado Wand",
            "Prophecy Wand",
            "Profane Wand"
        };

        public static List<string> BodyArmors = new List<string>()
        {
            "Plate Vest",
            "Shabby Jerkin",
            "Simple Robe",
            "Scale Vest",
            "Padded Vest",
            "Chainmail Vest",
            "Chestplate",
            "Chainmail Tunic",
            "Light Brigandine",
            "Strapped Leather",
            "Oiled Vest",
            "Silken Vest",
            "Buckskin Tunic",
            "Copper Plate",
            "Ringmail Coat",
            "Scale Doublet",
            "Scholar's Robe",
            "Padded Jacket",
            "Chainmail Doublet",
            "War Plate",
            "Infantry Brigandine",
            "Oiled Coat",
            "Wild Leather",
            "Silken Garb",
            "Scarlet Raiment",
            "Full Ringmail",
            "Full Scale",
            "Mage's Vestment",
            "Full Leather",
            "Full Plate",
            "Waxed Garb",
            "Soldier's Brigandine",
            "Silk Robe",
            "Arena Plate",
            "Full Chainmail",
            "Sun Leather",
            "Field Lamellar",
            "Cabalist Regalia",
            "Thief's Garb",
            "Holy Chainmail",
            "Bone Armour",
            "Lordly Plate",
            "Bronze Plate",
            "Eelskin Tunic",
            "Sage's Robe",
            "Wyrmscale Doublet",
            "Latticed Ringmail",
            "Quilted Jacket",
            "Frontier Leather",
            "Battle Plate",
            "Silken Wrap",
            "Hussar Brigandine",
            "Crusader Chainmail",
            "Sleek Coat",
            "Sun Plate",
            "Glorious Leather",
            "Conjurer's Vestment",
            "Full Wyrmscale",
            "Ornate Ringmail",
            "Crimson Raiment",
            "Coronal Leather",
            "Spidersilk Robe",
            "Colosseum Plate",
            "Commander's Brigandine",
            "Chain Hauberk",
            "Lacquered Garb",
            "Destroyer Regalia",
            "Majestic Plate",
            "Cutthroat's Garb",
            "Battle Lamellar",
            "Devout Chainmail",
            "Crypt Armour",
            "Golden Plate",
            "Savant's Robe",
            "Sharkskin Tunic",
            "Dragonscale Doublet",
            "Loricated Ringmail",
            "Destiny Leather",
            "Crusader Plate",
            "Necromancer Silks",
            "Sentinel Jacket",
            "Desert Brigandine",
            "Conquest Chainmail",
            "Exquisite Leather",
            "Varnished Coat",
            "Occultist's Vestment",
            "Astral Plate",
            "Full Dragonscale",
            "Elegant Ringmail",
            "Blood Raiment",
            "Widowsilk Robe",
            "Zodiac Leather",
            "Gladiator Plate",
            "General's Brigandine",
            "Saint's Hauberk",
            "Vaal Regalia",
            "Glorious Plate",
            "Assassin's Garb",
            "Sadist Garb",
            "Triumphant Lamellar",
            "Saintly Chainmail",
            "Carnal Armour",
            "Sacrificial Garb"
        };
        public static List<string> Boots = new List<string>()
        {
            "Iron Greaves",
            "Wool Shoes",
            "Rawhide Boots",
            "Chain Boots",
            "Wrapped Boots",
            "Leatherscale Boots",
            "Steel Greaves",
            "Velvet Slippers",
            "Goathide Boots",
            "Ringmail Boots",
            "Strapped Boots",
            "Ironscale Boots",
            "Deerskin Boots",
            "Silk Slippers",
            "Plated Greaves",
            "Clasped Boots",
            "Mesh Boots",
            "Bronzescale Boots",
            "Scholar Boots",
            "Reinforced Greaves",
            "Shackled Boots",
            "Nubuck Boots",
            "Steelscale Boots",
            "Riveted Boots",
            "Antique Greaves",
            "Satin Slippers",
            "Eelskin Boots",
            "Zealot Boots",
            "Trapper Boots",
            "Serpentscale Boots",
            "Sharkskin Boots",
            "Samite Slippers",
            "Ancient Greaves",
            "Ambush Boots",
            "Soldier Boots",
            "Wyrmscale Boots",
            "Conjurer Boots",
            "Goliath Greaves",
            "Shagreen Boots",
            "Carnal Boots",
            "Legion Boots",
            "Hydrascale Boots",
            "Arcanist Slippers",
            "Stealth Boots",
            "Vaal Greaves",
            "Assassin's Boots",
            "Crusader Boots",
            "Dragonscale Boots",
            "Sorcerer Boots",
            "Titan Greaves",
            "Murder Boots",
            "Slink Boots"
        };
        public static List<string> Gloves = new List<string>()
        {
            "Iron Gauntlets",
            "Rawhide Gloves",
            "Wool Gloves",
            "Fishscale Gauntlets",
            "Wrapped Mitts",
            "Chain Gloves",
            "Goathide Gloves",
            "Plated Gauntlets",
            "Velvet Gloves",
            "Golden Bracers",
            "Ironscale Gauntlets",
            "Strapped Mitts",
            "Ringmail Gloves",
            "Deerskin Gloves",
            "Bronze Gauntlets",
            "Silk Gloves",
            "Bronzescale Gauntlets",
            "Clasped Mitts",
            "Mesh Gloves",
            "Nubuck Gloves",
            "Steel Gauntlets",
            "Trapper Mitts",
            "Embroidered Gloves",
            "Steelscale Gauntlets",
            "Riveted Gloves",
            "Eelskin Gloves",
            "Antique Gauntlets",
            "Satin Gloves",
            "Zealot Gloves",
            "Serpentscale Gauntlets",
            "Sharkskin Gloves",
            "Ambush Mitts",
            "Samite Gloves",
            "Ancient Gauntlets",
            "Wyrmscale Gauntlets",
            "Carnal Mitts",
            "Soldier Gloves",
            "Goliath Gauntlets",
            "Shagreen Gloves",
            "Conjurer Gloves",
            "Legion Gloves",
            "Assassin's Mitts",
            "Hydrascale Gauntlets",
            "Arcanist Gloves",
            "Stealth Gloves",
            "Vaal Gauntlets",
            "Crusader Gloves",
            "Murder Mitts",
            "Dragonscale Gauntlets",
            "Sorcerer Gloves",
            "Titan Gauntlets",
            "Slink Gloves"
        };
        public static List<string> Helmets = new List<string>()
        {
            "Iron Hat",
            "Vine Circlet",
            "Leather Cap",
            "Battered Helm",
            "Scare Mask",
            "Rusted Coif",
            "Cone Helmet",
            "Iron Circlet",
            "Tricorne",
            "Plague Mask",
            "Soldier Helmet",
            "Sallet",
            "Torture Cage",
            "Iron Mask",
            "Barbute Helmet",
            "Leather Hood",
            "Great Helmet",
            "Visored Sallet",
            "Close Helmet",
            "Tribal Circlet",
            "Festival Mask",
            "Wolf Pelt",
            "Crusader Helmet",
            "Gilded Sallet",
            "Bone Circlet",
            "Golden Mask",
            "Gladiator Helmet",
            "Secutor Helm",
            "Aventail Helmet",
            "Raven Mask",
            "Lunaris Circlet",
            "Reaver Helmet",
            "Hunter Hood",
            "Fencer Helm",
            "Zealot Helmet",
            "Callous Mask",
            "Noble Tricorne",
            "Steel Circlet",
            "Siege Helmet",
            "Lacquered Helmet",
            "Regicide Mask",
            "Great Crown",
            "Necromancer Circlet",
            "Ursine Pelt",
            "Samite Helmet",
            "Harlequin Mask",
            "Fluted Bascinet",
            "Magistrate Crown",
            "Solaris Circlet",
            "Silken Hood",
            "Ezomyte Burgonet",
            "Vaal Mask",
            "Prophet Crown",
            "Pig-Faced Bascinet",
            "Sinner Tricorne",
            "Mind Cage",
            "Royal Burgonet",
            "Nightmare Bascinet",
            "Deicide Mask",
            "Praetor Crown",
            "Eternal Burgonet",
            "Hubris Circlet",
            "Lion Pelt"
        };
        public static List<string> Amulets = new List<string>()
        {
            "Jet Amulet",
            "Paua Amulet",
            "Coral Amulet",
            "Lapis Amulet",
            "Jade Amulet",
            "Amber Amulet",
            "Gold Amulet",
            "Turquoise Amulet",
            "Agate Amulet",
            "Citrine Amulet",
            "Onyx Amulet"
        };
        public static List<string> Rings = new List<string>()
        {
            "Iron Ring",
            "Coral Ring",
            "Paua Ring",
            "Sapphire Ring",
            "Topaz Ring",
            "Ruby Ring",
            "Gold Ring",
            "Golden Hoop",
            "Jet Ring",
            "Two-Stone Ring",
            "Two-Stone Ring",
            "Two-Stone Ring",
            "Moonstone Ring",
            "Diamond Ring",
            "Prismatic Ring",
            "Amethyst Ring",
            "Unset Ring"
        };
        public static List<string> Belts = new List<string>()
        {
            "Chain Belt",
            "Rustic Sash",
            "Heavy Belt",
            "Leather Belt",
            "Golden Obi",
            "Cloth Belt",
            "Studded Belt"
        };

        public static List<string> Shields = new List<string>()
        {
            "Splintered Tower Shield",
            "Goathide Buckler",
            "Twig Spirit Shield",
            "Rotted Round Shield",
            "Spiked Bundle",
            "Corroded Tower Shield",
            "Plank Kite Shield",
            "Pine Buckler",
            "Yew Spirit Shield",
            "Rawhide Tower Shield",
            "Fir Round Shield",
            "Driftwood Spiked Shield",
            "Linden Kite Shield",
            "Bone Spirit Shield",
            "Painted Buckler",
            "Cedar Tower Shield",
            "Studded Round Shield",
            "Alloyed Spike Shield",
            "Reinforced Kite Shield",
            "Hammered Buckler",
            "Tarnished Spirit Shield",
            "Copper Tower Shield",
            "Layered Kite Shield",
            "Burnished Spike Shield",
            "Scarlet Round Shield",
            "Jingling Spirit Shield",
            "War Buckler",
            "Reinforced Tower Shield",
            "Ornate Spiked Shield",
            "Splendid Round Shield",
            "Brass Spirit Shield",
            "Ceremonial Kite Shield",
            "Gilded Buckler",
            "Painted Tower Shield",
            "Walnut Spirit Shield",
            "Oak Buckler",
            "Redwood Spiked Shield",
            "Maple Round Shield",
            "Buckskin Tower Shield",
            "Etched Kite Shield",
            "Ivory Spirit Shield",
            "Enameled Buckler",
            "Mahogany Tower Shield",
            "Compound Spiked Shield",
            "Spiked Round Shield",
            "Ancient Spirit Shield",
            "Corrugated Buckler",
            "Steel Kite Shield",
            "Bronze Tower Shield",
            "Crimson Round Shield",
            "Chiming Spirit Shield",
            "Polished Spiked Shield",
            "Laminated Kite Shield",
            "Battle Buckler",
            "Girded Tower Shield",
            "Thorium Spirit Shield",
            "Golden Buckler",
            "Sovereign Spiked Shield",
            "Baroque Round Shield",
            "Angelic Kite Shield",
            "Crested Tower Shield",
            "Lacewood Spirit Shield",
            "Ironwood Buckler",
            "Alder Spike Shield",
            "Shagreen Tower Shield",
            "Teak Round Shield",
            "Branded Kite Shield",
            "Fossilised Spirit Shield",
            "Lacquered Buckler",
            "Ebony Tower Shield",
            "Spiny Round Shield",
            "Ezomyte Spiked Shield",
            "Vaal Spirit Shield",
            "Champion Kite Shield",
            "Vaal Buckler Shield",
            "Ezomyte Tower Shield",
            "Harmonic Spirit Shield",
            "Mosaic Kite Shield",
            "Mirrored Spiked Shield",
            "Crusader Buckler",
            "Cardinal Round Shield",
            "Colossal Tower Shield",
            "Archon Kite Shield",
            "Titanium Spirit Shield",
            "Imperial Buckler",
            "Elegant Round Shield",
            "Supreme Spiked Shield",
            "Pinnacle Tower Shield"
        };

        private static List<string> Jewels = new List<string>(){
            "Cobalt Jewel",
            "Crimson Jewel",
            "Viridian Jewel"
        };
        #endregion
    }
}
