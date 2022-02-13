﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel
{
    public class WindowLanguage : BaseViewModel
    {

        private string _OK;
        public string OK { get { return _OK; } set { _OK = value; OnPropertyChanged(); } }

        private string _Cancel;
        public string Cancel { get { return _Cancel; } set { _Cancel = value; OnPropertyChanged(); } }

        #region R01
        private string _Column;
        public string Column { get { return _Column; } set { _Column = value; OnPropertyChanged(); } }
        private string _R01_CreateStirrupBarsColumns;
        public string R01_CreateStirrupBarsColumns { get => _R01_CreateStirrupBarsColumns; set { _R01_CreateStirrupBarsColumns = value; OnPropertyChanged(); } }
        private string _R01_CreateMainBarsColumns;
        public string R01_CreateMainBarsColumns { get => _R01_CreateMainBarsColumns; set { _R01_CreateMainBarsColumns = value; OnPropertyChanged(); } }
        private string _R01_CreateTagBarsColumns;
        public string R01_CreateTagBarsColumns { get => _R01_CreateTagBarsColumns; set { _R01_CreateTagBarsColumns = value; OnPropertyChanged(); } }
        private string _R01_CreateDetailViewColumns;
        public string R01_CreateDetailViewColumns { get => _R01_CreateDetailViewColumns; set { _R01_CreateDetailViewColumns = value; OnPropertyChanged(); } }
        private string _R01_CreateSectionViewColumns;
        public string R01_CreateSectionViewColumns { get => _R01_CreateSectionViewColumns; set { _R01_CreateSectionViewColumns = value; OnPropertyChanged(); } }
        private string _R01_CreateDimensionViewColumns;
        public string R01_CreateDimensionViewColumns { get => _R01_CreateDimensionViewColumns; set { _R01_CreateDimensionViewColumns = value; OnPropertyChanged(); } }
        private string _R01_CreateDimensionSectionColumns;
        public string R01_CreateDimensionSectionColumns { get => _R01_CreateDimensionSectionColumns; set { _R01_CreateDimensionSectionColumns = value; OnPropertyChanged(); } }
        private string _R01_CreateDetailShopColumns;
        public string R01_CreateDetailShopColumns { get => _R01_CreateDetailShopColumns; set { _R01_CreateDetailShopColumns = value; OnPropertyChanged(); } }

        #endregion

        #region R02_
        private string _R02_CreateRebarBeams;
        public string R02_CreateRebarBeams { get => _R02_CreateRebarBeams; set { _R02_CreateRebarBeams = value; OnPropertyChanged(); } }
        private string _R02_CreateViewDimensionBeams;
        public string R02_CreateViewDimensionBeams { get => _R02_CreateViewDimensionBeams; set { _R02_CreateViewDimensionBeams = value; OnPropertyChanged(); } }
        private string _R02_CreateDetailShopBeams;
        public string R02_CreateDetailShopBeams { get => _R02_CreateDetailShopBeams; set { _R02_CreateDetailShopBeams = value; OnPropertyChanged(); } }
        private string _R02_CreateRebarDetailItemBeams;
        public string R02_CreateRebarDetailItemBeams { get => _R02_CreateRebarDetailItemBeams; set { _R02_CreateRebarDetailItemBeams = value; OnPropertyChanged(); } }
        #endregion
        #region R11
        private string _R11_CreateFoundationPile;
        public string R11_CreateFoundationPile { get { return _R11_CreateFoundationPile; } set { _R11_CreateFoundationPile = value; OnPropertyChanged(); } }
        private string _R11_CreatePileDetail;
        public string R11_CreatePileDetail { get { return _R11_CreatePileDetail; } set { _R11_CreatePileDetail = value; OnPropertyChanged(); } }
        private string _R11_CreateReinforcement;
        public string R11_CreateReinforcement { get { return _R11_CreateReinforcement; } set { _R11_CreateReinforcement = value; OnPropertyChanged(); } }
        #endregion

        public WindowLanguage(string languge)
        {
            ChangedLanguage(languge);
        }
        public void ChangedLanguage(string languge)
        {
            switch (languge)
            {
                case "EN": GetLanguageEN(); break;
                case "VN": GetLanguageVN(); break;
                default: GetLanguageEN(); break;
            }
        }
        private void GetLanguageEN()
        {
            OK = "OK";
            Cancel = "Cancel";
            Column = "Columns";
            R11_CreateFoundationPile = "Create FoundationPile";
            R11_CreatePileDetail = "Create PileDetail";
            R11_CreateReinforcement = "Create Reinforcement";

            R01_CreateStirrupBarsColumns = "Create StirrupBars";
            R01_CreateMainBarsColumns = "Create MainBars";
            R01_CreateTagBarsColumns = "Create TagBars";
            R01_CreateDetailViewColumns = "Create DetailView";
            R01_CreateSectionViewColumns = "Create SectionView";
            R01_CreateDimensionViewColumns = "Create DimensionView";
            R01_CreateDimensionSectionColumns = "Create DimensionSection";
            R01_CreateDetailShopColumns = "Create DetailShop";

            R02_CreateRebarBeams = "Create Rebar";
            R02_CreateViewDimensionBeams = "Create Dimension";
            R02_CreateDetailShopBeams = "Create DetailShop";
            R02_CreateRebarDetailItemBeams = "Create DetailItem";
            
        }
        private void GetLanguageVN()
        {
            OK = "Thực Hiện";
            Cancel = "Huỷ";
            Column = "Cột";
            R11_CreateFoundationPile = "Tạo Móng Cọc";
            R11_CreatePileDetail = "Tạo Chi tiết Cọc";
            R11_CreateReinforcement = "Tạo Cốt Thép";

            R01_CreateStirrupBarsColumns = "Tạo Thép Đai";
            R01_CreateMainBarsColumns = "Tạo Thép Chủ";
            R01_CreateTagBarsColumns = "Tạo Tag";
            R01_CreateDetailViewColumns = "Tạo Detail";
            R01_CreateSectionViewColumns = "Tạo MC";
            R01_CreateDimensionViewColumns = "Tạo KT Detail ";
            R01_CreateDimensionSectionColumns = "Tạo KT Detail";
            R01_CreateDetailShopColumns = "Tạo DetailShop";

            R02_CreateRebarBeams = "Tạo Thép ";
            R02_CreateViewDimensionBeams = "Tạo Kích thước ";
            R02_CreateDetailShopBeams = "Tạo DetailShop";
            R02_CreateRebarDetailItemBeams = "Tạo DetailItem";
        }
    }
}
