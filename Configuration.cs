using BadJujuKillAmmoFill.AmmoGroupHelpers;
using Rocket.API;
using System.Collections.Generic;

namespace BadJujuKillAmmoFill
{
    public class Configuration : IRocketPluginConfiguration
    {

     public List<AmmoGroup> Groups;

        public void LoadDefaults()
        {
            Groups = new List<AmmoGroup>()
            {
                new AmmoGroup
                {
                    Id = "default",
                    Blaclist = new List<ushort>()
                    {
                        520
                    }
                }
            };

        }
    }
}