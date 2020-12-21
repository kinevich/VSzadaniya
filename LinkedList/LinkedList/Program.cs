using System;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
         {
            LinkedList<int> linkedList = new LinkedList<int>();

            linkedList.AddLast(3);
            linkedList.AddLast(4);
            linkedList.AddLast(2);
            linkedList.AddLast(6);
            linkedList.AddLast(5);

            linkedList.PrintList();
            Console.WriteLine();

            linkedList.Remove(0);

            linkedList.PrintList();
            Console.WriteLine();

            linkedList.Remove(3);

            linkedList.PrintList();
            Console.WriteLine();

            linkedList.Remove(1);

            linkedList.PrintList();
            Console.WriteLine();

            linkedList.AddLast(50);
            linkedList.AddLast(234);

            linkedList.PrintList();
            Console.WriteLine();
        }
    }

    class LinkedList<T>
    {
        private Node<T> _head;

        private Node<T> _last;

        public void AddLast(T data)
        {
            Node<T> node = new Node<T>(data);

            if (_head == null)
            {
                _head = node;
                _last = node;
            }

            _last.Next = node;
            _last = node;          
        }

        public Node<T> Find(Func<Node<T>, bool> predicate)
        {
            Node<T> runner = _head;
            while (runner != null)
            {
                if (predicate(runner))
                {
                    return runner;
                }
                runner = runner.Next;
            }
            return null;
        }

        public void Remove(int index)
        {
            Node<T> runner = _head;

            if (index == 0)
            {
                _head = _head.Next;
                return;
            }

            int count = 0;
            while (runner != null)
            {
                if (index - 1 == count)
                {
                    var deletingItem = runner.Next;
                    runner.Next = runner.Next.Next;
                    deletingItem.Next = null;
                    return;
                }
                ++count;
                runner = runner.Next;
            }
        }

        public void PrintList()
        {
            Node<T> runner = _head;
            while (runner != null)
            {
                Console.WriteLine(runner.Data);
                runner = runner.Next;
            }
        }
    }

    class Node<T>
    {
        public T Data { get; set; }

        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }
}
