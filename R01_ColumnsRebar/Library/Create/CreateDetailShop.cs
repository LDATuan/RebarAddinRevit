﻿using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Windows.Threading;
using System.IO;
using System;
using WpfCustomControls;
using System.Windows.Controls;
using DSP;
namespace R01_ColumnsRebar
{
    public class CreateDetailShop
    {
        
        #region Create
        public static void Create(ColumnsWindow p,ColumnsModel columnsModel, Document document,UnitProject unit,List<Element> columns)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarDetailShop( columnsModel) * 1.0;
            string folder = FolderImage(document, columnsModel); columnsModel.ProgressModel.SetValue(uc, 1);
            CreateDetailShopRebar(p,columnsModel,document,unit, folder, columns);
            columnsModel.ProgressModel.ResetValue(uc);
        }

        private static void CreateDetailShopRebar(ColumnsWindow p, ColumnsModel columnsModel, Document document, UnitProject unit, string folder, List<Element> columns)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            double offset0 = (columnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE) ? (Math.Max((columnsModel.InfoModels[0].b), (columnsModel.InfoModels[0].h))) : (columnsModel.InfoModels[0].D);
            int number = 1;
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("aaa");
                columnsModel.DetailShopView.CreateDetailShopView(columnsModel.SectionStyle, document, unit, columnsModel.InfoModels, columnsModel.PlanarFaces, columns, columnsModel.SettingModel, columnsModel.SettingModel.DetailViewName + "S", columnsModel.SettingModel.L1, columnsModel.SettingModel.L2 * 5);
                columnsModel.ProgressModel.SetValue(uc, 1);
                for (int i = 0; i < columnsModel.BarsDivisionModels.Count; i++)
                {
                    if (columnsModel.BarsDivisionModels[i].Stirrup.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarsDivisionModels[i].Stirrup.Count; j++)
                        {
                            columnsModel.BarsDivisionModels[i].Stirrup[j].CreateDetailItem(document, columnsModel, unit, 0, number); columnsModel.ProgressModel.SetValue(uc, 1);
                            //columnsModel.BarsDivisionModels[i].Stirrup[j].CreateImage(p, document, columnsModel.SettingModel, folder, number); SetValue(p, columnsModel, 1);
                        }
                        columnsModel.BarsDivisionModels[i].Stirrup[0].CreateImage(p, document, columnsModel.SettingModel, folder, number); columnsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                }
                for (int i = 0; i < columnsModel.BarsDivisionModels.Count; i++)
                {
                    if (columnsModel.BarsDivisionModels[i].AddH.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarsDivisionModels[i].AddH.Count; j++)
                        {
                            columnsModel.BarsDivisionModels[i].AddH[j].CreateDetailItem(document, columnsModel, unit, offset0 * 0.5, number); columnsModel.ProgressModel.SetValue(uc, 1);
                            //columnsModel.BarsDivisionModels[i].AddH[j].CreateImage(p, document, columnsModel.SettingModel, folder, number); SetValue(p, columnsModel, 1);
                        }
                        columnsModel.BarsDivisionModels[i].AddH[0].CreateImage(p, document, columnsModel.SettingModel, folder, number); columnsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                    if (columnsModel.BarsDivisionModels[i].AddV.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarsDivisionModels[i].AddV.Count; j++)
                        {
                            columnsModel.BarsDivisionModels[i].AddV[j].CreateDetailItem(document, columnsModel, unit, offset0 * 0.5, number); columnsModel.ProgressModel.SetValue(uc, 1);
                            //columnsModel.BarsDivisionModels[i].AddV[j].CreateImage(p, document, columnsModel.SettingModel, folder, number); SetValue(p, columnsModel, 1);
                        }
                        columnsModel.BarsDivisionModels[i].AddV[0].CreateImage(p, document, columnsModel.SettingModel, folder, number); columnsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                }
                for (int i = 0; i < columnsModel.BarsDivisionModels.Count; i++)
                {
                    if (columnsModel.BarsDivisionModels[i].Main.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarsDivisionModels[i].Main.Count; j++)
                        {
                            columnsModel.BarsDivisionModels[i].Main[j].CreateDetailItem(document, columnsModel, unit, offset0  + j * columnsModel.SettingModel.L2, number); columnsModel.ProgressModel.SetValue(uc, 1);
                            columnsModel.BarsDivisionModels[i].Main[j].CreateImage(p, document, columnsModel.SettingModel, folder, number); columnsModel.ProgressModel.SetValue(uc, 1);
                            number++;
                        }
                    }
                }
                columnsModel.DetailColumnView.CreateSchedule(document, columnsModel.SettingModel);
                transaction.Commit();
                columnsModel.IsCreateDetailShop = true;
            }
        }

        public static string FolderImage(Document document, ColumnsModel columnsModel)
        {
            string folderName = document.PathName.Replace( document.Title + ".rvt", "ImageRebar");
            string pathString = Path.Combine(folderName, columnsModel.SettingModel.ColumnsName);
            Directory.CreateDirectory(pathString);
            return pathString;
        }
        #endregion
        #region Load Family


        #endregion
        #region Action

        private static int GetProgressBarDetailShop( ColumnsModel columnsModel)
        {
            int a = 0;
            a +=1;
            a += 1;
            for (int i = 0; i < columnsModel.BarsDivisionModels.Count; i++)
            {
                if (columnsModel.BarsDivisionModels[i].Stirrup.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarsDivisionModels[i].Stirrup.Count; j++)
                    {
                        a += 2;
                    }
                }
            }
            for (int i = 0; i < columnsModel.BarsDivisionModels.Count; i++)
            {
                if (columnsModel.BarsDivisionModels[i].AddH.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarsDivisionModels[i].AddH.Count; j++)
                    {
                        a += 2;
                    }
                }
                if (columnsModel.BarsDivisionModels[i].AddV.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarsDivisionModels[i].AddV.Count; j++)
                    {
                        a+= 2;
                    }
                }
            }
            for (int i = 0; i < columnsModel.BarsDivisionModels.Count; i++)
            {
                if (columnsModel.BarsDivisionModels[i].Main.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarsDivisionModels[i].Main.Count; j++)
                    {
                        a += 2;
                    }
                }
            }
            return a;
        }
        
      
        //private static bool ConditionDrawOverlap(ItemDivision itemDivision1, ItemDivision itemDivision2)
        //{
        //    if (itemDivision1.Type == DetailShopStyle.DS02 || itemDivision1.Type == DetailShopStyle.DS03 || itemDivision1.Type == DetailShopStyle.DS05 || itemDivision1.Type == DetailShopStyle.DS06)
        //    {
        //        return false;
        //    }
        //    if (itemDivision2.Type == DetailShopStyle.DS01 || itemDivision2.Type == DetailShopStyle.DS03 || itemDivision2.Type == DetailShopStyle.DS04 || itemDivision2.Type == DetailShopStyle.DS06)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        #endregion
    }
}
