using Newtonsoft.Json;

public class Drinks
{
    [JsonProperty("drinks")]
    //this tells the compiler when you see a JSON field called drinks, put its contents into this property

    public List<Drink> DrinksList { get; set; }
    //the list is the drink because its using the fields in the drink model
}

public class Drink
{

    public string idDrink { get; set; }

    public string strDrink { get; set; }

}
