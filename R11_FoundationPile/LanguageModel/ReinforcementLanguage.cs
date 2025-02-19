﻿using WpfCustomControls;

namespace R11_FoundationPile.LanguageModel
{
    public class ReinforcementLanguage : BaseViewModel
    {

        private string _ListFoundation;
        public string ListFoundation { get { return _ListFoundation; } set { _ListFoundation = value; OnPropertyChanged(); } }
        private string _GroupFoundation;
        public string GroupFoundation { get { return _GroupFoundation; } set { _GroupFoundation = value; OnPropertyChanged(); } }
        private string _Representative;
        public string Representative { get { return _Representative; } set { _Representative = value; OnPropertyChanged(); } }
        private string _SpanOrientation;
        public string SpanOrientation { get { return _SpanOrientation; } set { _SpanOrientation = value; OnPropertyChanged(); } }
       

        private string _FixNumber;
        public string FixNumber { get { return _FixNumber; } set { _FixNumber = value; OnPropertyChanged(); } }

        private string _ListBar;
        public string ListBar { get { return _ListBar; } set { _ListBar = value; OnPropertyChanged(); } }
        private string _IsModel;
        public string IsModel { get { return _IsModel; } set { _IsModel = value; OnPropertyChanged(); } }
        private string _ApplyAll;
        public string ApplyAll { get { return _ApplyAll; } set { _ApplyAll = value; OnPropertyChanged(); } }
        public ReinforcementLanguage(string language)
        {
            ChangedLanguage(language);
        }
        public void ChangedLanguage(string language)
        {
            switch (language)
            {
                case "EN": GetLanguageEN(); break;
                case "VN": GetLanguageVN(); break;
                default: GetLanguageEN(); break;
            }
        }
        private void GetLanguageEN()
        {
            ListFoundation = "List Foundation";
            GroupFoundation = "Group Foundation";
            Representative = "Representative";
            SpanOrientation = "Span Orientation";
            FixNumber = "Fix Number";
            ListBar = "List Bar";
            IsModel = "IsModel";
            ApplyAll = "Apply All"; 
        }
        private void GetLanguageVN()
        {
            ListFoundation = "Danh Sách Móng";
            GroupFoundation = "Nhóm Móng";
            Representative = "Đại diện";
            SpanOrientation = "Phương Nhịp";
            FixNumber = "Chẵn Thép";
            ListBar = "Danh Sách Thép";
            IsModel = "Thêm";
            ApplyAll = "Áp dụng hết";
        }
    }
}
