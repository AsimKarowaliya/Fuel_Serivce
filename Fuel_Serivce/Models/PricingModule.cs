using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Fuel_Service.Models
{
    public class PricingModule
    {
        double currentprice = 1.50;     //Standard Price
        double companyProfit = 0.10;    //Company 
        
        public (double,double) CalculatePrice(double gallons, string address, DateTime date, int userkey)
        {
            double locationFactor;
            double rateHistoryFactor;
            double gallonsRequestedFactor;
            double seasonFluctuation;

            //Set locationfactor
            if (address.Contains(" TX"))                
            {
                locationFactor = 0.02;
            }
            else
            {
                locationFactor = 0.04;
            }

            //set rateHistoryFactor;
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Quotes WHERE UserKey = '" + userkey + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();

            if (dt.Rows.Count != 0)  //User has ordered before
            {
                rateHistoryFactor = 0.01;
            }
            else
            {
                rateHistoryFactor = 0;
            }

            //set gallonsRequestedFactor
            if (gallons > 1000)
            {
                gallonsRequestedFactor = 0.02;
            }
            else
            {
                gallonsRequestedFactor = 0.03;
            }

            //set seasonFluctuation
            if (date.Month <= 8 && date.Month >= 6)
            {
                seasonFluctuation = 0.04;
            }
            else
            {
                seasonFluctuation = 0.03;
            }

            double margin = currentprice * (locationFactor - rateHistoryFactor + gallonsRequestedFactor + companyProfit + seasonFluctuation);

            double suggestedprice = currentprice + margin;
            double totalprice = suggestedprice * gallons;

            totalprice = Math.Truncate(totalprice * 100) / 100;
            return (suggestedprice, totalprice);
        }


    }
}
