using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Game
{
    [Serializable]
    [UnitDescription(cost = 20, health = 500)]
    class KnightUnit : IUnit, ICanBeHealth
    {
        public virtual int CurrentHealth { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public virtual Stack<KnightUnit> prevDecoration { get; set; }
        public virtual bool Melee(IUnit attacker)
        {
            this.CurrentHealth -= (attacker.Attack - this.Defence);
            if (this.CurrentHealth < 1)
                return true;
            return false;
        }
        public KnightUnit()
        {
            prevDecoration = new Stack<KnightUnit>();
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.Attack = 50;
            this.Defence = 0;
            this.CurrentHealth = ud.health;
        }
        public virtual void Heal(int count)
        {
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(this.GetType(), typeof(UnitDescription));
            this.CurrentHealth += count;
            if (CurrentHealth > ud.health)
                CurrentHealth = ud.health;
        }
    }
    [Serializable]
    abstract class KnightUnitDecorator : KnightUnit
    {
        public override Stack<KnightUnit> prevDecoration
        {
            get { return this.knightUnit.prevDecoration; }
            set { this.knightUnit.prevDecoration = value; }
        }
        
        public override int CurrentHealth
        {
            get { return this.knightUnit.CurrentHealth; }
            set { this.knightUnit.CurrentHealth = value; }
        }
        protected KnightUnit knightUnit = new KnightUnit();
        public KnightUnitDecorator(KnightUnit knightUnit)
        {
            this.knightUnit = knightUnit;
            Console.WriteLine("HI!");
        }


        public delegate void RemoveDecoratorHandler(object sender);
        public event RemoveDecoratorHandler RemoveDecoratorEvent;
        public override bool Melee(IUnit attacker)
        {
           this.CurrentHealth -= (attacker.Attack - this.Defence);
           if (attacker.Attack == 15)
           {
               if (RemoveDecoratorEvent != null)
                RemoveDecoratorEvent(this);
           }
           if (this.CurrentHealth < 1)
               return true;
           return false;
        }
        public override void Heal(int count)
        {
            knightUnit.Heal(count);
        }
        
    }
    [Serializable]
    class ShieldUp : KnightUnitDecorator
    {
        public ShieldUp(KnightUnit knightUnit) : base(knightUnit) { this.Defence = knightUnit.Defence + 5; prevDecoration.Push(this.knightUnit); }
    }
    [Serializable]
    class HelmetUp : KnightUnitDecorator
    {
        public HelmetUp(KnightUnit knightUnit) : base(knightUnit) { this.Defence = knightUnit.Defence + 5; prevDecoration.Push(this.knightUnit); }
    }
    [Serializable]
    class PlateUp : KnightUnitDecorator
    {
        public PlateUp(KnightUnit knightUnit) : base(knightUnit) { this.Defence = knightUnit.Defence + 5; prevDecoration.Push(this.knightUnit); }
    }
    [Serializable]
    class HorseUp : KnightUnitDecorator
    {
        public HorseUp(KnightUnit knightUnit) : base(knightUnit) { this.Defence = knightUnit.Defence + 5; prevDecoration.Push(this.knightUnit); }
        
        
    }
}
