using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Collections;
using System.Threading;
using System.Configuration;

namespace educationalProject
{
    public partial class PredictionModule : System.Web.UI.Page
    {
        Dictionary<string, double> DictionaryAllFrequentItems = new Dictionary<string, double>();

        protected void Page_Load(object sender, EventArgs e)
        {
            TrainingDS();
        }

        private void TrainingDS()
        {
            string FileName = "TrainingDataset.xls";

            string Extension = ".xls";

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string _Location = "TrainingDataset";

            string FilePath = Server.MapPath(FolderPath + FileName);

            Import_To_Grid(FilePath, Extension, _Location);
        }

        #region -- Algorithm Steps ---

        private void Solve()
        {
            double MinSupport = 0.1;
            double MinConfidence = 0.8;
            ////Scan the transaction database to get the support S of each 1-itemset,
            Dictionary<string, double> DictionaryFrequentItemsList1 = GetList1FrequentItems(MinSupport);
            Dictionary<string, double> DictionaryFrequentItemsMain = DictionaryFrequentItemsList1;
            Dictionary<string, double> DictionaryCandidates = new Dictionary<string, double>();
            do
            {
                DictionaryCandidates = GenerateCandidates(DictionaryFrequentItemsMain);
                DictionaryFrequentItemsMain = GetFrequentItems(DictionaryCandidates, MinSupport);
            }
            while (DictionaryCandidates.Count != 0);
            //MessageBox.Show("Hello");
            List<ClassRules> RulesList = GenerateRules();
            List<ClassRules> StrongRules = GetStrongRules(MinConfidence, RulesList);
            Result(DictionaryAllFrequentItems, StrongRules);
            //SolutionObject.ShowDialog();
        }

        //FUNCTION TO GET THE FIRST LIST OF FREQUENT ITEMS OCCURING IN THE SET OF TRANSACTIONS
        private Dictionary<string, double> GetList1FrequentItems(double MinSupport)
        {
            Dictionary<string, double> DictionaryFrequentItemsReturn = new Dictionary<string, double>();
            for (int i = 0; i < lv_Items.Items.Count; i++)
            {
                double Support = GetSupport(lv_Items.Items[i].Text.ToString());
                if ((Support / (double)(lv_Transactions.Items.Count) >= MinSupport))
                {
                    DictionaryFrequentItemsReturn.Add(lv_Items.Items[i].Text.ToString(), Support);

                    DictionaryAllFrequentItems.Add(lv_Items.Items[i].Text.ToString(), Support);
                }
            }
            return DictionaryFrequentItemsReturn;
        }

        //FUNCTION GETS THE SUPPORT FOR EACH INDIVIDUAL ITEMS IN SET OF TRANSACTIONS
        private double GetSupport(string GeneratedCandidate)
        {
            double SupportReturn = 0;

            string[] AllTransactions = new string[lv_Transactions.Items.Count];
            for (int i = 0; i < lv_Transactions.Items.Count; i++)
            {
                AllTransactions[i] = lv_Transactions.Items[i].Text.ToString();
            }
            foreach (string Transaction in AllTransactions)
            {
                if (IsSubstring(GeneratedCandidate, Transaction))
                {
                    SupportReturn++;
                }
            }

            return SupportReturn;
        }

        //FUNCTION TO CHECK IF THE ITEM EXISTS IN A GIVEN TRANSACTION
        private bool IsSubstring(string Child, string Parent)
        {
            string[] TransactionArray = Child.Split(',');
            //string value = null;
            foreach (string Item in TransactionArray)
            {
                if (!Parent.Contains(Item))
                    return false;
            }
            return true;
        }

        //FUNCTION TO GENERATE CANDIDATES FROM THE FREQUENT ITEM LIST
        //GET THE FIRST ITEM - ADD THE NEXT ITEM - SORT ITEMS
        //GET THE CANDIDATES EXCLUDING THE SIMILAR ITEMS
        //GET SUPPORT AND ADD TO DICTIONARY

