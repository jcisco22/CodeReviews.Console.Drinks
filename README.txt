drinks info app

We are making a drinks info app that uses an API to get and display details of a drink by allowing users to choose a category of drink as well as the drink ID.

to do this we need several classes:


Main

Main or program.cs is where we would call our methods as always, we are calling one method inside user input that calls methods from drinkservice.cs passing along the information the user entered into those drinkservice methods where the API methods live. 



RestSharp and HHTPclient differences
RestSharp already breaks down the data and gives us the string directly in the line             string rawResponse = response.Result.Content;

HttpClient does not do that, Httpclient needs to read the data it is receiving before breaking it down here we need  string rawResponse = await response.Content.ReadAsStringAsync(); this converts our data into a string that our deserializer can read. once it can be read it can break it down and map the data into the list.

UserInput.cs

UserInput Input = new(); needs to be called as none of the methods used in this program are static so instances must be created before we can access them.

Only Input.GetCategoriesInput(); needs to be called so we call that before placing a console.readkey() after it so the terminal dosent close automatically.

There are three model classes I need to make, one for each returned data set from the API. Model classes hold the shape of data and the core business logic of the application.

Category.cs - Category class holds the returned property strCategory when the API is called. the Categories class holds the property "drinks" as well as the list <Category> CatergoriesList 

Catergorieslist is later access through the serialize variable to show the returned categories from the API. 

DrinkDetail.cs - DrinkDetailObject is the wrapper class holds the property "drinks" as well as the <DrinkDetail> DrinkDetailList list which we use in our data. After deserialisation, the list is accessed via serialize.DrinkDetailList to show the returned drinks from the API.  

Drinks.cs - holds the property "drinks" which is what the set of data retuned from the api is named, public class Drink holds the variables returned by the API which are strDrink & idDrink

TableVisualisation.cs
This class is responsible for showing the data as a table using the consoletablebuilder.


DrinkService.cs
The most important class of all which is our drinkservice class, this class is responsible for carrying out all three API requests one for getting the category of drinks available, one for getting the drinks that match the users entered category and lastly getting the details of the drink that they chose, the details include many variables that we hold inside the DrinkDetail model class, this will list information for all the variables in the model except for null values.

Inside this class we create a "client" which is the website
A "request" which is responsible for the endpoint which fetches the EXACT request 
and response which executes the client using request as the passed in information.


Validation.cs
This class is for creating validation for userinput two methods exist in this class, IsStringValid & IsIdValid are for validating user input.

IsStringValid checks for null or empty input in the input (stringInput) return false.

IsIdValid checks whether the input is null or empty, and whether every character in the string is a digit; if either check fails, it returns false. Otherwise, it returns true, allowing the program to proceed with that ID.

There was a weird quirk with the formatted name, the formattedName only displayed the names correctly when I used the word key, when I tried to use "word" the table was misaligned.
All in all I think the project taught me about how to consume an API well, I learned about the request/response pipeline, deserialization, the two-class model pattern, why async/await exists, and how LINQ validates user input.

I learned that when deserialising API data there are two separate classes in the model class, one wrapper class representing the outer JSON object and one inner class representing each item in the list
To deserialize correctly we need both classes.

This class is responsible for defining the jsonproperty which is the  instruction telling the deserialiser which JSON field maps to this property returned from the API. The wrapper class uses [JsonProperty("drinks")] to tell the deserialiser that the JSON field called drinks should be mapped into this property, since the C# property name (DrinksList) is different from the JSON field name.

Directly above the property, we declare [JsonProperty("drinks")], and the property itself is public List<Drink> DrinksList { get; set; } — together they say: take the JSON field drinks and map it into this list.

The Drink class contains the properties named idDrink & strDrink this is why the list is named after this class, it uses the jsonproperty to locate the name of the dataset and uses the Drink list to define the returned values fields.

[JsonProperty] tells the deserialiser which JSON field to read from. List<Drink> tells it what shape to build for each item inside that field.









