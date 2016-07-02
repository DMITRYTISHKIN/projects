using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
   public class Observer : EventArgs
    {
        public string MSG { get; set; }
        public Observer(string MSG)
        {
            this.MSG = MSG;
        }
    }
    static class Logs
    {
        static public void Log(object sender, Observer e)
        {
            Console.WriteLine("{0}", e.MSG);
        }
    }
}
