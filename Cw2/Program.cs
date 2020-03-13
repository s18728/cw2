using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cw2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string pathToCsv;
            string pathToResult;
            string conversionType;
            if (args.Length == 3)
            {
                pathToCsv = args[0];
                pathToResult = args[1];
                conversionType = args[2];
            }
            else if (args.Length == 1)
            {
                pathToCsv = args[0];
                pathToResult = @"result.xml";
                conversionType = "xml";
            }else if (args.Length == 2)
            {
                pathToCsv = args[0];
                pathToResult = args[1];
                conversionType = "xml";
            }
            else if (args.Length == 0)
            {
                pathToCsv = @"dane.csv";
                pathToResult = @"result.xml";
                conversionType = "xml";
            }
            else
            {
                Console.Error.Write("Niepoprawna liczba argumentow!");
                return;
            }

            HashSet<Student> studenci = new HashSet<Student>(new OwnComparer());
            StringBuilder logsb = new StringBuilder();
            logsb.Append("Studenci nie dodani z powodu blednych danych:");
            logsb.AppendLine();
            try
            {
                
                var lines = File.ReadLines(pathToCsv);
                bool ok;
                foreach (var line in lines)
                {
                    ok = true;
                    string[] student = line.Split(',');
                    if (student.Length != 9)
                    {
                        logsb.Append(line + "\t | zla ilosc danych!");
                        logsb.AppendLine();
                        continue;
                    }

                    foreach (var pole in student)
                    {
                        if (pole.Length == 0)
                        {
                            ok = false;
                            logsb.Append(line + "\t | puste pole!");
                            logsb.AppendLine();
                            break;
                        }
                    }
                    if (!ok) continue;

                    Student st = new Student
                    {
                        fname = student[0],
                        lname = student[1],
                        studies = new Studies
                        {
                            name = student[2],
                            mode = student[3]
                        },
                        indexNumber = student[4],
                        birthdate = student[5],
                        email = student[6],
                        mothersName = student[7],
                        fathersName = student[8]
                    };
                    if (!studenci.Add(st))
                    {
                        logsb.Append(line + "\t | powtorka studenta!");
                        logsb.AppendLine();
                    }
                    
                }
            }
            catch (ArgumentException e)
            {
                logsb.AppendLine();
                logsb.Append("==================");
                logsb.AppendLine();
                logsb.Append(e);
                Console.WriteLine("Podana sciezka jest niepoprawna!");
            }
            catch (FileNotFoundException e)
            {
                logsb.AppendLine();
                logsb.Append("==================");
                logsb.AppendLine();
                logsb.Append(e);
                Console.WriteLine("Plik " + pathToCsv + " nie istnieje!");
            }
            
            File.WriteAllText(@"log.txt", logsb.ToString());

            //TODO toXML serialisation
        }
    }
}