        private Dictionary<string, double> GenerateCandidates(Dictionary<string, double> MainFrequentItems)
        {
            Dictionary<string, double> DictionaryCandidatesReturn = new Dictionary<string, double>();
            for (int i = 0; i < MainFrequentItems.Count - 1; i++)
            {
                string[] FirstItem = Alphabetize(MainFrequentItems.Keys.ElementAt(i));
                string FirstItemString = null;
                for (int k = 0; k < FirstItem.Length; k++)
                {
                    FirstItemString += FirstItem[k].ToString() + ",";
                }
                FirstItemString = FirstItemString.Remove(FirstItemString.Length - 1);
                for (int j = i + 1; j < MainFrequentItems.Count; j++)
                {
                    string[] SecondItem = Alphabetize(MainFrequentItems.Keys.ElementAt(j));
                    string SecondItemString = null;
                    for (int l = 0; l < SecondItem.Length; l++)
                    {
                        SecondItemString += SecondItem[l].ToString() + ",";
                    }
                    SecondItemString = SecondItemString.Remove(SecondItemString.Length - 1);
                    string GeneratedCandidate = GetCandidate(FirstItemString, SecondItemString);
                    //MessageBox.Show("A " + GeneratedCandidate);
                    //string GeneratedCandidate = GetCandidate("Brush,Lace,Socks,Shoe", "Brush,Lace,Socks,Polish");
                    if (GeneratedCandidate != string.Empty)
                    {
                        string[] CandidateArray = Alphabetize(GeneratedCandidate);
                        GeneratedCandidate = "";
                        for (int m = 0; m < CandidateArray.Length; m++)
                        {
                            GeneratedCandidate += CandidateArray[m].ToString() + ",";
                        }

                        GeneratedCandidate = GeneratedCandidate.Remove(GeneratedCandidate.Length - 1);
                        double Support = GetSupport(GeneratedCandidate);
                        DictionaryCandidatesReturn.Add(GeneratedCandidate, Support);
                    }
                }
            }
            return DictionaryCandidatesReturn;
        }

        //FUNCTION TO SORT THE GIVEN ITEMS IN ALPHABETICAL ORDER
        private string[] Alphabetize(string Token)
        {
            // Convert to char array, then sort and return
            string[] TokenArray = Token.Split(',');
            Array.Sort(TokenArray);
            return TokenArray;
        }

