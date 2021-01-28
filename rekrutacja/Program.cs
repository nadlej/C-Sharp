using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace rekrutacja
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            string doc = "";
            Node[] temp;

            while ((line = Console.ReadLine()) != "")
            {
                line = line.Replace("„", "'").Replace("”", "'");
                doc += line;
            }

            try
            {
                temp = JsonConvert.DeserializeObject<Node[]>(doc);
                GetTree tree = new GetTree();
                tree.Connections(temp);
                Console.WriteLine(tree.ConcatTree(temp));
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("Wrong file format.");
            }
        }
    }
    class Node
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }

        public List<Node> children = new List<Node>();

        public string Representation(int deep)
        {
            string representation = "";
            representation += "\n" + String.Concat(Enumerable.Repeat("\t", deep)) + "{" + "\n" + String.Concat(Enumerable.Repeat("\t", deep + 1));
            representation += "name:" + "„" + this.Name + "”," + "\n" + String.Concat(Enumerable.Repeat("\t", deep + 1));
            representation += "children: [";

            foreach (Node child in children)
                representation += child.Representation(deep + 2);

            if (!this.children.Any())
                representation += "]\n";
            else
                representation += String.Concat(Enumerable.Repeat("\t", deep + 1)) + "]\n";

            representation += String.Concat(Enumerable.Repeat("\t", deep)) + "},\n";

            return representation;
        }
    }

    class GetTree
    {
        public void Connections(Node[] vertex)
        {
            int counter = vertex.Last().Order;

            for (int i = 1; i < counter; i++)
            {
                int j;
                for (j = i; vertex[j].Level - vertex[i].Level != -1 && vertex[i].Level != 0; j--) ;
                if (vertex[i].Level != 0)
                    vertex[j].children.Add(vertex[i]);
            }
        }

        public string ConcatTree(Node[] vertex)
        {
            string resultString = "[";

            foreach (Node v in vertex)
                if (v.Level == 0)
                    resultString += v.Representation(1);

            resultString = resultString.Replace("\n\n", "\n") + "]";
            resultString = resultString.Remove(resultString.LastIndexOf(","), 1);

            return resultString;
        }
    }

}
