﻿using WpfCustomControls.Model;
using System.Windows;
using WpfCustomControls.LanguageModel;
namespace WpfCustomControls.ViewModel
{
    public class StatusBarViewModel :BaseViewModel
    {
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        private ProgressModel _ProgressModel;
        public ProgressModel ProgressModel { get => _ProgressModel; set { _ProgressModel = value; OnPropertyChanged(); } }
        private Visibility _Cancel;
        public Visibility Cancel { get => _Cancel; set { _Cancel = value; OnPropertyChanged(); } }
        private Visibility _OK;
        public Visibility OK { get => _OK; set { _OK = value; OnPropertyChanged(); } }
        #region R01
        private Visibility _IsDetailItemColumns;
        public Visibility IsDetailItemColumns { get => _IsDetailItemColumns; set { _IsDetailItemColumns = value; OnPropertyChanged(); } }
        private Visibility _IsRebarColumns;
        public Visibility IsRebarColumns { get => _IsRebarColumns; set { _IsRebarColumns = value; OnPropertyChanged(); } }
        #endregion
        #region R02
        private Visibility _IsDetailItemBeams;
        public Visibility IsDetailItemBeams { get => _IsDetailItemBeams; set { _IsDetailItemBeams = value; OnPropertyChanged(); } }
        private Visibility _IsRebarBeams;
        public Visibility IsRebarBeams { get => _IsRebarBeams; set { _IsRebarBeams = value; OnPropertyChanged(); } }
        #endregion
        #region R10
        private Visibility _IsDetailItemWallsShear;
        public Visibility IsDetailItemWallsShear { get => _IsDetailItemWallsShear; set { _IsDetailItemWallsShear = value; OnPropertyChanged(); } }
        private Visibility _IsRebarWallsShear;
        public Visibility IsRebarWallsShear { get => _IsRebarWallsShear; set { _IsRebarWallsShear = value; OnPropertyChanged(); } }
        #endregion
        #region R11
        private Visibility _CreateReinforcement;
        public Visibility CreateReinforcement { get => _CreateReinforcement; set { _CreateReinforcement = value; OnPropertyChanged(); } }
        private Visibility _CreatePileDetail;
        public Visibility CreatePileDetail { get => _CreatePileDetail; set { _CreatePileDetail = value; OnPropertyChanged(); } }
        private Visibility _CreateFoundationPile;
        public Visibility CreateFoundationPile { get => _CreateFoundationPile; set { _CreateFoundationPile = value; OnPropertyChanged(); } }
        #endregion
        public StatusBarViewModel(ProgressModel progressModel, Languages languages)
        {
            ProgressModel = progressModel;
            Languages = languages;
            HideAllStatusBar();
        }
        private void HideAllStatusBar()
        {
            HasCancel(false);
            HasOK(false);
            HasDetailItemColumns(false);
            HasRebarColumns(false);

            HasDetailItemBeams(false);
            HasRebarBeams(false);

            HasDetailItemWallsShear(false);
            HasRebarWallsShear(false);

            HasCreateReinforcement(false);
            HasCreatePileDetail(false);
            HasCreateFoundationPile(false);
        }
      
        public void SetStatusBarColumns()
        {
            HasDetailItemColumns(true);
            HasRebarColumns(true);
            HasCancel(true);
            HasOK(true);
        }
        public void SetStatusBarBeams()
        {
            HasDetailItemBeams(true);
            HasRebarBeams(true);
            HasCancel(true);
            HasOK(true);
        }
        public void SetStatusBarWallsShear()
        {
            HasDetailItemWallsShear(true);
            HasRebarWallsShear(true);
            HasCancel(true);
            HasOK(true);
        }
        public void SetStatusBarFoundationPile()
        {
            HasCancel(true);
            HasCreateFoundationPile(true);
        }

        private void HasCancel(bool cancel)
        {
            Cancel = (cancel) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasOK(bool ok)
        {
            OK = (ok) ? (Visibility.Visible) : (Visibility.Collapsed);
        }

        private void HasDetailItemColumns(bool detailItem)
        {
            IsDetailItemColumns = (detailItem) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasRebarColumns(bool rebar)
        {
            IsRebarColumns = (rebar) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasDetailItemBeams(bool detailItem)
        {
            IsDetailItemBeams = (detailItem) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasRebarBeams(bool rebar)
        {
            IsRebarBeams = (rebar) ? (Visibility.Visible) : (Visibility.Collapsed);
        }

        private void HasDetailItemWallsShear(bool detailItem)
        {
            IsDetailItemWallsShear = (detailItem) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasRebarWallsShear(bool rebar)
        {
            IsRebarWallsShear = (rebar) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        public void HasCreateReinforcement(bool createReinforcement)
        {
            CreateReinforcement = (createReinforcement) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        public void HasCreatePileDetail(bool createPileDetail)
        {
            CreatePileDetail = (createPileDetail) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateFoundationPile(bool createFoundationPile)
        {
            CreateFoundationPile = (createFoundationPile) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
    }
}
