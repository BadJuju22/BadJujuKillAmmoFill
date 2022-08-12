using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;

namespace BadJujuKillAmmoFill
{
    public class Plugin : RocketPlugin<Configuration>
    {

        public static Plugin Instance;
        public static Configuration Conf;
        public string NamePlugin = "KillAmmoFill";
        public Dictionary<CSteamID, ushort> ArrowID = new Dictionary<CSteamID, ushort>();
        protected override void Load()
        {
            Instance = this;
            Conf = Configuration.Instance;
            Rocket.Core.Logging.Logger.Log("####################################", ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("#      Thanks you for buying          #", ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("#      Plugin Created By BadJuju     #", ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("#      Plugin Version: 1.0.0.0         #", ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("####################################", ConsoleColor.Yellow);
            Rocket.Core.Logging.Logger.Log("", ConsoleColor.White);
            Rocket.Core.Logging.Logger.Log(NamePlugin + " is successfully loaded!", ConsoleColor.Green); 
            UnturnedPlayerEvents.OnPlayerDeath += OnDeath;
            U.Events.OnPlayerConnected += OnConnect;
     
         }

        private void OnConnect(UnturnedPlayer player)
        {
            if (!ArrowID.ContainsKey(player.CSteamID))
                ArrowID.Add(player.CSteamID, 0);
        }

        private void DoAutoReload(UnturnedPlayer uPlayer, ushort itemId)
        {
            PlayerEquipment equip = uPlayer.Player.equipment;
            var ammo = equip.state[10];
            var ammoId = (ushort)(equip.state[8] | equip.state[9] << 8);
            
            if (ammo == 1 && IsBow(itemId))
            {
                ArrowID[uPlayer.CSteamID] = ammoId;
            }

            if (HasSingleBullet(itemId))
            {
                if (ammo == 1)
                {
                    return;
                }
                if (IsBow(itemId))
                {
                    ammoId = ArrowID[uPlayer.CSteamID] == 0 ? (ushort)347 : ArrowID[uPlayer.CSteamID];
                    equip.state[17] = 100;
                }
                else if (itemId == 519 || itemId == 3517)
                {
                    ammoId = 520;
                }
                else if (itemId == 300)
                {
                    ammoId = 301;
                }

                equip.state[8] = (byte)(ammoId);
                equip.state[9] = (byte)(ammoId >> 8);
                equip.state[10] = 1;
                equip.sendUpdateState();
                return;
            }

            var magazine = Assets.find(EAssetType.ITEM, ammoId) as ItemMagazineAsset;

            if (magazine == null)
            {
                return;
            }



            equip.state[10] = magazine.amount;
            equip.sendUpdateState();

        }

        private bool IsBow(ushort itemId) => (itemId == 346 || itemId == 353 || itemId == 355 || itemId == 356 || itemId == 357);
        private bool HasSingleBullet(ushort itemId) => IsBow(itemId) || itemId == 519 || itemId == 3517 || itemId == 300;

        private void OnDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(murderer);
            DoAutoReload(killer, killer.Player.equipment.itemID);

        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= OnDeath;
            U.Events.OnPlayerConnected -= OnConnect;

            Rocket.Core.Logging.Logger.Log(NamePlugin + " is successfully unloaded!", ConsoleColor.Green);
        }
        public override TranslationList DefaultTranslations => new TranslationList
        {


        };
    }
}
    




