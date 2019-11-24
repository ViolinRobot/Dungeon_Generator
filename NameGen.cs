using System;
namespace nameGen{
  class ranNameGen{
    //setting up to grab random letters
    private Random rand = new Random();
    private string cons = "qwrtypsdfghjklzxcvbnm";
    private string vows = "aeuio";

     public string getName(int length){
       int choice = rand.Next(1,4);
       string val = "";
       int i =0;
       //building the name up to 3 letters at a time, but randomly choosing to use 1, 2, or 3 letters
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

        // a syllable one letter long, returns a random vowel
     private string sylOne(string name){
       int lett = rand.Next(0,5);
       char valt = vows[lett];
       return name += valt;
     }

        // a syllable two letters long, returns a random vowel and consonant
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
        // a syllable two letters long, returns a random vowel and two consonants in any order
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
