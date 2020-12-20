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

        private int _count;

        public void AddLast(T data)
        {
            Node<T> node = new Node<T>(data, _count);
            node.Next = _head;
            _head = node;
            ++_count;
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
            Node<T> removeNode = Find(node => node.Index == index);
            if (removeNode != null)
            {
                if (_count == 1)
                {
                    _head = null;
                    _count = 0;
                }
                else if (removeNode.Index == _count - 1)
                {
                    Node<T> penult = Find(node => node.Index == _count - 2);
                    _head = penult;
                    _count -= 1;
                }
                else
                {                    
                    Node<T> runner = _head;
                    while (runner != removeNode) // меняю индексы элементов, которые идут после удаляемого
                    {
                        runner.Index -= 1;
                        runner = runner.Next;
                    }
                    Node<T> replace = Find(node => node.Index == index); // меняем значения на месте удаленной
                    Node<T> replaceNext = Find(node => node.Index == index - 1);
                    replace.Next = replaceNext;
                    _count -= 1;
                }
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

        public int Index { get; set; }

        public Node<T> Next { get; set; }

        public Node(T data, int index)
        {
            Data = data;
            Next = null;
            Index = index;
        }
    }
}