        //FUNCTION TO GET CANDIDATE EXCLUDING THE SIMILAR ITEMS.
        private string GetCandidate(string FirstItemString, string SecondItemString)
        {
            string CandidateJoin = null;
            if (FirstItemString.Contains(',') || SecondItemString.Contains(','))
            {
                string[] First = FirstItemString.Split(',');
                string[] Second = SecondItemString.Split(',');
                if (First[0] != Second[0])
                {
                    return string.Empty;
                }
                else
                {
                    string firstString = FirstItemString.Substring(0, FirstItemString.LastIndexOf(','));
                    string secondString = SecondItemString.Substring(0, SecondItemString.LastIndexOf(','));
                    if (firstString == secondString)
                    {
                        return FirstItemString + SecondItemString.Substring(SecondItemString.LastIndexOf(','));
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                ////int i=0;
                ////int x = 0;
                ////for ( i = 0; i < First.Length; i++)
                ////{
                ////    if (Second.Length > i)
                ////    {
                ////        if (First[i] == Second[i])
                ////        {
                ////            CandidateJoin = CandidateJoin + "," + First[i];
                ////            x = i;
                ////        }
                ////    }
                ////}

                ////for (i=x+1; i < First.Length; i++)
                ////{
                ////    CandidateJoin = CandidateJoin + "," + First[i];
                ////}
                ////for (x=x+1; x < Second.Length; x++)
                ////{
                ////    CandidateJoin = CandidateJoin + "," + Second[x];
                ////}
                ////return CandidateJoin.Substring(1);


                //string FirstSubString = FirstItemString.Substring(0, FirstItemString.IndexOf(','));
                //string SecondSubString = SecondItemString.Substring(0, SecondItemString.IndexOf(','));
                //if (FirstSubString == SecondSubString)
                //{
                //    return FirstItemString + SecondItemString.Substring(SecondItemString.IndexOf(','));
                //}
                //else
                //    return string.Empty;
            }
            else
            {
                return FirstItemString + "," + SecondItemString;
            }
        }

        //FUNCTION TO GET FREQUENT ITEMS THROUGH GIVEN SUPPORT
        private Dictionary<string, double> GetFrequentItems(Dictionary<string, double> CandidatesDictionary, double MinimumSupport)
        {
            Dictionary<string, double> FrequentReturn = new Dictionary<string, double>();
            for (int i = CandidatesDictionary.Count - 1; i >= 0; i--)
            {
                string Item = CandidatesDictionary.Keys.ElementAt(i);
                double Support = CandidatesDictionary[Item];
                if ((Support / (double)(lv_Transactions.Items.Count) >= MinimumSupport))
                {
                    FrequentReturn.Add(Item, Support);
                    DictionaryAllFrequentItems.Add(Item, Support);
                }
            }
            return FrequentReturn;
        }

        //FUNCTION TO GENERATE RULES
        private List<ClassRules> GenerateRules()
        {
            List<ClassRules> RulesReturnList = new List<ClassRules>();
            foreach (string Item in DictionaryAllFrequentItems.Keys)
            {
                string[] ItemArray = Item.Split(',');
                if (ItemArray.Length > 1)
                {
                    int MaxCombinationLength = ItemArray.Length / 2;
                    GenerateCombination(Item, MaxCombinationLength, ref RulesReturnList);
                }
            }
            return RulesReturnList;
        }

        private void GenerateCombination(string Item, int CombinationLength, ref List<ClassRules> RulesReturnList)
        {
            string[] ItemArray = Item.Split(',');
            int ItemLength = ItemArray.Length;
            if (ItemLength == 2)
            {
                AddItem(ItemArray[0].ToString(), Item, ref RulesReturnList);
                return;
            }
            else if (ItemLength == 3)
            {
                for (int i = 0; i < ItemLength; i++)
                {
                    AddItem(ItemArray[i].ToString(), Item, ref RulesReturnList);
                }
                return;
            }
            else
            {
                for (int i = 0; i < ItemLength; i++)
                {
                    GetCombinationRecursive(ItemArray[i].ToString(), Item, CombinationLength, ref RulesReturnList);
                }
            }
        }

        private void AddItem(string Combination, string Item, ref List<ClassRules> RulesReturnList)
        {
            string Remaining = GetRemaining(Combination, Item);
            ClassRules Rule = new ClassRules(Combination, Remaining, 0);
            RulesReturnList.Add(Rule);
        }

        private string GetCombinationRecursive(string Combination, string Item, int CombinationLength, ref List<ClassRules> RulesReturnList)
        {
            AddItem(Combination, Item, ref RulesReturnList);
            string LastTokenItem = Combination;
            if (Combination.Contains(','))
                LastTokenItem = Combination.Substring(Combination.LastIndexOf(',') + 1);

            string NextItem = null; ;
            string LastItem = Item.Substring(Item.LastIndexOf(',') + 1);
            if (Combination.Split(',').Length == CombinationLength)
            {
                if (LastTokenItem != LastItem)
                {
                    string TempCombination = null;
                    foreach (string str in Combination.Split(','))
                    {
                        if (str != LastTokenItem)
                        {
                            TempCombination = TempCombination + "," + str;
                        }
                    }
                    Combination = TempCombination.Substring(1);
                    string[] strs = Item.Split(',');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (strs[i] == LastTokenItem)
                        {
                            NextItem = strs[i + 1];
                        }
                    }
                    //Combination = Combination.Remove(nLastTokenCharcaterIndex, 1);
                    //NextItem = Item[nLastTokenCharcaterIndexInParent + 1];
                    string strNewToken = Combination + "," + NextItem;
                    return (GetCombinationRecursive(strNewToken, Item, CombinationLength, ref RulesReturnList));
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                if (Combination != LastItem.ToString())
                {
                    string[] strs = Item.Split(',');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (strs[i] == LastTokenItem)
                        {
                            NextItem = strs[i + 1];
                        }
                    }
                    //NextItem = Item[nLastTokenCharcaterIndexInParent + 1];
                    string strNewToken = Combination + "," + NextItem;
                    return (GetCombinationRecursive(strNewToken, Item, CombinationLength, ref RulesReturnList));
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string GetRemaining(string Child, string Parent)
        {
            string[] childArray = Child.Split(',');
            for (int i = 0; i < childArray.Length; i++)
            {
                string Remaining = null;
                string[] ParentArray = Parent.Split(',');
                for (int j = 0; j < ParentArray.Length; j++)
                {
                    if (childArray[i] != ParentArray[j])
                    {
                        Remaining = Remaining + "," + ParentArray[j];
                    }
                }
                if (Remaining.Contains(','))
                    Parent = Remaining.Substring(1);
                else
                    Parent = Remaining;
            }
            return Parent;
        }

        private List<ClassRules> GetStrongRules(double MinConfidence, List<ClassRules> RulesList)
        {
            List<ClassRules> StrongRulesReturn = new List<ClassRules>();
            foreach (ClassRules Rule in RulesList)
            {
                string[] XY = Alphabetize(Rule.X + "," + Rule.Y);
                string XYString = null;
                for (int i = 0; i < XY.Length; i++)
                {
                    XYString += XY[i] + ",";
                }
                XYString = XYString.Remove(XYString.Length - 1);
                AddStrongRule(Rule, XYString, ref StrongRulesReturn, MinConfidence);
            }
            StrongRulesReturn.Sort();
            return StrongRulesReturn;
        }

        private void AddStrongRule(ClassRules Rule, string XY, ref List<ClassRules> StrongRulesReturn, double MinConfidence)
        {
            double Confidence = GetConfidence(Rule.X, XY);
            ClassRules NewRule;
            if (Confidence >= MinConfidence)
            {
                NewRule = new ClassRules(Rule.X, Rule.Y, Confidence);
                StrongRulesReturn.Add(NewRule);
            }
            Confidence = GetConfidence(Rule.Y, XY);
            if (Confidence >= MinConfidence)
            {
                NewRule = new ClassRules(Rule.Y, Rule.X, Confidence);
                StrongRulesReturn.Add(NewRule);
            }
        }

        private double GetConfidence(string X, string XY)
        {
            double Support_X, Support_XY;
            Support_X = DictionaryAllFrequentItems[X];
            Support_XY = DictionaryAllFrequentItems[XY];
            return Support_XY / Support_X;
        }

        public void Result(Dictionary<string, double> AllFrequentItems, List<ClassRules> StrongRulesList)
        {
            LoadFrequentItems(AllFrequentItems);
            LoadRules1(StrongRulesList);
        }

        private void LoadFrequentItems(Dictionary<string, double> AllFrequentItems)
        {

            foreach (string Item in AllFrequentItems.Keys)
            {
                ListItem items = new ListItem(Item);
                ListBox1.Items.Add(items);
            }
        }

        private void LoadRules(List<ClassRules> StrongRulesList)
        {
            Table4.Rows.Clear();
            System.Threading.Thread.Sleep(100);
            Table4.BorderStyle = BorderStyle.Double;
            Table4.GridLines = GridLines.Both;
            Table4.BorderColor = System.Drawing.Color.Black;

            TableRow mainrow = new TableRow();
            mainrow.HorizontalAlign = HorizontalAlign.Left;
            mainrow.Height = 30;
            mainrow.ForeColor = System.Drawing.Color.White;
            mainrow.Font.Bold = true;
            mainrow.BackColor = System.Drawing.Color.SteelBlue;

            TableHeaderCell cell1 = new TableHeaderCell();
            cell1.Width = 450;
            cell1.Text = "LHS";
            mainrow.Controls.Add(cell1);

            TableHeaderCell cell3 = new TableHeaderCell();
            cell3.Width = 100;
            cell3.Text = "->";
            mainrow.Controls.Add(cell3);

            TableHeaderCell cell4 = new TableHeaderCell();
            cell4.Width = 450;
            cell4.Text = "RHS";
            mainrow.Controls.Add(cell4);

            TableHeaderCell cell2 = new TableHeaderCell();
            cell2.Text = "Confidence";
            mainrow.Controls.Add(cell2);

            Table4.Controls.Add(mainrow);

            int i = 0;

            if (StrongRulesList.Count > 0)
            {
                //Session["patterns"] = StrongRulesList;
                ListBox2.Items.Clear();

                foreach (ClassRules Rule in StrongRulesList)
                {
                    ListItem items = new ListItem(Rule.X + "->" + Rule.Y);
                    ListBox2.Items.Add(items);

                    TableRow row = new TableRow();

                    TableCell cellX1 = new TableCell();
                    cellX1.Text = Rule.X;
                    row.Controls.Add(cellX1);

                    TableCell cell_rule2 = new TableCell();
                    //cell_rule2.HorizontalAlign = HorizontalAlign.Center;
                    cell_rule2.Width = 100;
                    cell_rule2.Text = "->";
                    row.Controls.Add(cell_rule2);


                    TableCell cellY1 = new TableCell();
                    cellY1.Text = Rule.Y;
                    row.Controls.Add(cellY1);

                    TableCell cell_confidence = new TableCell();
                    cell_confidence.HorizontalAlign = HorizontalAlign.Left;
                    cell_confidence.Width = 100;
                    cell_confidence.Text = String.Format("{0:0.00}", (Rule.Confidence * 100)) + "%";
                    row.Controls.Add(cell_confidence);

                    Table4.Controls.Add(row);
                    ++i;
                }
            }
            else
            {
                Table4.Rows.Clear();
                Table4.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Patterns Found for the Input!!!";
                row.Controls.Add(cell);

                Table4.Controls.Add(row);
            }
        }

        private void LoadRules1(List<ClassRules> StrongRulesList)
        {
            Table4.Rows.Clear();

            Table4.BorderStyle = BorderStyle.Double;
            Table4.GridLines = GridLines.Both;
            Table4.BorderColor = System.Drawing.Color.DarkGray;
            System.Threading.Thread.Sleep(100);
            TableRow mainrow = new TableRow();
            mainrow.HorizontalAlign = HorizontalAlign.Left;
            mainrow.Height = 30;
            mainrow.ForeColor = System.Drawing.Color.White;
            mainrow.Font.Bold = true;
            mainrow.BackColor = System.Drawing.Color.DeepSkyBlue;

            TableHeaderCell cell1 = new TableHeaderCell();
            cell1.Width = 250;
            cell1.Text = "Rule X";
            mainrow.Controls.Add(cell1);

            TableHeaderCell cell3 = new TableHeaderCell();
            cell3.Width = 100;
            cell3.Text = "->";
            mainrow.Controls.Add(cell3);

            TableHeaderCell cell4 = new TableHeaderCell();
            cell4.Width = 250;
            cell4.Text = "Rule Y";
            mainrow.Controls.Add(cell4);

            TableHeaderCell cell2 = new TableHeaderCell();
            cell2.Text = "Confidence";
            mainrow.Controls.Add(cell2);

            Table4.Controls.Add(mainrow);

            int i = 0;

            if (StrongRulesList.Count > 0)
            {
                //Session["patterns"] = StrongRulesList;
                ListBox2.Items.Clear();

                foreach (ClassRules Rule in StrongRulesList)
                {
                    //Rule X
                    if (Rule.X.Contains(','))
                    {
                        string[] _SM = Rule.X.Split(',');

                        if (_SM.Contains("ExcellentResults") || _SM.Contains("GoodResults") || _SM.Contains("AverageResults") || _SM.Contains("PoorResults"))
                        {
                            ListItem items = new ListItem(Rule.X + "->" + Rule.Y);
                            ListBox2.Items.Add(items);

                            TableRow row = new TableRow();
                            row.ForeColor = System.Drawing.Color.Red;

                            TableCell cellX1 = new TableCell();
                            cellX1.Text = Rule.X;
                            row.Controls.Add(cellX1);

                            TableCell cell_rule2 = new TableCell();
                            //cell_rule2.HorizontalAlign = HorizontalAlign.Center;
                            cell_rule2.Width = 100;
                            cell_rule2.Text = "->";
                            row.Controls.Add(cell_rule2);


                            TableCell cellY1 = new TableCell();
                            cellY1.Text = Rule.Y;
                            row.Controls.Add(cellY1);

                            TableCell cell_confidence = new TableCell();
                            cell_confidence.HorizontalAlign = HorizontalAlign.Left;
                            cell_confidence.Width = 100;
                            cell_confidence.Text = String.Format("{0:0.00}", (Rule.Confidence * 100)) + "%";
                            row.Controls.Add(cell_confidence);

                            Table4.Controls.Add(row);
                            ++i;
                        }
                        else
                        {
                            ListItem items = new ListItem(Rule.X + "->" + Rule.Y);
                            ListBox2.Items.Add(items);

                            TableRow row = new TableRow();

                            TableCell cellX1 = new TableCell();
                            cellX1.Text = Rule.X;
                            row.Controls.Add(cellX1);

                            TableCell cell_rule2 = new TableCell();
                            //cell_rule2.HorizontalAlign = HorizontalAlign.Center;
                            cell_rule2.Width = 100;
                            cell_rule2.Text = "->";
                            row.Controls.Add(cell_rule2);


                            TableCell cellY1 = new TableCell();
                            cellY1.Text = Rule.Y;
                            row.Controls.Add(cellY1);

                            TableCell cell_confidence = new TableCell();
                            cell_confidence.HorizontalAlign = HorizontalAlign.Left;
                            cell_confidence.Width = 100;
                            cell_confidence.Text = String.Format("{0:0.00}", (Rule.Confidence * 100)) + "%";
                            row.Controls.Add(cell_confidence);

                            Table4.Controls.Add(row);
                            ++i;
                        }

                    }
                    else
                    {
                        if (Rule.X.Contains("ExcellentResults") || Rule.X.Contains("GoodResults") || Rule.X.Contains("AverageResults") || Rule.X.Contains("PoorResults"))
                        {
                            ListItem items = new ListItem(Rule.X + "->" + Rule.Y);
                            ListBox2.Items.Add(items);

                            TableRow row = new TableRow();
                            row.ForeColor = System.Drawing.Color.Red;

                            TableCell cellX1 = new TableCell();
                            cellX1.Text = Rule.X;
                            row.Controls.Add(cellX1);

                            TableCell cell_rule2 = new TableCell();
                            //cell_rule2.HorizontalAlign = HorizontalAlign.Center;
                            cell_rule2.Width = 100;
                            cell_rule2.Text = "->";
                            row.Controls.Add(cell_rule2);


                            TableCell cellY1 = new TableCell();
                            cellY1.Text = Rule.Y;
                            row.Controls.Add(cellY1);

                            TableCell cell_confidence = new TableCell();
                            cell_confidence.HorizontalAlign = HorizontalAlign.Left;
                            cell_confidence.Width = 100;
                            cell_confidence.Text = String.Format("{0:0.00}", (Rule.Confidence * 100)) + "%";
                            row.Controls.Add(cell_confidence);

                            Table4.Controls.Add(row);
                            ++i;
                        }

                    }


                    //rule Y
                    if (Rule.Y.Contains(','))
                    {
                        string[] _SM = Rule.Y.Split(',');

                        if (_SM.Contains("ExcellentResults") || _SM.Contains("GoodResults") || _SM.Contains("AverageResults") || _SM.Contains("PoorResults"))
                        {
                            ListItem items = new ListItem(Rule.X + "->" + Rule.Y);
                            ListBox2.Items.Add(items);

                            TableRow row = new TableRow();
                            row.ForeColor = System.Drawing.Color.Red;

                            TableCell cellX1 = new TableCell();
                            cellX1.Text = Rule.X;
                            row.Controls.Add(cellX1);

                            TableCell cell_rule2 = new TableCell();
                            //cell_rule2.HorizontalAlign = HorizontalAlign.Center;
                            cell_rule2.Width = 100;
                            cell_rule2.Text = "->";
                            row.Controls.Add(cell_rule2);


                            TableCell cellY1 = new TableCell();
                            cellY1.Text = Rule.Y;
                            row.Controls.Add(cellY1);

                            TableCell cell_confidence = new TableCell();
                            cell_confidence.HorizontalAlign = HorizontalAlign.Left;
                            cell_confidence.Width = 100;
                            cell_confidence.Text = String.Format("{0:0.00}", (Rule.Confidence * 100)) + "%";
                            row.Controls.Add(cell_confidence);

                            Table4.Controls.Add(row);
                            ++i;
                        }

                    }
                    else
                    {
                        if (Rule.Y.Contains("ExcellentResults") || Rule.Y.Contains("GoodResults") || Rule.Y.Contains("AverageResults") || Rule.Y.Contains("PoorResults"))
                        {
                            ListItem items = new ListItem(Rule.X + "->" + Rule.Y);
                            ListBox2.Items.Add(items);

                            TableRow row = new TableRow();
                            row.ForeColor = System.Drawing.Color.Red;

                            TableCell cellX1 = new TableCell();
                            cellX1.Text = Rule.X;
                            row.Controls.Add(cellX1);

                            TableCell cell_rule2 = new TableCell();
                            //cell_rule2.HorizontalAlign = HorizontalAlign.Center;
                            cell_rule2.Width = 100;
                            cell_rule2.Text = "->";
                            row.Controls.Add(cell_rule2);


                            TableCell cellY1 = new TableCell();
                            cellY1.Text = Rule.Y;
                            row.Controls.Add(cellY1);

                            TableCell cell_confidence = new TableCell();
                            cell_confidence.HorizontalAlign = HorizontalAlign.Left;
                            cell_confidence.Width = 100;
                            cell_confidence.Text = String.Format("{0:0.00}", (Rule.Confidence * 100)) + "%";
                            row.Controls.Add(cell_confidence);

                            Table4.Controls.Add(row);
                            ++i;
                        }

                    }

                    ++i;
                }
            }
            else
            {
                Table4.Rows.Clear();
                Table4.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Pattrens Found for the Input!!!";
                row.Controls.Add(cell);

                Table4.Controls.Add(row);
            }
        }

        #endregion
        
        private void Import_To_Grid(string FilePath, string Extension, string _Location)
        {
            string conStr = "";

            switch (Extension)
            {

                case ".xls": //Excel 97-03

                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

                             .ConnectionString;

                    break;

                case ".xlsx": //Excel 07

                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

                              .ConnectionString;

                    break;

            }

            conStr = String.Format(conStr, FilePath, _Location);

            OleDbConnection connExcel = new OleDbConnection(conStr);

            OleDbCommand cmdExcel = new OleDbCommand();

            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;

            oda.Fill(dt);

            //BLL obj = new BLL();

            if (dt.Rows.Count > 0)
            {
                //Bind Data to GridView
                lv_Transactions.Items.Clear();
                
                lv_Items.Items.Clear();
                DictionaryAllFrequentItems.Clear();
                ListBox1.Items.Clear();
                ListBox2.Items.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string _transaction = dt.Rows[i]["MentalHealthProblems"].ToString() + "," + dt.Rows[i]["Result"].ToString();

                    lv_Transactions.Items.Add(_transaction);

                    //code to identify the distinct items
                    string[] items = null;
                    items = lv_Transactions.Items[i].Text.Split(',');

                    for (int w = 0; w < items.Length; w++)
                    {
                        ListItem item = new ListItem();
                        item.Text = items[w];

                        if (lv_Items.Items.Contains(item))
                        {

                        }
                        else
                        {
                            if (item.Text.Equals(""))
                            {

                            }
                            else
                            {
                                lv_Items.Items.Add(items[w]);
                            }

                        }
                    }
                }

                string _time;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Thread.Sleep(50);
                
                
                
                Solve();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                _time = elapsedMs.ToString();

                lblTime.Text = string.Empty;
                lblTime.ForeColor = System.Drawing.Color.Red;
                lblTime.Font.Bold = true;
                lblTime.Text = "Execution Time: " + _time + " milliseconds";

                Session["A_Time"] = null;
                Session["A_Time"] = _time;        
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('No Training Dataset Found!!!')</script>");
            }

            connExcel.Close();
        }               

    }
}