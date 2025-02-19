﻿
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfCustomControls;
using DSP;
namespace R02_BeamsRebar
{
    public class CreateRebarDetailtem
    {
        #region Create
        public static void Create(string action, BeamsWindow p, BeamsModel BeamsModel, Document document, UnitProject unit)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarRebarDetailItem(document, BeamsModel);
            bool hasSpecial = (BeamsModel.SpecialBarModel.Count != 0);
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            CreateStirrupBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateAntiStirrupBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateDimensionStirrup(action, uc, BeamsModel, document, unit, dsmax);
            CreateMainTopBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateMainBottomBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateAddTopBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateAddBottomBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateDimensionAddBottomBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateSideBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateSpecialBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateSpecialStirrupBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateStirrupSectionBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateLongSectionBar(action, uc, BeamsModel, document, unit, dsmax);
        }
        #endregion
        #region Create Item
        private static void CreateStirrupBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.StirrupsDetail.Count!=0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.StirrupsDetail.Count; i++)
                {
                    InfoModel b = BeamsModel.InfoModels.Where(x => x.startOffset <= BeamsModel.DetailItemModel.StirrupsDetail[i].Location.X && x.endPosition >= BeamsModel.DetailItemModel.StirrupsDetail[i].Location.X + BeamsModel.DetailItemModel.StirrupsDetail[i].Length).FirstOrDefault();
                    double L1 = BeamsModel.DetailItemModel.StirrupsDetail[i].Location.X + BeamsModel.DetailItemModel.StirrupsDetail[i].Length / 2 - b.startPosition;
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[0]);
                        BeamsModel.DetailItemModel.StirrupsDetail[i].CreateStirrupDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        BeamsModel.DetailItemModel.StirrupsDetail[i].CreateTagStirrupDetail(BeamsModel.DetailBeamView.DetailView, b, document, unit, BeamsModel.SettingModel, L1, BeamsModel.SettingModel.L1 * 0.85);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }
                    
                }
            }
            if (BeamsModel.DetailItemModel.AntiSection.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SectionBeamViews.Count; i++)
                {
                    List<DetailItem> detailItems = BeamsModel.DetailItemModel.AntiSection.Where(x => x.Location.X >= BeamsModel.InfoModels[i].startPosition && x.Location.X <= BeamsModel.InfoModels[i].endPosition).ToList();
                    if (detailItems.Count != 0)
                    {
                        if (detailItems.Count == 3)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[1]);
                                detailItems[0].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[1].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[2].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                        if (detailItems.Count == 6)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[1]);
                                detailItems[0].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[1].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[2].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[3].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[4].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[5].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }

                    }
                }
            }
        }
        private static void CreateAntiStirrupBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.AntiSection.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SectionBeamViews.Count; i++)
                {
                    List<DetailItem> detailItems = BeamsModel.DetailItemModel.AntiSection.Where(x => x.Location.X >= BeamsModel.InfoModels[i].startPosition && x.Location.X <= BeamsModel.InfoModels[i].endPosition).ToList();
                    if (detailItems.Count != 0)
                    {
                        if (detailItems.Count == 3)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[1]);
                                detailItems[0].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[1].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[2].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                        if (detailItems.Count == 6)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[1]);
                                detailItems[0].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[1].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[2].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[3].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[4].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[5].CreateAntiStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }

                    }
                }
            }
        }
        private static void CreateDimensionStirrup(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[2]);
                    BeamsModel.ReferenceStirrupBar.Append(BeamsModel.StirrupModels[i].GetReferenceItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.DistributeStirrups[i], BeamsModel.PlanarFaces[0], unit, BeamsModel.DistributeStirrups[i].L1));
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    BeamsModel.ReferenceStirrupBar.Append(BeamsModel.StirrupModels[i].GetReferenceItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.DistributeStirrups[i], BeamsModel.PlanarFaces[0], unit, BeamsModel.DistributeStirrups[i].L1 + BeamsModel.DistributeStirrups[i].L2));
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }

            }
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[2]);
                BeamsModel.DimensionView.CreateDimensionHorizontalAddTopBar(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.ReferenceStirrupBar, BeamsModel.SettingModel, BeamsModel.InfoModels, true, BeamsModel.SettingModel.L1);
                BeamsModel.ProgressModel.SetValue(uc, 1);
                transaction.Commit();
            }
        }
        private static void CreateMainTopBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.MainTopDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.MainTopDetail.Count; i++)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[3]);
                        BeamsModel.DetailItemModel.MainTopDetail[i].CreateLongDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }
                }
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    DetailItem a = null;
                    if (i == 0 && BeamsModel.InfoModels[i].ConsolLeft)
                    {
                        a = BeamsModel.DetailItemModel.MainTopDetail.Where(x => ((x.AllLocation[0].X) > BeamsModel.InfoModels[i].startPosition) && (x.AllLocation[0].X != x.AllLocation[1].X)).FirstOrDefault();
                    }
                    else
                    {
                        a = BeamsModel.DetailItemModel.MainTopDetail.Where(x => ((x.AllLocation[0].X) < BeamsModel.InfoModels[i].startPosition) && ((x.AllLocation[1].X) > BeamsModel.InfoModels[i].endPosition)).FirstOrDefault();
                    }
                    if (a != null)
                    {
                        double x0 = BeamsModel.InfoModels[i].startPosition + BeamsModel.InfoModels[i].Length / 2;
                        using (Transaction transaction = new Transaction(document))
                        {
                            transaction.Start(ActionRebar[3]);
                            a.CreateTagLongDetailTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, a.AllLocation[0].Y, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                            transaction.Commit();
                        }
                    }
                }
            }
        }
        private static void CreateMainBottomBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
          
            if (BeamsModel.DetailItemModel.MainBottomDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.MainBottomDetail.Count; i++)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[4]);
                        BeamsModel.DetailItemModel.MainBottomDetail[i].CreateLongDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }
                }
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    DetailItem a = null;
                    if (i == 0 && BeamsModel.InfoModels[i].ConsolLeft)
                    {
                        a = BeamsModel.DetailItemModel.MainBottomDetail.Where(x => ((x.AllLocation[0].X) > BeamsModel.InfoModels[i].startPosition) && (x.AllLocation[0].X != x.AllLocation[1].X)).FirstOrDefault();
                    }
                    else
                    {
                        a = BeamsModel.DetailItemModel.MainBottomDetail.Where(x => ((x.AllLocation[0].X) < BeamsModel.InfoModels[i].startPosition) && ((x.AllLocation[1].X) > BeamsModel.InfoModels[i].endPosition)).FirstOrDefault();
                    }
                    if (a != null)
                    {
                        double x0 = BeamsModel.InfoModels[i].startPosition + BeamsModel.InfoModels[i].Length / 2;
                        using (Transaction transaction = new Transaction(document))
                        {
                            transaction.Start(ActionRebar[4]);
                            a.CreateTagLongDetailBottom(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, a.AllLocation[1].Y, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                            transaction.Commit();
                        }
                    }
                }
            }
        }
        private static void CreateAddTopBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.AddTopDetail.Count != 0)
            {
               
                for (int i = 0; i < BeamsModel.DetailItemModel.AddTopDetail.Count; i++)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[5]);
                        BeamsModel.DetailItemModel.AddTopDetail[i].CreateLongDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }
                }
                for (int i = 0; i < BeamsModel.NodeModels.Count; i++)
                {
                    List<DetailItem> left = BeamsModel.DetailItemModel.AddTopDetail.Where(x => (x.AllLocation[0].X < BeamsModel.NodeModels[i].End) && (x.AllLocation[1].X > BeamsModel.NodeModels[i].End)).ToList();
                    if (left.Count != 0)
                    {
                        double delta = BeamsModel.SettingModel.TagV / left.Count;
                        for (int j = 0; j < left.Count; j++)
                        {
                            double x0 = (BeamsModel.NodeModels[i].End + left[j].AllLocation[1].X) / 2;
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[5]);
                                left[j].CreateTagLongDetailTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, left[j].AllLocation[1].Y, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV + j * delta);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                    }
                    List<DetailItem> right = BeamsModel.DetailItemModel.AddTopDetail.Where(x => (x.AllLocation[0].X < BeamsModel.NodeModels[i].Start) && (x.AllLocation[1].X > BeamsModel.NodeModels[i].Start)).ToList();
                    if (right.Count != 0)
                    {
                        double delta = BeamsModel.SettingModel.TagV / left.Count;
                        for (int j = 0; j < right.Count; j++)
                        {
                            double x0 = (BeamsModel.NodeModels[i].Start + right[j].AllLocation[0].X) / 2;
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[5]);
                                right[j].CreateTagLongDetailTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, right[j].AllLocation[1].Y, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV + j * delta);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                    }
                }
            }
        }
        private static void CreateAddBottomBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.AddBottomDetail.Count != 0)
            {

                for (int i = 0; i < BeamsModel.DetailItemModel.AddBottomDetail.Count; i++)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[6]);
                        BeamsModel.DetailItemModel.AddBottomDetail[i].CreateLongDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        if (BeamsModel.DetailItemModel.AddBottomDetail[i].AllLocation[0].X != BeamsModel.DetailItemModel.AddBottomDetail[i].AllLocation[1].X)
                        {
                            double x0 = (BeamsModel.DetailItemModel.AddBottomDetail[i].AllLocation[0].X + BeamsModel.DetailItemModel.AddBottomDetail[i].AllLocation[1].X) / 2;
                            BeamsModel.DetailItemModel.AddBottomDetail[i].CreateTagLongDetailBottom(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, BeamsModel.DetailItemModel.AddBottomDetail[i].AllLocation[1].Y, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                        }
                        transaction.Commit();
                    }
                }
            }
        }
        private static void CreateDimensionAddBottomBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.AddBottomDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    List<DetailItem> detailItems = BeamsModel.DetailItemModel.AddBottomDetail.Where(x => ConditionFindDetailItemAddBottomBar(BeamsModel.InfoModels[i], x)).ToList();
                    if (detailItems.Count != 0)
                    {
                        for (int j = 0; j < detailItems.Count; j++)
                        {
                            if (detailItems[j].AllLocation[0].X < BeamsModel.InfoModels[i].startPosition)
                            {
                                using (Transaction transaction = new Transaction(document))
                                {
                                    transaction.Start(ActionRebar[7]);
                                    Reference r = GetReferenceAddBottomBarItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.PlanarFaces[0], unit, detailItems[j].AllLocation[1].X);
                                    BeamsModel.ReferenceAddBottomBar.Append(r);
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                    transaction.Commit();
                                }
                            }
                            if ((detailItems[j].AllLocation[0].X > BeamsModel.InfoModels[i].startPosition) && (detailItems[j].AllLocation[1].X > BeamsModel.InfoModels[i].endPosition))
                            {
                                using (Transaction transaction = new Transaction(document))
                                {
                                    transaction.Start(ActionRebar[7]);
                                    Reference r = GetReferenceAddBottomBarItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.PlanarFaces[0], unit, detailItems[j].AllLocation[0].X);
                                    BeamsModel.ReferenceAddBottomBar.Append(r);
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                    transaction.Commit();
                                }
                            }
                            if ((detailItems[j].AllLocation[0].X > BeamsModel.InfoModels[i].startPosition) && (detailItems[j].AllLocation[1].X < BeamsModel.InfoModels[i].endPosition))
                            {
                                using (Transaction transaction = new Transaction(document))
                                {
                                    transaction.Start(ActionRebar[7]);
                                    Reference r1 = GetReferenceAddBottomBarItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.PlanarFaces[0], unit, detailItems[j].AllLocation[0].X);
                                    BeamsModel.ReferenceAddBottomBar.Append(r1);
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                    Reference r2 = GetReferenceAddBottomBarItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.PlanarFaces[0], unit, detailItems[j].AllLocation[1].X);
                                    BeamsModel.ReferenceAddBottomBar.Append(r2);
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                }
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[7]);
                    BeamsModel.DimensionView.CreateDimensionHorizontalAddTopBar(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.ReferenceAddBottomBar, BeamsModel.SettingModel, BeamsModel.InfoModels, false, BeamsModel.SettingModel.L1);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }
            }

        }
        private static void CreateSideBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.SideBarDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.SideBarDetail.Count; i++)
                {
                    double x0 = (BeamsModel.DetailItemModel.SideBarDetail[i].AllLocation[0].X + BeamsModel.DetailItemModel.SideBarDetail[i].AllLocation[1].X) / 2;
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[8]);
                        BeamsModel.DetailItemModel.SideBarDetail[i].CreateLongDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        BeamsModel.DetailItemModel.SideBarDetail[i].CreateTagLongDetailTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, BeamsModel.DetailItemModel.SideBarDetail[i].AllLocation[1].Y, BeamsModel.SettingModel.TagH, -BeamsModel.SettingModel.TagV);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }
                }
            }
        }
        private static void CreateSpecialBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.SpecialDetail.Count != 0)
            {
               
                for (int i = 0; i < BeamsModel.DetailItemModel.SpecialDetail.Count; i++)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[9]);
                        BeamsModel.DetailItemModel.SpecialDetail[i].CreateLongDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        if (i % 5 == 4)
                        {
                            double x0 = (BeamsModel.DetailItemModel.SpecialDetail[i].AllLocation[0].X + BeamsModel.DetailItemModel.SpecialDetail[i].AllLocation[1].X) / 2;
                            BeamsModel.DetailItemModel.SpecialDetail[i].CreateTagLongDetailTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, BeamsModel.DetailItemModel.SpecialDetail[i].AllLocation[1].Y, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                        }
                        transaction.Commit();
                    }
                }
            }
        }
        private static void CreateSpecialStirrupBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.SpecialStirrupDetail.Count != 0)
            {
               
                for (int i = 0; i < BeamsModel.DetailItemModel.SpecialStirrupDetail.Count; i++)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[10]);
                        BeamsModel.DetailItemModel.SpecialStirrupDetail[i].CreateStirrupDetailItem(document, BeamsModel, unit);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }
                }
            }
        }
        private static void CreateStirrupSectionBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.StirrupsSection.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SectionBeamViews.Count; i++)
                {
                    List<DetailItem> detailItems = BeamsModel.DetailItemModel.StirrupsSection.Where(x => x.Location.X >= BeamsModel.InfoModels[i].startPosition && x.Location.X <= BeamsModel.InfoModels[i].endPosition).ToList();
                    if (BeamsModel.StirrupModels[i].Type == 0)
                    {
                        if (detailItems.Count != 0 && detailItems.Count == 3)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[11]);
                                detailItems[0].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView, BeamsModel.SettingModel.TagH, BeamsModel.InfoModels[i].h / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[1].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView, BeamsModel.SettingModel.TagH, BeamsModel.InfoModels[i].h / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[2].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView, BeamsModel.SettingModel.TagH, BeamsModel.InfoModels[i].h / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                    }
                    else
                    {
                        if (detailItems.Count != 0 && detailItems.Count == 6)
                        {
                            double x = (BeamsModel.InfoModels[i].b - 4 * BeamsModel.StirrupModels[i].c - BeamsModel.StirrupModels[i].a) / 2;
                            double la = x + BeamsModel.StirrupModels[i].c + BeamsModel.StirrupModels[i].a;
                            double deltaTagH = BeamsModel.InfoModels[i].b - (la + 2 * BeamsModel.StirrupModels[i].c);
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[11]);
                                detailItems[0].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView, BeamsModel.SettingModel.TagH + deltaTagH, BeamsModel.InfoModels[i].h / 2 - BeamsModel.SettingModel.TagV / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[1].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].StartView, BeamsModel.SettingModel.TagH, BeamsModel.InfoModels[i].h / 2 + BeamsModel.SettingModel.TagV / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[2].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView, BeamsModel.SettingModel.TagH + deltaTagH, BeamsModel.InfoModels[i].h / 2 - BeamsModel.SettingModel.TagV / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[3].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].MidView, BeamsModel.SettingModel.TagH, BeamsModel.InfoModels[i].h / 2 + BeamsModel.SettingModel.TagV / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[4].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView, BeamsModel.SettingModel.TagH + deltaTagH, BeamsModel.InfoModels[i].h / 2 - BeamsModel.SettingModel.TagV / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                detailItems[5].CreateStirrupSectionItem(document, BeamsModel, BeamsModel.InfoModels[i], unit, BeamsModel.SectionBeamViews[i].EndView, BeamsModel.SettingModel.TagH, BeamsModel.InfoModels[i].h / 2 + BeamsModel.SettingModel.TagV / 2);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                    }

                }
            }
        }
        private static void CreateLongSectionBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.DetailItemModel.LongSection.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SectionAreaModels.Count; i++)
                {
                    double x0Start = BeamsModel.InfoModels[i].startPosition + 0.125 * BeamsModel.InfoModels[i].Length;
                    double x0Mid = BeamsModel.InfoModels[i].startPosition + 0.5 * BeamsModel.InfoModels[i].Length;
                    double x0End = BeamsModel.InfoModels[i].startPosition + 0.875 * BeamsModel.InfoModels[i].Length;
                    List<DetailItem> detailItemStarts = BeamsModel.DetailItemModel.LongSection.Where(x => PointModel.AreEqual(x.Location.X, x0Start)).ToList();
                    List<DetailItem> detailItemMiddles = BeamsModel.DetailItemModel.LongSection.Where(x => PointModel.AreEqual(x.Location.X, x0Mid)).ToList();
                    List<DetailItem> detailItemEnds = BeamsModel.DetailItemModel.LongSection.Where(x => PointModel.AreEqual(x.Location.X, x0End)).ToList();
                    if (detailItemStarts.Count != 0)
                    {
                        for (int j = 0; j < detailItemStarts.Count; j++)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[12]);
                                bool middle = (detailItemStarts[j].Location.Y > GetMinY0SectionDetailItem(detailItemStarts) && detailItemStarts[j].Location.Y < GetMaxY0SectionDetailItem(detailItemStarts));
                                detailItemStarts[j].CreateLongSectionItem(document, BeamsModel, unit, BeamsModel.SectionBeamViews[i].StartView, middle);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                    }
                    if (detailItemMiddles.Count != 0)
                    {
                        for (int j = 0; j < detailItemMiddles.Count; j++)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[12]);
                                bool middle = (detailItemMiddles[j].Location.Y > GetMinY0SectionDetailItem(detailItemMiddles) && detailItemMiddles[j].Location.Y < GetMaxY0SectionDetailItem(detailItemMiddles));
                                detailItemMiddles[j].CreateLongSectionItem(document, BeamsModel, unit, BeamsModel.SectionBeamViews[i].MidView, middle);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                    }
                    if (detailItemEnds.Count != 0)
                    {
                        for (int j = 0; j < detailItemEnds.Count; j++)
                        {
                            using (Transaction transaction = new Transaction(document))
                            {
                                transaction.Start(ActionRebar[12]);
                                bool middle = (detailItemEnds[j].Location.Y > GetMinY0SectionDetailItem(detailItemEnds) && detailItemEnds[j].Location.Y < GetMaxY0SectionDetailItem(detailItemEnds));
                                detailItemEnds[j].CreateLongSectionItem(document, BeamsModel, unit, BeamsModel.SectionBeamViews[i].EndView, middle);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                transaction.Commit();
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region Action
        private static int GetProgressBarStirrupBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            for (int i = 0; i < BeamsModel.DetailItemModel.StirrupsDetail.Count; i++)
            {
                pro += 2;
            }
            return pro;
        }
        private static int GetProgressBarAntiStirrupBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.AntiSection.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SectionBeamViews.Count; i++)
                {
                    List<DetailItem> detailItems = BeamsModel.DetailItemModel.AntiSection.Where(x => x.Location.X >= BeamsModel.InfoModels[i].startPosition && x.Location.X <= BeamsModel.InfoModels[i].endPosition).ToList();
                    if (detailItems.Count != 0)
                    {
                        if (detailItems.Count == 3)
                        {
                            pro += 3;
                        }
                        if (detailItems.Count == 6)
                        {
                            pro += 6;
                        }

                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarDimensionStirrupBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                pro += 2;

            }
            pro += 1;
            return pro;
        }
        private static int GetProgressBarMainTopBar(BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.MainTopDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.MainTopDetail.Count; i++)
                {
                    pro += 1;
                }
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    DetailItem a = null;
                    if (i == 0 && BeamsModel.InfoModels[i].ConsolLeft)
                    {
                        a = BeamsModel.DetailItemModel.MainTopDetail.Where(x => ((x.AllLocation[0].X) > BeamsModel.InfoModels[i].startPosition) && (x.AllLocation[0].X != x.AllLocation[1].X)).FirstOrDefault();
                    }
                    else
                    {
                        a = BeamsModel.DetailItemModel.MainTopDetail.Where(x => ((x.AllLocation[0].X) < BeamsModel.InfoModels[i].startPosition) && ((x.AllLocation[1].X) > BeamsModel.InfoModels[i].endPosition)).FirstOrDefault();
                    }
                    if (a != null)
                    {
                        pro += 1;
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarMainBottomBar(BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.MainBottomDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.MainBottomDetail.Count; i++)
                {
                    pro += 1;
                }
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    DetailItem a = null;
                    if (i == 0 && BeamsModel.InfoModels[i].ConsolLeft)
                    {
                        a = BeamsModel.DetailItemModel.MainBottomDetail.Where(x => ((x.AllLocation[0].X) > BeamsModel.InfoModels[i].startPosition) && (x.AllLocation[0].X != x.AllLocation[1].X)).FirstOrDefault();
                    }
                    else
                    {
                        a = BeamsModel.DetailItemModel.MainBottomDetail.Where(x => ((x.AllLocation[0].X) < BeamsModel.InfoModels[i].startPosition) && ((x.AllLocation[1].X) > BeamsModel.InfoModels[i].endPosition)).FirstOrDefault();
                    }
                    if (a != null)
                    {
                        pro += 1;
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarAddTopBar(BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.AddTopDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.AddTopDetail.Count; i++)
                {
                    pro += 1;
                }
                for (int i = 0; i < BeamsModel.NodeModels.Count; i++)
                {
                    List<DetailItem> left = BeamsModel.DetailItemModel.AddTopDetail.Where(x => (x.AllLocation[0].X < BeamsModel.NodeModels[i].End) && (x.AllLocation[1].X > BeamsModel.NodeModels[i].End)).ToList();
                    if (left.Count != 0)
                    {
                        for (int j = 0; j < left.Count; j++)
                        {
                            pro += 1;
                        }
                    }
                    List<DetailItem> right = BeamsModel.DetailItemModel.AddTopDetail.Where(x => (x.AllLocation[0].X < BeamsModel.NodeModels[i].Start) && (x.AllLocation[1].X > BeamsModel.NodeModels[i].Start)).ToList();
                    if (right.Count != 0)
                    {
                        pro += 1;
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarAddBottomBar(BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.AddBottomDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.AddBottomDetail.Count; i++)
                {
                    pro += 1;
                    if (BeamsModel.DetailItemModel.AddBottomDetail[i].AllLocation[0].X != BeamsModel.DetailItemModel.AddBottomDetail[i].AllLocation[1].X)
                    {
                        pro += 1;
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarDimensionAddBottomBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.AddBottomDetail.Count != 0)
            {
                
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    List<DetailItem> detailItems = BeamsModel.DetailItemModel.AddBottomDetail.Where(x => ConditionFindDetailItemAddBottomBar(BeamsModel.InfoModels[i], x)).ToList();
                    if (detailItems.Count != 0)
                    {
                        for (int j = 0; j < detailItems.Count; j++)
                        {
                            if (detailItems[j].AllLocation[0].X < BeamsModel.InfoModels[i].startPosition)
                            {
                                pro += 1;
                            }
                            if ((detailItems[j].AllLocation[0].X > BeamsModel.InfoModels[i].startPosition) && (detailItems[j].AllLocation[1].X > BeamsModel.InfoModels[i].endPosition))
                            {
                                pro += 1;
                            }
                            if ((detailItems[j].AllLocation[0].X > BeamsModel.InfoModels[i].startPosition) && (detailItems[j].AllLocation[1].X < BeamsModel.InfoModels[i].endPosition))
                            {
                                pro += 2;
                            }
                        }
                    }
                }
                pro += 1;
            }
            return pro;
        }
        private static int GetProgressBarSideBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.SideBarDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.SideBarDetail.Count; i++)
                {
                    pro += 2;
                }
            }
            return pro;
        }
        private static int GetProgressBarSpecialBar(BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.SpecialDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.SpecialDetail.Count; i++)
                {
                    pro += 1;
                    if (i % 5 == 4)
                    {
                        pro += 1;
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarSpecialStirrupBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.SpecialStirrupDetail.Count != 0)
            {
                for (int i = 0; i < BeamsModel.DetailItemModel.SpecialStirrupDetail.Count; i++)
                {
                    pro += 1;
                }
            }
            return pro;
        }
        private static int GetProgressBarStirrupSectionBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.StirrupsSection.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SectionBeamViews.Count; i++)
                {
                    List<DetailItem> detailItems = BeamsModel.DetailItemModel.StirrupsSection.Where(x => x.Location.X >= BeamsModel.InfoModels[i].startPosition && x.Location.X <= BeamsModel.InfoModels[i].endPosition).ToList();
                    if (BeamsModel.StirrupModels[i].Type == 0)
                    {
                        pro += 3;
                    }
                    else
                    {
                        if (detailItems.Count != 0 && detailItems.Count == 6)
                        {
                            pro += 6;
                        }
                    }

                }
            }
            return pro;
        }
        private static int GetProgressBarLongSectionBar(BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.DetailItemModel.LongSection.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SectionAreaModels.Count; i++)
                {
                    double x0Start = BeamsModel.InfoModels[i].startPosition + 0.125 * BeamsModel.InfoModels[i].Length;
                    double x0Mid = BeamsModel.InfoModels[i].startPosition + 0.5 * BeamsModel.InfoModels[i].Length;
                    double x0End = BeamsModel.InfoModels[i].startPosition + 0.875 * BeamsModel.InfoModels[i].Length;
                    List<DetailItem> detailItemStarts = BeamsModel.DetailItemModel.LongSection.Where(x => PointModel.AreEqual(x.Location.X, x0Start)).ToList();
                    List<DetailItem> detailItemMiddles = BeamsModel.DetailItemModel.LongSection.Where(x => PointModel.AreEqual(x.Location.X, x0Mid)).ToList();
                    List<DetailItem> detailItemEnds = BeamsModel.DetailItemModel.LongSection.Where(x => PointModel.AreEqual(x.Location.X, x0End)).ToList();
                    if (detailItemStarts.Count != 0)
                    {
                        for (int j = 0; j < detailItemStarts.Count; j++)
                        {
                            pro += 1;
                        }
                    }
                    if (detailItemMiddles.Count != 0)
                    {
                        for (int j = 0; j < detailItemMiddles.Count; j++)
                        {
                            pro += 1;
                        }
                    }
                    if (detailItemEnds.Count != 0)
                    {
                        for (int j = 0; j < detailItemEnds.Count; j++)
                        {
                            pro += 1;
                        }
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarRebarDetailItem(Document document, BeamsModel BeamsModel)
        {
            int a = 0;
            a = 0;
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            #region Stirrup  bar
            BeamsModel.DetailItemModel.GetStirrupDetail(BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.SpecialBarModel, BeamsModel.SpecialNodeModels);
            a += GetProgressBarStirrupBar( BeamsModel, dsmax);
            BeamsModel.DetailItemModel.GetAntiStirrupSection(BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.SettingModel);
            a += GetProgressBarAntiStirrupBar( BeamsModel, dsmax);
            #endregion

            #region Dimension Stirrup
            BeamsModel.ReferenceStirrupBar = BeamsModel.DimensionView.GetReferenceArray(document, BeamsModel.PlanarFaces);
            a += GetProgressBarDimensionStirrupBar( BeamsModel, dsmax);
            #endregion

            #region MainTop
            BeamsModel.DetailItemModel.GetMainTopDetail(BeamsModel.SingleMainTopBarModel, BeamsModel.MainTopBarModel, BeamsModel.SelectedIndexModel, BeamsModel.InfoModels);
            a += GetProgressBarMainTopBar( BeamsModel, dsmax);
            #endregion

            #region Main Bottom Bar
            BeamsModel.DetailItemModel.GetMainBottomDetail(BeamsModel.MainBottomBarModel);
            a += GetProgressBarMainBottomBar( BeamsModel, dsmax);
            #endregion

            #region Add Top Bar
            BeamsModel.DetailItemModel.GetAddTopDetail(BeamsModel.AddTopBarModel, BeamsModel.InfoModels);
            a += GetProgressBarAddTopBar( BeamsModel, dsmax);
            #endregion

            #region Add Bottom Bar
            BeamsModel.DetailItemModel.GetAddBottomDetail(BeamsModel.AddBottomBarModel, BeamsModel.SelectedBottomModels);
            a += GetProgressBarAddBottomBar( BeamsModel, dsmax);
            #endregion
            #region Dimension AddBottomBar
            BeamsModel.ReferenceAddBottomBar = BeamsModel.DimensionView.GetReferenceArray(document, BeamsModel.PlanarFaces);
           a += GetProgressBarDimensionAddBottomBar(BeamsModel, dsmax);
            #endregion
            #region Side Bar- Special Bar
            BeamsModel.DetailItemModel.GetSideDetail(BeamsModel.SideBarModel);
            a += GetProgressBarSideBar( BeamsModel, dsmax);
            BeamsModel.DetailItemModel.GetSpecialDetail(BeamsModel.SpecialBarModel);
            a += GetProgressBarSpecialBar( BeamsModel, dsmax);
            BeamsModel.DetailItemModel.GetSpecialStirrupDetail(BeamsModel.SpecialBarModel, BeamsModel.InfoModels, BeamsModel.Cover);
            a += GetProgressBarSpecialStirrupBar( BeamsModel, dsmax);
            #endregion

            #region Stirrup Section
            BeamsModel.DetailItemModel.GetStirrupSection(BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups);
            a += GetProgressBarStirrupSectionBar( BeamsModel, dsmax);
            #endregion

            #region Long Section
            BeamsModel.CheangedSectionArea();
            BeamsModel.DetailItemModel.GetLongSection(BeamsModel.SectionAreaModels, BeamsModel.InfoModels, BeamsModel.SideBarModel, BeamsModel.Cover, dsmax);
            a += GetProgressBarLongSectionBar( BeamsModel, dsmax);
            #endregion
            return a;
        }
       
        private static List<string> ActionRebar = new List<string>()
        {
            "Create Stirrup Bars",
            "Create Anti-Stirrup Bars",
            "Create Dimension Stirrup",
            "Create Main-Top Bars",
            "Create Main-Bottom Bars",
            "Create Additional - Top Bars",
            "Create Additional - Bottom Bars",
            "Create Dimension AddBottom Bars",
            "Create Side Bar",
            "Create Special Bars",
            "Create Special Stirrup Bars",
            "Create Stirrups Section Bars",
            "Create Long Section Bars"
        };
        #endregion
        #region Item
        private static double GetMinY0SectionDetailItem(List<DetailItem> detailItems)
        {
            double min = detailItems[0].Location.Y;
            for (int i = 0; i < detailItems.Count; i++)
            {
                if (min >= detailItems[i].Location.Y)
                {
                    min = detailItems[i].Location.Y;
                }
            }
            return min;
        }
        private static double GetMaxY0SectionDetailItem(List<DetailItem> detailItems)
        {
            double max = 0;
            for (int i = 0; i < detailItems.Count; i++)
            {
                if (max <= detailItems[i].Location.Y)
                {
                    max = detailItems[i].Location.Y;
                }
            }
            return max;
        }
        private static Reference GetReferenceAddBottomBarItem(ViewSection view, Document document, InfoModel infoModel, PlanarFace planarFace0, UnitProject unit, double x)
        {
            double l = unit.Convert(x);
            XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
            XYZ p2 = p1 + unit.Convert(infoModel.b / 2) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p3 = PointModel.ProjectToPlane(p2, planarFace0);
            XYZ p4 = p3 + l * (-1) * planarFace0.FaceNormal;
            XYZ p5 = p4 + 0.01 * XYZ.BasisZ;
            Line line = Line.CreateBound(p4, p5);
            DetailCurve detailCurve = document.Create.NewDetailCurve(view, line);
            return detailCurve.GeometryCurve.Reference;
        }
        private static bool ConditionFindDetailItemAddBottomBar(InfoModel infoModel, DetailItem detailItem)
        {
            if (PointModel.AreEqual(detailItem.AllLocation[0].X, detailItem.AllLocation[1].X))
            {
                return false;
            }
            bool a1 = (detailItem.AllLocation[1].X > infoModel.startPosition) && (detailItem.AllLocation[1].X < infoModel.endPosition);
            bool a2 = (detailItem.AllLocation[0].X > infoModel.startPosition) && (detailItem.AllLocation[0].X < infoModel.endPosition);
            return (a1 || a2);
        }
        #endregion
    }
}
