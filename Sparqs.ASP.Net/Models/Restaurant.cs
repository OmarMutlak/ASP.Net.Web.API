﻿namespace Sparqs.ASP.Net.Models
{
    public class Restaurant
    {
        public Restaurant(string id, string title, string street, string cityCode, string city)
        {
            this.id = id;
            this.title = title;
            this.street = street;
            this.cityCode = cityCode;
            this.city = city;
        }

        public string id { get; set; }
        public string title { get; set; }
        public string street { get; set; }
        public string cityCode { get; set; }
        public string city { get; set; }
       
    }
}
