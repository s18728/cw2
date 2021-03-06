﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

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
            Dictionary<string, Studies> studiesDict = new Dictionary<string, Studies>();
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
                        if (string.IsNullOrEmpty(pole) || string.IsNullOrWhiteSpace(pole))
                        {
                            ok = false;
                            logsb.Append(line + "\t | puste pole!");
                            logsb.AppendLine();
                            break;
                        }
                    }
                    if (!ok) continue;
                    Studies studiestmp = new Studies
                    {
                        name = student[2],
                        mode = student[3]
                    };

                    Student st = new Student
                    {
                        fname = student[0],
                        lname = student[1],
                        studies = studiestmp,
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
                        ok = false;
                    }

                    if (ok)
                    {
                        if (studiesDict.ContainsKey(studiestmp.name))
                        {
                            studiesDict[studiestmp.name].numberOfStudents++;
                        }
                        else
                        {
                            studiesDict[studiestmp.name] = studiestmp;
                        }
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

            var today = DateTime.Today;
            List<ActiveStudies> activeStudies = new List<ActiveStudies>();
            foreach (var keyPar in studiesDict)
            {
                ActiveStudies tmp = new ActiveStudies()
                {
                    name = keyPar.Value.name,
                    numberOfStudents = keyPar.Value.numberOfStudents
                };
                activeStudies.Add(tmp);
            }

            Uczelnia uczelnia = new Uczelnia()
            {
                author = "Jakub Oleksiak",
                createdAt = today.ToShortDateString(),
                studenci = studenci,
                activeStudies = activeStudies
            };
            
            if (conversionType.Equals("xml"))
            {
                FileStream toResWriter = new FileStream(pathToResult, FileMode.Create);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Uczelnia));
                xmlSerializer.Serialize(toResWriter, uczelnia, ns);
                toResWriter.Close();
                toResWriter.Dispose();
            }
            else if (conversionType.Equals("json"))
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.WriteIndented = true;
                Package package = new Package();
                package.uczelnia = new Uczelnia(uczelnia);
                var json = JsonSerializer.Serialize(package, jsonSerializerOptions);
                File.WriteAllText(pathToResult, json);
            }
            
            

            

            
        }
    }
}
