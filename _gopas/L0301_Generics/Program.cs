using System;

namespace L0301_Generics
{
    class Stack
    {
        private readonly int size;
        private int CurrentIndex { set; get; }
        private object[] Items { set; get; }

        public Stack(int s)
        {
            CurrentIndex = 0;
            size = s;
            Items = new object[size];
        }

        public Stack() : this(100)
        {
        }

        public void Push(object item)
        {
            if (CurrentIndex >= size) throw new StackOverflowException();
            Items[CurrentIndex] = item;
            CurrentIndex++;
        }

        public object Pop()
        {
            CurrentIndex--;
            if(CurrentIndex < 0)
            {
                CurrentIndex = 0;
                throw new InvalidOperationException("Cannot pop an empty stack");
            }
            return Items[CurrentIndex];
        }
    }

    class GenericsStack<T>
    {
        private readonly int size;
        private int CurrentIndex { set; get; }
        private T[] Items { set; get; }

        public GenericsStack(int s)
        {
            CurrentIndex = 0;
            size = s;
            Items = new T[size];
        }

        public GenericsStack() : this(100) { }

        public void Push(T item)
        {
            if (CurrentIndex >= size) throw new StackOverflowException();
            Items[CurrentIndex] = item;
            CurrentIndex++;
        }

        public T Pop()
        {
            CurrentIndex--;
            if (CurrentIndex < 0)
            {
                CurrentIndex = 0;
                throw new InvalidOperationException("Cannot pop an empty stack");
            }
            return Items[CurrentIndex];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GenericsStack<string> MyStack = new GenericsStack<string>();

            MyStack.Push("0");
            MyStack.Push("7");
            MyStack.Push("generics");
            
            Console.WriteLine(MyStack.Pop());
            Console.WriteLine(MyStack.Pop());
            Console.WriteLine(MyStack.Pop());

            Console.ReadLine();
        }
    }
}
