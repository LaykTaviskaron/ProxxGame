using System;
using System.Text;

namespace Proxx
{
    public class ProxxPrinter
    {
        public void Print(ProxxModel model)
        {
            Console.Clear();

            for (int i = 0; i < model.BoardSize; i++) 
            {
                var sb = new StringBuilder();
                
                for (int j = 0; j < model.BoardSize; j++)
                {
                    var val = model.Cells[i, j].AdjacentBlackHolesCount.ToString();
                    if (!model.Cells[i, j].IsVisible)
                    {
                        val = "x";
                    }
                    else
                    {
                        if (model.Cells[i, j].IsBlackHole)
                        {
                            val = "*";
                        }
                    }


                    sb.Append(" " + val);
                }

                Console.WriteLine(sb);
            }

            Console.WriteLine();
        }
    }
}
