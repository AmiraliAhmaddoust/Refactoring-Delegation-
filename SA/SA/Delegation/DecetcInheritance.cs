using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SA.Lexer;

namespace SA.Delegation
{
    
         public class DecetcInheritance
    {
               
                  private List<Token> tokens { get; set; }
        private List<Token> Editedtokens = new List<Token>();


        public List<DetailOfClass> detailOfClasses = new List<DetailOfClass>();
        public List<DetailOfClass> SimpleClassREfactored = new List<DetailOfClass>();


       Monitoring monitoring = new Monitoring();
       

        public List<structOfInheritanceTokens> structOfInheritanceTokens = new List<structOfInheritanceTokens>();
        //میاد مثلا میگه تایپ  کلاس است و اسم کلاس چی هست 

        public List<structOfInheritance> ClassMustChangetoDelegation = new List<structOfInheritance>();
        // private List<Token> DetectInheritanceTokens{ get; set; }
        private int lookAhead = 0;
        public DecetcInheritance(List<Token> tokens) { this.tokens = tokens.ToList(); Editedtokens = tokens.ToList(); }


        private void UnderstandNames()
        {
            int LoopStep = 0;
            bool ClassObserve = false;
            bool CodeBettweenBrace = false;
            while (LoopStep < tokens.Count)
            {

                lookAhead++;
               

                if (tokens[LoopStep].TokenType == TokenType.Class || tokens[LoopStep].TokenType == TokenType.Interface)
                {


                    if (LoopStep - 1 > 0)
                    {
                        if (tokens[LoopStep - 1].TokenType == TokenType.Abstract)
                        {
                            ClassObserve = true;
                            structOfInheritanceTokens.Add(new structOfInheritanceTokens()
                            {
                                TokenType = tokens[LoopStep].TokenType.ToString(),
                                TokenNameINProgram = tokens[lookAhead].Attribute.ToString(),
                                IsAbstarct = true,
                            });

                        }
                        else
                        {
                            ClassObserve = true;
                            structOfInheritanceTokens.Add(new structOfInheritanceTokens()
                            {
                                TokenType = tokens[LoopStep].TokenType.ToString(),
                                TokenNameINProgram = tokens[lookAhead].Attribute.ToString(),
                            });
                        }
                    }
                    else
                    {
                        ClassObserve = true;
                        structOfInheritanceTokens.Add(new structOfInheritanceTokens()
                        {
                            TokenType = tokens[LoopStep].TokenType.ToString(),
                            TokenNameINProgram = tokens[lookAhead].Attribute.ToString(),
                        });
                    }
                }

                if (ClassObserve == true)
                {
                    detailOfClasses.Add(new DetailOfClass()
                    {
                        ClassName = tokens[lookAhead].Attribute.ToString(),
                        dtails = new List<DtailsMehods> { },
                        ids = new List<DtailsMehods> { },
                        beforeClassName = new List<DtailsMehods> { },
                        StartClassToken= lookAhead

                    });
                    ClassObserve = false;
                    for (int i = LoopStep; i >= 0; i--)
                    {
                        if (tokens[i].TokenType != TokenType.ClosedBrace)
                        {
                            detailOfClasses[detailOfClasses.Count - 1].beforeClassName.Add(new DtailsMehods()
                            {
                                token = tokens[i],
                            });
                        }
                        else
                        {
                            break;
                        }
                    }

                }


                if (tokens[LoopStep].TokenType == TokenType.OpenBrace && tokens[LoopStep-2].Attribute != "namespace")
                {
                    CodeBettweenBrace = true;
                    detailOfClasses[detailOfClasses.Count - 1].dtails.Add(new DtailsMehods
                    {
                        token = tokens[LoopStep]
                    });
                }
                else if (tokens[LoopStep].TokenType == TokenType.ClosedBrace)
                {
                    detailOfClasses[detailOfClasses.Count - 1].dtails.Add(new DtailsMehods
                    {
                        token = tokens[LoopStep]
                    });
                    if (LoopStep + 4 <= tokens.Count - 1)
                    {
                        //if (tokens[LoopStep + 2].TokenType != TokenType.DataType && tokens[LoopStep + 2].TokenType != TokenType.Id && tokens[LoopStep + 1].TokenType != TokenType.ClosedBrace &&   tokens[LoopStep + 1].Attribute != "get")
                        //{

                        //    CodeBettweenBrace = false;
                        //}

                        for (int o = 1; o< 5; o++)
                        {
                            if(tokens[LoopStep+o].TokenType==TokenType.Id &&( tokens[LoopStep+o-1].TokenType==TokenType.Class || tokens[LoopStep + o - 1].TokenType == TokenType.Interface) && (tokens[LoopStep + o + 1].TokenType == TokenType.OpenBrace|| tokens[LoopStep + o + 1].TokenType == TokenType.IneritanceSymbole))
                            {
                                CodeBettweenBrace = false;
                                break;
                            }
                        }
                    }




                }

                else
                {
                    if (CodeBettweenBrace == true)
                    {
                        if (tokens[LoopStep].TokenType != TokenType.Interface)
                        {
                            
                            detailOfClasses[detailOfClasses.Count - 1].dtails.Add(new DtailsMehods
                            {
                                token = tokens[LoopStep]
                            });
                        }
                        if (tokens[lookAhead].TokenType == TokenType.Id && tokens[LoopStep].TokenType == TokenType.DataType)
                        {
                            if (tokens[LoopStep].Attribute != "void")
                            {
                                if (lookAhead + 1 < tokens.Count)
                                {
                                    if (tokens[lookAhead + 1].TokenType != TokenType.OpenParentheses && tokens[lookAhead + 1].TokenType != TokenType.ClosedParentheses)
                                    {
                                        detailOfClasses[detailOfClasses.Count - 1].ids.Add(new DtailsMehods { token = tokens[lookAhead] });
                                    }
                                }
                            }
                        }
                    }
                }

                LoopStep++;
            }
        }
      
