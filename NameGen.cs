using System;
namespace nameGen{
  class ranNameGen{
    private Random rand = new Random();
    private string cons = "qwrtypsdfghjklzxcvbnm";
    private string vows = "aeuio";

     public string getName(int length){
       int choice = rand.Next(1,4);
       string val = "";
       int i =0;
       while (i<length){
        if(choice ==1){
          val = sylOne(val);
        }
        if(choice ==2){
          val = sylTwo(val);
        }
        if(choice ==3){
          val = sylThree(val);
        }
        i+= choice;
        choice = rand.Next(1,4);
       }
      return val;
     }
     private string sylOne(string name){
       int lett = rand.Next(0,5);
       char valt = vows[lett];
       return name += valt;
     }
     private string sylTwo(string name){
       int cho = rand.Next(0,2);
       string valt ="";
       if(cho == 0){
         valt += Convert.ToString(cons[rand.Next(0,20)]) + Convert.ToString(vows[rand.Next(0,5)]);
       }
       if(cho == 1){
         valt += Convert.ToString(vows[rand.Next(0,5)]) + Convert.ToString(cons[rand.Next(0,20)]);
       }
       return name += valt;
     }
     private string sylThree(string name){
       int cho = rand.Next(0,3);
       string valt= "";
       if(cho == 0){
         valt +=Convert.ToString(cons[rand.Next(0,20)])+ Convert.ToString(vows[rand.Next(0,5)])+ Convert.ToString(cons[rand.Next(0,20)]);
       }
       if(cho == 1){
         valt += Convert.ToString(vows[rand.Next(0,5)])+ Convert.ToString(cons[rand.Next(0,20)]) + Convert.ToString(cons[rand.Next(0,20)]);
       }
       if(cho == 2){
         return name += Convert.ToString(cons[rand.Next(0,20)])+Convert.ToString(cons[rand.Next(0,20)]) +Convert.ToString(vows[rand.Next(0,5)]);
       }
       return name += valt;
     }
  }
}
