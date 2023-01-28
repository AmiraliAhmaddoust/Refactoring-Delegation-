using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Delegation
{
    public class Preconditions
    {
        public List<structOfInheritanceTokens> structOfInheritanceToken = new List<structOfInheritanceTokens>();
        public List<DetailOfClass> detailOfClasse = new List<DetailOfClass>();
        public List<structOfInheritance> ClassMustChangetoDelegations = new List<structOfInheritance>();
        public void CreateBasics()
        {
            //abstract class detected above

            //decet open recursion

            foreach (var item in ClassMustChangetoDelegations)
            {
                bool inParentCont = false;
                for (int i = 0; i < detailOfClasse.Count(); i++)
                {
                    if (detailOfClasse[i].ClassName == item.ClassParent)
                    {
                        for (int j = 0; j < detailOfClasse[i].dtails.Count()-1; j++)
                        {
                            if (detailOfClasse[i].dtails[j].token.TokenType == TokenType.ClassIdentifires)
                            {
                                if (detailOfClasse[i].dtails[j + 1].token.Attribute == item.ClassParent)
                                {
                                    //in constructor parent
                                    inParentCont = true;
                                }
                            }
                            if (inParentCont == true)
                            {
                                if (detailOfClasse[i].dtails[j].token.TokenType == TokenType.ClosedBrace)
                                {
                                    inParentCont = false;
                                }
                                else
                                {
                                    if (detailOfClasse[i].dtails[j].token.TokenType == TokenType.OpenRecursionSymbole)
                                    {
                                        item.OpenRecurtionExistInSuperClass = true;
                                    }
                                }

                            }
                        }
                    }
                }


            }
            //throwable(Exception in c#)

            foreach (var item in ClassMustChangetoDelegations)
            {

                if (item.ClassParent == "Exception")
                {
                    item.IsParentThorwable = true;
                }
            }

            //Synchronizing method calls (Therad) both class and super class
            foreach (var item in ClassMustChangetoDelegations)
            {

                for (int i = 0; i < detailOfClasse.Count(); i++)
                {
                    if (item.ClassParent == detailOfClasse[i].ClassName || item.Chilld == detailOfClasse[i].ClassName)
                    {
                        for (int j = 0; j < detailOfClasse[i].dtails.Count(); j++)
                        {
                            if (detailOfClasse[i].dtails[j].token.TokenType == TokenType.Therad)
                            {
                                item.SynchronizingExist = true;
                                break;
                            }

                        }

                    }
                }

            }



            //2 and 4 its ok by them selfs in code

        }

        public void CanREfactorClass()
        {
            CreateBasics();
            foreach (var item in ClassMustChangetoDelegations)
            {
                if (item.IsParentAbstarct == true || item.OpenRecurtionExistInSuperClass == true || item.SynchronizingExist == true || item.IsParentThorwable == true)
                {
                    item.REfactorable = false;
                }
            }


        }
}
}
