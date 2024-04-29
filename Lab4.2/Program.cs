using System;
using System.Collections.Generic;
using System.Linq;

class FileRecord
{
    public string FileName { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
    public DateTime CreationDate { get; set; }
    public string Attribute { get; set; }
}

class FileManager
{
    private const string FilePath = "database.txt";

    public List<FileRecord> ReadRecords()
    {
        List<FileRecord> records = new List<FileRecord>();
        // Read records from the text file and populate the list
        return records;
    }

    public void WriteRecords(List<FileRecord> records)
    {
        // Write records to the text file
    }
}

class Program
{
    static void Main(string[] args)
    {
        FileManager fileManager = new FileManager();
        List<FileRecord> records = fileManager.ReadRecords();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Record");
            Console.WriteLine("2. Edit Record");
            Console.WriteLine("3. Delete Record");
            Console.WriteLine("4. Display Records");
            Console.WriteLine("5. Search Records by Creation Date");
            Console.WriteLine("6. Sort Records by Extension");
            Console.WriteLine("7. Display Database");
            Console.WriteLine("Enter choice:");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddRecord(records);
                    break;
                case "2":
                    EditRecord(records);
                    break;
                case "3":
                    DeleteRecord(records);
                    break;
                case "4":
                    DisplayRecordsTable(records);
                    break;
                case "5":
                    SearchRecordsByCreationDate(records);
                    break;
                case "6":
                    SortRecordsByExtension(records);
                    break;
                case "7":
                    DisplayDatabase(records);
                    break;
                case "exit":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        fileManager.WriteRecords(records);
    }

    static void AddRecord(List<FileRecord> records)
    {
        Console.WriteLine("Enter File Name:");
        string fileName = Console.ReadLine();

        Console.WriteLine("Enter Extension:");
        string extension = Console.ReadLine();

        Console.WriteLine("Enter Size:");
        if (!long.TryParse(Console.ReadLine(), out long size))
        {
            Console.WriteLine("Invalid Size!");
            return;
        }

        DateTime creationDate;
        while (true)
        {
            Console.WriteLine("Enter Creation Date (YYYY-MM-DD):");
            if (!DateTime.TryParse(Console.ReadLine(), out creationDate))
            {
                Console.WriteLine("Invalid Date Format!");
                continue;
            }
            break;
        }

        Console.WriteLine("Enter Attribute:");
        string attribute = Console.ReadLine();

        records.Add(new FileRecord
        {
            FileName = fileName,
            Extension = extension,
            Size = size,
            CreationDate = creationDate,
            Attribute = attribute
        });
        Console.WriteLine("Record Added Successfully!");
    }

    static void EditRecord(List<FileRecord> records)
    {
        Console.WriteLine("Enter File Name to Edit:");
        string fileName = Console.ReadLine();

        FileRecord record = records.FirstOrDefault(r => r.FileName == fileName);
        if (record == null)
        {
            Console.WriteLine("Record Not Found!");
            return;
        }

        Console.WriteLine("Enter New Size:");
        if (!long.TryParse(Console.ReadLine(), out long newSize))
        {
            Console.WriteLine("Invalid Size!");
            return;
        }

        record.Size = newSize;
        Console.WriteLine("Record Edited Successfully!");
    }

    static void DeleteRecord(List<FileRecord> records)
    {
        Console.WriteLine("Enter File Name to Delete:");
        string fileName = Console.ReadLine();

        FileRecord record = records.FirstOrDefault(r => r.FileName == fileName);
        if (record == null)
        {
            Console.WriteLine("Record Not Found!");
            return;
        }

        records.Remove(record);
        Console.WriteLine("Record Deleted Successfully!");
    }

    static void DisplayRecordsTable(List<FileRecord> records)
    {
        Console.WriteLine("┌────────────────────────────┬────────────┬───────┬───────────────┬────────────┐");
        Console.WriteLine("│         File Name          │ Extension  │  Size │ Creation Date │  Attribute │");
        Console.WriteLine("├────────────────────────────┼────────────┼───────┼───────────────┼────────────┤");

        foreach (var record in records)
        {
            Console.WriteLine($"│ {record.FileName,-27} │ {record.Extension,-10} │ {record.Size,-5} │ {record.CreationDate.ToString("yyyy-MM-dd"),-13} │ {record.Attribute,-10} │");
        }

        Console.WriteLine("└────────────────────────────┴────────────┴───────┴───────────────┴────────────┘");
    }

    static void DisplayRecordInfo(FileRecord record)
    {
        Console.WriteLine("┌────────────────────────────┬────────────┬───────┬───────────────┬────────────┐");
        Console.WriteLine("│         File Name          │ Extension  │  Size │ Creation Date │  Attribute │");
        Console.WriteLine("├────────────────────────────┼────────────┼───────┼───────────────┼────────────┤");
        Console.WriteLine($"│ {record.FileName,-27} │ {record.Extension,-10} │ {record.Size,-5} │ {record.CreationDate.ToString("yyyy-MM-dd"),-13} │ {record.Attribute,-10} │");
        Console.WriteLine("└────────────────────────────┴────────────┴───────┴───────────────┴────────────┘");
    }

    static void DisplayDatabase(List<FileRecord> records)
    {
        Console.WriteLine("Database Contents:");
        DisplayRecordsTable(records);
    }

    static void DisplayRecords(List<FileRecord> records)
    {
        foreach (var record in records)
        {
            Console.WriteLine($"File Name: {record.FileName}");
            Console.WriteLine($"Extension: {record.Extension}");
            Console.WriteLine($"Size: {record.Size}");
            Console.WriteLine($"Creation Date: {record.CreationDate}");
            Console.WriteLine($"Attribute: {record.Attribute}");
            Console.WriteLine();
        }
    }

    static void SearchRecordsByCreationDate(List<FileRecord> records)
    {
        Console.WriteLine("Enter Search Creation Date (YYYY-MM-DD):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime searchDate))
        {
            Console.WriteLine("Invalid Date Format!");
            return;
        }

        var searchResults = records.Where(r => r.CreationDate.Date == searchDate.Date);

        if (searchResults.Any())
        {
            Console.WriteLine("Search Results:");
            foreach (var result in searchResults)
            {
                DisplayRecordInfo(result); 
            }
        }
        else
        {
            Console.WriteLine("No matching records found.");
        }
    }

    static void SortRecordsByExtension(List<FileRecord> records)
    {
        records = records.OrderBy(r => r.Extension).ToList();
        Console.WriteLine("Records Sorted by Extension Successfully!");
        DisplayRecordsTable(records); 
    }
}
