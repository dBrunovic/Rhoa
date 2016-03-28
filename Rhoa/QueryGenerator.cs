using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rhoa
{
    public static class QueryGenerator
    {
        private static Dictionary<string, string> AllParams = new Dictionary<string, string>(){{"league",""}, {"type",""}, {"base",""}, {"name",""}, {"dmg_min",""}, {"dmg_max",""},
                                            {"aps_min", ""}, {"aps_max",""}, {"crit_min",""}, {"crit_max",""}, {"dps_min",""},
                                            {"dps_max",""}, {"edps_min",""}, {"edps_max",""}, {"pdps_min",""}, {"pdps_max",""}, {"armour_min",""}, {"armour_max",""}, 
                                            {"evasion_min",""},{"evasion_max",""},{"shield_min",""},{"shield_max",""},{"block_min",""},{"block_max",""},{"sockets_min",""},
                                            {"sockets_max",""}, {"link_min",""}, {"link_max",""}, {"sockets_r",""}, {"sockets_g",""}, {"sockets_b",""}, {"sockets_w",""},
                                            {"linked_r",""},{"linked_g",""},{"linked_b",""},{"linked_w",""}, {"rlevel_min",""}, {"rlevel_max",""}, {"rstr_min",""}, {"rstr_max",""},
                                            {"rdex_min",""}, {"rdex_max",""},{"rint_min",""}, {"rint_max",""},
                                            {"q_min",""}, {"q_max",""}, {"level_min",""}, {"level_max",""}, {"mapq_min",""}, {"mapq_max",""}, {"rarity",""},
                                            {"seller",""}, {"thread",""}, {"time",""}, {"identified",""}, {"corrupted",""}, {"online",""}, {"buyout",""}, {"altart",""}, {"capquality","x"}, {"buyout_min",""},
                                            {"buyout_max",""}, {"buyout_currency",""}, {"crafted",""}, {"ilvl_min",""}, {"ilvl_max",""}};

        private static List<string> RareMods = new List<string>();
   
        public static string GenerateQuery(List<string> itemParams, List<string> queryValues, Dictionary<string,string> generalParams)
        {
            if (generalParams.Count() > 0)
            {
                if(!generalParams.ContainsKey("league"))
                {
                    MessageBox.Show("Query did not receive the league name. This should never happen, please report this issue. Setting default league to Standard...");
                    AllParams["league"] = "Standard";
                }
                else
                {
                    AllParams["league"] = generalParams["league"];
                }
                if (generalParams.ContainsKey("base"))
                    AllParams["base"] = generalParams["base"];
                if (generalParams.ContainsKey("type"))
                    AllParams["type"] = generalParams["type"];
                if (generalParams.ContainsKey("pdps_min")) 
                    AllParams["pdps_min"] = generalParams["pdps_min"];
                if (generalParams.ContainsKey("pdps_max")) 
                    AllParams["pdps_max"] = generalParams["pdps_max"];
                if (generalParams.ContainsKey("edps_min"))
                    AllParams["edps_min"] = generalParams["edps_min"];
                if (generalParams.ContainsKey("edps_max"))
                    AllParams["edps_max"] = generalParams["edps_max"];
                if (generalParams.ContainsKey("crit_min"))
                    AllParams["crit_min"] = generalParams["crit_min"];
                if (generalParams.ContainsKey("crit_max"))
                    AllParams["crit_max"] = generalParams["crit_max"];
                if (generalParams.ContainsKey("sockets_min"))
                    AllParams["sockets_min"] = generalParams["sockets_min"];
                if (generalParams.ContainsKey("sockets_max"))
                    AllParams["sockets_max"] = generalParams["sockets_max"];
                if (generalParams.ContainsKey("links_min"))
                    AllParams["link_min"] = generalParams["links_min"];
                if (generalParams.ContainsKey("links_max"))
                    AllParams["link_max"] = generalParams["links_max"];
                if (generalParams.ContainsKey("rarity"))
                    AllParams["rarity"] = generalParams["rarity"];
                if (generalParams.ContainsKey("name"))
                    AllParams["name"] = generalParams["name"];
                if (generalParams.ContainsKey("level_min"))
                    AllParams["level_min"] = generalParams["level_min"];
                if (generalParams.ContainsKey("level_max"))
                    AllParams["level_max"] = generalParams["level_max"];
                if (generalParams.ContainsKey("quality_min"))
                    AllParams["q_min"] = generalParams["quality_min"];
                if (generalParams.ContainsKey("quality_max"))
                    AllParams["q_max"] = generalParams["quality_max"];

                if (generalParams.ContainsKey("eShield_min"))
                    AllParams["shield_min"] = generalParams["eShield_min"];
                if (generalParams.ContainsKey("eShield_max"))
                    AllParams["shield_max"] = generalParams["eShield_max"];
                if (generalParams.ContainsKey("evasion_min"))
                    AllParams["evasion_min"] = generalParams["evasion_min"];
                if (generalParams.ContainsKey("evasion_max"))
                    AllParams["evasion_max"] = generalParams["evasion_max"];
                if (generalParams.ContainsKey("armour_min"))
                    AllParams["armour_min"] = generalParams["armour_min"];
                if (generalParams.ContainsKey("armour_max"))
                    AllParams["armour_max"] = generalParams["armour_max"];

                //pseudos
                if (generalParams.ContainsKey("totalRes_min") && generalParams.ContainsKey("totalRes_max"))
                {
                    RareMods.Add("mod_name=(pseudo) +#% total Elemental Resistance&mod_min=" + generalParams["totalRes_min"] +
                        "&mod_max="+generalParams["totalRes_max"]);
                }
                if (generalParams.ContainsKey("totalLife_min") && generalParams.ContainsKey("totalLife_max"))
                {
                    RareMods.Add("mod_name=(pseudo) (total) +# to maximum Life&mod_min=" + generalParams["totalLife_min"] +
                        "&mod_max=" + generalParams["totalLife_max"]);
                }
            }
            StringBuilder postBuilder = new StringBuilder();
            createQueryParams(itemParams, queryValues);
            
            foreach (var param in AllParams)
            {
                if (param.Key == "q_min")
                {      
                    foreach (var rareMod in RareMods)
                    {
                        postBuilder.Append(Mods.UrlEncodeRareMod(rareMod)).Append("&");
                    }
                    string groupParams = "group_type=And&group_min=&group_max=&group_count=" + RareMods.Count() + "&";
                    postBuilder.Append(groupParams);
                }
                postBuilder.Append(param.Key).Append("=").Append(param.Value).Append("&");
            }
            
            if(postBuilder[postBuilder.Length-1] == '&')
                postBuilder.Remove(postBuilder.Length - 1, 1);
            return postBuilder.ToString();
        }

        private static void createQueryParams(List<string> itemParams, List<string> queryParams)
        {
            int modCounter = 0;
            foreach (var param in itemParams)
            {
                if (Mods.RareModExists(param))
                {
                    string expression = new String(param.Where(c => c!= '+' && c!='%' && c != '-' && (c < '0' || c > '9')).ToArray());
                    string prefix;
                    if (expression.Contains("Resistance"))
                        prefix = "mod_name=+#%";
                    else if (expression.Contains("increased"))
                        prefix = "mod_name=#%";
                    else
                        prefix = "mod_name=+#";

                    RareMods.Add(prefix + expression + "&mod_min="+queryParams[modCounter+1] + "&mod_max="+queryParams[modCounter]);
                    modCounter += 2;
                }           
            }
        }  
    }
}