        public void DetectionInheritanceClass()
        {
            UnderstandNames();
            for (int i = 0; i < tokens.Count; i++)
            {
                lookAhead = i + 1;
                var token1 = tokens[i];


                if (token1.TokenType == TokenType.IneritanceSymbole && tokens[i-2].Attribute=="class") //ارث بری وجود دارد با  این حساب
                {

                    string ChildClass = tokens[i - 1].Attribute.ToString();
                    if (ChildClass == ")")
                    {
                        continue;
                    }
                    string ParentClass = null;
                    bool AbstractParent = false;
                    List<string> ParentInterfaces = new List<string>();
                    // var tt = tokens[lookAhead];

                    do
                    {

                        string type = "";

                        if (tokens[lookAhead].Attribute != "Exception")
                        {
                            type = structOfInheritanceTokens.Where(t => t.TokenNameINProgram.Equals(tokens[lookAhead].Attribute)).Select(t => t.TokenType)?.FirstOrDefault().ToString();
                        }
                        else
                        {
                            type = "Class";
                        }

                        AbstractParent = structOfInheritanceTokens.Where(t => t.TokenNameINProgram.Equals(tokens[lookAhead].Attribute)).Select(t => t.IsAbstarct).FirstOrDefault();

                        if (type == "Class")
                        {
                            ParentClass = tokens[lookAhead].Attribute.ToString();

                        }
                        else if (type == "Interface")
                        {
                            ParentInterfaces.Add(tokens[lookAhead].Attribute.ToString());
                        }
                        lookAhead++;
                        if (tokens[lookAhead].TokenType == TokenType.Comma)
                        {
                            lookAhead++;
                        }
                        else if (tokens[lookAhead].TokenType == TokenType.OpenBrace)
                        {
                            break;
                        }

                    } while (true);

                    ClassMustChangetoDelegation.Add(new structOfInheritance
                    {
                        Chilld = ChildClass,
                        ClassParent = ParentClass,
                        InterfaceParennts = ParentInterfaces,
                        IsParentAbstarct = AbstractParent
                    });

                    lookAhead = i + 1;


                }
            }

        }



