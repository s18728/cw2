using System;
using System.Collections.Generic;
using System.IO;

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
                pathToCsv = @"data.csv";
                pathToResult = @"result.xml";
                conversionType = "xml";
            }
            else
            {
                Console.Error.Write("Niepoprawna liczba argumentow!");
                return;
            }

            HashSet<Student> studenci = new HashSet<Student>(new OwnComparer());
            try
            {
                var lines = File.ReadLines(pathToCsv);
                bool ok = true;
                foreach (var line in lines)
                {
                    string[] student = line.Split(',');
                    if (student.Length != 9)
                    {
                        //TODO do log.txt
                        continue;
                    }

                    foreach (var pole in student)
                    {
                        if (pole.Length == 0)
                        {
                            ok = false;
                            //TODO do log.txt
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
                        //TODO nie dodane, do log.txt
                    }
                    
                }
            }
            catch (ArgumentException e)
            {
                //TODO to log.txt
                throw new ArgumentException("Podana sciezka jest niepoprawna!");
            }
            catch (FileNotFoundException e)
            {
                //TODO to log.txt
                throw new FileNotFoundException("Plik " + pathToCsv + " nie istnieje!");
            }
            

            //TODO toXML serialisation
        }
    }
}
