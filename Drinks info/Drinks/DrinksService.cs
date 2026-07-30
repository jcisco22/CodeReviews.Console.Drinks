using System.Reflection;
using System.Web;
using drinks_info.Models;
using Newtonsoft.Json;
using RestSharp;

public class DrinksService
{
    // resposible for interacting with the drinks API


    public List<Category> GetCategories()
    {
        //similar to var connection = new SqlConnection(connectionString);

        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest("List.php?c=list");
        var response = client.ExecuteAsync(request);

        List<Category> categories = new();

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);
            // List<Category> returnedList = serialize.CatergoriesList;
            categories = serialize.CatergoriesList;

            TableVisualisationEngine.ShowTable(categories, "Categories Menu");
            return categories;
        }
        return categories;

    }



    internal List<Drink> GetDrinksByCategory(string category)
    {

        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
        var response = client.ExecuteAsync(request);

        List<Drink> drinks = new();

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;

            var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);
            // converts the rawResponse JSON string into a Drinks object
            // Drinks is the class we wrote, and serialize is the actual object that gets created from it,
            // filled with the real data from the API

            drinks = serialize.DrinksList;
            //create a list that returns our converted rawResponse (serialize) serialize.DrinksList
            // accesses a property on the object

            TableVisualisationEngine.ShowTable(drinks, "Drinks List");
            //print the list

            return drinks;
        }

        return drinks;

    }


    internal void GetDrink(string drink)
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"lookup.php?i={drink}");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;

            var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);
            //Takes the raw JSON string and converts it into a DrinkDetailObject

            List<DrinkDetail> returnedList = serialize.DrinkDetailList;
            //because DrinkDetailList lives inside DrinkDetailObject we can reach in and grab it 
            DrinkDetail drinkDetail = returnedList[0];
            // Get the first DrinkDetail object from the list

            List<object> prepList = new();
            //Creates an empty list.
            //The goal is to fill it with rows for the table.

            string formatedName = "";

            foreach (PropertyInfo prop in drinkDetail.GetType().GetProperties())
            {
                //drinkDetail.GetType() returns DrinkDetail
                //.GetProperties() returns all properties in that class: strDrink strCategory etc
                if (prop.Name.Contains("str"))

                {
                    formatedName = prop.Name.Substring(3);
                }//remove str from each property


                if (!string.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
                {//if drinkdetail is not empty
                    prepList.Add(new
                    {
                        word = formatedName,
                        Value = prop.GetValue(drinkDetail)
                    });
                }
            }
            TableVisualisationEngine.ShowTable(prepList, "Drink Detail");

        }



    }



}