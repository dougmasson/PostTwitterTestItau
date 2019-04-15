using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi.Models;

namespace PostTwitter.BusinnesLayer
{
    public static class MyCredentials
    {
        public static string CONSUMER_API_KEY = "cr5RQk9Ak1n2apLAGy7DIG2Yv";
        public static string CONSUMER_API_SECRET_KEY = "3UdyNpRUrLJZrE3uO5ShQdYH3oSMHQmBZd7w6fz7jjCT0V3jjG";
        public static string ACCESS_TOKEN = "969634848641077248-a1tIn75x6r8iyQkzad76r3TYYE3gAEI";
        public static string ACCESS_TOKEN_SECRET = "euEnq03b7TtyESa4uKAtPGY23WuDHZKLi6ws20IbrrIiG";

        public static ITwitterCredentials GenerateCredentials()
        {
            return new TwitterCredentials(CONSUMER_API_KEY, CONSUMER_API_SECRET_KEY, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
        }
    }
}
