using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;


namespace Dungeon_Generator
{
     //setting up to connect and use methods using HTTP connect
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }


    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }

        public RestClient()//constructor to do setup once object is made
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()//this will return a JSON string that will be parsed outside after connecting to an outside API
        {
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);//setting up the location of the API to connect to
            request.Method = httpMethod.ToString();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//initial testing of a connection to ensure it works
            {
                if (response.StatusCode != HttpStatusCode.OK)//checking to make sure API is responding correctly
                {
                    throw new ApplicationException("error Code:" + response.StatusCode); 
                }
                using (Stream responseStream = response.GetResponseStream())//this is what's getting the information from the API
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))//turning the APIs response into a massively painful string to be parsed later
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }

                return strResponseValue; //Returning the massively painful string to be parsed later
            }

        }

    
    }

}
