using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace Game
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            
            BattlefieldFacade bf = new BattlefieldFacade();
            int s = 0;
            while (s < 7)
            {
                Console.WriteLine("1. Создание армии");
                Console.WriteLine("2. Провести фазу боя");
                Console.WriteLine("3. Статистика боя");
                Console.WriteLine("4. Undo");
                Console.WriteLine("5. Redo");
                s = int.Parse(Console.ReadLine());
                string typeFight = null;
                switch (s)
                {
                    case 1:
                        Console.WriteLine("Введите кол-во золота:");
                        int gold = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите режим боя:");
                        Console.WriteLine("1. 1 на 1:");
                        Console.WriteLine("2. 3 на 3:");
                        Console.WriteLine("3. Стенка на стенку:");
                        int type = int.Parse(Console.ReadLine());
                        switch (type)
                        {
                            case 1:
                                typeFight = "OneToOne";
                                break;
                            case 2:
                                typeFight = "ThreeToThree";
                                break;
                            case 3:
                                typeFight = "WallToWall";
                                break;
                        }
                        BattlefieldFacade.CreateArmies(gold, typeFight);
                        break;
                    case 2:
                        BattlefieldFacade.Go();
                        break;
                    case 3:
                        BattlefieldFacade.GetStateInfo();
                        break;
                    case 4:
                        BattlefieldFacade.Undo();
                        break;
                    case 5:
                        BattlefieldFacade.Redo();
                        break;
                }
            }
        }
    }
}
