using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Game
{
    [Serializable]
    [UnitDescription(cost = 5000, health = 500)]
    class BarrierUnit : IUnit
    {
        
        SpecialUnits.GulyayGorod g;
        public int CurrentHealth { get { return g.GetCurrentHealth(); } }
        public int Attack { get { return g.GetStrength(); } set {  } }
        public int Defence { get { return g.GetDefence(); } set {  } }
        public BarrierUnit()
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            g = new SpecialUnits.GulyayGorod(ud.health, 0, ud.cost);
        }
        public bool Melee(IUnit attacker)
        {
            g.TakeDamage(attacker.Attack);
            if (this.CurrentHealth < 1)
                return true;
            return false;
        }
    }
    
}
