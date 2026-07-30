using System.Diagnostics.CodeAnalysis;
using ConsoleTableExt;

public class TableVisualisationEngine
{

    // SHOWS CONSOLE DATA SPECTRE NEEDED

    public static void ShowTable<Tbl>(List<Tbl> tableData, [AllowNull] string tableName) where Tbl :
    class
    {
        Console.Clear();

        if (tableName == null)
            tableName = "";

        Console.WriteLine("\n\n");

        ConsoleTableBuilder
        .From(tableData)
        .WithColumn(tableName)
        .ExportAndWriteLine(TableAligntment.Center);
        Console.WriteLine("\n\n");


    }


}
