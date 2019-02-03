using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvBreaker ctt = new CsvBreaker("D:\\document\\网站备份\\iaders\\wp_posts.csv", Encoding.UTF8);
            List<StringBuilder> sbs = new List<StringBuilder>();
            sbs = ctt.LoadCsvFile();
        }
    }
}
