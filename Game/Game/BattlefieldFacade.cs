using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    interface IStrategy
    {
        void ToFight(IArmy one, IArmy two);
    }
    class OneToOne : IStrategy
    {
        public void ToFight(IArmy one, IArmy two)
        {
            if (one.Units.Count() == 0)
            {
                Console.WriteLine("Вторая армия одержала победу");
                return;
            }
            else if(two.Units.Count() == 0){
                Console.WriteLine("Первая армия одержала победу");
                return;
            }
            two.MeleeAtack(0, one.Units.ElementAt(0), one);
            for (int i = 1; i < one.Units.Count(); i++)
                if (one.Units.ElementAt(i).GetType().GetInterface("ISpecialAction") == typeof(ISpecialAction))
                    ((ISpecialAction)one.Units.ElementAt(i)).DoAction(one, two, "OneToOne");
            if (one.Units.Count() == 0)
            {
                Console.WriteLine("Вторая армия одержала победу");
                return;
            }
            else if (two.Units.Count() == 0)
            {
                Console.WriteLine("Первая армия одержала победу");
                return;
            }
            if (two.Units.ElementAt(0).GetType() == typeof(BarrierUnit) && one.Units.ElementAt(0).GetType() == typeof(BarrierUnit))
            {
                two.Units.RemoveAt(0);
                one.Units.RemoveAt(0);
            }
                
            one.MeleeAtack(0, two.Units.ElementAt(0), two);
            for (int i = 1; i < two.Units.Count(); i++)
                if (two.Units.ElementAt(i).GetType().GetInterface("ISpecialAction") == typeof(ISpecialAction))
                    ((ISpecialAction)two.Units.ElementAt(i)).DoAction(two, one, "OneToOne");
            
        }
    }
    class ThreeToThree : IStrategy
    {
        public void ToFight(IArmy one, IArmy two)
        {
            if (one.Units.Count() == 0)
            {
                Console.WriteLine("Вторая армия одержала победу");
                return;
            }
            else if (two.Units.Count() == 0)
            {
                Console.WriteLine("Первая армия одержала победу");
                return;
            }
            for (int i = 0; i < one.Units.Count() && i < 3 && i < two.Units.Count(); i++)
                two.MeleeAtack(i, one.Units.ElementAt(i), one);
            for (int i = 3; i < one.Units.Count(); i++)
                if (one.Units.ElementAt(i).GetType().GetInterface("ISpecialAction") == typeof(ISpecialAction))
                    ((ISpecialAction)one.Units.ElementAt(i)).DoAction(one, two, "ThreeToThree");
            if (one.Units.Count() == 0)
            {
                Console.WriteLine("Вторая армия одержала победу");
                return;
            }
            else if (two.Units.Count() == 0)
            {
                Console.WriteLine("Первая армия одержала победу");
                return;
            }
            for (int i = 0; i < one.Units.Count() && i < 3 && i < two.Units.Count(); i++)
                one.MeleeAtack(i, two.Units.ElementAt(i), two);
            for (int i = 3; i < two.Units.Count(); i++)
                if (two.Units.ElementAt(i).GetType().GetInterface("ISpecialAction") == typeof(ISpecialAction))
                    ((ISpecialAction)two.Units.ElementAt(i)).DoAction(two, one, "ThreeToThree");
        }
    }
    class WallToWall : IStrategy
    {
        public void ToFight(IArmy one, IArmy two)
        {
            if (one.Units.Count() == 0)
            {
                Console.WriteLine("Вторая армия одержала победу");
                return;
            }
            else if (two.Units.Count() == 0)
            {
                Console.WriteLine("Первая армия одержала победу");
                return;
            }
            for (int i = 0; i < one.Units.Count() && i < two.Units.Count(); i++)
                two.MeleeAtack(i, one.Units.ElementAt(i), one);
            if (one.Units.Count() > two.Units.Count())
                for (int i = two.Units.Count(); i < one.Units.Count(); i++)
                    if (one.Units.ElementAt(i).GetType().GetInterface("ISpecialAction") == typeof(ISpecialAction))
                        ((ISpecialAction)one.Units.ElementAt(i)).DoAction(one, two, "WallToWall");
            if (one.Units.Count() == 0)
            {
                Console.WriteLine("Вторая армия одержала победу");
                return;
            }
            else if (two.Units.Count() == 0)
            {
                Console.WriteLine("Первая армия одержала победу");
                return;
            }
            for (int i = 0; i < one.Units.Count() && i < two.Units.Count(); i++)
                one.MeleeAtack(i, two.Units.ElementAt(i), two);
            if (one.Units.Count() < two.Units.Count())
                for (int i = one.Units.Count(); i < two.Units.Count(); i++)
                    if (two.Units.ElementAt(i).GetType().GetInterface("ISpecialAction") == typeof(ISpecialAction))
                        ((ISpecialAction)two.Units.ElementAt(i)).DoAction(two, one, "WallToWall");
        }
    }

    interface IBattlefieldFacade
    {
        void CreateArmies(int cost);
        void Phase();
        void GetStateInfo();
    }
    class BattlefieldFacade
    {
        static public IArmy One;
        static public IArmy Two;
        static public UndoRedoStack unre = new UndoRedoStack();
        static private IStrategy go;
        static public void CreateArmies(int cost, string type)
        {
            One = ArmyFactory.CreateArmy(cost);
            Two = ArmyFactory.CreateArmy(cost);
            Armies arm = new Armies();
            arm.One = One;
            arm.Two = Two;
            switch (type)
            {
                case "OneToOne":
                    go = new OneToOne();
                    break;
                case "ThreeToThree":
                    go = new ThreeToThree();
                    break;
                case "WallToWall":
                    go = new WallToWall();
                    break;
            }
        }
        static public void GetStateInfo()
        {
            Console.WriteLine(string.Format("{0,-31}||{1,-26}", One.ToString(), Two.ToString()));
            List<string> OneArmyInfo = new List<string>();
            List<string> TwoArmyInfo = new List<string>();
            foreach (IUnit unitInfo in One.Units)
            {
                OneArmyInfo.Add(string.Format("#{0,-3}|{1,-20}|{2,-5}|", One.Units.IndexOf(unitInfo) + 1, unitInfo.GetType(), unitInfo.CurrentHealth));
            }
            foreach (IUnit unitInfo in Two.Units)
            {
                TwoArmyInfo.Add(string.Format("#{0,-3}|{1,-20}|{2,-5}|\n", Two.Units.IndexOf(unitInfo) + 1, unitInfo.GetType(), unitInfo.CurrentHealth));
            }
            for (int i = 0; i < Math.Max(OneArmyInfo.Count(), TwoArmyInfo.Count()); i++)
            {
                if (OneArmyInfo.Count() > i)
                    Console.Write(OneArmyInfo.ElementAt(i));
                else Console.Write(string.Format(" {0,-3}|{0,-20}|{0,-5}|", " "));
                if (TwoArmyInfo.Count() > i)
                    Console.Write(TwoArmyInfo.ElementAt(i));
                else Console.Write(string.Format(" {0,-3}|{0,-20}|{0,-5}|\n", " "));
            }
        }
        public BattlefieldFacade()
        {
            
        }
        static public void Undo()
        {
            var arm = new Armies();
            arm.One = One;
            arm.Two = Two;
            var armies = unre.Undo(arm);
            One = armies.One;
            Two = armies.Two;
            GetStateInfo();
        }
        static public void Redo()
        {
            var arm = new Armies();
            arm.One = One;
            arm.Two = Two;
            var armies = unre.Redo(arm);
            One = armies.One;
            Two = armies.Two;
            GetStateInfo();
        }
        static public void Go()
        {
            var arm = new Armies();
            arm.One = One;
            arm.Two = Two;
            unre.Do(arm);
            if (go == null)
                return;
            go.ToFight(One, Two);
            GetStateInfo();
        }
    }
}
