using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Game
{
    class ProxyMelee : IUnit
    {
        event EventHandler<Observer> EVENT;
        IUnit UNIT;
        public int CurrentHealth { get { return UNIT.CurrentHealth; } }
        public int Attack { get { return UNIT.Attack; } set { UNIT.Attack = value; } }
        public int Defence { get { return UNIT.Defence; } set { UNIT.Defence = value; } }
        public ProxyMelee(IUnit UNIT)
        {
            this.UNIT = UNIT;
            EVENT += Logs.Log;
        }
        public bool Melee(IUnit attacker)
        {
            UNIT.Melee(attacker);
            EVENT(this, new Observer(string.Format("{0} ударяет юнита {1} на {2} единиц ", attacker.GetType().Name, UNIT.GetType().Name, attacker.Attack - UNIT.Defence)));
            if (UNIT.CurrentHealth < 1)
            {
                
                Console.WriteLine("{0} погибает", UNIT.GetType().Name);
                return true;
            }
            return false;
        }
    }

    
    public interface IUnit
    {
        int CurrentHealth { get;}
        int Attack {get; set; }
        int Defence {get; set; }
        bool Melee(IUnit attacker);
    }
    [UnitDescription(cost = 10, health = 300)]
    [Serializable]
    class InfontryUnit : IUnit, ICanBeHealth, ICanBeCloned, ISpecialAction
    {
        public int CurrentHealth { get; private set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public bool Melee(IUnit attacker)
        {
            this.CurrentHealth -= (attacker.Attack - this.Defence);
            if (this.CurrentHealth < 1)
                return true;
            return false;
        }
        public InfontryUnit()
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.Attack = 30;
            this.Defence = 0;
            this.CurrentHealth = ud.health;
        }
        public void Heal(int count)
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.CurrentHealth += count;
            if (CurrentHealth > ud.health)
                CurrentHealth = ud.health;
        }
        public IUnit Clone()
        {
            return new InfontryUnit();
        }
        public void DoAction(IArmy one, IArmy two, string strategy)
        {
            SpecialActionDefend.DoAction(one, two, this, strategy);
        }
    }
    [UnitDescription(cost = 10, health = 100)]
    [Serializable]
    class ArcherUnit : IUnit, ICanBeHealth, ICanBeCloned, ISpecialAction
    {
        public int CurrentHealth { get; private set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public bool Melee(IUnit attacker)
        {
            this.CurrentHealth -= (attacker.Attack - this.Defence);
            if (this.CurrentHealth < 1)
                return true;
            return false;
        }
        public ArcherUnit()
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.Attack = 30;
            this.Defence = 0;
            this.CurrentHealth = ud.health;
        }
        public IUnit Clone()
        {
            return new ArcherUnit();
        }
        public void Heal(int count)
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.CurrentHealth += count;
            if (CurrentHealth > ud.health)
                CurrentHealth = ud.health;
        }
        public void DoAction(IArmy one, IArmy two, string strategy)
        {
            SpecialActionArrow.DoAction(one, two, this, strategy);
        }
    }
    [UnitDescription(cost = 10, health = 100)]
    [Serializable]
    class HealerUnit : IUnit, ICanBeHealth, ISpecialAction
    {
        public int CurrentHealth { get; private set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public bool Melee(IUnit attacker)
        {
            this.CurrentHealth -= (attacker.Attack - this.Defence);
            if (this.CurrentHealth < 1)
                return true;
            return false;
        }
        public HealerUnit()
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.Attack = 50;
            this.Defence = 0;
            this.CurrentHealth = ud.health;
        }
        public void Heal(int count)
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.CurrentHealth += count;
            if (CurrentHealth > ud.health)
                CurrentHealth = ud.health;
        }
        public void DoAction(IArmy one, IArmy two, string strategy)
        {
            SpecialActionHeal.DoAction(one, two, this, strategy);
        }
    }
    [UnitDescription(cost = 10, health = 200)]
    [Serializable]
    class MageUnit : IUnit, ICanBeHealth, ISpecialAction
    {
        public int CurrentHealth { get; private set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public bool Melee(IUnit attacker)
        {
            this.CurrentHealth -= (attacker.Attack - this.Defence);
            if (this.CurrentHealth < 1)
                return true;
            return false;
        }
        public MageUnit()
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.Attack = 150;
            this.Defence = 0;
            this.CurrentHealth = ud.health;
        }
        public void Heal(int count)
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.CurrentHealth += count;
            if (CurrentHealth > ud.health)
                CurrentHealth = ud.health;
        }
        
        public void DoAction(IArmy one, IArmy two, string strategy)
        {
            SpecialActionClone.DoAction(one, two, this, strategy);
            
        }
    }
    

    [System.AttributeUsage(System.AttributeTargets.Class)]
    class UnitDescription : Attribute
    {
        public int cost;
        public int health;
    }
}