        public void InheritanceToDelegationForClass()
        {

            DetectionInheritanceClass();
            Preconditions preconditions = new Preconditions();
            preconditions.structOfInheritanceToken = structOfInheritanceTokens;
            preconditions.detailOfClasse = detailOfClasses;
            preconditions.ClassMustChangetoDelegations = ClassMustChangetoDelegation;
            preconditions.CanREfactorClass();
            SimpleClassREfactored.AddRange(detailOfClasses);

            foreach (var item in ClassMustChangetoDelegation)
            {

            //    if (item.REfactorable == true)
            //    {
                    for (int i = 0; i < SimpleClassREfactored.Count; i++)
                    {
                        if (item.Chilld == SimpleClassREfactored[i].ClassName)
                        {
                            for (int j = 0; j < SimpleClassREfactored[i].dtails.Count(); j++)
                            {
                                if (SimpleClassREfactored[i].dtails[j].token.TokenType == TokenType.OpenRecursionSymbole)
                                {
                                    bool ForOwnClass = false;
                                    for (int k = j + 1; k < SimpleClassREfactored[i].dtails.Count - 2; k++)
                                    {
                                        if (SimpleClassREfactored[i].dtails[k].token.TokenType == TokenType.Dot)
                                        {
                                            if (SimpleClassREfactored[i].dtails[k + 1].token.TokenType == TokenType.Id)
                                            {
                                                //if its for class or for it super class
                                                for (int o = 0; o < j; o++)
                                                {
                                                    if (SimpleClassREfactored[i].dtails[k + 1].token.Attribute == SimpleClassREfactored[i].dtails[o].token.Attribute)
                                                    {
                                                        ForOwnClass = true;
                                                        break;
                                                    }
                                                }
                                                if (ForOwnClass == false)
                                                {
                                                    //
                                                    for (int y = 0; y < ClassMustChangetoDelegation.Count; y++)
                                                    {
                                                        if (item.ClassParent == ClassMustChangetoDelegation[y].Chilld)
                                                        {
                                                        if (ClassMustChangetoDelegation[y].ClassParent != null)
                                                        {
                                                            ClassMustChangetoDelegation[y].REfactorable = false;
                                                            ClassMustChangetoDelegation[y].OpenRecurtionExistInSuperClass = true;
                                                            Token t1 = new Token(TokenType.Id, "delegatee");
                                                            SimpleClassREfactored[i].dtails.RemoveAt(j);
                                                            SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods { token = t1 });
                                                        }
                                                    }
                                                }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
            //    }//if
            }

            // int a = 0;
            foreach (var item in ClassMustChangetoDelegation)
            {
                if (item.REfactorable == true)
                {
                    List<Token> SubClassVariable = new List<Token>();
                    bool StartOvereide = false;
                    if (item.InterfaceParennts.Count() == 0)
                    {

                        for (int i = 0; i < Editedtokens.Count; i++)
                        {
                            if (Editedtokens[i].TokenType == TokenType.IneritanceSymbole && Editedtokens[i + 1].Attribute == item.ClassParent)
                            {
                                for (int j = 0; j < 2; j++)
                                    Editedtokens.RemoveAt(i);
                            }
                        }
                        for (int i = 0; i < SimpleClassREfactored.Count; i++)
                        {
                            if (item.ClassParent == SimpleClassREfactored[i].ClassName)
                            {
                                foreach (var any in SimpleClassREfactored[i].ids)
                                {

                                    SubClassVariable.Add(any.token);
                                }

                            }
                        }
                        for (int i = 0; i < SimpleClassREfactored.Count; i++)
                        {

                            if (item.Chilld == SimpleClassREfactored[i].ClassName)
                            {
                                for (int j = 0; j < SimpleClassREfactored[i].dtails.Count(); j++)
                                {

                                    if (j == 0)
                                    {
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.ClassIdentifires, "private")
                                        });

                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.Id, item.ClassParent.ToString())
                                        });

                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.Id, "delegatee")
                                        });
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.SemiColon, ";")
                                        });

                                    }
                                    if (SimpleClassREfactored[i].dtails[j].token.TokenType == TokenType.ClassIdentifires && SimpleClassREfactored[i].dtails[j + 1].token.Attribute == item.Chilld)
                                    {
                                        bool TillFinish = false;
                                        int IndexStartChange = 0;

                                        for (int k = j; k < SimpleClassREfactored[i].dtails.Count(); k++)
                                        {
                                            if (SimpleClassREfactored[i].dtails[k].token.TokenType == TokenType.IneritanceSymbole)
                                            {
                                                TillFinish = true;

                                            }
                                            if (SimpleClassREfactored[i].dtails[k].token.TokenType == TokenType.OpenBrace)
                                            {
                                                IndexStartChange = k + 1;
                                                TillFinish = false;
                                                break;
                                            }
                                            if (TillFinish == true)
                                            {
                                                if (SimpleClassREfactored[i].dtails[k].token.TokenType == TokenType.Id)
                                                {
                                                    if (SimpleClassREfactored[i].RemovedInConsdtractourbyDelegation == null)
                                                    {
                                                        SimpleClassREfactored[i].RemovedInConsdtractourbyDelegation = new List<DtailsMehods> {

                                                new DtailsMehods                                                {
                                                    token=new Token(TokenType.Id, SimpleClassREfactored[i].dtails[k].token.Attribute)
                                                }

                                                };

                                                    }
                                                    else
                                                    {
                                                        SimpleClassREfactored[i].RemovedInConsdtractourbyDelegation.Add(new DtailsMehods
                                                        {
                                                            token = new Token(TokenType.Id, SimpleClassREfactored[i].dtails[k].token.Attribute)
                                                        });
                                                    }
                                                }
                                                SimpleClassREfactored[i].dtails.RemoveAt(k);
                                                k--;
                                            }
                                        }

                                        //in constractor
                                        j = IndexStartChange;
                                        //delegatee=new class parent 
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.Id, "delegatee")
                                        });
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.Assign, "=")
                                        });
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.New, "new")
                                        });
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.Id, item.ClassParent.ToString())
                                        });
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.OpenParentheses, "(")
                                        });
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.ClosedParentheses, ")")
                                        });
                                        j = j + 1;
                                        SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                        {
                                            token = new Token(TokenType.SemiColon, ";")
                                        });



                                    }//
                                    if (SimpleClassREfactored[i].dtails[j].token.TokenType == TokenType.Override)
                                    {
                                        SimpleClassREfactored[i].dtails.RemoveAt(j);
                                        StartOvereide = true;
                                    }
                                    if (StartOvereide == true)
                                    {
                                        if (SimpleClassREfactored[i].dtails[j].token.TokenType == TokenType.Id)
                                        {
                                            if (SimpleClassREfactored[i].dtails[j - 1].token.TokenType != TokenType.DataType)
                                            {
                                                if (j + 1 < SimpleClassREfactored[i].dtails.Count())
                                                {
                                                    if (SimpleClassREfactored[i].dtails[j + 1].token.TokenType == TokenType.OpenParentheses)
                                                    {
                                                        string Parent = item.ClassParent;
                                                        int indexParent = 0;
                                                        for (int p = 0; p < SimpleClassREfactored.Count(); p++)
                                                        {
                                                            if (SimpleClassREfactored[p].ClassName == Parent)
                                                            {
                                                                indexParent = p;
                                                                break;
                                                            }
                                                        }

                                                        foreach (var any in SimpleClassREfactored[indexParent].dtails)
                                                        {

                                                            if (SimpleClassREfactored[i].dtails[j].token.Attribute == any.token.Attribute)
                                                            {
                                                                if (SimpleClassREfactored[i].dtails[j - 2].token.Attribute != "Console") { 

                                                                SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                                                {
                                                                    token = new Token(TokenType.Id, "delegatee")
                                                                });
                                                                j = j + 1;
                                                                SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                                                {
                                                                    token = new Token(TokenType.Dot, ".")
                                                                });


                                                                j = j + 1;
                                                            }
                                                        }


                                                        }


                                                    }
                                                }
                                            }
                                        }   //باید حداقل 3 توکن اسکیپ بشه 
                                    }


                                    foreach (var evrey in SubClassVariable)
                                    {
                                        if (SimpleClassREfactored[i].dtails[j].token.TokenType == evrey.TokenType && SimpleClassREfactored[i].dtails[j].token.Attribute == evrey.Attribute)
                                        {
                                            if (SimpleClassREfactored[i].dtails[j - 1].token.TokenType != TokenType.DataType)
                                            {
                                                j = j + 1;
                                                SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                                {
                                                    token = new Token(TokenType.Id, "delegatee")
                                                });
                                                j = j + 1;
                                                SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                                {
                                                    token = new Token(TokenType.Dot, ".")
                                                });
                                                j = j + 1;
                                                SimpleClassREfactored[i].dtails.Insert(j, new DtailsMehods()
                                                {
                                                    token = new Token(TokenType.Id, evrey.Attribute)
                                                });
                                                SimpleClassREfactored[i].dtails.RemoveAt(j - 3);
                                                j = j - 1;
                                            }
                                        }
                                    }

                                }
                            }

                        }


                    }
                }
                else
                {
                    bool OneCircule = false;
                    int StartClass=0;
                    for(int i = 0; i < SimpleClassREfactored.Count; i++)
                    {
                        if (SimpleClassREfactored[i].ClassName == item.Chilld)
                        {
                            StartClass = SimpleClassREfactored[i].StartClassToken;
                        }
                    }
                    for (int i = StartClass; i < tokens.Count; i++)
                    {
                        if (OneCircule != true)
                        {
                            if (tokens[i].Attribute == item.Chilld)
                            {
                                for (int j = 0; j < SimpleClassREfactored.Count; j++)
                                {
                                    if (SimpleClassREfactored[j].ClassName == item.Chilld)
                                    {
                                        int inserthome = 0;
                                        for (int k = i + 1; ; k++)
                                        {
                                            if (i + 1 > tokens.Count - 1)
                                            {
                                                break;
                                            }
                                            if (tokens[k].TokenType == TokenType.OpenBrace)
                                            {
                                                break;

                                            }
                                            else
                                            {
                                                OneCircule = true;
                                                SimpleClassREfactored[j].dtails.Insert(inserthome, new DtailsMehods { token = tokens[k] });
                                                inserthome++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
        }
               
               
          public void Monitoring()
        {
            int RefactoredClass = 0;
            int NotRefactorByAbstarct = 0;
            int NotRefactorByThrowable = 0;
            int NotRefactorByOpenRecursion = 0;
            int NotRefactorBySyncronozation = 0;
            foreach (var item in ClassMustChangetoDelegation)
            {
               
                 NotRefactorByAbstarct = 0;
                 NotRefactorByThrowable = 0;
                 NotRefactorByOpenRecursion = 0;
                 NotRefactorBySyncronozation = 0;

                if (item.REfactorable == true)
                {
                    RefactoredClass++;

                }
                else
                {
                    if (item.IsParentAbstarct == true)
                    {
                        NotRefactorByAbstarct++;
                    }
                    if(item.IsParentThorwable == true)
                    {
                        NotRefactorByThrowable++;
                    }
                    if(item.OpenRecurtionExistInSuperClass == true)
                    {
                        NotRefactorByOpenRecursion++;
                    }
                    if (item.SynchronizingExist == true)
                    {
                        NotRefactorBySyncronozation++;
                    }
                }
               
                monitoring.datilsForEachClassMonitors.Add(new DatilsForEachClassMonitor {
               
                ClassName=item.Chilld,
                NotRefactorByAbstarct=NotRefactorByAbstarct,
                NotRefactorByThrowable=NotRefactorByThrowable,
                NotRefactorByOpenRecursion=NotRefactorByOpenRecursion,
                NotRefactorBySyncronozation=NotRefactorBySyncronozation,
                Isrefactore=item.REfactorable
               
                });
            }
            monitoring.RefactoreClass = RefactoredClass;
        }     
             public  void WriteAns(List<string> SeprationFiles,List<Token>KeepUsing, List<string> DestanationPath)
        {
         
            int counter = 0;
            int CounterIndex = 0;
            Console.Clear();
            Monitoring();
            string[] DirectoryBulder = new string[DestanationPath.Count-1];
 
            string Monitoringpath = @"D:\PrograminApps\ostad sbti\sakhtemanDade_project\New folder\" + "RefactoreClass.txt";
            using (StreamWriter sw = File.CreateText(Monitoringpath))
            {
                sw.WriteLine("تعداد کلاس های رفکتور شده :  "+monitoring.RefactoreClass);
                foreach (var item in monitoring.datilsForEachClassMonitors)
                {
                   
                    if (item.Isrefactore == false)
                    {
                        sw.WriteLine(" شرح کلاس های ریفکتور نشده  ");
                        sw.WriteLine(" نام کلاس :  "+ item.ClassName);

                        if (item.NotRefactorByAbstarct != 0)
                        {
                            sw.WriteLine(" کلاس پدر ابسترکت دارد   ");
                        }
                        if(item.NotRefactorByThrowable != 0)
                        {
                            sw.WriteLine(" کلاس پدری از جنس  تروابل   دارد   ");
                        }
                        if(item.NotRefactorByOpenRecursion != 0)
                        {
                            sw.WriteLine(" یافتن اوپن ریکرشن    ");
                        }
                        if(item.NotRefactorBySyncronozation != 0)
                        {
                            sw.WriteLine(" یافتن سینکرانیزشن متد ها     ");
                        }
                    }
                   

                }
            }
            foreach (var item in SimpleClassREfactored)
            {
                if (SeprationFiles[counter] == item.ClassName)
                {
                   
                    CounterIndex = counter;


                    //یعنی باید یه فایل جدید بسازیم
                 //   File.Delete(@"D:\PrograminApps\ostad sbti\sakhtemanDade_project\New folder\" + SeprationFiles[CounterIndex] + ".cs");
                 //   string path = @"D:\PrograminApps\ostad sbti\sakhtemanDade_project\New folder\" + SeprationFiles[CounterIndex] + ".cs";
                    string path=DestanationPath.Where(x=>x.Contains(SeprationFiles[CounterIndex]+"."+"cs")).FirstOrDefault();
               File.Delete(path);  
                    using (StreamWriter sw = File.CreateText(path))
                    {
                   
                        for (int z = 0; z < KeepUsing.Count; z++)
                        {
                            if (KeepUsing[z].TokenType != TokenType.OpenBrace)
                            {
                                if (KeepUsing[z].Attribute == ".")
                                {
                                    sw.Write(KeepUsing[z].Attribute);
                                }
                                else if (KeepUsing[z].Attribute == ";")
                                {
                                    sw.WriteLine(KeepUsing[z].Attribute);
                                }
                                else
                                {
                                    sw.Write(KeepUsing[z].Attribute + " ");
                                }
                                KeepUsing.RemoveAt(z);
                                z--;
                            }
                            else
                            {
                                sw.WriteLine(KeepUsing[z].Attribute);
                                KeepUsing.RemoveAt(z);
                                break;
                            }
                        }



                        for (int i = item.beforeClassName.Count - 1; i >= 0; i--)
                        {
                            sw.Write(item.beforeClassName[i].token.Attribute + " ");
                        }
                        sw.WriteLine(item.ClassName);
                        int step = 0;
                        foreach (var any in item.dtails)
                        {
                            step++;

                            if (any.token.TokenType == TokenType.OpenBrace || any.token.TokenType == TokenType.ClosedBrace || any.token.TokenType == TokenType.SemiColon)
                            {
                                sw.WriteLine(any.token.Attribute);
                            }
                            else
                            {
                                if (step + 1 < item.dtails.Count)
                                {
                                    if (any.token.TokenType == TokenType.Dot || item.dtails[step + 1].token.TokenType == TokenType.Dot)
                                    {
                                        sw.Write(any.token.Attribute);
                                    }
                                    else
                                    {
                                        sw.Write(any.token.Attribute + " ");
                                    }
                                }
                                else
                                {
                                    sw.Write(any.token.Attribute + " ");
                                }

                            }

                        }
                    }



                    if (counter + 1 < SeprationFiles.Count)
                    {
                        counter++;
                    }

                }
                else
                {
                  //  string myfile = "D:\\PrograminApps\\ostad sbti\\sakhtemanDade_project\\New folder\\" + SeprationFiles[CounterIndex] + ".cs";
                    string myfile = DestanationPath.Where(x => x.Contains(SeprationFiles[CounterIndex])).FirstOrDefault();
                    using (StreamWriter sw = File.AppendText(myfile))
                    {
                        for (int i = item.beforeClassName.Count - 1; i >= 0; i--)
                        {
                            sw.Write(item.beforeClassName[i].token.Attribute + " ");
                        }
                        sw.WriteLine(item.ClassName);
                        int step = 0;
                        foreach (var any in item.dtails)
                        {
                            step++;

                            if (any.token.TokenType == TokenType.OpenBrace || any.token.TokenType == TokenType.ClosedBrace || any.token.TokenType == TokenType.SemiColon)
                            {
                                sw.WriteLine(any.token.Attribute);
                            }
                            else
                            {
                                if (step + 1 < item.dtails.Count)
                                {
                                    if (any.token.TokenType == TokenType.Dot || item.dtails[step + 1].token.TokenType == TokenType.Dot)
                                    {
                                        sw.Write(any.token.Attribute);
                                    }
                                    else
                                    {
                                        sw.Write(any.token.Attribute + " ");
                                    }
                                }
                                else
                                {
                                    sw.Write(any.token.Attribute + " ");
                                }

                            }

                        }
                    }
                        //در همون فایل قبلی میریزه
                     
               
                }
         
            }
       
        }  
         }
      

}
