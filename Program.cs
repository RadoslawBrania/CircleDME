// See https://aka.ms/new-console-template for more information

using dme;

namespace dme { 

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
            node.hasToken = false;
            PassToken(node, queue);
            queue = null;
        }
    }
    void PassToken(Node node,
    Queue<Node> passQueue)
    {
        Console.WriteLine($"Node {id} recieves the queue and becomes the token holder.{passQueue}");
        node.hasToken = true;
        node.queue = passQueue;
    }
    public void ProcessRequest(Request req)
    {
        if (hasToken)
        {

                Console.WriteLine($"Node: {id} Enqueues the request of node {req.reqNode.id} {queue}");
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
        for(int i=0; i < 10; i++)
        {
            Node node = new(i);
            Reqqueue.Enqueue(node);
            prev.next = node;
            prev = node;
            
        }
        prev.next = first;
        for(int i=0; i < 11; i++)
        {
            Console.WriteLine(prev.id);
            prev = prev.next;
        }
        while (Reqqueue.Count > 0)
        {
            int a = rand.Next();
            if (rand.Next() % 2 == 0)
            {
                Console.WriteLine(a);
                Node tmp = Reqqueue.Dequeue();
                Console.WriteLine($"QUEUE SIZE R-EWR-EW-R {Reqqueue.Count}");
                tmp.ProcessRequest(new Request(tmp));

            }
            else
            {
                while (true) { 
                    if (prev.hasToken)
                    {
                        if (prev.queue!=null )prev.EnterCs() ;
                        break;
                    }
                prev = prev.next;
                }
                
            }
        }
    }
}