using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;
namespace Game
{
    [XmlInclude(typeof(IArmy))]
    interface IArmy
    {
        List<IUnit> Units { get; }
        void RemoveDecorationKnight(object sender);
        /// <summary>
        /// Метод ближней атаки
        /// </summary>
        /// <param name="targetIndex">Индекс цели</param>
        /// <param name="attacker">Атакующий юнит</param>
        /// <param name="attackerArmy">Армия атакующего</param>
        void MeleeAtack(int targetIndex, IUnit attacker, IArmy attackerArmy);
        /// <summary>
        /// Метод дальней атаки
        /// </summary>
        /// <param name="targetIndex">Индекс цели</param>
        /// <param name="attacker">Атакующий юнит</param>
        /// <param name="attackerArmy">Армия атакующего</param>
        void RangedAttack(int targetIndex, IUnit attacker, IArmy attackerArmy);
        /// <summary>
        /// Удаляем мертвого юнита
        /// </summary>
        /// <param name="T">Индекс юнита или сам юнит</param>
        void RemoveKilledUnit<T>(T index);
    }
    [Serializable]
    class Army : IArmy
    {
        public List<IUnit> Units { get; set; }
        public Army()
        {
            
        }
        public void RemoveDecorationKnight(object sender)
        {

            int indexUnit = 0;
            KnightUnit Unit = null;
            for (int i = 0; i < Units.Count(); i++)
                if (sender == Units.ElementAt(i))
                {
                    indexUnit = i;
                    Unit = (KnightUnit)Units.ElementAt(i);
                    break;
                }
            var prevUnit = Unit.prevDecoration.Pop();
            Units.RemoveAt(indexUnit);
            Units.Insert(indexUnit, prevUnit);
        }

