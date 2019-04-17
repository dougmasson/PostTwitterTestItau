using PostTwitter.BusinnesLayer;
using System;

namespace PostTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BLL bll = new BLL())
            {
                bll.Init();
                bll.ListarPostagensTwitter();
            }

            Console.ReadLine();
        }
    }
}
