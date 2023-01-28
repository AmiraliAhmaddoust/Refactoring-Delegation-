using SA.Delegation;
using SA.Lexer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SA
{
   
    class Program
    {
        static void Main(string[] args)
        {
         
            

            List<string> AllSources = new List<string>();
         //   var t = GetAllFiles("D:\\Refactoring\\Refactorint");
            var t = GetAllFiles("D:\\Refactoring\\Refactoring");
            List<string> dd = new List<string>();
            foreach (var item in t)
            {
                if (item.ToString().Contains(".cs"))
                {
                    if (item.ToString().Contains(".cspr") || item.ToString().Contains("Debug") || item.ToString().Contains("Properties"))
                    {

                    }
                    else
                    {
                        dd.Add(item.ToString());
                        // dd[l] = item.ToString();
                        // l++;
                    }
                }

            }


            foreach (var d in dd)
            {
               
                        AllSources.Add(File.ReadAllText(d));
                   
               
            }

            string MainSource1 = "";
            foreach (var item in AllSources)
            {
                MainSource1 += item;
            }







            //foreach (var document in Documents)
            //{
            //    if (document.Contains(".cs")){
            //        if (document.Contains(".cspr")) { }
            //        else
            //        {
            //            sources.Add(File.ReadAllText(document));
            //        }
            //    }
            //}
            //string MainSource = "";
            //foreach(var item in sources)
            //{
            //    MainSource += item;
            //}
           
            List<string>SeprationFiles=new List<string>();
            var tokenss = new Lexer_().Tokenize(MainSource1);
         
            List<Token> KeepUsings=new List<Token>();
            bool Remove_loop = false;
            bool TryTOFindSeoartionClass=false;
            for(int i = 0; i < tokenss.Count; i++)
            {
                if (tokenss[i].Attribute == "namespace")
                {
                    TryTOFindSeoartionClass = true;
                }
                if (TryTOFindSeoartionClass == true)
                {
                    if (tokenss[i].Attribute == "class" ||tokenss[i].TokenType==TokenType.Interface )
                    {
                        TryTOFindSeoartionClass = false;
                        SeprationFiles.Add(tokenss[i + 1].Attribute);
                    }
                }
                if (Remove_loop == true || tokenss[i].TokenType == TokenType.Using)
                {
                    if (tokenss[i].TokenType == TokenType.OpenBrace)
                    {
                        Remove_loop = false;
                        KeepUsings.Add(tokenss[i]);
                        tokenss.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        Remove_loop = true;
                        KeepUsings.Add(tokenss[i]);
                        tokenss.RemoveAt(i);
                        i--;
                    }
                }
             
            }
           
            //آدرس فایل تست ورودی در زیر تصحیح باید بشه فایل ها توی پوشه اینپوت قرار داره
            //string source = File.ReadAllText("");
            //// try
            //// {
            //var tokens = new Lexer().Tokenize(source);
            ////چاپ کردن توکن ها
            //foreach (var item in tokenss)
            //{
            //    Console.WriteLine(item);
            //}

            DecetcInheritance decetcInheritance = new DecetcInheritance(tokenss);
          //  decetcInheritance.DetectionInheritanceClass();
            decetcInheritance.InheritanceToDelegationForClass();
            //decetcInheritance.WriteAns();
            decetcInheritance.WriteAns(SeprationFiles,KeepUsings, dd);
            //Preconditions preconditions = new Preconditions();
            //preconditions.structOfInheritanceToken = decetcInheritance.structOfInheritanceTokens;
            //preconditions.detailOfClasse = decetcInheritance.detailOfClasses;
            //preconditions.ClassMustChangetoDelegations = decetcInheritance.ClassMustChangetoDelegation;
            //preconditions.CreateBasics();
            //    decetcInheritance.DetectionInheritanceClass();

            //Parser parser=new Parser(tokens);
            //parser.parse();
            //Console.WriteLine("Accepted!");
            //Console.WriteLine("##### Tokens #####");
            Console.ReadKey();
         
            //   }
            //   catch (Exception e)
            //   {
            //    Console.WriteLine("Not Accepted!");
            //     Console.WriteLine("###############################");
            //     Console.WriteLine(e);
            //     throw;
        //}

        }










        public static List<String> GetAllFiles(String directory)
        {
            return Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories).ToList();
        }

    }
}
