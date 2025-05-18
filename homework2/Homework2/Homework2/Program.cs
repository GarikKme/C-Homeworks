using Homework2.OOP1;
using Homework2.OOP2;
using Homework2.STRUCT;

namespace Homework2;

class Program
{
    static void Main(string[] args)
    {
        //OOP1
        Product bread = new Product("Bread", 10, 50); 
        bread.ShowMoney();  // Before discount

        bread.ReducePrice(2, 70); 
        bread.ShowMoney();  // after discount
        
        //OOP2
        MusicInstrument violin = new Violin();
        violin.Show();
        violin.Sound();
        violin.Desc();
        violin.History();
        
        //Struct
        DecimalNumber number = new DecimalNumber(255);
        number.Show();
    }
    
    

    

    
}