// See https://aka.ms/new-console-template for more information

using dme;

namespace dme {

    public class QueueShuffler
    {
        private static Random rng = new Random();

        public static Queue<T> ShuffleQueue<T>(Queue<T> queue)
        {
            List<T> list = new List<T>(queue);      // Step 1: Copy to list
            int n = list.Count;

            // Step 2: Shuffle using Fisher-Yates
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            return new Queue<T>(list);              // Step 3: Create new queue
        }
    }
    class Token
{
    Node tokenNode;
}
class Request
{
    public Node reqNode;
    
    public Request(Node node)
        {
            reqNode = node;
        }
}
class Node
{
    public Node next;
    public int id;
    public bool hasToken;
    public Queue<Node> queue;

        public Node(int Id)
        {
            id = Id;
        }
    public void EnterCs()
    {
        if (hasToken && queue.Count != 0)
        {
            Console.WriteLine($"Node {id} enters the cs");
            Node node = queue.Dequeue();
                Console.WriteLine($"Node {node.id} begins recieving the token");
                hasToken = false;
            PassToken(node, queue);
        }
    }
    void PassToken(Node node,
    Queue<Node> passQueue)
    {
        Console.WriteLine($"Node {node.id} recieves the queue and becomes the token holder.");
        node.hasToken = true;
        node.queue = passQueue;
    }
    public void ProcessRequest(Request req)
    {
        if (hasToken)
        {

                Console.WriteLine($"Node: {id} Enqueues the request of node {req.reqNode.id}");
                queue.Enqueue(req.reqNode);
        }
        else
        {
            Console.WriteLine($"Node: {id} passes the request to {next.id}" );
            next.ProcessRequest(req);
        }
    }
}
}


class Program
{

    static void Main()
    {
        var rand = new Random();
        Queue<Node> Reqqueue = new();
        Node prev = new Node(10);
        Node first = prev;
        first.hasToken = true;
        first.queue = new();
        for (int i = 0; i < 10; i++)
        {
            Node node = new(i);
            Reqqueue.Enqueue(node);
            prev.next = node;
            prev = node;

        }

        Queue<Node> shuffledQueue = QueueShuffler.ShuffleQueue(Reqqueue);
        /* Notes:
        The version is synchronous and single-threaded but to proprely demonstrate the algorithm's implementation im randomizing wheter we now process a request or add one.
        if we add then starting from the asking node we find token holder which enqueues the request, if we're entering the cs we're dequeueing the next req node and passing token along with the queue
        this helps better demonstrate that the algorithm works. 
        Again, since it is randomized I won't be providing 2 datasets since randomization can simulate numerous versions. 
         */
        prev.next = first;
        for (int i = 0; i < 11; i++)
        {
            Console.WriteLine(prev.id);
            prev = prev.next;
        }
        int reqTodo = shuffledQueue.Count;
        while (shuffledQueue.Count > 0)
        {
            int a = rand.Next();
            if (rand.Next() % 2 == 0)
            {
                Node tmp = shuffledQueue.Dequeue();

                Console.WriteLine($"Node {tmp.id} sends a request");
                tmp.ProcessRequest(new Request(tmp));
                Console.WriteLine($"Requests left to send: {shuffledQueue.Count}");

            }   
            else
            {
                while (true) {
                    if (prev.hasToken)
                    {
                        if (prev.queue.Count() > 0)
                        {
                            prev.EnterCs();
                            reqTodo--;
                        }
                        break;
                    }
                    prev = prev.next;
                }
            }
        }
        Console.WriteLine($"Requests left on the queue to process: {reqTodo}");
        while (reqTodo > 0)
        {
            if (prev.hasToken)
            {
                if (prev.queue.Count() > 0)
                {
                    prev.EnterCs();
                    reqTodo--;
                }
            }
            prev = prev.next;
        }
    }
}