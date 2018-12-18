using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viewer;

namespace ResizeXML
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2) return;
            var xml = new XMLViewer(args[0]);
            Console.WriteLine(args[0]);
            var list = xml.GetDatasetImages();
            var count = 0;
            foreach (var image in list)
            {
                count++;
                image.file = "01h/" + count.ToString("D5") + ".jpg";
                image.box.height /= 2;
                image.box.width /= 2;
                image.box.left /= 2;
                image.box.top /= 2;
                foreach (var part in image.box.part)
                {
                    part.x /= 2;
                    part.y /= 2;
                }
            }
            xml.SaveXML(args[1]);
            Console.WriteLine(args[1]);
        }
    }
}
