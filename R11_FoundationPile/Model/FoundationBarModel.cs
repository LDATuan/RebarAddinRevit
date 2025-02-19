﻿using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSP;
using WpfCustomControls;
namespace R11_FoundationPile
{
    public class FoundationBarModel:BaseViewModel
    {
        #region property
        private int _Type;
        public int Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        private int _Image;
        public int Image { get => _Image; set { _Image = value; OnPropertyChanged(); } }
        private string _LocationName;
        public string LocationName { get => _LocationName; set { _LocationName = value; OnPropertyChanged(); } }
        private string _SpanOrientation;
        public string SpanOrientation { get => _SpanOrientation; set { _SpanOrientation = value; OnPropertyChanged(); } }
      
        private ObservableCollection<BarModel> _BarModels;
        public ObservableCollection<BarModel> BarModels { get => _BarModels; set { _BarModels = value; OnPropertyChanged(); } }
       

        #endregion
        public FoundationBarModel(int type,int image,string locationName,string spanOrientation,Document document,SettingModel settingModel, List<RebarBarModel> AllBars)
        {
            Type = type;
            Image = image;
            LocationName = locationName;
            SpanOrientation = spanOrientation;
            double coverSide = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, settingModel.SelectedSideCover.CoverDistance, false));
            double maxDiameter = AllBars.Max(x => x.Diameter);
            BarModels = new ObservableCollection<BarModel>();
            BarModels.Add ( new BarModel("MainBottom", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1,1, true));
            BarModels.Add(new BarModel("MainTop", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter,1, 1, false));
            BarModels.Add(new BarModel("MainAddHorizontal", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1, 1, false));
            BarModels.Add(new BarModel("MainAddVertical", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1, 1, false));
            BarModels.Add(new BarModel("SecondaryBottom", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1, 1, true));
            BarModels.Add(new BarModel("SecondaryTop", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1, 1, false));
            BarModels.Add(new BarModel("SecondaryAddHorizontal", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1, 1, false));
            BarModels.Add(new BarModel("SecondaryAddVertical", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1, 1, false));
            BarModels.Add(new BarModel("Side", AllBars[3], (settingModel.HeightFoundation - coverSide), 5 * maxDiameter, 1, 1, true));
            
        }
        public void FixNumber(double p1, double p2,double p3,double p4, double coverSide)
        {
            BarModel mainBottom = BarModels.Where(x => x.Name.Equals("MainBottom")).FirstOrDefault();
            BarModel secondaryBottom = BarModels.Where(x => x.Name.Equals("SecondaryBottom")).FirstOrDefault();
            BarModel side = BarModels.Where(x => x.Name.Equals("Side")).FirstOrDefault();
            for (int i = 0; i < BarModels.Count; i++)
            {
                BarModels[i].Number = BarModels[i].FixNumber(p1, p2, p3, p4, coverSide, mainBottom, secondaryBottom, side);
            }
        }
        public void FixDistance(double p1, double p2, double p3, double p4, double coverSide)
        {
            BarModel mainBottom = BarModels.Where(x => x.Name.Equals("MainBottom")).FirstOrDefault();
            BarModel secondaryBottom = BarModels.Where(x => x.Name.Equals("SecondaryBottom")).FirstOrDefault();
            BarModel side = BarModels.Where(x => x.Name.Equals("Side")).FirstOrDefault();
            for (int i = 0; i < BarModels.Count; i++)
            {
                BarModels[i].Distance = BarModels[i].FixDistance(p1, p2, p3, p4, coverSide, mainBottom, secondaryBottom, side);
            }
        }

    }
}
