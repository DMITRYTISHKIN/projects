using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Game
{
    public static class ObjectCloner
    {
        public static T DeepClone<T>(T source) where T : class
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, source);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }
    [Serializable]
    class Armies
    {
        public IArmy One { get; set; }
        public IArmy Two { get; set; }
        public Armies()
        {
        }
    }
    interface ICommand{
        Armies Do(Armies input);
        Armies Undo(Armies input);

    }
    class UndoRedoStack
    {
        private Stack<Armies> UndoStack;
        private Stack<Armies> RedoStack;
        public int UndoCount { get { return UndoStack.Count; } }
        public int RedoCount { get { return RedoStack.Count; } }
        public UndoRedoStack()
        {
            Reset();
        }
        public void Reset()
        {
            UndoStack = new Stack<Armies>();
            RedoStack = new Stack<Armies>();
        }
        public Armies Do(Armies input)
        {

            UndoStack.Push(ObjectCloner.DeepClone<Armies>(input));
            RedoStack.Clear();
            return input;
        }
        public Armies Undo(Armies input)
        {
            if (UndoStack.Count > 0)
            {
                Armies output = UndoStack.Pop();
                RedoStack.Push(ObjectCloner.DeepClone<Armies>(input));
                return output;
            }
            else
            {
                return input;
            }
        }
        public Armies Redo(Armies input)
        {
            if (RedoStack.Count > 0)
            {
                Armies output = RedoStack.Pop();
                UndoStack.Push(ObjectCloner.DeepClone<Armies>(input));
                return output;
            }
            else
            {
                return input;
            }
        }
    }
}
