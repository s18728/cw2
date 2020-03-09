using System;
using System.Collections.Generic;
using System.IO;

namespace Cw2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = @"C:\Users\s18728\Desktop\cw2\dane.csv";
            HashSet<Student> studenci = new HashSet<Student>(new OwnComparer());
            try
            {
                var lines = File.ReadLines(path);
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
                Console.Error.Write("Niepoprawna sciezka!");
                //TODO to log.txt
            }
            catch (FileNotFoundException e)
            {
                Console.Error.Write("Plik nie istnieje!");
                //TODO to log.txt
            }
            

            
        }
    }
}
