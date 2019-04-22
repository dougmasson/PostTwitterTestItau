using PostTwitter.BusinnesLayer;
using System;

namespace PostTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (BLL bll = new BLL())
                {
                    bll.Init();
                    var result = bll.ListarPostagensTwitter();

                    if (result != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("..::: ERRO: {0}", ex.Message));
            }

            Console.ReadKey();
        }
    }
}
