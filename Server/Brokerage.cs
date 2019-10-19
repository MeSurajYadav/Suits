// namespace HackathonSample
// {
//     public class Brokerage
//     {
//         //Number of stocks
//         public int N { get; set; }

//         //Brokerage Rate - constant
//         double const int R { get; set; }
    
//         //brokerage
//         double b;

//         //tuple
//         public tuple<SQty, SN, UPrice, BaughtOrSold>[] Details { get; set; }
    
//         public setTouple(){
//             foreach(ln in args)
//         }

//         public double GetBrokerage(){
//             foreach (var t in Details)
//             {
//                 b = b + t.
//             }
//         }

//         public static void Main(string[][] args )
//         {            
//             //VALIDATE INPUTS

//             //CREATE INPUTS TO FEED TO BROKERATE CLASS
//             for(i=0;i<args.length-1;i++)
//             {
//                 if(i=0){
//                     this.N = Convert.ToDouble(args[i]);
//                     continue;
//                 }

//                 if(i=1){
//                     this.R = Convert.ToDouble(args[i]);
//                     continue;
//                 }

//                 this.Details.SQty = Convert.ToDouble(args[i]);
//                 this.Details.SN = Convert.ToDouble(args[i]);
//                 this.Details.UPrice = Convert.ToDouble(args[i]);
//                 this.Details.BaughtOrSold = Convert.ToDouble(args[i]);

//             }

//             //CREATE BROKERAGE CLASS AND 
//             Brokerage brokerage = new Brokerage(args);
//             Console.WriteLine("Brokerage for given inputs is" + " " + brokerage.GetBrokerage());
//             Console.WriteLine("Press any key to continue...");
//             Console.ReadLine();
//         }

//     }
// }
