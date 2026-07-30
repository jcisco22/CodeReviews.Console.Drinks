
public class UserInput
{

    DrinksService drinksService = new();

    internal void GetCategoriesInput()
    {

        var categories = drinksService.GetCategories();

        Console.WriteLine("Choose Category:");

        string category = Console.ReadLine();


        while (!Validation.IsStringValid(category))
        {
            Console.WriteLine("\nInvalid category");
            category = Console.ReadLine();
        }

        if (!categories.Any(x => x.strCategory == category))
        {
            Console.WriteLine("Category does not exist.");
            GetCategoriesInput();
        }


        GetDrinksInput(category);


    }

    private void GetDrinksInput(string category)
    {
        var drinks = drinksService.GetDrinksByCategory(category);
        Console.WriteLine("Choose a drink ID or go back to category menu by typing 0:");

        //eeds to ask for drinkid not drink

        string drink = Console.ReadLine();

        if (drink == "0") GetCategoriesInput();

        while (!Validation.IsIdValid(drink))
        {
            Console.WriteLine("\nInvalid Drink");
            drink = Console.ReadLine();
        }

        if (!drinks.Any(x => x.idDrink == drink))
        {
            Console.WriteLine("Category does not exist.");
            GetCategoriesInput();
        }

        drinksService.GetDrink(drink);
        
        Console.WriteLine("Press any key to go back to the categories menu.");
        Console.ReadKey();
        if(!Console.KeyAvailable) GetCategoriesInput();

    }
}