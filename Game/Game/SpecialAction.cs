using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Game
{
    interface ISpecialAction
    {
        //int Range { get; }
        void DoAction(IArmy one, IArmy two, string strategy);
    }
    class SpecialActionHeal
    {
        private static List<IUnit> TargetUnit = new List<IUnit>();
        public static int Range { get; private set; }
        public static void DoAction(IArmy one, IArmy two, IUnit sender, string strategy)
        {
            Range = 3;
            int index = 0;
            TargetUnit.Clear();
            for (int i = 0; i < one.Units.Count(); i++)
            {
                if (one.Units.ElementAt(i) == sender)
                {
                    index = i;
                    break;
                }
            }
            switch(strategy){
                case "OneToOne":
                    for (int i = 0; i < Range; i++)
                    {
                        if ((index - 1 - i) >= 0)
                        {
                            addUnitTargetList(index - 1 - i, one);
                        }
                        if ((index + 1 + i) < one.Units.Count())
                        {
                            addUnitTargetList(index + 1 + i, one);
                        }
                    }
                    break;
                case "ThreeToThree":
                    for (int i = 0; i < Range * 3 - 1; i++)
                    {
                        if ((index - (index % 3) - 1 - i) >= 0)
                            addUnitTargetList(index - (index % 3) - 1 - i, one);
                        if ((index - (index % 3) + 3 + i) < one.Units.Count())
                            addUnitTargetList(index - (index % 3) + 3 + i, one);
                    }
                    if (index % 3 == 0)
                    {
                        if(index + 1 < one.Units.Count())
                            addUnitTargetList(index + 1, one);
                        if (index + 2 < one.Units.Count())
                            addUnitTargetList(index + 2, one);
                    }
                    if (index % 3 == 1)
                    {
                        addUnitTargetList(index - 1, one);
                        if (index + 1 < one.Units.Count())
                            addUnitTargetList(index + 1, one);
                    }
                    if (index % 3 == 2)
                    {
                        addUnitTargetList(index - 1, one);
                        addUnitTargetList(index - 2, one);
                    }
                    break;
                case "WallToWall":
                    for (int i = 0; i < Range; i++)
                    {
                        if ((index - 1 - i) >= 0)
                        {
                            addUnitTargetList(index - 1 - i, one);
                        }
                        if ((index + 1 + i) < one.Units.Count())
                        {
                            addUnitTargetList(index + 1 + i, one);
                        }
                    }
                    break;
            }

            if (TargetUnit.Count() == 0)
                return;
            Random rand = new Random();
            int targetIndex = rand.Next(0, TargetUnit.Count() - 1);
            ICanBeHealth unitHealed = (ICanBeHealth)TargetUnit.ElementAt(targetIndex);
            UnitDescription ud = (UnitDescription)Attribute.GetCustomAttribute(((IUnit)unitHealed).GetType(), typeof(UnitDescription));
            if (((IUnit)unitHealed).CurrentHealth != ud.health)
            {
                unitHealed.Heal(150);
                Console.WriteLine(string.Format("{1} #{0} применил магию и вылечил: {2}", index + 1, sender.GetType().Name, TargetUnit.ElementAt(targetIndex).GetType().Name));
            }

        }
        static private void addUnitTargetList(int index, IArmy one)
        {
            if (one.Units.ElementAt(index).GetType().GetInterface("ICanBeHealth") == typeof(ICanBeHealth))
            {
                TargetUnit.Add(one.Units.ElementAt(index));
            }
        }
    }
    class SpecialActionClone
    {
        private static List<IUnit> TargetUnit = new List<IUnit>();
        public static int Range { get; private set; }
        public static void DoAction(IArmy one, IArmy two, IUnit sender, string strategy)
        {
            Range = 3;
            int index = 0;
            TargetUnit.Clear();

            for (int i = 0; i < one.Units.Count(); i++)
            {
                if (one.Units.ElementAt(i) == sender)
                {
                    index = i;
                    break;
                }
            }

            switch (strategy)
            {
                case "OneToOne":
                    for (int i = 0; i < Range; i++)
                    {
                        if ((index - 1 - i) >= 0)
                        {
                            addUnitTargetList(index - 1 - i, one);
                        }
                        if ((index + 1 + i) < one.Units.Count())
                        {
                            addUnitTargetList(index + 1 + i, one);
                        }
                    }
                    break;
                case "ThreeToThree":
                    for (int i = 0; i < Range * 3; i++)
                    {
                        if ((index - (index % 3) - 1 - i) >= 0)
                            addUnitTargetList(index - (index % 3) - 1 - i, one);
                        if ((index - (index % 3) + 3 + i) < one.Units.Count())
                            addUnitTargetList(index - (index % 3) + 3 + i, one);
                    }
                    if (index % 3 == 0)
                    {
                        if(index + 1 < one.Units.Count())
                            addUnitTargetList(index + 1, one);
                        if (index + 2 < one.Units.Count())
                            addUnitTargetList(index + 2, one);
                    }
                    if (index % 3 == 1)
                    {
                        addUnitTargetList(index - 1, one);
                        if (index + 1 < one.Units.Count())
                            addUnitTargetList(index + 1, one);
                    }
                    if (index % 3 == 2)
                    {
                        addUnitTargetList(index - 1, one);
                        addUnitTargetList(index - 2, one);
                    }
                    break;
                case "WallToWall":
                    for (int i = 0; i < Range; i++)
                    {
                        if ((index - 1 - i) >= 0)
                        {
                            addUnitTargetList(index - 1 - i, one);
                        }
                        if ((index + 1 + i) < one.Units.Count())
                        {
                            addUnitTargetList(index + 1 + i, one);
                        }
                    }
                    break;
            }
            
            if (TargetUnit.Count() == 0)
                return;

            Random rand = new Random();
            int targetIndex = rand.Next(0, TargetUnit.Count() - 1);
            int baf = rand.Next(0, 10);
            if (baf == 5)
            {
                ICanBeCloned unitCloned = (ICanBeCloned)TargetUnit.ElementAt(targetIndex);
                one.Units.Add(unitCloned.Clone());
                Console.WriteLine(string.Format("{1} #{0} применил магию клонирования: {2}", index + 1, sender.GetType().Name, one.Units.Last().GetType().Name));
            }
        }
        private static void addUnitTargetList(int index, IArmy one)
        {
            if (one.Units.ElementAt(index).GetType().GetInterface("ICanBeCloned") == typeof(ICanBeCloned))
            {
                TargetUnit.Add(one.Units.ElementAt(index));
            }
        }
    }
    class SpecialActionArrow
    {
        public static int Range { get; private set; }
        public static void DoAction(IArmy one, IArmy two, IUnit sender, string strategy)
        {
            Range = 3;
            int index = 0;
            for (int i = 0; i < one.Units.Count(); i++)
            {
                if (one.Units.ElementAt(i) == sender)
                {
                    index = i;
                    break;
                }
            }
            Dictionary<IUnit, IArmy> UnitInRange = new Dictionary<IUnit,IArmy>();
            switch (strategy)
            {
                case "OneToOne":
                    if (index > Range - 1)
                        return;
                    for (int i = 0; i < Range; i++)
                    {
                        if ((index - 1 - i) >= 0)
                            UnitInRange.Add(one.Units.ElementAt(index - 1 - i), one);
                        else if (Math.Abs(index - 1 - i) < two.Units.Count())
                            UnitInRange.Add(two.Units.ElementAt(Math.Abs(index - i)), two);
                    }
                    break;
                case "ThreeToThree":
                    if (index > Range*3 - 1)
                        return;
                    for (int i = 0; i < Range*3; i++)
                    {
                        if ((index - (index % 3) - 1 - i) >= 0)
                            UnitInRange.Add(one.Units.ElementAt(index - (index % 3) - 1 - i), one);
                        else if (Math.Abs(index - (index % 3) - i) < two.Units.Count())
                            UnitInRange.Add(two.Units.ElementAt(Math.Abs(index - (index % 3) - i)), two);
                    }
                    break;
                case "WallToWall":
                    if (index - Range > two.Units.Count())
                        return;
                    for (int i = 1; i < Range; i++)
                    {
                        if (index - i < 0)
                            break;
                        if(two.Units.Count() > index - i)
                            UnitInRange.Add(two.Units.ElementAt(index - i), two);
                        UnitInRange.Add(one.Units.ElementAt(index - i), one);
                    }
                    break;
            }
            if (UnitInRange.Count() == 0)
                return;
            Random rand = new Random();
            var targetIndex = rand.Next(0, UnitInRange.Count());
            ProxyMelee proxy = new ProxyMelee(UnitInRange.ElementAt(targetIndex).Key);
            bool Killed = proxy.Melee(sender);
            if (Killed) 
                UnitInRange.ElementAt(targetIndex).Value.RemoveKilledUnit(UnitInRange.ElementAt(targetIndex).Key);
        }
    }
    class SpecialActionDefend
    {
        private static List<IUnit> targetKnight = new List<IUnit>();
        private static List<int> targetIndex = new List<int>();
        public static int Range { get; private set; }
        public static void DoAction(IArmy one, IArmy two, IUnit sender, string strategy)
        {
            Range = 1;
            int probability = 50;
            int index = 0;
            targetKnight.Clear();
            targetIndex.Clear();
            for (int i = 0; i < one.Units.Count(); i++)
                if (one.Units.ElementAt(i) == sender)
                {
                    index = i;
                    break;
                }

            switch (strategy)
            {
                case "OneToOne":
                    if(one.Units.Count() > index + 1)
                        addUnitTargetList(index + 1, one);
                    addUnitTargetList(index - 1, one);
                    break;
                case "ThreeToThree":
                    if (one.Units.Count() > index + 3)
                        addUnitTargetList(index + 3, one);
                    if (index%3 < 2 && one.Units.Count() > index + 1)
                        addUnitTargetList(index + 1, one);
                    if (index % 3 > 0)
                        addUnitTargetList(index - 1, one);
                    addUnitTargetList(index - 3, one);
                    break;
                case "WallToWall":
                    if(one.Units.Count() > index + 1)
                        addUnitTargetList(index + 1, one);
                    addUnitTargetList(index - 1, one);
                    break;
            }
            // Если не нашли подходящего юнита, выходим
            if (targetIndex.Count() == 0 || targetKnight.Count() == 0)
            {
                return;
            }
            // Находим декораторы, которых нет
            Random rand = new Random();
            int target = rand.Next(0, targetKnight.Count());
            KnightUnit targetUnit = (KnightUnit)targetKnight.ElementAt(target);
            IEnumerable<Type> listBaf = GetAllDerivedTypesOf(typeof(KnightUnitDecorator));
            if (targetUnit.prevDecoration.Count() != 0)
                foreach (var prevDecorator in targetUnit.prevDecoration)
                {
                    listBaf = listBaf.Select(e => e).Where(e => e != prevDecorator.GetType());
                }
            listBaf = listBaf.Select(e => e).Where(e => e != targetKnight.ElementAt(target).GetType());
            // Если рыцарь полностью одет, выходим
            if (listBaf.Count() == 0)
                return;
            // Пытаемся навесить
            int shanceBaf = rand.Next(0, 100);
            if (shanceBaf < probability + 1)
            {
                int indexTypeBaf = rand.Next(0, listBaf.Count());
                ConstructorInfo ci = listBaf.ElementAt(indexTypeBaf).GetConstructor(new Type[] { typeof(KnightUnit) });
                KnightUnitDecorator unitDecorator = (KnightUnitDecorator)ci.Invoke(new KnightUnit[] { (KnightUnit)targetKnight.ElementAt(target) });
                Console.WriteLine(string.Format("{1} #{0} снарядил в {2}: {3}", index + 1, sender.GetType().Name, listBaf.ElementAt(indexTypeBaf).Name, one.Units.ElementAt(targetIndex.ElementAt(target)).GetType().Name));
                one.Units.RemoveAt(targetIndex.ElementAt(target));
                one.Units.Insert(targetIndex.ElementAt(target), unitDecorator);
                unitDecorator.RemoveDecoratorEvent += one.RemoveDecorationKnight;
            }
        }
        private static void addUnitTargetList(int index, IArmy one)
        {
            if (index < 0)
                return;
            if (one.Units.ElementAt(index).GetType() == typeof(KnightUnit) || one.Units.ElementAt(index).GetType().BaseType == typeof(KnightUnitDecorator))
            {
                targetKnight.Add(one.Units.ElementAt(index));
                targetIndex.Add(index);
            }
        }

        private static IEnumerable<Type> GetAllDerivedTypesOf(Type baseType)
        {
            Type[] types = Assembly.GetAssembly(baseType).GetTypes();
            return types.Where(baseType.IsAssignableFrom).Where(t => t != baseType);
        }
       
    }
    interface ICanBeHealth
    {
        void Heal(int count);
    }
    interface ICanBeCloned
    {
        IUnit Clone();
    }
    
}