        public void MeleeAtack(int targetIndex, IUnit attacker, IArmy attackerArmy)
        {
            Type typeGeneric = typeof(UnitFactory<>);
            
            if (Units.Count() == 0)
                return;
            int indexAttacker = attackerArmy.Units.IndexOf(attacker);
            if(targetIndex != indexAttacker)
                throw new Exception("Юнит находится вне радиуса ближней атаки!");
            IUnit targetUnit = Units.ElementAt(targetIndex);
            ProxyMelee on = new ProxyMelee(targetUnit);
            bool MyUnitKilled = on.Melee(attacker);
            if (MyUnitKilled) RemoveKilledUnit(targetIndex);
            ProxyMelee tw = new ProxyMelee(attacker);
            bool AttackerKilled = tw.Melee(targetUnit);
            if (AttackerKilled) attackerArmy.RemoveKilledUnit(indexAttacker);
        }
        public void RangedAttack(int targetIndex, IUnit attacker, IArmy attackerArmy)
        {
            IUnit targetUnit = Units.ElementAt(targetIndex);
            ProxyMelee on = new ProxyMelee(targetUnit);
            bool MyUnitKilled = on.Melee(attacker);
            if (MyUnitKilled) RemoveKilledUnit(targetIndex);
        }
        public void RemoveKilledUnit<T>(T index)
        {
            var tip = index.GetType().GetInterface("IUnit");
            if (index.GetType().GetInterface("IUnit") == typeof(IUnit))
            {
                int i = Units.IndexOf((IUnit)index);
                if (Units.ElementAt(i).CurrentHealth < 1)
                    Units.RemoveAt(i);
                else throw new Exception("Нельзя удалить живого юнита!");
            }
            else if (index.GetType() == typeof(int)){
                int i = Convert.ToInt32(index);
                if (Units.ElementAt(i).CurrentHealth < 1)
                    Units.RemoveAt(i);
                else throw new Exception("Нельзя удалить живого юнита!");
            }
            
        }

    }
    class ArmyFactory
    {
        private static IArmy armyFactory;
        private static List<IUnit> unitFactory;
        private ArmyFactory(){}
        private static Random rand = new Random();
        private class IntervalUnit
        {
            public Type TypeUnit { get; set; }
            public int Interval { get; set; }
            public int CostUnit { get; set; }
            public int ChanseUnit { get; set; }
            public IntervalUnit(Type TypeUnit, int CostUnit, int Interval, int ChanseUnit)
            {
                this.TypeUnit = TypeUnit;
                this.CostUnit = CostUnit;
                this.Interval = Interval;
                this.ChanseUnit = ChanseUnit;
            }
        }
        public static IArmy CreateArmy(int gold){
            unitFactory = new List<IUnit>();
            IEnumerable<Type> listUnits = GetAllDerivedTypesOf(typeof(IUnit));

            int TotalCost = 0, MaxCost = 0;
            foreach (Type type in listUnits)
            {
                TotalCost += GetCost(type);
                if (MaxCost < GetCost(type)) MaxCost = GetCost(type);
            }

            List<IntervalUnit> intervalList = new List<IntervalUnit>();
            intervalList.Add( new IntervalUnit(listUnits.ElementAt(0), GetCost(listUnits.ElementAt(0)), TotalCost - GetCost(listUnits.ElementAt(0)), TotalCost - GetCost(listUnits.ElementAt(0))));
            for (int i = 1; i < listUnits.Count(); i++ )
            {
                intervalList.Add(new IntervalUnit(listUnits.ElementAt(i), GetCost(listUnits.ElementAt(i)), intervalList[i - 1].Interval + TotalCost - GetCost(listUnits.ElementAt(i)), TotalCost - GetCost(listUnits.ElementAt(i))));
            }

            //Random rand = new Random();
            while (true)
            {
                if (gold < MaxCost)
                {
                    IEnumerable<int> cc = intervalList.Select((item, index) => new { Item = item, Index = index }).Where(e => e.Item.CostUnit > gold).Select(e => e.Index);
                    if (cc.Count() != 0)
                    {
                        intervalList.RemoveAt(cc.First());
                        if (intervalList.Count() == 0) 
                            break;
                        sortIntervalList(intervalList);
                        continue;
                    }

                }
                int valueFromInterval = rand.Next(0, intervalList.Last().Interval);

                IntervalUnit randomizerUnit = intervalList.Select(item => new { item, il = intervalList.Where(e => e.Interval >= valueFromInterval).Select(e => e.Interval).Min()}).Select(e => e).Where(e => e.item.Interval == e.il).Select(eo => eo.item).First();
                
                Type typeUnit = randomizerUnit.TypeUnit;
                if (gold < randomizerUnit.CostUnit)
                    continue;
                gold -= randomizerUnit.CostUnit;


                Type typeGeneric = typeof(UnitFactory<>);
                Type specificType = typeGeneric.MakeGenericType(typeUnit);
                MethodInfo method = specificType.GetMethod("CreateUnit");
                object obj = Activator.CreateInstance(specificType);

                unitFactory.Add((IUnit)method.Invoke(obj, new IUnit[] { }));
            }
            Army arm = new Army();
            arm.Units = unitFactory;
            armyFactory = arm;
            return armyFactory;
        }
        
        static private IEnumerable<Type> GetAllDerivedTypesOf(Type baseType)
        {
            var types = Assembly.GetAssembly(baseType).GetTypes();
            return types.Where(baseType.IsAssignableFrom).Where(t => t != baseType && !typeof(KnightUnitDecorator).IsAssignableFrom(t) && t != typeof(ProxyMelee));
        }
        static private int GetCost(Type type){
            return (Attribute.GetCustomAttribute(type, typeof(UnitDescription)) as UnitDescription).cost;
        }
        static private List<IntervalUnit> sortIntervalList(List<IntervalUnit> list)
        {
            list[0].Interval = list[0].ChanseUnit;
            for (int i = 1; i < list.Count; i++)
            {
                list[i].Interval = list[i-1].Interval + list[i].ChanseUnit;
            }
            return list;
        }
    }
    interface IUnitFactory
    {

        IUnit CreateUnit();
    }


    public class UnitFactory<T> where T : IUnit, new()
    {
        public static IUnit CreateUnit()
        {
            return new T();
        }
        
    }




}
