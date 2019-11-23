using System;
using System.IO;
using System.Net;
using System.Text;

class MainClass {
  public static void Main (string[] args) {
		int i = 0;
		int s = 0;
		int c =1;
		string tabs = "";
    RestClient rClient = new RestClient();
    rClient.endPoint = "https://api.open5e.com/monsters/?limit=2&search=dragon";

    Console.WriteLine("Rest Client Created");
		string Results = rClient.makeRequest();
		int end = Results.Length;
    while(i<Results.Length-1){

			if(Results[i].Equals('}')){

				Console.Write("\n" + tabs);
			}

			if(Results[i].Equals('{')){
				Console.Write("\n" + tabs);
				tabs = tabs + "  ";
			}

			Console.Write(Results[i]);

			if(Results[i].Equals(',') ){
				Console.Write("\n" + tabs);
			}


			if(Results[i].Equals('}')){

				tabs = tabs.Substring(0,(tabs.Length-2));
				i++;
				Console.Write(Results[i]);
				Console.Write("\n" + tabs);
			}

			if(Results[i].Equals('{')){
				Console.Write("\n" + tabs);
			}

			i++;
		}

  }
}

public enum httpVerb{
  GET,
  POST,
  PUT,
  DELETE
}


class RestClient{
  public string endPoint{get;set;}
  public httpVerb httpMethod{get;set;}

  public RestClient(){
    endPoint = string.Empty;
    httpMethod = httpVerb.GET;
  }

  public string makeRequest(){
    string strResponseValue = string.Empty;

    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

    request.Method = httpMethod.ToString();

    using(HttpWebResponse response = (HttpWebResponse)request.GetResponse()){
      if(response.StatusCode != HttpStatusCode.OK){
        throw new ApplicationException("error Code:" +response.StatusCode);
      }

      using (Stream responseStream = response.GetResponseStream()){
        if(responseStream !=null){
          using(StreamReader reader = new StreamReader(responseStream)){
            strResponseValue = reader.ReadToEnd();
          }
        }
      }

    return strResponseValue;
    }

  }

}
