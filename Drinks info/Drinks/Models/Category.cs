using Newtonsoft.Json;

public class Categories
{

    [JsonProperty("drinks")]

    public List<Category> CatergoriesList { get; set; }
}


public class Category
{
    public string strCategory { get; set; }

}

