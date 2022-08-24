using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BadJujuKillAmmoFill.AmmoGroupHelpers
{
  public class AmmoGroup
    {
        
        [XmlAttribute]
        public string Id { get; set; }
       
        public List<ushort> Blaclist { get; set; }
       
        }
    }

