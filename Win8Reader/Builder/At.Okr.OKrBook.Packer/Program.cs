using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace At.Okr.OKrBook.Packer
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        static void Main2(string[] args)
        {
            //Console.WriteLine(123.ToString("D3"));

            //Liaozhai();

            //Soushenji();

            //JinPingmei();

            //Sanguo();

            //Xiyouji();

            //Shuihu();

            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine(Guid.NewGuid().ToString());
            }

            Console.WriteLine("下载完成");

            Console.ReadLine();
        }

        public static void Fengshen()
        {
            string src = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\封神演义\封神演义.txt";
            string target = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\封神演义\book\";
            string prefix = "正文";
            int preCount = prefix.Length;

            TxtParser parser = new TxtParser(src, target, prefix, preCount);
            parser.Parse();
        }

        public static void Liaozhai()
        {
            string src = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\聊斋志异\聊斋志异.txt";
            string target = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\聊斋志异\book\";
            string prefix = "卷";

            TxtParser parser = new TxtParser(src, target, prefix, 0);
            parser.Parse();
        }

        public static void Soushenji()
        {
            string src = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\搜神记\搜神记.txt";
            string target = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\搜神记\book\";
            string prefix = "搜神记";

            TxtParser parser = new TxtParser(src, target, prefix, 3);
            parser.Parse();
        }

        public static void JinPingmei()
        {
            string src = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\06 金瓶梅\金瓶梅.txt";
            string target = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\06 金瓶梅\book\";
            string prefix = "第";

            TxtParser parser = new TxtParser(src, target, prefix, 0);
            parser.Parse();
        }

        public static void Sanguo()
        {
            string src = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\02 三国演义\三国演义.txt";
            string target = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\02 三国演义\book\";
            string prefix = "正文";

            TxtParser parser = new TxtParser(src, target, prefix, 3);
            parser.Parse();
        }

        public static void Xiyouji()
        {
            string src = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\04 西游记\西游记.txt";
            string target = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\04 西游记\book\";
            string prefix = "第";

            TxtParser parser = new TxtParser(src, target, prefix, 0);
            parser.Parse();
        }

        public static void Shuihu()
        {
            string src = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\03 水浒传\水浒传.txt";
            string target = @"D:\XiQi\Prod\iApps\OkrBook\_doc\_books\03 水浒传\book\";
            string prefix = "第";

            TxtParser parser = new TxtParser(src, target, prefix, 0);
            parser.Parse();
        }
    }
}
