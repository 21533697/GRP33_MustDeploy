//using PayPal.Api;
//using System.Collections.Generic;

//namespace MyBookingRoles.Models.PayPals
//{
//    public static class PaypalConfiguration
//    {

//        public readonly static string ClientId;
//        public readonly static string ClientSecret;

//        //contsructor
//        static PaypalConfiguration()
//        {
//            var config = GetConfig();
//            ClientId = config["clientId"];
//            ClientSecret = config["clientSecret"];
//        }
//        // getting properties from the web.config
//        public static Dictionary<string, string> GetConfig()
//        {
//            return PayPal.Api.ConfigManager.Instance.GetProperties();
//        }
//        private static string GetAccessToken()
//        {
//            // getting accesstocken from paypal               
//            string accessToken = new OAuthTokenCredential
//        (ClientId, ClientSecret, GetConfig()).GetAccessToken();

//            return accessToken;
//        }

//        public static APIContext GetAPIContext()
//        {
//            // return apicontext object by invoking it with the accesstoken
//            APIContext apiContext = new APIContext(GetAccessToken());
//            apiContext.Config = GetConfig();
//            return apiContext;

//        }
//    }
//}