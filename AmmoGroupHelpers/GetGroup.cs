using Rocket.Core;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadJujuKillAmmoFill.AmmoGroupHelpers
{
    public static class GetGroup
    {
        public static bool getAmmoGroup(this UnturnedPlayer uPlayer, out AmmoGroup group)
        {

            group = null;

            foreach (var raidGroup in Plugin.Instance.Configuration.Instance.Groups)
            {
                foreach (var permGroup in R.Permissions.GetGroups(uPlayer, false))
                {
                    if (raidGroup.Id.ToLower() == permGroup.Id.ToLower())
                    {
                        group = raidGroup;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
